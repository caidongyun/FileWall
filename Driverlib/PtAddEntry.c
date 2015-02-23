#ifndef _NTIFS_
	#include <ntifs.h>
#endif
#include "..\Driver\ProtectedEntry.h"

extern NTSTATUS PtAddEntry(__in PRTL_GENERIC_TABLE pGenericTable, __in PAP_PROTECTED_ENTRY pEntry)
{
	NTSTATUS status;
	BOOLEAN IsInserted = FALSE;
	PAP_PROTECTED_ENTRY pInsertedEntry = NULL;

	if(NULL == pGenericTable)
		return STATUS_INVALID_PARAMETER_1;
	if(NULL == pEntry)
		return STATUS_INVALID_PARAMETER_2;

	pInsertedEntry = RtlInsertElementGenericTable(pGenericTable,
		pEntry,
		sizeof(AP_PROTECTED_ENTRY),
		&IsInserted);

	if(IsInserted == TRUE && pInsertedEntry != NULL)// Entry inserted.
		status = STATUS_SUCCESS;
	else if(IsInserted == FALSE && pInsertedEntry != NULL)// Entry not inserted, but already exists.
		status = STATUS_OBJECTID_EXISTS;
	else
		status = STATUS_INTERNAL_ERROR;	// Internal error occurred.

	return status;
}
