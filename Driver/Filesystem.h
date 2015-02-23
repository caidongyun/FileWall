#pragma once

/*
A minifilter driver's PFLT_PRE_OPERATION_CALLBACK
routine performs preoperation processing for I/O operations.
*/
FLT_PREOP_CALLBACK_STATUS PreRead(__inout			PFLT_CALLBACK_DATA		Data,
								  __in				PCFLT_RELATED_OBJECTS	FltObjects,
								  __deref_out_opt	PVOID					*CompletionContext);



/*
A minifilter driver can register one or more routines of type
PFLT_POST_OPERATION_CALLBACK to perform completion processing for I/O operations.
*/
FLT_POSTOP_CALLBACK_STATUS PostRead(__inout		PFLT_CALLBACK_DATA			Data,
									__in		PCFLT_RELATED_OBJECTS		FltObjects,
									__in_opt	PVOID						CompletionContext,
									__in		FLT_POST_OPERATION_FLAGS	Flags);



/*
This routine is called whenever a new instance is created on a volume. This
gives us a chance to decide if we need to attach to this volume or not.

Return Value:
STATUS_SUCCESS - attach
STATUS_FLT_DO_NOT_ATTACH - do not attach
*/
NTSTATUS InstanceSetup(__in PCFLT_RELATED_OBJECTS		FltObjects,
					   __in FLT_INSTANCE_SETUP_FLAGS	Flags,
					   __in DEVICE_TYPE					VolumeDeviceType,
					   __in FLT_FILESYSTEM_TYPE			VolumeFilesystemType);
