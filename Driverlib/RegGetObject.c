#ifndef _NTIFS_
	#include <ntifs.h>
#endif
#include "..\Driver\ProtectedEntry.h"

extern NTSTATUS RegGetObject(int Argument1, PVOID Argument2, PVOID* ppObject)
{
	if(Argument1 < 0)
		return STATUS_INVALID_PARAMETER_1;
	if(NULL == Argument2)
		return STATUS_INVALID_PARAMETER_2;
	if(NULL == ppObject)
		return STATUS_INVALID_PARAMETER_3;

	switch((int)Argument1)
	{
	case RegNtPreDeleteKey:
		*ppObject = ((REG_DELETE_KEY_INFORMATION*)Argument2)->Object;
		break;
	case RegNtPreSetValueKey:
		*ppObject = ((REG_SET_VALUE_KEY_INFORMATION*)Argument2)->Object;
		break;
	case RegNtPreDeleteValueKey:
		*ppObject = ((REG_DELETE_VALUE_KEY_INFORMATION*)Argument2)->Object;
		break;
	case RegNtPreSetInformationKey:
		*ppObject = ((REG_SET_INFORMATION_KEY_INFORMATION*)Argument2)->Object;
		break;
	case RegNtPreEnumerateKey:
		*ppObject = ((REG_ENUMERATE_KEY_INFORMATION*)Argument2)->Object;
		break;
	case RegNtPreEnumerateValueKey:
		*ppObject = ((REG_ENUMERATE_VALUE_KEY_INFORMATION*)Argument2)->Object;
		break;
	case RegNtPreQueryKey:
		*ppObject = ((REG_QUERY_KEY_INFORMATION*)Argument2)->Object;
		break;
	case RegNtPreQueryValueKey:
		*ppObject = ((REG_QUERY_VALUE_KEY_INFORMATION*)Argument2)->Object;
		break;
	case RegNtPreQueryMultipleValueKey:
		*ppObject = ((REG_QUERY_MULTIPLE_VALUE_KEY_INFORMATION*)Argument2)->Object;
		break;
	default:
		return STATUS_INVALID_PARAMETER_1;		
	}

	return STATUS_SUCCESS;
}