using System;
using System.Diagnostics;
using VitaliiPianykh.FileWall.Client;
using VitaliiPianykh.FileWall.Shared;
using AdvTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Test.Client
{
    // NOTE: Unfortunally I can't find how to stub EventLog class,
    // NOTE: so I used real event log.
    [TestClass]
    public class TestLogViewPresenter
    {
        private EventLog eventLog;

        #region Test Environment
        
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            // Create event log for testing
            if (EventLog.SourceExists("APTester") == false)
                EventLog.CreateEventSource("APTester", "APTest");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            EventLog.DeleteEventSource("APTester");
            EventLog.Delete("APTest");
        }

        [TestInitialize]
        public void TestInitialize()
        {
            eventLog = new EventLog("APTest");
            eventLog.Clear();
            var bytes = LogEntryData.Serialize(new LogEntryData(DateTime.Now.ToShortTimeString(),
                                                                false, AccessType.FILESYSTEM, "c:\\test.txt", "c:\\malware.exe"));
            EventLog.WriteEntry("APTester", "Hello world!", EventLogEntryType.Information, 0, 0, bytes);
            bytes = LogEntryData.Serialize(new LogEntryData(DateTime.Now.ToShortTimeString(),
                                                            false, AccessType.REGISTRY, "HKLM\\classes\\glavriba", "c:\\trojan.exe"));
            EventLog.WriteEntry("APTester", "Hello world!", EventLogEntryType.Information, 0, 0, bytes);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            eventLog = null;
        }

        #endregion

        [TestMethod]
        public void Constructor()
        {
            var logViewModel = new LogViewModel(eventLog);
            var presenter = new LogViewPresenter(logViewModel);

            Assert.IsNull(presenter.LogView);
            Assert.AreEqual(logViewModel, presenter.LogViewModel);
        }

        [TestMethod]
        public void Constructor_AndDetailsVisible()
        {
            var form = new LogViewStub() {DetailsVisible = true};
            var presenter = new LogViewPresenter(new LogViewModel(eventLog));
            
            presenter.LogView = form;

            Assert.IsFalse(form.DetailsVisible);
        }

        [TestMethod]
        public void Constructor_NullEventLog()
        {
            AdvAssert.ThrowsArgumentNull(() => new LogViewPresenter(null), "logViewModel");
        }

        [TestMethod]
        public void LogView_SetWillSetViewData()
        {
            var logViewModel = new LogViewModel(eventLog);
            var presenter = new LogViewPresenter(logViewModel);
            var logView = new LogViewStub();

            presenter.LogView = logView;
            Assert.AreSame(logViewModel.Data, logView.Data);
        }

        [TestMethod]
        public void LogView_SetNull()
        {
            var presenter = new LogViewPresenter(new LogViewModel(eventLog));
            presenter.LogView = null;
            Assert.IsNull(presenter.LogView);
        }

        [TestMethod]
        public void DetailsVisibile()
        {
            var form = new LogViewStub();
            var presenter = new LogViewPresenter(new LogViewModel(eventLog)) {LogView = form};

            presenter.DetailsVisible = true;
            Assert.IsTrue(form.DetailsVisible);

            presenter.DetailsVisible = false;
            Assert.IsFalse(form.DetailsVisible);
        }

        [TestMethod]
        public void DetailsVisibile_WhenLogViewIsNull()
        {
            var presenter = new LogViewPresenter(new LogViewModel(eventLog));
            AdvAssert.ThrowsInvalidOperation(() => presenter.DetailsVisible.ToString());
            AdvAssert.ThrowsInvalidOperation(() => presenter.DetailsVisible = false);
        }
    }
}
