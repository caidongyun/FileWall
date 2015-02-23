#pragma once

#include <fltKernel.h>
#include <ntifs.h>
#include <wdm.h>

#define KERNEL_MODE

#include "..\inc\apapi.h"
#include "ProtectedEntry.h"
#include "..\Driverlib\Common.h"


#include "Communication.h"
#include "Filesystem.h"
#include "Registry.h"


//////////////////////////////////////////////////////////////////////////
// Global variables.
//////////////////////////////////////////////////////////////////////////

PFLT_FILTER		g_FilterHandle;
PFLT_PORT		g_hServerPort;
PFLT_PORT		g_hClientPort;
LARGE_INTEGER	g_RegistryCookie;
HANDLE			g_hTrustedProcess;
PRTL_GENERIC_TABLE g_pGenericTable;

//////////////////////////////////////////////////////////////////////////
// Declarations.
//////////////////////////////////////////////////////////////////////////

/*
Description:
DriverEntry is the first routine called after a driver is loaded,
and is responsible for initializing the driver.

Arguments:
DriverObject - Caller-supplied pointer to a DRIVER_OBJECT structure. This is the driver's driver object. 
RegistryPath - Pointer to a counted Unicode string specifying the path to the driver's registry key.

Return Value:
If the routine succeeds, it must return STATUS_SUCCESS.
Otherwise, it must return one of the error status values defined in ntstatus.h.
*/
NTSTATUS DriverEntry(__in PDRIVER_OBJECT DriverObject, __in PUNICODE_STRING RegistryPath);

/*
Description:
This is the unload routine for this filter driver. This is called
when the minifilter is about to be unloaded. We can fail this unload
request if this is not a mandatory unloaded indicated by  the Flags
parameter.

Arguments:
Flags - Indicating if this is a mandatory unload.

Return Value:
Returns the final status of this operation.
*/
NTSTATUS FilterUnload(__in FLT_FILTER_UNLOAD_FLAGS Flags);

#include "Utilites.h"