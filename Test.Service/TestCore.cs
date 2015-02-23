using System;
using System.Diagnostics;
using System.Reflection;
using System.ServiceProcess;
using VitaliiPianykh.FileWall.Service;
using VitaliiPianykh.FileWall.Service.Native;
using VitaliiPianykh.FileWall.Shared;
using VitaliiPianykh.FileWall.Testing;
using AdvTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;


namespace Test.Service
{
    [TestClass]
    public class TestCore
    {
        private AdvSCStub driverSC;
        private FltLibStub fltLib;
        private Driver driver;
        private Ruleset ruleset;
        private ServiceInterface serviceInterface;
        private Core core;
        
        [TestInitialize]
        public void TestInitialize()
        {
            ruleset = new Ruleset();
            serviceInterface = new ServiceInterface(ruleset);

            // Creating instance of driver class.
            driverSC = new AdvSCStub();
            fltLib = new FltLibStub();
            var ci = typeof(Driver).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                    null,
                                                    new[] { typeof(AdvSC), typeof(FltLib) },
                                                    null);
            Assert.IsNotNull(ci);
            driver = (Driver)ci.Invoke(new object[] { driverSC, fltLib });

            // Creating the core instance
            core = new Core(driver);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            core = null;
        }

        #region Start

        [TestMethod]
        public void Start_RulesetIsNull()
        {
            AdvAssert.ThrowsArgumentNull(() => core.Start(null, serviceInterface, null), "ruleset");
        }

        [TestMethod]
        public void Start_ServiceInterfaceIsNull()
        {
            AdvAssert.ThrowsArgumentNull(() => core.Start(ruleset, null, null), "serviceInterface");
        }

        [TestMethod]
        public void Start_MustStartDriver()
        {
            core.Start(ruleset, serviceInterface, null);

            Assert.AreEqual(ServiceControllerStatus.Running, driverSC.Status);
        }

        [TestMethod]
        public void Start_MustAddPathsToDriver_IncludingRelative()
        {
            // Arange
            var AbsolutePath    = "C:\\absolute_filesys_path.txt".ToUpper();
            var AbsolutePathID  = (uint) ruleset.Paths.AddPathsRow(AbsolutePath).ID;
            var RelativePath    = "%windir%\\relative_filesys_path.txt";
            var RelativePathID  = (uint) ruleset.Paths.AddPathsRow(RelativePath).ID;

            // Act
            core.Start(ruleset, serviceInterface, null);

            // Assert
            var ExpandedPath = AdvEnvironment.ExpandEnvironmentVariables(RelativePath).ToUpper();
            Assert.AreEqual(AbsolutePathID, fltLib.Paths[AbsolutePath]);
            Assert.AreEqual(RelativePathID, fltLib.Paths[ExpandedPath]);
        }

        [TestMethod]
        public void Start_AlreadyStartedCore()
        {
            core.Start(ruleset, serviceInterface, null);
            AdvAssert.ThrowsInvalidOperation(() => core.Start(ruleset, serviceInterface, null));
        }

        [TestMethod, Isolated]
        public void Start_MayBeCalledAgainAfterStopCall()
        {
            Isolate.Fake.StaticMethods(typeof(ServiceInterface), Members.ReturnNulls);

            core.Start(ruleset, serviceInterface, null);
            core.Stop();
            core.Start(ruleset, serviceInterface, null);
        }

        [TestMethod]
        public void Start_WillNotFallIfRulesetContainsDuplicatePaths()
        {
            ruleset.Paths.AddPathsRow("%windir%\\test.txt");
            ruleset.Paths.AddPathsRow("c:\\windows\\test.txt");

            // This call must not throw exceptions.
            core.Start(ruleset, serviceInterface, null);
        }

        #endregion


        #region Stop

        [TestMethod, Isolated]
        public void Stop_MustStopDriverAndCallUnregisterChannels()
        {
            // Arrange
            Isolate.Fake.StaticMethods(typeof(ServiceInterface), Members.ReturnNulls);
            core.Start(ruleset, serviceInterface, null);

            // Act
            core.Stop();

            // Assert
            Assert.AreEqual(ServiceControllerStatus.Stopped, driverSC.Status);
            Assert.AreEqual(IntPtr.Zero, driver.PortHandle);
        }

        #endregion


        #region Ruleset

        [TestMethod]
        public void Ruleset_Default()
        {
            Assert.AreEqual(null, core.Ruleset); 
        }

        [TestMethod]
        public void Ruleset_AfterStart()
        {
            core.Start(ruleset, serviceInterface, null);

            Assert.AreSame(ruleset, core.Ruleset);
        }

        [TestMethod, Isolated]
        public void Ruleset_AfterStop()
        {
            Isolate.Fake.StaticMethods(typeof(ServiceInterface), Members.ReturnNulls);
            core.Start(ruleset, serviceInterface, null);
            core.Stop();

            Assert.IsNull(core.Ruleset);
        }


        #endregion


        #region Works with ruleset

        [TestMethod]
        public void AddAbsolutePath()
        {
            // Arrange
            core.Start(ruleset, serviceInterface, null);

            // Act
            var ExpectedPath = "c:\\mytestpath.txt".ToUpper(); // Note that driver uppercase all paths.
            var ExpectedPathID = (uint)core.Ruleset.Paths.AddPathsRow(ExpectedPath).ID;

            // Assert
            Assert.AreEqual(ExpectedPathID, fltLib.Paths[ExpectedPath]);
        }

        [TestMethod]
        public void AddRelativePath()
        {
            // Arrange
            core.Start(ruleset, serviceInterface, null);

            // Act
            var ExpectedPath = "%windir%\\mytestpath.txt";
            var ExpectedPathID = (uint)core.Ruleset.Paths.AddPathsRow(ExpectedPath).ID;

            // Assert
            ExpectedPath = AdvEnvironment.ExpandEnvironmentVariables(ExpectedPath).ToUpper();
            Assert.AreEqual(ExpectedPathID, fltLib.Paths[ExpectedPath]);
        }

        [TestMethod]
        public void AddEqualPathWillNotAddSamePath()
        {
            // Arrange
            var expectedPath = AdvEnvironment.ExpandEnvironmentVariables("%WINDIR%\\TEST.txt").ToUpper();
            core.Start(ruleset, serviceInterface, null);

            core.Ruleset.Paths.AddPathsRow("%WINDIR%\\TEST.txt");
            core.Ruleset.Paths.AddPathsRow("%SystemRoot%\\TEST.txt");

            Assert.AreEqual(1, fltLib.Paths.Count);
            Assert.IsTrue(fltLib.Paths.ContainsKey(expectedPath));
        }

        [TestMethod]
        public void EditAbsolutePath()
        {
            // Arrange
            core.Start(ruleset, serviceInterface, null);
            var ExpectedPath = "c:\\mytestpath.txt";
            var ExpectedPathID = core.Ruleset.Paths.AddPathsRow(ExpectedPath).ID;

            // Act
            var NewExpectedPath = "c:\\newpath.txt".ToUpper();
            core.Ruleset.Paths[ExpectedPathID].Path = NewExpectedPath;

            // Assert
            Assert.AreEqual((uint)ExpectedPathID, fltLib.Paths[NewExpectedPath]);
        }

        [TestMethod]
        public void EditRelativePath()
        {
            // Arrange
            core.Start(ruleset, serviceInterface, null);
            var ExpectedPath = "%tmp%\\mytestpath.txt";
            var ExpectedPathID = core.Ruleset.Paths.AddPathsRow(ExpectedPath).ID;

            // Act
            var NewExpectedPath = "%windir%\\newpath.txt";
            core.Ruleset.Paths[ExpectedPathID].Path = NewExpectedPath;

            // Assert
            NewExpectedPath = AdvEnvironment.ExpandEnvironmentVariables(NewExpectedPath).ToUpper();
            Assert.AreEqual((uint)ExpectedPathID, fltLib.Paths[NewExpectedPath]);
        }

        [TestMethod]
        public void DeleteAbsolutePath()
        {
            // Arrange
            core.Start(ruleset, serviceInterface, null);
            var ExpactedPathID = ruleset.Paths.AddPathsRow("c:\testfile.txt").ID;

            // Act
            core.Ruleset.Paths[ExpactedPathID].Delete();

            // Assert
            Assert.AreEqual(0, fltLib.Paths.Count);
        }

        #endregion


        #region WaitRequest

        [TestMethod]
        public void WaitRequest_Stopped()
        {
            AdvAssert.ThrowsInvalidOperation(() => core.WaitRequest());
        }

        //[TestMethod]
        //public void WaitRequest_CallGetRequest()
        //{
        //    // Arrange
        //    var ProtectedPath = "c:\\test.txt";
        //    AddRule(ruleset, RuleAction.Allow, ProtectedPath);
        //    core.Start(ruleset, serviceInterface);

        //    // Act
        //    core.WaitRequest();

        //    // Assert
        //    Isolate.Verify.WasCalledWithAnyArguments(() => Driver.Instance.GetRequest());
        //}

        [TestMethod]
        public void WaitRequest_AllowRule()
        {
            // Arrange
            AddRule(ruleset, RuleAction.Allow, "c:\\test.txt");
            core.Start(ruleset, serviceInterface, null);
            var ExpectedRequest = new ACCESS_REQUEST { Path = "c:\\test.txt", MessageId = 333 };
            fltLib.SetGetMessageReturn(0, ExpectedRequest);

            // Act
            core.WaitRequest();

            // Assert
            Assert.AreEqual((ulong)333, fltLib.LastAllowedMessageID);
        }

        [TestMethod]
        public void WaitRequest_BlockRule()
        {
            // Arrange
            var ExpectedPath = "c:\\test.txt";
            AddRule(ruleset, RuleAction.Block, ExpectedPath);
            var ExpectedRequest = new ACCESS_REQUEST {Path = ExpectedPath, MessageId = 123};
            fltLib.SetGetMessageReturn(0, ExpectedRequest);
            core.Start(ruleset, serviceInterface, null);

            // Act
            core.WaitRequest();

            // Assert
            Assert.AreEqual((ulong)123, fltLib.LastBlockedMessageID);
        }

        [TestMethod]
        public void WaitRequest_NoRule()
        {
            // Arrange
            var ExpectedRequest = new ACCESS_REQUEST {Path = "c:\\test.txt", MessageId = 123};
            fltLib.SetGetMessageReturn(0, ExpectedRequest);
            core.Start(ruleset, serviceInterface, null);

            // Act
            core.WaitRequest();

            // Assert
            Assert.AreEqual((ulong)123, fltLib.LastAllowedMessageID);
        }

        [TestMethod]
        public void WaitRequest_BUG_WildcardRuleAndProcessAdded()
        {
            // Arrange
            var ExpectedPath = "c:\\test.txt";
            var ExpectedRequest = new ACCESS_REQUEST { Path = ExpectedPath, MessageId = 123 };
            fltLib.SetGetMessageReturn(0, ExpectedRequest);
            AddRule(ruleset, RuleAction.Block, "*");
            ruleset.Processes.AddProcessesRow(ExpectedRequest.ProcessPath);
            core.Start(ruleset, serviceInterface, null);

            // Act
            core.WaitRequest();

            // Assert
            Assert.AreEqual((ulong)123, fltLib.LastBlockedMessageID);
        }

        [TestMethod]
        public void WaitRequest_PathAndProcessExists_RuleDoesnt()
        {
            // Arrange
            var ExpectedRequest = new ACCESS_REQUEST { Path = "c:\\test.txt", ProcessID = 4, MessageId = 123 };
            fltLib.SetGetMessageReturn(0, ExpectedRequest);
            ruleset.Paths.AddPathsRow("c:\\test.txt");
            ruleset.Processes.AddProcessesRow("System");
            core.Start(ruleset, serviceInterface, null);

            // Act
            core.WaitRequest();

            // Assert
            Assert.AreEqual((ulong)123, fltLib.LastAllowedMessageID);
        }

        [TestMethod]
        public void WaitRequest_SpecificPath_BlockRule()
        {
            // Arrange
            var ExpectedRequest = new ACCESS_REQUEST {Path = "c:\\test.txt", ProcessID = 4, MessageId = 123};
            fltLib.SetGetMessageReturn(0, ExpectedRequest);
            AddRule(ruleset, RuleAction.Block, ExpectedRequest.Path, ExpectedRequest.ProcessPath);
            core.Start(ruleset, serviceInterface, null);

            // Act
            core.WaitRequest();

            // Assert
            Assert.AreEqual(123ul, fltLib.LastBlockedMessageID);
        }

        [TestMethod]
        public void WaitRequest_SpecificRuleOverridesWildcard()
        {
            // Arrange
            var ExpectedRequest = new ACCESS_REQUEST { Path = "c:\\test.txt", ProcessID = 4, MessageId = 123 };
            fltLib.SetGetMessageReturn(0, ExpectedRequest);
            AddRule(ruleset, RuleAction.Allow, "c:\\test.txt", "*"); // Wildcard rule.
            AddRule(ruleset, RuleAction.Block, ExpectedRequest.Path, ExpectedRequest.ProcessPath); // Specific rule.
            core.Start(ruleset, serviceInterface, null);

            // Act
            core.WaitRequest();

            // Assert
            Assert.AreEqual(123ul, fltLib.LastBlockedMessageID);
        }

        [TestMethod]
        public void WaitRequest_WildcardRuleWorks()
        {
            // Arrange
            var ExpectedRequest = new ACCESS_REQUEST { Path = "c:\\test.txt", ProcessID = 4, MessageId = 123 };
            fltLib.SetGetMessageReturn(0, ExpectedRequest);
            AddRule(ruleset, RuleAction.Block, "c:\\test.txt", "*"); // Wildcard rule.
            core.Start(ruleset, serviceInterface, null);

            // Act
            core.WaitRequest();

            // Assert
            Assert.AreEqual(123ul, fltLib.LastBlockedMessageID);
        }

        // TODO: Pease fix me
//        [TestMethod]
//        public void WaitRequest_CallsAccessRequested()
//        {
//            // Arrange
//            var ExpectedRequest = new ACCESS_REQUEST { Path = "c:\\test.txt", ProcessID = 4, MessageId = 123 };
//            fltLib.SetGetMessageReturn(0, ExpectedRequest);
//            var ExpectedEA = new CoreAccessRequestedEventArgs("", ExpectedRequest.Path, ExpectedRequest.ProcessPath);
//            AddRule(ruleset, RuleAction.Ask, ExpectedRequest.Path, ExpectedRequest.ProcessPath);
//            core.Start(ruleset, serviceInterface);
//
//            // Act
//            var actualEA = AdvAssert.Raises<CoreAccessRequestedEventArgs>(() => core.WaitRequest(), serviceInterface, "AccessRequested");
//
//            // Assert
//            Assert.AreEqual(ExpectedEA, actualEA);
//        }

        [TestMethod]
        public void WaitRequest_CreatesSpecificRuleToOverridWildcardRule()
        {
            // Arrange
            var Request = ArrangeForCreateRuleTests("*");

            // Act
            core.WaitRequest();

            // Assert
            var ExpectedRule = ruleset.GetRulesRow(Request.RuleID, Request.ProcessPath);
            Assert.IsNotNull(ExpectedRule);
            Assert.AreEqual(RuleAction.Allow, ExpectedRule.Action);
            Assert.IsTrue(ExpectedRule.Log);
        }

        [TestMethod]
        public void WaitRequest_CreatesSpecificRuleToOverridWildcardRule_ProcessExists()
        {
            // Arrange
            var Request = ArrangeForCreateRuleTests("*");
            ruleset.Processes.AddProcessesRow(Request.ProcessPath);

            // Act
            core.WaitRequest();

            // Assert
            var ExpectedRule = ruleset.GetRulesRow(Request.RuleID, Request.ProcessPath);
            Assert.IsNotNull(ExpectedRule);
            Assert.AreEqual(RuleAction.Allow, ExpectedRule.Action);
            Assert.IsTrue(ExpectedRule.Log);
        }

        [TestMethod]
        public void WaitRequest_EditsSpecificRule()
        {
            // Arrange
            var Request = ArrangeForCreateRuleTests("System");

            // Act
            core.WaitRequest();

            // Assert
            var ExpectedRule = ruleset.GetRulesRow(Request.RuleID, Request.ProcessPath);
            Assert.IsNotNull(ExpectedRule);
            Assert.AreEqual(RuleAction.Allow, ExpectedRule.Action);
        }

        private ACCESS_REQUEST ArrangeForCreateRuleTests(string processPath)
        {
            // User wants to create rule.
            serviceInterface = new ServiceInterface(ruleset);
            
            serviceInterface.AccessRequested += ((sender, e) =>
                                                     {
                                                         e.CreateRule = true;
                                                         e.Allow = true;
                                                     });
            // Request which will be thrown.
            var Request = new ACCESS_REQUEST { Path = "c:\\test.txt", ProcessID = 4, MessageId = 123 };
            fltLib.SetGetMessageReturn(0, Request);
            // Add "wildcard" rule for which WaitRequest will create "specific" rule.
            AddRule(ruleset, RuleAction.Ask, Request.Path, processPath);
            core.Start(ruleset, serviceInterface, null);
            return Request;
        }

        #region Test Counters

        [TestMethod]
        public void WaitRequest_IncrementsFSBlocks()
        {
            // Arrange
            ArrangeForTestCounters(new ACCESS_REQUEST { AccessType = ACCESS_TYPE.FILESYSTEM, MessageId = 123 }, RuleAction.Block);
            // Act
            core.WaitRequest();
            // Assert
            Assert.AreEqual(1u, serviceInterface.FilesysBlocks);
            // Other counters must not be changed.
            Assert.AreEqual(0u, serviceInterface.FilesysPermits);
            Assert.AreEqual(0u, serviceInterface.RegistryBlocks);
            Assert.AreEqual(0u, serviceInterface.RegistryPermits);
        }

        [TestMethod]
        public void WaitRequest_IncrementsFSPermits()
        {
            // Arrange
            ArrangeForTestCounters(new ACCESS_REQUEST { AccessType = ACCESS_TYPE.FILESYSTEM, MessageId = 123 }, RuleAction.Allow);
            // Act
            core.WaitRequest();
            // Assert
            Assert.AreEqual(1u, serviceInterface.FilesysPermits);
            // Other counters must not be changed.
            Assert.AreEqual(0u, serviceInterface.FilesysBlocks);
            Assert.AreEqual(0u, serviceInterface.RegistryBlocks);
            Assert.AreEqual(0u, serviceInterface.RegistryPermits);

        }

        [TestMethod]
        public void WaitRequest_IncrementsRegBlocks()
        {
            // Arrange
            ArrangeForTestCounters(new ACCESS_REQUEST { AccessType = ACCESS_TYPE.REGISTRY, MessageId = 123}, RuleAction.Block);
            // Act
            core.WaitRequest();
            // Assert
            Assert.AreEqual(1u, serviceInterface.RegistryBlocks);
            // Other counters must not be changed.
            Assert.AreEqual(0u, serviceInterface.RegistryPermits);
            Assert.AreEqual(0u, serviceInterface.FilesysBlocks);
            Assert.AreEqual(0u, serviceInterface.FilesysPermits);
        }

        [TestMethod]
        public void WaitRequest_IncrementsRegPermits()
        {
            // Arrange
            ArrangeForTestCounters(new ACCESS_REQUEST { AccessType = ACCESS_TYPE.REGISTRY, MessageId = 123 }, RuleAction.Allow);
            // Act
            core.WaitRequest();
            // Assert
            Assert.AreEqual(1u, serviceInterface.RegistryPermits);
            // Other counters must not be changed.
            Assert.AreEqual(0u, serviceInterface.RegistryBlocks);
            Assert.AreEqual(0u, serviceInterface.FilesysBlocks);
            Assert.AreEqual(0u, serviceInterface.FilesysPermits);
        }

        private void ArrangeForTestCounters(ACCESS_REQUEST Request, RuleAction ruleAction)
        {
            core.Start(ruleset, serviceInterface, null);
            fltLib.SetGetMessageReturn(0, Request);
            AddRule(ruleset, ruleAction, "some path", "*");
        }

        #endregion


        /// <summary>Add rule to ruleset. Category="Test", Item="Test", Rule="Test".</summary>
        //private static Ruleset.RulesRow AddRule(Ruleset ruleset, RuleAction ruleAction, string path)
        //{
        //    return AddRule(ruleset, ruleAction, path);
        //}

        private static Ruleset.RulesRow AddRule(Ruleset ruleset, RuleAction ruleAction, string path)
        {
            return AddRule(ruleset, ruleAction, path, "*");
        }

        // TODO: Move it to ruleset.
        private static Ruleset.RulesRow AddRule(Ruleset ruleset, RuleAction ruleAction, string path, string processPath)
        {
            return AddRule(ruleset, ruleAction, false, path, processPath);
        }

        private static Ruleset.RulesRow AddRule(Ruleset ruleset, RuleAction ruleAction, bool log, string path, string processPath)
        {
            var Pr = ruleset.Processes.FindByPath(processPath);
            if (Pr == null)
                Pr = ruleset.Processes.AddProcessesRow(processPath);
            var Pth = ruleset.Paths.FindByPath(path);
            if (Pth == null)
                Pth = ruleset.Paths.AddPathsRow(path);

            var C = ruleset.Categories.FindByName("Test");
            if (C == null)
                C = ruleset.Categories.AddCategoriesRow("Test", null, string.Empty);
            var I = ruleset.Items.GetItemsRow("Test", "Test");
            if (I == null)
                I = ruleset.Items.AddItemsRow("Test", null, C);

            return ruleset.Rules.AddRulesRow("Test", ruleAction, log, Pth, Pr, I);
        }

        #endregion


        #region Test Logging functionality

        [TestMethod, Isolated]
        public void Test_Log()
        {
            // Arrange
            var eventLogStub = Isolate.Fake.Instance<EventLog>(Members.MustSpecifyReturnValues);
            fltLib.SetGetMessageReturn(0, new ACCESS_REQUEST { Path = "c:\\test.txt", MessageId = 333 });
            AddRule(ruleset, RuleAction.Allow, true, "c:\\test.txt", "*");
            var ExpectedBytes = LogEntryData.Serialize(new LogEntryData
                                                                 (
                                                                     DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString(),
                                                                     true,
                                                                     AccessType.FILESYSTEM,
                                                                     "c:\\test.txt",
                                                                     "Idle"
                                                                 ));
            core.Start(ruleset, serviceInterface, eventLogStub);

            // Act
            core.WaitRequest();

            // Assert
            Isolate.Verify.WasCalledWithExactArguments(() => eventLogStub.WriteEntry("Please use FileWall to view this log.", EventLogEntryType.Information, 0, 0, ExpectedBytes));
        }

        [TestMethod, Isolated(DesignMode.Pragmatic)]
        public void Test_NoLog()
        {
            // Arrange
            var eventLogStub = Isolate.Fake.Instance<EventLog>(Members.MustSpecifyReturnValues);
            fltLib.SetGetMessageReturn(0, new ACCESS_REQUEST { Path = "c:\\test.txt", MessageId = 333 });
            AddRule(ruleset, RuleAction.Allow, false, "c:\\test.txt", "*");
            core.Start(ruleset, serviceInterface, eventLogStub);

            // Act
            core.WaitRequest();

            // Assert
            Isolate.Verify.WasNotCalled(() => eventLogStub.WriteEntry("Please use FileWall to view this log.", EventLogEntryType.Information, 0, 0, new byte[] { }));
        }

        [TestMethod]
        public void Test_LogNoRule()
        {
            // Arrange
            var eventLogStub = Isolate.Fake.Instance<EventLog>(Members.MustSpecifyReturnValues);
            fltLib.SetGetMessageReturn(0, new ACCESS_REQUEST {Path = "c:\\test.txt", MessageId = 123});
            var ExpectedBytes = LogEntryData.Serialize(new LogEntryData
                                                                 (
                                                                     DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString(),
                                                                     true,
                                                                     AccessType.FILESYSTEM,
                                                                     "c:\\test.txt",
                                                                     "Idle"
                                                                 ));
            core.Start(ruleset, serviceInterface, eventLogStub);

            // Act
            core.WaitRequest();

            // Assert
            Isolate.Verify.WasCalledWithExactArguments(() => eventLogStub.WriteEntry("Please use FileWall to view this log.", EventLogEntryType.Information, 0, 0, ExpectedBytes));
        }

        #endregion
    }
}