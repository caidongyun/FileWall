using System.ComponentModel;
using VitaliiPianykh.FileWall.Client;
using VitaliiPianykh.FileWall.Shared;


namespace Test.Client
{
    internal class LogViewStub : ILogView
    {
        #region Implementation of ILogView

        public BindingList<LogEntryData> Data { get; set; }
        public bool DetailsVisible
        {
            get; set;
        }

        #endregion
    }
}
