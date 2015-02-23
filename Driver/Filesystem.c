// Filesystem.cpp
// File system callbacks and couple utility functions.

#include "FileWall.h"

FLT_PREOP_CALLBACK_STATUS PreRead(__inout			PFLT_CALLBACK_DATA		Data,
								  __in				PCFLT_RELATED_OBJECTS	FltObjects,
								  __deref_out_opt	PVOID					*CompletionContext)
{
	PAGED_CODE();

	// Just pass if FileWall application is not running or it's FileWall tries  to access to file.
	if(g_hClientPort == NULL || g_hTrustedProcess == PsGetCurrentProcessId())
		return FLT_PREOP_SUCCESS_NO_CALLBACK;

	return FLT_PREOP_SUCCESS_WITH_CALLBACK;

}



FLT_POSTOP_CALLBACK_STATUS PostRead(__inout PFLT_CALLBACK_DATA		Data,
									 __in PCFLT_RELATED_OBJECTS		FltObjects,
									 __in_opt PVOID					CompletionContext,
									 __in FLT_POST_OPERATION_FLAGS	Flags)
{
	NTSTATUS					status = STATUS_SUCCESS;
	POBJECT_NAME_INFORMATION	pObjNameInfo = NULL;	
	FLT_POSTOP_CALLBACK_STATUS	CallbackStatus = FLT_POSTOP_FINISHED_PROCESSING;
	
	PAGED_CODE();
	
	// Getting the file name.
	status = IoQueryFileDosDeviceName(FltObjects->FileObject, &pObjNameInfo);
	
	if(NT_SUCCESS(status))
	{
		// Just pass if length of path longer than AP_MAX_PATH.
		if(pObjNameInfo->Name.Length > AP_MAX_PATH)
		{
			KdBreakPoint();
			status = STATUS_SUCCESS;
			goto SuchLengthNotSupported;
		}

		if(NT_SUCCESS(status))
		{
			PAP_PROTECTED_ENTRY ProtectedEntry = ApIsUnderProtect(g_pGenericTable, &pObjNameInfo->Name);
			if(ProtectedEntry == NULL)
			{
				// This entry is not under protection, just allow access.
				status = STATUS_SUCCESS;
			}
			else
			{
				PACCESS_DATA pAccessData;
				
				//KdPrint(("PostRead: %wZ\n", &pObjNameInfo->Name));
				
				//
				// Allocating memory for ACCESS_DATA struct.
				// NOTE: pAccessData must bee freed by ApRequestAccess routine.
				//

				pAccessData = (PACCESS_DATA)ExAllocatePoolWithTag(NonPagedPool, sizeof(ACCESS_DATA), POOL_TAG);

				if(pAccessData != NULL)
				{
					//
					// Filling FS_ACCESS_DATA struct.
					//

					pAccessData->ProcessID	= FltGetRequestorProcessId(Data);
					pAccessData->AccessType	= ACCESS_FILESYSTEM;

					switch(Data->Iopb->MajorFunction)
					{
					case IRP_MJ_CREATE:
						pAccessData->Operation = FSOP_CREATE;
						break;
					case IRP_MJ_READ:
						pAccessData->Operation = FSOP_READ;
						break;
					case IRP_MJ_WRITE:
						pAccessData->Operation = FSOP_WRITE;
					    break;
					default:
						pAccessData->Operation = FSOP_OTHER;
					    break;
					}

					pAccessData->ID = ProtectedEntry->ID;
					RtlZeroMemory(pAccessData->Path, AP_MAX_PATH*sizeof(wchar_t));
					RtlCopyMemory(pAccessData->Path, pObjNameInfo->Name.Buffer, pObjNameInfo->Name.Length);

					// REQUESTING ACCESS.
					status = ApRequestAccess(pAccessData);
				}
				else
					status = STATUS_INSUFFICIENT_RESOURCES;
			}
		}
		else
		{
			KdBreakPoint();
			status = STATUS_SUCCESS;	// ??? Why we can't get image name? THINK ABOUT IT!!!
		}
	}

SuchLengthNotSupported:

	// Deciding to allow or deny the request.
	if(NT_SUCCESS(status))
	{
		// If the minifilter driver's preoperation callback routine returns FLT_PREOP_SUCCESS_NO_CALLBACK, it must return NULL in its CompletionContext output parameter. 
		//*CompletionContext = NULL;
		CallbackStatus = FLT_POSTOP_FINISHED_PROCESSING;
	}
	else
	{
		//
		// Some debug info.
		//

		DbgPrintStatus(status);

		//
		// Completing the operation in PreRead routine.
		//

		Data->IoStatus.Status		= STATUS_ACCESS_DENIED;
		Data->IoStatus.Information	= 0;

		CallbackStatus = FLT_POSTOP_FINISHED_PROCESSING;
	}
	
	return CallbackStatus;
}



NTSTATUS InstanceSetup(IN PCFLT_RELATED_OBJECTS		FltObjects,
					   IN FLT_INSTANCE_SETUP_FLAGS	Flags,
					   IN DEVICE_TYPE				VolumeDeviceType,
					   IN FLT_FILESYSTEM_TYPE		VolumeFilesystemType)
{
	UNREFERENCED_PARAMETER(FltObjects);
	UNREFERENCED_PARAMETER(Flags);
	UNREFERENCED_PARAMETER(VolumeFilesystemType);

	PAGED_CODE();

	ASSERT(FltObjects->Filter == g_FilterHandle);

	// Don't attach to network volumes.
	if (VolumeDeviceType == FILE_DEVICE_NETWORK_FILE_SYSTEM)
		return STATUS_FLT_DO_NOT_ATTACH;

	return STATUS_SUCCESS;
}