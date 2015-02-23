using System.Diagnostics;
using System.Threading;
using VitaliiPianykh.FileWall.Service.Native;
using VitaliiPianykh.FileWall.Shared;


namespace VitaliiPianykh.FileWall.Service
{
    public sealed class AsyncCore: Core
    {
        #region Singleton implementation

        private static AsyncCore _Instance;
        public static AsyncCore Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new AsyncCore();
                return _Instance;
            }
        }

        #endregion


        private Thread _ThreadWaitRequest;

        // Close AsyncCore constructor
        private AsyncCore():base(Driver.Instance){}

        public override void Start(Ruleset ruleset, ServiceInterface serviceInterface, EventLog eventLog)
        {
            base.Start(ruleset, serviceInterface, eventLog);

            // Start processing of access requests in parallel thread.
            _ThreadWaitRequest = new Thread(CycledWaitRequest);
            _ThreadWaitRequest.Start();
        }

        public override void Stop()
        {
            if (_ThreadWaitRequest != null)
            {
                _ThreadWaitRequest.Abort();
                _ThreadWaitRequest = null;
            }

            base.Stop();
        }
        
        private void CycledWaitRequest()
        {
            while (true)
                WaitRequest();
        }
    }
}
