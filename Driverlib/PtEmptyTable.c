#ifndef _NTIFS_
#include <ntifs.h>
#endif
#include "..\Driver\ProtectedEntry.h"

extern NTSTATUS PtEmptyTable(__in PRTL_GENERIC_TABLE pGT)
{
	NTSTATUS status = STATUS_SUCCESS;

	PAP_PROTECTED_ENTRY pEntryToDelete = RtlEnumerateGenericTable(pGT, TRUE);
	while(pEntryToDelete != NULL)
	{
		BOOLEAN IsDeleted = FALSE;
		IsDeleted = RtlDeleteElementGenericTable(pGT, pEntryToDelete);
		if(IsDeleted == FALSE)
			status = STATUS_CANNOT_DELETE;

		pEntryToDelete = RtlEnumerateGenericTable(pGT, TRUE);
	}

	return status;
}