using System;
using System.ServiceProcess;
using System.Threading;
using VitaliiPianykh.FileWall.Shared;


namespace VitaliiPianykh.FileWall.Testing
{
    public class AdvSCStub : AdvSC
    {
        private ServiceControllerStatus status;
        private Thread statusSetter;

        public AdvSCStub()
        {
            status = ServiceControllerStatus.Stopped;
        }

        public override void Start()
        {
            if(statusSetter != null)
                throw new ApplicationException();
            if (status == ServiceControllerStatus.Running)
                throw new InvalidOperationException("The service cannot be stopped.");

            statusSetter = new Thread(SetStatusWithDelay);
            statusSetter.Start(ServiceControllerStatus.Running);
        }

        public override void Stop()
        {
            if (statusSetter != null)
                throw new ApplicationException();
            if (status == ServiceControllerStatus.Stopped)
                throw new InvalidOperationException("The service cannot be stopped.");

            statusSetter = new Thread(SetStatusWithDelay);
            statusSetter.Start(ServiceControllerStatus.Stopped);
        }

        public override void WaitForStatus(ServiceControllerStatus desiredStatus)
        {
            if (statusSetter == null && Status == desiredStatus)
                return;

            statusSetter.Join();
            if (status != desiredStatus)
                throw new ApplicationException();
        }

        public override ServiceControllerStatus Status
        {
            get { return status; }
        }

        public void SetStatus(ServiceControllerStatus status)
        {
            this.status = status;
//            if(status == ServiceControllerStatus.Stopped)
//                ((FltLibStub)FltLib.Instance).CloseAllHandles();
        }

        private void SetStatusWithDelay(object newStatus)
        {
            Thread.Sleep(100);
            lock (this)
            {
                status = (ServiceControllerStatus)newStatus;
                statusSetter = null;
            }
        }

    }
}