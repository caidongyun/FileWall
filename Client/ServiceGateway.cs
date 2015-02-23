using System;
using System.ServiceProcess;
using VitaliiPianykh.FileWall.Shared;


namespace VitaliiPianykh.FileWall.Client
{
    // NOTE: This class is Model (regarding to MVP pattern).

    public sealed class ServiceGateway
    {
        private readonly AdvSC _SC;
        private readonly ServiceInterfaceManager _ServiceInterfaceManager;

        public event EventHandler Started;
        public event EventHandler Stopped;


        #region Public Methods

        public ServiceGateway(AdvSC sc, ServiceInterfaceManager serviceInterfaceManager)
        {
            if(sc == null)
                throw new ArgumentNullException("sc");
            if(serviceInterfaceManager == null)
                throw new ArgumentNullException("serviceInterfaceManager");

            _SC = sc;
            _ServiceInterfaceManager = serviceInterfaceManager;

            if (IsStarted)
                ServiceInterface = _ServiceInterfaceManager.GetMarshalledInteface();
        }

        public void Start()
        {
            if(IsStarted)
                throw new InvalidOperationException("Service is already started.");

            _SC.Start();
            _SC.WaitForStatus(ServiceControllerStatus.Running);

            // Get fresh service interface.
            ServiceInterface = _ServiceInterfaceManager.GetMarshalledInteface();

            OnStarted();
        }

        public void Stop()
        {
            _SC.Stop();
            _SC.WaitForStatus(ServiceControllerStatus.Stopped);

            // Close all ports and null ServiceInterface property.
            _ServiceInterfaceManager.ClientDisconnect();
            ServiceInterface = null;

            OnStopped();
        }

        #endregion


        #region Public Properties

        public bool IsStarted
        {
            get
            {
                if (_SC.Status == ServiceControllerStatus.Running)
                    return true;

                return false;
            }
        }

        public ServiceInterface ServiceInterface { get; private set; }

        #endregion


        #region Event Invokators

        private void OnStarted()
        {
            if (Started != null)
                Started(this, EventArgs.Empty);
        }

        private void OnStopped()
        {
            if (Stopped != null)
                Stopped(this, EventArgs.Empty);
        }

        #endregion
    }
}
