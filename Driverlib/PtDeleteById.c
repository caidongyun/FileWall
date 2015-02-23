#ifndef _NTIFS_
	#include <ntifs.h>
#endif
#include "..\Driver\ProtectedEntry.h"
#include "Common.h"

typedef struct _ENTRY_TO_DELETE
{
	INT32	Hash;
} ENTRY_TO_DELETE, *PENTRY_TO_DELETE;

RTL_GENERIC_COMPARE_RESULTS DeleteList_CompareRoutine(__in struct _RTL_GENERIC_TABLE *Table, __in PVOID FirstStruct, __in PVOID SecondStruct);
PVOID DeleteList_AllocateRoutine(__in struct _RTL_GENERIC_TABLE *Table, __in CLONG ByteSize);
VOID DeleteList_FreeRoutine(__in struct _RTL_GENERIC_TABLE *Table, __in PVOID Buffer);


extern NTSTATUS PtDeleteById(__in PRTL_GENERIC_TABLE pGenericTable, __in INT32 PathID)
{
	NTSTATUS			status = STATUS_SUCCESS;
	PAP_PROTECTED_ENTRY	pEntry = NULL;
	PRTL_GENERIC_TABLE	pgtDeleteList = NULL;
	PENTRY_TO_DELETE	pEntryToDelete = NULL;	

	if(NULL == pGenericTable)
		return STATUS_INVALID_PARAMETER_1;
	if(PathID < 0)
		return STATUS_INVALID_PARAMETER_2;

	// Initializa "to delete" list.
	pgtDeleteList = (PRTL_GENERIC_TABLE)ExAllocatePoolWithTag(NonPagedPool, sizeof(RTL_GENERIC_TABLE), POOL_TAG);
	if (pgtDeleteList == NULL)
		return STATUS_INSUFFICIENT_RESOURCES;
	RtlInitializeGenericTable(pgtDeleteList,
		DeleteList_CompareRoutine,
		DeleteList_AllocateRoutine,
		DeleteList_FreeRoutine,
		NULL);

	// Iterate through "protected" list, and add each entry with proper ID into "delete list"
	for (pEntry = RtlEnumerateGenericTable(pGenericTable, TRUE);
		pEntry != NULL;
		pEntry = RtlEnumerateGenericTable (pGenericTable, FALSE))
	{
		ENTRY_TO_DELETE EntryToDelete = { 0 };
		PENTRY_TO_DELETE pInsertedEntry = NULL;
		BOOLEAN IsInserted = FALSE;

		if(pEntry->ID != PathID)
			continue;

		EntryToDelete.Hash = pEntry->Hash;

		pInsertedEntry = RtlInsertElementGenericTable(pgtDeleteList,
			&EntryToDelete,
			sizeof(ENTRY_TO_DELETE),
			&IsInserted);

		if(IsInserted == FALSE && pInsertedEntry != NULL)// Entry not inserted, but already exists.
			KdBreakPoint();	// It's impossible.
	}

	if(RtlIsGenericTableEmpty(pgtDeleteList) == FALSE)
	{
		pEntryToDelete = RtlEnumerateGenericTable(pgtDeleteList, TRUE);
		while(pEntryToDelete != NULL)
		{
			AP_PROTECTED_ENTRY ProtectedEntry = {0};
			ProtectedEntry.Hash = pEntryToDelete->Hash;

			// Delete from protected table.
			RtlDeleteElementGenericTable(pGenericTable, &ProtectedEntry);

			// Delete from "to delete list".
			RtlDeleteElementGenericTable(pgtDeleteList, pEntryToDelete);

			pEntryToDelete = RtlEnumerateGenericTable(pgtDeleteList, TRUE);
		}

		status = STATUS_SUCCESS;
	}
	else
		status = STATUS_NOT_FOUND;

	ExFreePool(pgtDeleteList);

	return status;
}

RTL_GENERIC_COMPARE_RESULTS DeleteList_CompareRoutine(__in	struct _RTL_GENERIC_TABLE	*Table,
													  __in	PVOID						FirstStruct,
													  __in	PVOID						SecondStruct)
{
	PENTRY_TO_DELETE FirstEntry	= (PENTRY_TO_DELETE)FirstStruct;
	PENTRY_TO_DELETE SecondEntry = (PENTRY_TO_DELETE)SecondStruct;

	UNREFERENCED_PARAMETER(Table);

	if(FirstEntry->Hash > SecondEntry->Hash)
		return GenericGreaterThan;
	else if(FirstEntry->Hash < SecondEntry->Hash)
		return GenericLessThan;
	else if(FirstEntry->Hash == SecondEntry->Hash)
		return GenericEqual;

	return GenericEqual;
}

PVOID DeleteList_AllocateRoutine(__in	struct _RTL_GENERIC_TABLE	*Table,
								 __in	CLONG						ByteSize)
{
	UNREFERENCED_PARAMETER(Table);
	return ExAllocatePoolWithTag(NonPagedPool, ByteSize, POOL_TAG);
}

VOID DeleteList_FreeRoutine(__in	struct _RTL_GENERIC_TABLE	*Table,
							__in	PVOID						Buffer)
{
	UNREFERENCED_PARAMETER(Table);
	ExFreePool(Buffer);
}