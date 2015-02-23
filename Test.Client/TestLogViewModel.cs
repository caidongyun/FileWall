using System;
using System.Diagnostics;
using VitaliiPianykh.FileWall.Client;
using VitaliiPianykh.FileWall.Shared;
using AdvTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Test.Client
{
    [TestClass]
    public class TestLogViewModel
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


        #region Constructor

        [TestMethod]
        public void Constructor()
        {
            var logViewModel = new LogViewModel(eventLog);
            Assert.AreEqual(eventLog, logViewModel.EventLog);
            Assert.IsNotNull(logViewModel.Data);
        }

        [TestMethod]
        public void Constructor_NullEventLog()
        {
            AdvAssert.ThrowsArgumentNull(() => new LogViewModel(null), "eventLog");
        }

        #endregion

        [TestMethod]
        public void Refresh()
        {
            var logViewModel = new LogViewModel(eventLog);
            // This entry will not be displayed, until user not clicked "Refresh".
            var bytes =
                LogEntryData.Serialize(new LogEntryData(DateTime.Now.ToShortTimeString(), false, AccessType.FILESYSTEM,
                                                        "hello", "hello2"));
            EventLog.WriteEntry("APTester", "Hello world!", EventLogEntryType.Information, 0, 0, bytes);

            // Emulate click on "Refresh" button.
            logViewModel.Refresh();

            // Assert
            // Check the equality of logView.Data and eventLog.Entries.
            for (var i = 0; i < eventLog.Entries.Count; i++)
                Assert.IsTrue(PropertyComparer.AreEqual(LogEntryData.Deserialize(eventLog.Entries[i].Data),
                    logViewModel.Data[i]));
        }

        [TestMethod]
        public void Clear()
        {
            var logViewModel = new LogViewModel(eventLog);

            logViewModel.Clear();

            Assert.AreEqual(0, logViewModel.Data.Count);
            Assert.AreEqual(0, eventLog.Entries.Count);
        }

        [TestMethod]
        public void Export()
        {
            var csvDelimiter = AdvEnvironment.CSVDelimiter;
            var logViewModel = new LogViewModel(eventLog);
            var expectedString = string.Format("Date{0} Action{0} AccessType{0} Path{0} ProcessPath\r\n", csvDelimiter);
            foreach (var logEntryData in logViewModel.Data)
            {
                expectedString += string.Format("{0}{5} {1}{5} {2}{5} {3}{5} {4}\r\n",
                                                logEntryData.Date,
                                                logEntryData.IsAllowed ? "Allow" : "Block",
                                                logEntryData.AccessType,
                                                logEntryData.Path,
                                                logEntryData.ProcessPath,
                                                csvDelimiter);
            }

            var exportedString = logViewModel.Export();

            Assert.AreEqual(expectedString, exportedString);
        }

    }
}
