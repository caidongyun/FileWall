using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using VitaliiPianykh.FileWall.Service.Native;
using VitaliiPianykh.FileWall.Shared;


namespace Test.Service
{
    public class FltLibStub : FltLib
    {
        public new static FltLibStub Instance
        {
            get
            {
                return (FltLibStub) FltLib.Instance;
            }
        }

        public Dictionary<string, uint> Paths = new Dictionary<string, uint>();
        public int FilterConnectCommunicationPortReturnCode { get; set; }
        public int FilterSendMessageReturnCode { get; set; }

        public int FilterReplyMessageReturnCode { get; set; }
        public bool CloseHandleReturn { get; set; }

        private readonly List<IntPtr> registeredHandles = new List<IntPtr>();

        public FltLibStub()
        {
            CloseHandleReturn = true;// Usually CloseHandle not fails.
        }

        public override int FilterConnectCommunicationPort(string lpPortName, uint dwOptions, IntPtr lpContext, uint dwSizeOfContext, IntPtr lpSecurityAttributes, out IntPtr hPort)
        {
            if (registeredHandles.Count == 0)
                hPort = (IntPtr)100;
            else
                hPort = new IntPtr((int)registeredHandles[registeredHandles.Count - 1] + 1);

            registeredHandles.Add(hPort);

            return FilterConnectCommunicationPortReturnCode;
        }

        public override int FilterGetMessage(IntPtr hPort, ref ACCESS_REQUEST lpMessageBuffer, int dwMessageBufferSize, IntPtr lpOverlapped)
        {
            lpMessageBuffer = getMessageRequest;
            return getMessageReturn;
        }

        private ulong lastAllowedMessageID;
        private ulong lastBlockedMessageID;
        
        /// <summary>The ID of last allowed message <see cref="PERMISSION.MessageId"/>.</summary>
        public ulong LastAllowedMessageID
        {
            get
            {
                if(lastAllowedMessageID == 0)
                    throw new InvalidOperationException("FilterReplyMessage didn't allowed any messages.");
                return lastAllowedMessageID;
            }
        }

        /// <summary>The ID of last blocked message <see cref="PERMISSION.MessageId"/>.</summary>
        public ulong LastBlockedMessageID
        {
            get
            {
                if (lastBlockedMessageID == 0)
                    throw new InvalidOperationException("FilterReplyMessage didn't blocked any messages.");
                return lastBlockedMessageID;
            }
        }

        public override int FilterReplyMessage(IntPtr hPort, ref PERMISSION lpReplyBuffer, int dwReplyBufferSize)
        {
            if(lpReplyBuffer.MessageId == 0)
                throw new InvalidOperationException("lpReplyBuffer.MessageId == 0");

            if(lpReplyBuffer.Allow == 0)
                lastBlockedMessageID = lpReplyBuffer.MessageId;
            else if (lpReplyBuffer.Allow == 1)
                lastAllowedMessageID = lpReplyBuffer.MessageId;
            else
                throw new ArgumentOutOfRangeException("lpReplyBuffer", "lpReplyBuffer.Allow can be 0 or zero");
            
            return FilterReplyMessageReturnCode;
        }

        public override int FilterSendMessage(IntPtr hPort, ref COMMAND lpInBuffer, uint dwInBufferSize, IntPtr lpOutBuffer, uint dwOutBufferSize, out uint lpBytesReturned)
        {
            if (lpInBuffer.CommandType == COMMAND_TYPE.ADD)
            {
                // Original FilterSendMessage returns this HRESULT when adding equal objects.
                if (Paths.ContainsKey(AdvEnvironment.ExpandEnvironmentVariables(lpInBuffer.Path).ToUpper()))
                {
                    lpBytesReturned = 0;
                    unchecked
                    {
                        return (int)0xC000022B; //NT_STATUS_DUPLICATE_OBJECTID
                    }
                }
                Paths.Add(AdvEnvironment.ExpandEnvironmentVariables(lpInBuffer.Path).ToUpper(), lpInBuffer.ID);
            }
            else
            {
                var ToDelete = new List<string>();

                foreach (var key in Paths.Keys)
                    if (lpInBuffer.ID == Paths[key])
                        ToDelete.Add(key);

                unchecked
                {
                    if (ToDelete.Count == 0)
                        throw new COMException("(Exception from HRESULT: 0x80070490)", (int)0x80070490);
                }

                foreach (var key in ToDelete)
                    Paths.Remove(key);
            }

            lpBytesReturned = 0;

            return FilterSendMessageReturnCode;
        }

        public override bool CloseHandle(IntPtr hObject)
        {
            if (registeredHandles.Contains(hObject) == false)
                return false;
            registeredHandles.Remove(hObject);

            return CloseHandleReturn;
        }

        #region SetGetMessageReturn

        private int getMessageReturn;
        private ACCESS_REQUEST getMessageRequest;
        public void SetGetMessageReturn(int returnCode, ACCESS_REQUEST request)
        {
            getMessageRequest = request;
            getMessageReturn = returnCode;
        }

        #endregion

        public bool IsHandleCorrect(IntPtr handle)
        {
            return registeredHandles.Contains(handle);
        }

        public void CloseAllHandles()
        {
            registeredHandles.Clear();
        }
    }
}