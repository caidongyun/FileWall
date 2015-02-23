#pragma once

/*
A filter driver's RegistryCallback routine monitor or block a registry operation.
*/
NTSTATUS RegistryCallback(__in PVOID  CallbackContext,
						  __in PVOID  Argument1,
						  __in PVOID  Argument2);