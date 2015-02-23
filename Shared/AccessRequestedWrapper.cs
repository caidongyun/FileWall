using System;


namespace VitaliiPianykh.FileWall.Shared
{
    [Serializable]
    public class AccessRequestedWrapper : MarshalByRefObject
    {
        public event AccessRequestedEventHandler AccessRequested;

        public AccessRequestedWrapper(ServiceInterface serviceInterface)
        {
            serviceInterface.AccessRequested += OnAccessRequested;
        }


        public override object InitializeLifetimeService()
        {
            return null; // This object has to live "forever"
        }


        public void OnAccessRequested(object sender, CoreAccessRequestedEventArgs e)
        {
            // forward the message to the client
            if (AccessRequested != null)
                AccessRequested(this, e);
        }
    }
}