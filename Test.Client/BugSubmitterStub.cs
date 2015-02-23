using System;
using VitaliiPianykh.FileWall.Shared;


namespace Test.Client
{
    class BugSubmitterStub : BugSubmitter
    {
        private BugReport _ReportFake = new BugReport("windows xp sp2", DateTime.Now, "v1.0.0.0", "v1.0.0.0", "v1.0.0.0", string.Empty, string.Empty, null);

        public override void Submit(BugReport bugReport)
        {
            if (SentData != null)
                throw new InvalidOperationException("SentData called more than once.");
            SentData = bugReport;
        }

        public override BugReport CollectInfo(Exception ex)
        {
            return _ReportFake;
        }

        public BugReport SentData { get; private set; }

        public BugReport ReportFake
        {
            get {
                return _ReportFake;
            }
        }
    }
}
