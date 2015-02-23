using System;
using System.ServiceProcess;


namespace VitaliiPianykh.FileWall.Shared
{
    /// <summary>
    /// Simple service service controller like <see cref="ServiceController"/>.
    /// There is only one feature: virtual members. This is very useful in unit testing.
    /// </summary>
    public class AdvSC
    {
        private ServiceController sc;

        /// <summary>Empty constructor for inheritors.</summary>
        protected AdvSC() {}

        public AdvSC(string serviceName)
        {
            if(serviceName == null)
                throw new ArgumentNullException("serviceName");
            sc = new ServiceController(serviceName);
        }

        public virtual void Start()
        {
            try
            {
                sc.Refresh();

                // NOTE: This is special case
                // NOTE: If service trying to start now Start call will just pass.
                if (sc.Status == ServiceControllerStatus.StartPending)
                    return;
                sc.Start();
            }
            catch (InvalidOperationException ex)
            {
                // This exception needs to be more informative so we wrapping it.
                throw new InvalidOperationException("The service cannot be started. Current state: " + sc.Status,
                                                    ex);
            }
        }

        public virtual void WaitForStatus(ServiceControllerStatus status)
        {
            sc.WaitForStatus(status, new TimeSpan(0, 0, 0, 60));
        }

        public virtual void Stop()
        {
            sc.Refresh();
            sc.Stop();
        }

        public virtual ServiceControllerStatus Status
        {
            get
            {
                sc.Refresh();
                return sc.Status;
            }
        }
    }
}