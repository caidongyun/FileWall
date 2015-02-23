// FileWall.cpp
// Driver initialization and deinitialization.

#include "FileWall.h"



NTSTATUS DriverEntry(__in PDRIVER_OBJECT DriverObject, __in PUNICODE_STRING RegistryPath)
{
	NTSTATUS status = STATUS_SUCCESS;

	FLT_OPERATION_REGISTRATION Operations[] =
	{
		{
			IRP_MJ_CREATE,
			0,
			(PFLT_PRE_OPERATION_CALLBACK)PreRead,
			(PFLT_POST_OPERATION_CALLBACK)PostRead
		},
		{IRP_MJ_OPERATION_END}
	};

	FLT_CONTEXT_REGISTRATION Contexts[] =
	{
		{
			FLT_INSTANCE_CONTEXT,
			0,
			NULL,
			sizeof(FLT_CONTEXT_REGISTRATION),
			'AP',
			NULL,
			NULL,
			NULL
		},
		{FLT_CONTEXT_END}
	};

	FLT_REGISTRATION FilterRegistration = {0};

	FilterRegistration.Size								= sizeof(FLT_REGISTRATION);
	FilterRegistration.Version							= FLT_REGISTRATION_VERSION;
	FilterRegistration.Flags							= 0;	// Disable driver unloading?
	FilterRegistration.ContextRegistration				= Contexts;
	FilterRegistration.OperationRegistration			= Operations;
	FilterRegistration.FilterUnloadCallback				= FilterUnload;
	FilterRegistration.InstanceSetupCallback			= InstanceSetup;
	FilterRegistration.InstanceQueryTeardownCallback	= NULL;
	FilterRegistration.InstanceTeardownStartCallback	= NULL;
	FilterRegistration.InstanceTeardownCompleteCallback = NULL;
	FilterRegistration.GenerateFileNameCallback			= NULL;
	FilterRegistration.NormalizeNameComponentCallback	= NULL;
	FilterRegistration.NormalizeContextCleanupCallback	= NULL;
#if FLT_MGR_LONGHORN
	FilterRegistration.TransactionNotificationCallback	= NULL;
	FilterRegistration.NormalizeNameComponentExCallback	= NULL;
#endif

	//
	// Initializing global variables.
	//

	g_FilterHandle	= NULL;
	g_hServerPort	= NULL;
	g_hClientPort	= NULL;
	g_pGenericTable = NULL;
	g_RegistryCookie.QuadPart = 0;

	//
	// Registering the FileWall minifilter driver.
	//
	status = FltRegisterFilter(DriverObject,
		&FilterRegistration,
		&g_FilterHandle);

	if(NT_SUCCESS(status))
	{
		g_pGenericTable = PtInitializeTable();

		if (g_pGenericTable != NULL)
		{
			OBJECT_ATTRIBUTES		ObjectAttributes;
			PSECURITY_DESCRIPTOR	pSecurityDescriptor = NULL;
			UNICODE_STRING			PortName;

			//
			// Creating communication port.
			//

			RtlInitUnicodeString(&PortName, L"\\FileWallPort");

			// Building a default security descriptor for use with FltCreateCommunicationPort.
			status = FltBuildDefaultSecurityDescriptor(&pSecurityDescriptor, FLT_PORT_ALL_ACCESS);

			if(NT_SUCCESS(status))
			{
				InitializeObjectAttributes(&ObjectAttributes,
					&PortName,
					OBJ_KERNEL_HANDLE,
					NULL,
					pSecurityDescriptor);

				status = FltCreateCommunicationPort(g_FilterHandle,
					&g_hServerPort,
					&ObjectAttributes,
					NULL,	// No cookie.
					ConnectNotify,
					DisconnectNotify,
					MessageNotify,
					1);		// Maximum connections.

				if(NT_SUCCESS(status))
				{
					//
					// Start filtering I/O.
					//

					status = FltStartFiltering(g_FilterHandle);

					if(NT_SUCCESS(status))
					{
						//
						// Register registry callback.
						//

						status = CmRegisterCallback((PEX_CALLBACK_FUNCTION)&RegistryCallback, NULL, &g_RegistryCookie);
					}
				}

				//  Free the security descriptor in all cases. It is not needed once
				//  the call to FltCreateCommunicationPort() is made.
				FltFreeSecurityDescriptor(pSecurityDescriptor);
			}
		}
		else
			status = STATUS_INSUFFICIENT_RESOURCES;
	}

	// TODO: Correct uninitialization if error occurs.

	if(NT_SUCCESS(status) == FALSE)
	{
		DbgPrint("DriverEntry failed. Status = %d", status);
		DbgPrintStatus(status);
	}

	return status;
}



NTSTATUS FilterUnload(__in FLT_FILTER_UNLOAD_FLAGS Flags)
{
	NTSTATUS status = STATUS_SUCCESS;

	// If the IRQL > APC_LEVEL, PAGED_CODE() causes the system to ASSERT.
	PAGED_CODE();
	// It's just to suppress the warning.
	UNREFERENCED_PARAMETER(Flags);	

	// Close the server communication port.
	FltCloseCommunicationPort(g_hServerPort);
	g_hServerPort = NULL;

	// Unregister filter.
	FltUnregisterFilter(g_FilterHandle);
	g_FilterHandle = NULL;

	// Unregister registry monitor.
	status = CmUnRegisterCallback(g_RegistryCookie);
	ASSERT(NT_SUCCESS(status));
	
	if(g_pGenericTable != NULL)
	{
		PtEmptyTable(g_pGenericTable);
		ExFreePool(g_pGenericTable);
		g_pGenericTable = NULL;
	}

	return STATUS_SUCCESS;
}