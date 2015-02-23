#include "Test.Driver.h"

static void __stdcall PtInitializeTable_ReturnsEmptyGenericTable(void)
{
	PRTL_GENERIC_TABLE pGT = PtInitializeTable();

	CFIX_ASSERT(pGT != NULL);
	CFIX_ASSERT(RtlIsGenericTableEmpty(pGT) == TRUE);
}

CFIX_BEGIN_FIXTURE(PtInitializeTable)
	CFIX_FIXTURE_ENTRY(PtInitializeTable_ReturnsEmptyGenericTable)
CFIX_END_FIXTURE()
