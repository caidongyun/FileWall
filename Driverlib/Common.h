// NOTE: Needs <ntifs.h>!!!

// Packs calling function name to ULONG. Used with POOL_TAG and ExAllocatePoolWithTag.
extern ULONG32	PackPoolTag(__in PCHAR func);

// Routines for works with protected table.
// Prefix Pt means Protected table.
extern PRTL_GENERIC_TABLE PtInitializeTable();
extern NTSTATUS PtAddEntry(__in PRTL_GENERIC_TABLE, __in PAP_PROTECTED_ENTRY);
extern NTSTATUS PtDeleteById(__in PRTL_GENERIC_TABLE pGenericTable, __in INT32 PathID);
extern NTSTATUS PtEmptyTable(__in PRTL_GENERIC_TABLE pGT);

extern NTSTATUS RegGetObject(int Argument1, PVOID Argument2, PVOID* ppObject);


//////////////////////////////////////////////////////////////////////////
// Macro definitions
//////////////////////////////////////////////////////////////////////////
#if DBG
	#define DbgPrintStatus(status) {KdPrint(("%s\n", Status2String(status)));}
	#define POOL_TAG PackPoolTag(__FUNCTION__)
#else
	#define DbgPrintStatus
	#define POOL_TAG '_PA@'
#endif