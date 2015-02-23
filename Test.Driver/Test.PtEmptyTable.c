#include "Test.Driver.h"

static void __stdcall PtEmptyTable_CoupleEntriesInTable(void)
{
	PRTL_GENERIC_TABLE pGTable = NULL;
	AP_PROTECTED_ENTRY Entry = { 0 };
	pGTable = PtInitializeTable();
	CFIX_ASSERT(pGTable != NULL);
	Entry.ID = 1;
	Entry.Hash = 111;
	PtAddEntry(pGTable, &Entry);
	Entry.ID = 2;
	Entry.Hash = 222;
	PtAddEntry(pGTable, &Entry);
	Entry.ID = 3;
	Entry.Hash = 333;
	PtAddEntry(pGTable, &Entry);
	CFIX_ASSERT(RtlNumberGenericTableElements(pGTable) == 3);

	CFIX_ASSERT(PtEmptyTable(pGTable) == STATUS_SUCCESS);
	CFIX_ASSERT(RtlIsGenericTableEmpty(pGTable) == TRUE);
}

CFIX_BEGIN_FIXTURE(PtEmptyTable)
	CFIX_FIXTURE_ENTRY(PtEmptyTable_CoupleEntriesInTable)
CFIX_END_FIXTURE()