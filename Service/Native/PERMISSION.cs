using System;
using System.Runtime.InteropServices;


namespace VitaliiPianykh.FileWall.Service.Native
{
    /// <summary>
    /// Represents GUI reply for the specified access request.
    /// It's very simple. You can simply allow or deny request by using <see cref="Allow"/>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PERMISSION
    {
        #region FILTER_REPLY_HEADER. Message header required by FltLib functions.

        public Int32 Status;
        public UInt64 MessageId;

        #endregion

        #region REPLY_DATA. User defined part, specifically REPLY_DATA struct.

        /// <summary>
        /// Decision about requested operation.
        /// 1 if allowed.
        /// 0 if denied.
        /// </summary>
        public Int32 Allow;

        /// <summary>
        /// Reserved and must equal 0.
        /// It's used only for alignment. No undocumented features here :)
        /// </summary>
        public UInt32 Reserved;

        #endregion
    };
}