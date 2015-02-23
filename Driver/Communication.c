// Communication.cpp
// Handling communication with user mode via minifilter communication port.

#include "FileWall.h"

NTSTATUS ConnectNotify(__in	PFLT_PORT	ClientPort,
					   __in	PVOID		ServerPortCookie,
					   __in	PVOID		ConnectionContext,
					   __in	ULONG		SizeOfContext,
					   __out PVOID		*ConnectionPortCookie)
{
	ASSERT(g_hClientPort == NULL);

	g_hTrustedProcess	= PsGetCurrentProcessId();
	g_hClientPort		= ClientPort;

	return STATUS_SUCCESS;
}



VOID DisconnectNotify(__in PVOID ConnectionCookie)
{
	PAP_PROTECTED_ENTRY pEntry = NULL;
	
	PAGED_CODE();
	ASSERT(g_FilterHandle	 != NULL);
	ASSERT(g_hTrustedProcess != NULL);

	//
	// Cleaning generic table.
	//	
	PtEmptyTable(g_pGenericTable);

	//  Close our handle to the connection: note, since we limited max connections to 1,
	//  another connect will not be allowed until we return from the disconnect routine.
	FltCloseClientPort(g_FilterHandle, &g_hClientPort);
	g_hClientPort		= NULL;
	g_hTrustedProcess	= NULL;
}



NTSTATUS MessageNotify(__in			PVOID	PortCookie,
					   __in_opt		PVOID	InputBuffer,
					   __in			ULONG	InputBufferLength,
					   __out_opt	PVOID	OutputBuffer,
					   __in			ULONG	OutputBufferLength,
					   __out		PULONG	ReturnOutputBufferLength)
{
	NTSTATUS	status;
	PAP_COMMAND	command;

	PAGED_CODE();

	UNREFERENCED_PARAMETER(PortCookie);

	//
	//                      **** PLEASE READ ****
	//
	//  The INPUT and OUTPUT buffers are raw user mode addresses.  The filter
	//  manager has already done a ProbedForRead (on InputBuffer) and
	//  ProbedForWrite (on OutputBuffer) which guarentees they are valid
	//  addresses based on the access (user mode vs. kernel mode).  The
	//  minifilter does not need to do their own probe.
	//
	//  The filter manager is NOT doing any alignment checking on the pointers.
	//  The minifilter must do this themselves if they care (see below).
	//
	//  The minifilter MUST continue to use a try/except around any access to
	//  these buffers.
	//

	if(InputBuffer != NULL && InputBufferLength == sizeof(AP_COMMAND))
	{
		AP_PROTECTED_ENTRY Entry = { 0 };

		__try
		{
			//
			//  Probe and capture input message: the message is raw user mode
			//  buffer, so need to protect with exception handler
			//
			command = ((PAP_COMMAND)InputBuffer);
		}
		__except(EXCEPTION_EXECUTE_HANDLER)
		{
			return GetExceptionCode();
		}

		//
		// Initialize Entry structure.
		//

		// TODO: Check this code for memory leaks.
		{
			UNICODE_STRING Path;
			RtlInitUnicodeString(&Path, command->Path);

			Entry.Hash = GetHash(&Path);
			Entry.ID = command->ID;
		}

		switch (command->Command)
		{
		case COMMAND_ADD:
			{
				KdPrint(("ADD_DRV %d\n", Entry.ID));
				status = PtAddEntry(g_pGenericTable, &Entry);

				// TODO: Delete this old code.
				//BOOLEAN IsInserted = FALSE;
				//PAP_PROTECTED_ENTRY pInsertedEntry = NULL;

				//pInsertedEntry = RtlInsertElementGenericTable(g_pGenericTable,
				//	&Entry,
				//	sizeof(AP_PROTECTED_ENTRY),
				//	&IsInserted);

				//if(IsInserted == TRUE && pInsertedEntry != NULL)	// Entry inserted.
				//	status = STATUS_SUCCESS;
				//else if(IsInserted == FALSE && pInsertedEntry != NULL)	// Entry not inserted, but already exists.
				//	status = STATUS_OBJECTID_EXISTS;
				//else	// Some error occurred.
				//	status = STATUS_INTERNAL_ERROR;

				break;
			}

		case COMMAND_DEL:
			{
				status = PtDeleteById(g_pGenericTable, command->ID);

				// TODO: Delete this old code.
				//if(RtlDeleteElementGenericTable(g_pGenericTable, &Entry) == FALSE)
				//	status = STATUS_NOT_FOUND;
				//else
				//	status = STATUS_SUCCESS;
				break;
			}

		default:
			status = STATUS_INVALID_PARAMETER;
			break;
		}
	}
	else
		status = STATUS_INVALID_PARAMETER;

	return status;
}

