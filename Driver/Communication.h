#pragma once

/*
The Filter Manager calls this routine whenever a user-mode application calls
FilterConnectCommunicationPort to send a connection request to the minifilter
driver. This parameter is required and cannot be NULL.

Look MSDN "PFLT_CONNECT_NOTIFY".
*/
NTSTATUS ConnectNotify(__in	PFLT_PORT	ClientPort,
					   __in	PVOID		ServerPortCookie,
					   __in	PVOID		ConnectionContext,
					   __in	ULONG		SizeOfContext,
					   __out PVOID		*ConnectionPortCookie);



/*
Callback routine called whenever the user-mode handle count for the client port
reaches zero or when the minifilter driver is about to be unloaded. This
parameter is required and cannot be NULL. 

Look MSDN "PFLT_DISCONNECT_NOTIFY".
*/
VOID DisconnectNotify(__in PVOID ConnectionCookie);



/*
The Filter Manager calls this routine, at IRQL = PASSIVE_LEVEL, whenever a user-mode
application calls FilterSendMessage to send a message to the minifilter driver through the client port.

Look MSDN "PFLT_MESSAGE_NOTIFY"
*/
NTSTATUS MessageNotify(__in			PVOID	PortCookie,
					   __in_opt		PVOID	InputBuffer,
					   __in			ULONG	InputBufferLength,
					   __out_opt	PVOID	OutputBuffer,
					   __in			ULONG	OutputBufferLength,
					   __out		PULONG	ReturnOutputBufferLength);
