using System;
using System.Runtime.InteropServices;


namespace VitaliiPianykh.FileWall.Service.Native
{
    /// <summary>Type of command for the driver. See <see cref="COMMAND.CommandType"/></summary>
    public enum COMMAND_TYPE { ADD, DEL };

    /// <summary>
    /// Represents command for the driver.
    /// We can send it by using <see cref="Driver.SendCommand"/> or <see cref="FltLib.FilterSendMessage"/>
    /// There are two types of commands for the moment.
    /// We can add path to the list of protected entries or delete it from the list.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
    public struct COMMAND
    {
        /// <summary>Type of command.</summary>
        [MarshalAs(UnmanagedType.U4)]
        public COMMAND_TYPE CommandType;

        /// <summary>Rule ID.</summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint ID;

        /// <summary>Path to operate.</summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Driver.MAX_PATH)]
        public String Path;
    };
}