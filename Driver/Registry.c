// Registry.cpp
// Registry callback.

#include "FileWall.h"

NTSTATUS RegistryCallback(__in PVOID  CallbackContext,
						  __in PVOID  Argument1,
						  __in PVOID  Argument2)
{
	NTSTATUS					status	= STATUS_SUCCESS;
	PVOID						pObject = NULL;
	POBJECT_NAME_INFORMATION	pObjectName	= NULL;
	ULONG						ulKeypathLengh = 0;	

	// Just pass if FileWall application is not running or it's FileWall tries  to access to the registry.
	if(g_hClientPort == NULL || g_hTrustedProcess == PsGetCurrentProcessId())
		return STATUS_SUCCESS;

	status = RegGetObject((int)Argument1, Argument2, &pObject);
	if(STATUS_INVALID_PARAMETER_1 == status) // We can't get object from such request.
		return STATUS_SUCCESS;
	else if(NT_SUCCESS(status) == FALSE)
		return status;

	ASSERT(NULL != pObject);

	// Determining the size, in bytes of the buffer needed to hold the object name information.
	status = ObQueryNameString(pObject, pObjectName, 0, &ulKeypathLengh);

	// Just pass if length of key path longer than AP_MAX_PATH.
	if(ulKeypathLengh > AP_MAX_PATH)
	{
		KdBreakPoint();
		status = STATUS_SUCCESS;
		goto SuchLengthNotSupported;
	}

	// STATUS_INFO_LENGTH_MISMATCH for us is success code.
	if(NT_SUCCESS(status) || status == STATUS_INFO_LENGTH_MISMATCH)
	{
		pObjectName = (POBJECT_NAME_INFORMATION)ExAllocatePoolWithTag(NonPagedPool,
			ulKeypathLengh,
			POOL_TAG);
			
		if(pObjectName != NULL)
		{
			// Querying object name information.
			status = ObQueryNameString(pObject, pObjectName, ulKeypathLengh, &ulKeypathLengh);
			
			if(NT_SUCCESS(status))
			{
				PAP_PROTECTED_ENTRY pProtectedEntry = ApIsUnderProtect(g_pGenericTable, &pObjectName->Name);

				KdPrint(("RegistryCallback: %wZ\n", &pObjectName->Name));

				if(pProtectedEntry == NULL)
				{
					//
					// This entity is not under protection, just allow access.
					//

					status = STATUS_SUCCESS;
				}
				else
				{
					PACCESS_DATA pAccessData = NULL;

					//
					// Allocating memory for ACCESS_DATA struct.
					// NOTE: pAccessData must bee freed by ApRequestAccess routine.
					//

					pAccessData = (PACCESS_DATA)ExAllocatePoolWithTag(NonPagedPool,
						sizeof(ACCESS_DATA),
						POOL_TAG);

					if(pAccessData != NULL)
					{
						//
						// Filling FS_ACCESS_DATA struct.
						//
						
						pAccessData->ProcessID	= (ULONG)PsGetCurrentProcessId();
						pAccessData->AccessType	= ACCESS_REGISTRY;
						pAccessData->Operation	= (int)Argument1;
						pAccessData->ID = pProtectedEntry->ID;

						RtlZeroMemory(pAccessData->Path, AP_MAX_PATH*sizeof(wchar_t));
						RtlCopyMemory(pAccessData->Path, pObjectName->Name.Buffer, pObjectName->Name.Length);

						//
						// Sending request to the application.
						//
						
						status = ApRequestAccess(pAccessData);
					}
					else
						status = STATUS_INSUFFICIENT_RESOURCES;
				}
			}
		}
		else
			status = STATUS_INSUFFICIENT_RESOURCES;
	}

SuchLengthNotSupported:

	if(pObjectName != NULL)
		ExFreePool(pObjectName);

	return status;
}