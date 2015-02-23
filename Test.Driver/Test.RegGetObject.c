#include "Test.Driver.h"

// Array with all possible values.
int SupportedValues[] =
{
	RegNtPreDeleteKey,
	RegNtPreSetValueKey,
	RegNtPreDeleteValueKey,
	RegNtPreSetInformationKey,
	RegNtPreEnumerateKey,
	RegNtPreEnumerateValueKey,
	RegNtPreQueryKey,
	RegNtPreQueryValueKey,
	RegNtPreQueryMultipleValueKey
};

static void __stdcall RegGetObject_Argument1_Negative(void)
{
	PVOID pObject = NULL;
	CFIX_ASSERT(STATUS_INVALID_PARAMETER_1 == RegGetObject(-1, NULL, &pObject));
	// If RegGetObject fails it must not return any pointers.
	CFIX_ASSERT(NULL == pObject);
}

static void __stdcall RegGetObject_Argument1_AllSupportedValues(void)
{
	// Try to run RegGetObject with every supported value.
	int i;
	for (i = 0; i < sizeof(SupportedValues)/sizeof(int); i++)
	{
		NTSTATUS					status	= STATUS_SUCCESS;
		PVOID						pObject = NULL;
		REG_DELETE_KEY_INFORMATION	KeyInfo = { 0 };// It's no matter what struct passed to RegGetObject, anyway the first member is pObject.
		 
		KeyInfo.Object = (PVOID)133; // Just pass any value, RegGetObject will not use it.
		status = RegGetObject(SupportedValues[i], &KeyInfo, &pObject);

		CFIX_ASSERT_MESSAGE(NT_SUCCESS(status), L"RegGetObject fails if Argument1=%d", i);
		CFIX_ASSERT(((PVOID)133) == pObject);
	}
}

static void __stdcall RegGetObject_Argument1_NotSupportedValues()
{
	// Try to run RegGetObject with every not supported value.
	int i, j;
	for (i = 0; i < MaxRegNtNotifyClass; i++)
	{
		NTSTATUS					status	= STATUS_SUCCESS;
		PVOID						pObject = NULL;
		REG_DELETE_KEY_INFORMATION	KeyInfo = { 0 };// It's no matter what struct passed to RegGetObject, anyway the first member is pObject.

		// Continue if it's supported value.
		for (j = 0; j < sizeof(SupportedValues)/sizeof(int); j++)
			if(i == SupportedValues[j])
				break;
		if(i == SupportedValues[j])
			continue;

		KeyInfo.Object = (PVOID)133; // Just pass any value, RegGetObject will not use it.
		status = RegGetObject(i, &KeyInfo, &pObject);

		CFIX_ASSERT_MESSAGE(STATUS_INVALID_PARAMETER_1 == status, L"RegGetObject must fail if Argument1=%d, but it passes.", i);
		CFIX_ASSERT(NULL == pObject);
	}
}

static void __stdcall RegGetObject_Argument2_NULL(void)
{
	PVOID pObject = NULL;
	CFIX_ASSERT(STATUS_INVALID_PARAMETER_2 == RegGetObject(RegNtPreDeleteKey, NULL, &pObject));
	// If RegGetObject fails it must not return any pointers.
	CFIX_ASSERT(NULL == pObject);
}

static void __stdcall RegGetObject_ppObject_NULL(void)
{
	REG_DELETE_KEY_INFORMATION	KeyInfo = { 0 };// It's no matter what struct passed to RegGetObject, anyway the first member is pObject.

	CFIX_ASSERT(STATUS_INVALID_PARAMETER_3 == RegGetObject(RegNtPreDeleteKey, &KeyInfo, NULL));
}



CFIX_BEGIN_FIXTURE(TestUtilites)
        CFIX_FIXTURE_ENTRY(RegGetObject_Argument1_Negative)
		CFIX_FIXTURE_ENTRY(RegGetObject_Argument1_AllSupportedValues)
		CFIX_FIXTURE_ENTRY(RegGetObject_Argument1_NotSupportedValues)
		CFIX_FIXTURE_ENTRY(RegGetObject_Argument2_NULL)		
		CFIX_FIXTURE_ENTRY(RegGetObject_ppObject_NULL)
CFIX_END_FIXTURE()