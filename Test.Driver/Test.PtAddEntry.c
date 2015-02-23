#include "Test.Driver.h"

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

static void __stdcall PtAddEntry_pGenericTable_NULL(void)
{
	AP_PROTECTED_ENTRY Entry = { 0 };

	CFIX_ASSERT(PtAddEntry(NULL, &Entry) == STATUS_INVALID_PARAMETER_1);
}


static void __stdcall PtAddEntry_pEntry_NULL(void)
{
	CFIX_ASSERT(PtAddEntry(g_pGenericTable, NULL) == STATUS_INVALID_PARAMETER_2);	
}

static void __stdcall PtAddEntry_UsualCall(void)
{
	AP_PROTECTED_ENTRY Entry = { 0 };
	Entry.ID = 1;
	Entry.Hash = 111;

	CFIX_ASSERT(PtAddEntry(g_pGenericTable, &Entry) == STATUS_SUCCESS);
	CFIX_ASSERT(RtlLookupElementGenericTable(g_pGenericTable, &Entry) != NULL);
}

static void __stdcall PtAddEntry_AddingSimilairElement(void)
{
	AP_PROTECTED_ENTRY Entry1 = { 0 };
	AP_PROTECTED_ENTRY Entry2 = { 0 };
	Entry1.ID	= 1;
	Entry1.Hash	= 111;
	Entry2.ID	= 1;
	Entry2.Hash	= 111;

	PtAddEntry(g_pGenericTable, &Entry1);

	CFIX_ASSERT(PtAddEntry(g_pGenericTable, &Entry1) == STATUS_OBJECTID_EXISTS);
}

CFIX_BEGIN_FIXTURE(PtAddEntry)
	CFIX_FIXTURE_BEFORE(TestInitialize)
	CFIX_FIXTURE_AFTER(TestCleanup)

	CFIX_FIXTURE_ENTRY(PtAddEntry_pGenericTable_NULL)
	CFIX_FIXTURE_ENTRY(PtAddEntry_pEntry_NULL)
	CFIX_FIXTURE_ENTRY(PtAddEntry_UsualCall)
	CFIX_FIXTURE_ENTRY(PtAddEntry_AddingSimilairElement)
	//CFIX_FIXTURE_ENTRY()
	//CFIX_FIXTURE_ENTRY()
CFIX_END_FIXTURE()