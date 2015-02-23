using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace VitaliiPianykh.FileWall.Service.Native
{
    /// <summary>Type of requested access.</summary>
    public enum ACCESS_TYPE { FILESYSTEM, REGISTRY };

    public enum FILESYS_OPERATION { CREATE, READ, WRITE, OTHER };

    public enum REG_NOTIFY_CLASS
    {
        RegNtDeleteKey,
        RegNtPreDeleteKey = RegNtDeleteKey,
        RegNtSetValueKey,
        RegNtPreSetValueKey = RegNtSetValueKey,
        RegNtDeleteValueKey,
        RegNtPreDeleteValueKey = RegNtDeleteValueKey,
        RegNtSetInformationKey,
        RegNtPreSetInformationKey = RegNtSetInformationKey,
        RegNtRenameKey,
        RegNtPreRenameKey = RegNtRenameKey,
        RegNtEnumerateKey,
        RegNtPreEnumerateKey = RegNtEnumerateKey,
        RegNtEnumerateValueKey,
        RegNtPreEnumerateValueKey = RegNtEnumerateValueKey,
        RegNtQueryKey,
        RegNtPreQueryKey = RegNtQueryKey,
        RegNtQueryValueKey,
        RegNtPreQueryValueKey = RegNtQueryValueKey,
        RegNtQueryMultipleValueKey,
        RegNtPreQueryMultipleValueKey = RegNtQueryMultipleValueKey,
        RegNtPreCreateKey,
        RegNtPostCreateKey,
        RegNtPreOpenKey,
        RegNtPostOpenKey,
        RegNtKeyHandleClose,
        RegNtPreKeyHandleClose = RegNtKeyHandleClose,
        //
        // The following values apply only to Microsoft Windows Server 2003 and later.
        //    
        RegNtPostDeleteKey,
        RegNtPostSetValueKey,
        RegNtPostDeleteValueKey,
        RegNtPostSetInformationKey,
        RegNtPostRenameKey,
        RegNtPostEnumerateKey,
        RegNtPostEnumerateValueKey,
        RegNtPostQueryKey,
        RegNtPostQueryValueKey,
        RegNtPostQueryMultipleValueKey,
        RegNtPostKeyHandleClose,
        RegNtPreCreateKeyEx,
        RegNtPostCreateKeyEx,
        RegNtPreOpenKeyEx,
        RegNtPostOpenKeyEx,
        //
        // The following values apply only to Microsoft Windows Vista and later.
        //    
        RegNtPreFlushKey,
        RegNtPostFlushKey,
        RegNtPreLoadKey,
        RegNtPostLoadKey,
        RegNtPreUnLoadKey,
        RegNtPostUnLoadKey,
        RegNtPreQueryKeySecurity,
        RegNtPostQueryKeySecurity,
        RegNtPreSetKeySecurity,
        RegNtPostSetKeySecurity,
        RegNtCallbackContextCleanup,
        MaxRegNtNotifyClass 
    };

    /// <summary>
    /// Represents access request from the driver.
    /// Driver just sends second part (ACCESS_DATA struct) by using FltSendMessage function.
    /// And we receive filled structure by using <see cref="Driver.GetRequest"/> or <see cref="FltLib.FilterGetMessage"/>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
    public struct ACCESS_REQUEST
    {
        #region FILTER_MESSAGE_HEADER. Message header required by FltLib functions.

        public UInt32 ReplyLength;

        public UInt64 MessageId;

        #endregion

        #region ACCESS_DATA. User defined part, specifically ACCESS_DATA struct.

        /// <summary>Caller process ID.</summary>
        public UInt32 ProcessID;

        /// <summary>Type of requested access.</summary>
        [MarshalAs(UnmanagedType.U4)]
        public ACCESS_TYPE AccessType;

        /// <summary>Operation which process tries to do. See <see cref="FilesysOperation"/> and <see cref="RegOperation"/></summary>
        [MarshalAs(UnmanagedType.U4)]
        public int Operation;

        [MarshalAs(UnmanagedType.U4)]
        public int RuleID;

        /// <summary>Path to accessed entity.</summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Driver.MAX_PATH)]
        public String Path;

        #endregion

        #region Utilites
        
        /// <summary>Access this property if <see cref="AccessType"/> is <see cref="ACCESS_TYPE.FILESYSTEM"/> to get operation type.</summary>
        public FILESYS_OPERATION FilesysOperation { get { return (FILESYS_OPERATION)Operation; } }

        /// <summary>Access this property if <see cref="AccessType"/> is <see cref="ACCESS_TYPE.REGISTRY"/> to get operation type.</summary>
        public REG_NOTIFY_CLASS RegOperation { get { return (REG_NOTIFY_CLASS)Operation; } }

        /// <summary>Returns process path by it's ID.</summary>
        public String ProcessPath
        {
            get
            {
                var p = Process.GetProcessById((Int32)ProcessID);

                if (ProcessID == 0 || ProcessID == 4)// Idle and System.
                    return p.ProcessName;
                return p.MainModule.FileName;
            }
        }

        #endregion
    };
}