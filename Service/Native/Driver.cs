using System;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using VitaliiPianykh.FileWall.Shared;


namespace VitaliiPianykh.FileWall.Service.Native
{
    public class Driver
    {
        public const Int32 MAX_PATH = 1024;

        #region Singletone implementation

        private static Driver instance;
        // TODO: Instance must be internal.
        public static Driver Instance
        {
            get
            {
                if(instance == null)
                    instance = new Driver(new AdvSC("FileWall"), FltLib.Instance);
                return instance;
            }
        }

        #endregion

        private readonly AdvSC driverSC;
        private readonly FltLib fltLib;
        private IntPtr portHandle = IntPtr.Zero;
        

        protected Driver(AdvSC _driverSC, FltLib _fltLib)
        {
            driverSC = _driverSC;
            fltLib = _fltLib;
        }

        #region Public Properties

        public virtual IntPtr PortHandle
        {
            get { return portHandle; }
        }

        #endregion


        #region Public Methods

        /// <summary>Starts FileWall driver service and connects to minifilter port.</summary>
        public virtual void Start()
        {
            if (driverSC.Status != ServiceControllerStatus.Running && driverSC.Status != ServiceControllerStatus.Stopped)
                throw new NotSupportedException("FileWall driver have unsupported state - " + driverSC.Status + ". Try again later.");
            if (IsRunning)
                throw new InvalidOperationException("Driver already started. Stop it first.");

            if (driverSC.Status == ServiceControllerStatus.Stopped)
            {
                driverSC.Start();
                driverSC.WaitForStatus(ServiceControllerStatus.Running);
            }

            var hr = fltLib.FilterConnectCommunicationPort("\\FileWallPort",
                                                           0,
                                                           IntPtr.Zero,
                                                           0,
                                                           IntPtr.Zero,
                                                           out portHandle);
            Marshal.ThrowExceptionForHR(hr);
            //if (PortHandle.ToInt32() == -1)
            //    throw new ApplicationException("Invalid handle.");
        }
        
        /// <summary>Stops FileWall driver service and disconnects from the port.</summary>
        public virtual void Stop()
        {
            // Close port if it opened.
            if (PortHandle != IntPtr.Zero)
            {
                if (fltLib.CloseHandle(PortHandle) == false)
                    throw new InvalidOperationException("CloseHandle(" + PortHandle + ") failed.");
                portHandle = IntPtr.Zero;
            }

            // Stop driver if it running.
            if (driverSC.Status == ServiceControllerStatus.Running)
            {
                driverSC.Stop();
                driverSC.WaitForStatus(ServiceControllerStatus.Stopped);
            }
        }

        /// <summary>Send command to FileWall port.</summary>
        public virtual void SendCommand(COMMAND_TYPE CommandType, String path, int ruleID)
        {
            if (!IsRunning)
                throw new InvalidOperationException("FileWall is stopped.");
            if (CommandType == COMMAND_TYPE.ADD && string.IsNullOrEmpty(path))
                throw new ArgumentException("Path cannot be empty or null when adding.", "path");
            if (CommandType == COMMAND_TYPE.DEL && path != null)
                throw new ArgumentException("Path must equal null when deleting.", "path");

            try
            {
                uint lpBytesReturned;
                var Command = new COMMAND { CommandType = CommandType, Path = path, ID = (uint)ruleID };

                var hr = fltLib.FilterSendMessage(portHandle,
                                                  ref Command,
                                                  (UInt32) Marshal.SizeOf(Command),
                                                  IntPtr.Zero,
                                                  0,
                                                  out lpBytesReturned);
                Marshal.ThrowExceptionForHR(hr);
            }
            catch(COMException ex)
            {
                switch ((uint)ex.ErrorCode)
                {
                    case 0xc000022b:// NT_STATUS_DUPLICATE_OBJECTID or HRESULT=0xC000022B
                    case 0x80071392:// The same for Vista.
                        throw new PathAlreadyAddedException(path, ex);
                    case 0x80070490://Element not found. (Exception from HRESULT: 0x80070490)
                        throw new IdNotFoundException(ruleID, ex);
                }
                throw;
            }
        }

        /// <summary>Gets request from the queue.</summary>
        public virtual ACCESS_REQUEST GetRequest()
        {
            if (!IsRunning)
                throw new InvalidOperationException("FileWall is stopped.");

            var AccessRequest = new ACCESS_REQUEST();
            var hr = fltLib.FilterGetMessage(portHandle, ref AccessRequest, Marshal.SizeOf(AccessRequest), IntPtr.Zero);
            Marshal.ThrowExceptionForHR(hr);

            return AccessRequest;
        }

        /// <summary>Replies to the request.</summary>
        public virtual void ReplyRequest(ACCESS_REQUEST AccessRequest, Boolean Allow)
        {
            if(!IsRunning)
                throw new InvalidOperationException("FileWall is stopped.");

            var Permission = new PERMISSION
                                        {
                                                Status = 0, // STATUS_SUCCESS
                                                MessageId = AccessRequest.MessageId,
                                                Allow = (Allow ? 1 : 0)
                                        };

            var hr = fltLib.FilterReplyMessage(portHandle, ref Permission, Marshal.SizeOf(Permission));

            Marshal.ThrowExceptionForHR(hr);
        }

        #endregion


        // TODO: Unit tests for IsRunning.
        private bool IsRunning
        {
            get
            {
                return driverSC.Status == ServiceControllerStatus.Running && PortHandle != IntPtr.Zero;
            }
        }


//        /// <summary>Connects to FileWall port "FileWallPort".</summary>
//        private void Connect()
//        {
//            if (AdvSC.Instance.Status != ServiceControllerStatus.Running)
//                throw new InvalidOperationException("FileWall driver is not started.");
//
//            try
//            {
//                int hr = FltLib.Instance.FilterConnectCommunicationPort("\\FileWallPort",
//                                                             0,
//                                                             IntPtr.Zero,
//                                                             0,
//                                                             IntPtr.Zero,
//                                                             out portHandle);
//                Marshal.ThrowExceptionForHR(hr);
//            }
//            catch (FileNotFoundException ex)
//            {
//                throw new ApplicationException("Problems with connecting to communcation port.", ex);
//            }
//
//            if(portHandle.ToInt32() == -1)
//                throw new ApplicationException("Invalid handle.");
//        }


//        /// <summary>Desconnects from FileWall port.</summary>
//        private void Disconnect()
//        {
//            Debug.Print(MethodBase.GetCurrentMethod().Module.Name + "." + MethodBase.GetCurrentMethod().Name);
//
//
//
//            if (portHandle == IntPtr.Zero)
//                throw new ArgumentNullException("FileWall port is already disconnected.");
//
//            bool IsClosed;
//
//            IsClosed = FltLib.Instance.CloseHandle(portHandle);
//            
//            if (IsClosed != true)
//                throw new ArgumentException("Kernel32.CloseHandle failed." +
//                                            new Win32Exception(Marshal.GetLastWin32Error()).Message);
//
//            portHandle = IntPtr.Zero;
//
//        }
    }
}