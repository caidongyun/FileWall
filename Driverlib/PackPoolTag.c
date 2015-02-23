#ifndef _NTIFS_
	#include <ntifs.h>
#endif
#include "..\Driver\ProtectedEntry.h"

extern ULONG32	PackPoolTag(__in PCHAR func)
{
	int len = strlen(func);
	int caps_count = 0;
	int shift_count = 0;
	unsigned long res = 0;
	int i;

	{
		int sym = '@';
		sym <<= 24;

		res >>= 8;
		res |= sym;

		shift_count++;
	}

	for (i=0; i<len && caps_count != 3; i++)
	{
		if(func[i] == toupper(func[i]))
		{
			int sym = func[i];
			sym <<= 24;

			res >>= 8;
			res |= sym;
			caps_count++;
			shift_count++;
		}
	}

	if(caps_count == 0)
	{
		for (i=0; i<len && i<3; i++)
		{
			int sym = func[i];
			sym <<= 24;

			res >>= 8;
			res |= sym;
			shift_count++;
		}
	}

	for (; shift_count < 4; shift_count++)
		res	>>=8;

	return res;
}