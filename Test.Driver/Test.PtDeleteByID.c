#include "Test.Driver.h"

// TODO:
// All fixtures must free all resources.
// So we must add replace ExAllocatePool to ExAllocatePoolWithTag, with macro from driver project [POOL_TAG].
// Then test it!
// Check http://www.koders.com/c/fid5F2C5898DA6AF71829012587E2E30B7F46DC0B4A.aspx

static void __stdcall TestInitialize(void)
{
	g_pGenericTable = PtInitializeTable();
}

static void __stdcall TestCleanup(void)
{
	PtEmptyTable(g_pGenericTable);
	ExFreePool(g_pGenericTable);
	g_pGenericTable = NULL;
}

//
// Test fixtures.
// 

static void __stdcall PtDeleteById_pGenericTable_NULL(void)
{
	CFIX_ASSERT(STATUS_INVALID_PARAMETER_1 == PtDeleteById(NULL, 0));
}

static void __stdcall PtDeleteById_PathID_Negative(void)
{
	PRTL_GENERIC_TABLE pGT = (PRTL_GENERIC_TABLE)123; // Don't care, PtDeleteById any way will not touch this value.
	CFIX_ASSERT(STATUS_INVALID_PARAMETER_2 == PtDeleteById(pGT, -1));
}

static void __stdcall PtDeleteById_EntriesSuccessfullyDeleted(void)
{
	NTSTATUS status;

	// Initialize generic table.

	PRTL_GENERIC_TABLE pGT = NULL;
	pGT = PtInitializeTable();

	// Add entries which will be deleted.
	{
		AP_PROTECTED_ENTRY EntryToDelete = { 0 };

		EntryToDelete.Hash = 111;
		EntryToDelete.ID = 1;
		status = PtAddEntry(pGT, &EntryToDelete);
		CFIX_ASSERT_MESSAGE(NT_SUCCESS(status), L"PtAddEntry failed.");

		EntryToDelete.Hash = 222;
		EntryToDelete.ID = 1;
		status = PtAddEntry(pGT, &EntryToDelete);
		CFIX_ASSERT_MESSAGE(NT_SUCCESS(status), L"PtAddEntry failed.");
	}

	CFIX_ASSERT(STATUS_SUCCESS == PtDeleteById(pGT, 1));
	CFIX_ASSERT(RtlIsGenericTableEmpty(pGT) == TRUE);
}

static void __stdcall PtDeleteById_EntriesDeletedExceptOthers(void)
{
	NTSTATUS status;
	AP_PROTECTED_ENTRY Entry1 = { 0 };
	AP_PROTECTED_ENTRY Entry2 = { 0 };
	AP_PROTECTED_ENTRY Entry3 = { 0 };
	PRTL_GENERIC_TABLE pGT = PtInitializeTable();

	// Add couple entries with different ID's.
	Entry1.Hash = 111;
	Entry1.ID = 1;
	status = PtAddEntry(pGT, &Entry1);
	CFIX_ASSERT_MESSAGE(NT_SUCCESS(status), L"PtAddEntry failed.");
	Entry2.Hash = 222;
	Entry2.ID = 2;
	status = PtAddEntry(pGT, &Entry2);
	CFIX_ASSERT_MESSAGE(NT_SUCCESS(status), L"PtAddEntry failed.");
	Entry3.Hash = 333;
	Entry3.ID = 3;
	status = PtAddEntry(pGT, &Entry3);
	CFIX_ASSERT_MESSAGE(NT_SUCCESS(status), L"PtAddEntry failed.");

	// Try to delete first element.
	CFIX_ASSERT(STATUS_SUCCESS == PtDeleteById(pGT, 1));

	// PtDeleteById had to delete first entry.
	CFIX_ASSERT(RtlLookupElementGenericTable(pGT, &Entry1) == NULL);
	// ... but second and third entries had to remain.
	CFIX_ASSERT(RtlLookupElementGenericTable(pGT, &Entry2) != NULL);
	CFIX_ASSERT(RtlLookupElementGenericTable(pGT, &Entry3) != NULL);
	
	// CLEAR ALL RESOURCES!
	PtEmptyTable(pGT);
	CFIX_ASSERT(RtlNumberGenericTableElements(pGT) == 0);	
}


static void __stdcall PtDeleteById_EmptyTable(void)
{
	// Try to call PtDeleteById on empty table.
	CFIX_ASSERT(PtDeleteById(g_pGenericTable, 4) == STATUS_NOT_FOUND);
}

static void __stdcall PtDeleteById_NotAddedID(void)
{
	AP_PROTECTED_ENTRY Entry = { 0 };
	Entry.ID	= 1;
	Entry.Hash	= 111;

	CFIX_ASSERT(PtAddEntry(g_pGenericTable, &Entry) == STATUS_SUCCESS);

	CFIX_ASSERT(PtDeleteById(g_pGenericTable, 2) == STATUS_NOT_FOUND);
}



//
// Generic table utilites declarations.
//



CFIX_BEGIN_FIXTURE(PtDeleteByID)
	CFIX_FIXTURE_BEFORE(TestInitialize)
	CFIX_FIXTURE_AFTER(TestCleanup)


	CFIX_FIXTURE_ENTRY(PtDeleteById_pGenericTable_NULL)
	CFIX_FIXTURE_ENTRY(PtDeleteById_PathID_Negative)
	CFIX_FIXTURE_ENTRY(PtDeleteById_EntriesSuccessfullyDeleted)
	CFIX_FIXTURE_ENTRY(PtDeleteById_EntriesDeletedExceptOthers)
	CFIX_FIXTURE_ENTRY(PtDeleteById_EmptyTable)
	CFIX_FIXTURE_ENTRY(PtDeleteById_NotAddedID)	
CFIX_END_FIXTURE()