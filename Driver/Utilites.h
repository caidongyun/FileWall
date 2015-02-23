#pragma once

// Generates hash from UNICODE_STRING. Using SDBM hashing algorithm.
UINT32	GetHash(__in UNICODE_STRING *pStr);



/*
Sends access request to client application.
pAccessData must be allocated with ExAllocatePoolWithTag or similar function. It will be freed by function.
*/
NTSTATUS ApRequestAccess(__in PACCESS_DATA pAccessData);



// Determines is specified path is under protect.
PAP_PROTECTED_ENTRY ApIsUnderProtect(__in	PRTL_GENERIC_TABLE	pGenericTable,
									 __in	UNICODE_STRING		*pName);


// Converts NTSTATUS to it's string representation. Used with DbgPrintStatus macro.
PCHAR Status2String(__in NTSTATUS status);




