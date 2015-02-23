#ifndef _NTIFS_
	#include <ntifs.h>
#endif
#include "..\Driver\ProtectedEntry.h"
#include "Common.h"


//////////////////////////////////////////////////////////////////////////
// Declarations for generic table routines.
//////////////////////////////////////////////////////////////////////////
RTL_GENERIC_COMPARE_RESULTS CompareRoutine(__in	struct _RTL_GENERIC_TABLE	*Table,
										   __in	PVOID						FirstStruct,
										   __in	PVOID						SecondStruct);
PVOID AllocateRoutine(__in	struct _RTL_GENERIC_TABLE	*Table,
					  __in	CLONG						ByteSize);
VOID FreeRoutine(__in	struct _RTL_GENERIC_TABLE	*Table,
				 __in	PVOID						Buffer);


//////////////////////////////////////////////////////////////////////////
// PtInitializeTable routine
//////////////////////////////////////////////////////////////////////////
extern PRTL_GENERIC_TABLE PtInitializeTable() 
{
	PRTL_GENERIC_TABLE pGT = (PRTL_GENERIC_TABLE)ExAllocatePoolWithTag(NonPagedPool, sizeof(RTL_GENERIC_TABLE), POOL_TAG);
	if (pGT == NULL)
		return NULL;

	RtlInitializeGenericTable(pGT,
		CompareRoutine,
		AllocateRoutine,
		FreeRoutine,
		NULL);
	return pGT;
}


//////////////////////////////////////////////////////////////////////////
// Definitions of generic table routines.
//////////////////////////////////////////////////////////////////////////
RTL_GENERIC_COMPARE_RESULTS CompareRoutine(__in	struct _RTL_GENERIC_TABLE	*Table,
										   __in	PVOID						FirstStruct,
										   __in	PVOID						SecondStruct)
{
	PAP_PROTECTED_ENTRY FirstRule	= (PAP_PROTECTED_ENTRY)FirstStruct;
	PAP_PROTECTED_ENTRY SecondRule = (PAP_PROTECTED_ENTRY)SecondStruct;

	UNREFERENCED_PARAMETER(Table);

	if(FirstRule->Hash > SecondRule->Hash)
		return GenericGreaterThan;
	else if(FirstRule->Hash < SecondRule->Hash)
		return GenericLessThan;
	else if(FirstRule->Hash == SecondRule->Hash)
		return GenericEqual;

	return GenericEqual;
}

PVOID AllocateRoutine(__in	struct _RTL_GENERIC_TABLE	*Table,
					  __in	CLONG						ByteSize)
{
	UNREFERENCED_PARAMETER(Table);

	return ExAllocatePoolWithTag(NonPagedPool, ByteSize, POOL_TAG);
}

VOID FreeRoutine(__in	struct _RTL_GENERIC_TABLE	*Table,
				 __in	PVOID						Buffer)
{
	UNREFERENCED_PARAMETER(Table);

	ExFreePool(Buffer);
}