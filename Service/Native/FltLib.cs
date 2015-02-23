using System;
using System.Runtime.InteropServices;

namespace VitaliiPianykh.FileWall.Service.Native
{
    /// <summary>Filter Driver Library functions.</summary>
    public class FltLib
    {
        // Default protected constructor to avoid creating instances.
        protected FltLib()
        { }

        private static FltLib instance;
        // TODO: Instance must be internal.
        public static FltLib Instance
        {
            get
            {
                if (instance == null)
                    instance = new FltLib();
                return instance;
            }
        }


        #region Public Methods
        
        public virtual Int32 FilterConnectCommunicationPort(String lpPortName,
                                                            UInt32 dwOptions,
                                                            IntPtr lpContext,
                                                            UInt32 dwSizeOfContext,
                                                            IntPtr lpSecurityAttributes,
                                                            out IntPtr hPort)
        {
            return _FilterConnectCommunicationPort(lpPortName,
                                                   dwOptions,
                                                   lpContext,
                                                   dwSizeOfContext,
                                                   lpSecurityAttributes,
                                                   out hPort);
        }

        public virtual int FilterGetMessage(IntPtr hPort,
                                            ref ACCESS_REQUEST lpMessageBuffer,
                                            int dwMessageBufferSize,
                                            IntPtr lpOverlapped)
        {
            return _FilterGetMessage(hPort, ref lpMessageBuffer, dwMessageBufferSize, lpOverlapped);
        }

        public virtual int FilterReplyMessage(IntPtr hPort,
                                              ref PERMISSION lpReplyBuffer,
                                              Int32 dwReplyBufferSize)
        {
            return _FilterReplyMessage(hPort, ref lpReplyBuffer, dwReplyBufferSize);
        }

        public virtual int FilterSendMessage(IntPtr hPort,
                                     ref COMMAND lpInBuffer,
                                     UInt32 dwInBufferSize,
                                     IntPtr lpOutBuffer,
                                     UInt32 dwOutBufferSize,
                                     out UInt32 lpBytesReturned)
        {
            return _FilterSendMessage(hPort, ref lpInBuffer, dwInBufferSize, lpOutBuffer, dwOutBufferSize, out lpBytesReturned);
        }


        public virtual bool CloseHandle(IntPtr hObject)
        {
            return _CloseHandle(hObject);
        }

        #endregion


        #region Native routines declarations

        [DllImport("FltLib", EntryPoint = "FilterConnectCommunicationPort", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        private static extern Int32 _FilterConnectCommunicationPort(String lpPortName,
                                                                  UInt32              dwOptions,
                                                                  IntPtr              lpContext,
                                                                  UInt32              dwSizeOfContext,
                                                                  IntPtr              lpSecurityAttributes,
                                                                  out IntPtr  hPort);



        [DllImport("FltLib", EntryPoint = "FilterGetMessage", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto)]
        private static extern int _FilterGetMessage(IntPtr hPort,
                                                  ref ACCESS_REQUEST  lpMessageBuffer,
                                                  int             dwMessageBufferSize,
                                                  IntPtr          lpOverlapped);



        [DllImport("FltLib", EntryPoint = "FilterReplyMessage", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto)]
        private static extern int _FilterReplyMessage(IntPtr hPort,
                                                    ref PERMISSION   lpReplyBuffer,
                                                    Int32            dwReplyBufferSize);



        [DllImport("FltLib", EntryPoint = "FilterSendMessage", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto)]
        private static extern int _FilterSendMessage(IntPtr hPort,
                                                   ref COMMAND    lpInBuffer,
                                                   UInt32         dwInBufferSize,
                                                   IntPtr         lpOutBuffer,
                                                   UInt32         dwOutBufferSize,
                                                   out UInt32     lpBytesReturned);

        [DllImport("Kernel32", EntryPoint = "CloseHandle", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern Boolean _CloseHandle(IntPtr hObject);

        #endregion
    }
}