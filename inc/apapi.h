// APAPI.h
// FileWall API
// Structures for interaction between FileWall driver and client application.
#pragma once

//////////////////////////////////////////////////////////////////////////
// Include appropriate headers.
//////////////////////////////////////////////////////////////////////////

#ifdef KERNEL_MODE
	#ifndef _WINDEF_
		#include <windef.h>
	#endif
	#ifndef __FLTKERNEL__
		#include <fltKernel.h>
	#endif	
#else
	#ifndef __FLTUSER_H__
		#include <fltUser.h>
	#endif
#endif


/////////////////////////////////////////////////////////////////////////
// Kernel-mode and user-mode structures.
/////////////////////////////////////////////////////////////////////////

#define AP_MAX_PATH 1024

enum ACCESS_TYPE	{ ACCESS_FILESYSTEM, ACCESS_REGISTRY };
enum COMMAND_TYPE	{ COMMAND_ADD, COMMAND_DEL };
enum FS_OPERATION	{ FSOP_CREATE, FSOP_READ, FSOP_WRITE, FSOP_OTHER };


// Data sent to client application.
// Contains all necessary data to detail access request.
// Note that it is part of ACCESS_REQUEST struct.
typedef struct _ACCESS_DATA
{
	UINT32	ProcessID;			// Process ID.
	UINT32	AccessType;			// Type of access. Member of ACCESS_TYPE enum.
	INT32	Operation;			// If (AccessType == ACCESS_REGISTRY) DetailedAccessType will contain REG_NOTIFY_CLASS Value, ACCESS_FILESYSTEM - FILESYS_OPERATION.
	UINT32	ID;					// ID of path.
	WCHAR	Path[AP_MAX_PATH];	// [2048 Bytes] Full path to entry.
} ACCESS_DATA, *PACCESS_DATA;


// Data sent in reply to ACCESS_REQUESTs.
// Note that it is part of PERMISSION struct.
typedef struct _REPLY_DATA
{
	BOOLEAN	Allow;		// TRUE if request is permitted, FALSE if not permitted.
	UINT32	Reserved;	// Alignment. Must be NULL.
} REPLY_DATA, *PREPLY_DATA;


// FileWall filter command.
// For the moment allow add/delete entries from "protected" list.
typedef struct _AP_COMMAND
{
	UINT32	Command;			// Command code. Member of COMMAND_TYPE enum.
	UINT32	ID;					// ID of rule.
	WCHAR	Path[AP_MAX_PATH];	// Full path to entry.
} AP_COMMAND, *PAP_COMMAND;


//////////////////////////////////////////////////////////////////////////
// User-mode structures.
//////////////////////////////////////////////////////////////////////////

// Access request.
typedef struct _FS_ACCESS_REQUEST
{
	FILTER_MESSAGE_HEADER	MessageHeader;
	ACCESS_DATA				AccessData;	
} ACCESS_REQUEST, *PACCESS_REQUEST;



// Permission.
typedef struct _PERMISSION
{
	FILTER_REPLY_HEADER ReplyHeader;
	REPLY_DATA			ReplyData;
} PERMISSION, *PPERMISSION;