using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using VitaliiPianykh.FileWall.Service.Native;
using VitaliiPianykh.FileWall.Shared;
using VitaliiPianykh.FileWall.Testing;
using AdvTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace Test.Service
{
    [TestClass]
    public class TestDriver
    {
        private Driver          driver; // System under test.
        private AdvSCStub    driverSC;
        private FltLibStub      fltLib;

        [TestInitialize]
        public void TestInitialize()
        {
            driverSC = new AdvSCStub();
            fltLib = new FltLibStub();
            var ci = typeof (Driver).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                    null,
                                                    new[] { typeof(AdvSC), typeof(FltLib) }, 
                                                    null);
            Assert.IsNotNull(ci);
            driver = (Driver)ci.Invoke(new object[] { driverSC, fltLib });
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver = null;
            driverSC = null;
            fltLib = null;
        }


        #region Start

        [TestMethod]
        public void Start()
        {
            driver.Start();

            Assert.AreEqual(ServiceControllerStatus.Running, driverSC.Status);
        }

        [TestMethod]
        public void Start_AlreadyStarted()
        {
            driver.Start();

            AdvAssert.ThrowsInvalidOperation(driver.Start);
        }

        // NOTE: Such situations appears when somebody starts the driver passing Driver class.
        [TestMethod]
        public void Start_StartedOutside()
        {
            //Start outside
            driverSC.SetStatus(ServiceControllerStatus.Running);

            driver.Start();

            Assert.IsTrue(fltLib.IsHandleCorrect(driver.PortHandle));
        }

        [TestMethod]
        public void Start_IntermediateStatusesArentSupported()
        {
            // Assert
            var UnsupportedStatuses = new[]
                                          {
                                              ServiceControllerStatus.ContinuePending,
                                              ServiceControllerStatus.Paused,
                                              ServiceControllerStatus.PausePending,
                                              ServiceControllerStatus.StartPending,
                                              ServiceControllerStatus.StopPending
                                          };

            // Act - Assert
            foreach (var unsupportedStatus in UnsupportedStatuses)
            {
                driverSC.SetStatus(unsupportedStatus);
                AdvAssert.Throws<NotSupportedException>(() => driver.Start());
            }
        }
        
        [TestMethod]
        public void Start_ConnectFails()
        {
            // Start will throw COMException in case if FilterConnectCommunication fail.

            // FilterConnectCommunicationPort will return this error code.
            fltLib.FilterConnectCommunicationPortReturnCode = -123; 

            // Act - Assert
            AdvAssert.Throws<COMException>(() => driver.Start(), "ErrorCode", -123);
        }

        #endregion


        #region Stop

        [TestMethod]
        public void Stop_StartedDriver()
        {
            driver.Start();

            driver.Stop();

            Assert.AreEqual(ServiceControllerStatus.Stopped, driverSC.Status);
        }

        [TestMethod]
        public void Stop_StoppedDriver()
        {
            driver.Start();
            driver.Stop();

            driver.Stop();
        }

        [TestMethod]
        public void Stop_StoppedOutside()
        {
            driver.Start();

            // Stop Outside
            driverSC.SetStatus(ServiceControllerStatus.Running);

            driver.Stop();
        }

        [TestMethod]
        public void Stop_CloseHandleFails()
        {
            driver.Start();

            fltLib.CloseHandleReturn = false;

            AdvAssert.ThrowsInvalidOperation(() => driver.Stop());
        }

        #endregion


        #region PortHandle

        [TestMethod]
        public void PortHandle_Default()
        {
            Assert.AreEqual(IntPtr.Zero, driver.PortHandle);
        }

        [TestMethod]
        public void PortHandle_ValidHandleAfterStart()
        {
            driver.Start();
            Assert.IsTrue(fltLib.IsHandleCorrect(driver.PortHandle));
        }

        [TestMethod]
        public void PortHandle_ZeroAfterStop()
        {
            driver.Start();
            var PortHandle = driver.PortHandle; // This needed to test that Driver.Stop closed PortHandle.

            driver.Stop();

            Assert.AreEqual(IntPtr.Zero, driver.PortHandle);
            // This checks that Driver.Stop correctly closed PortHandle.
            Assert.IsFalse(fltLib.IsHandleCorrect(PortHandle));
        }

        #endregion


        #region SendCommand

        // ADD

        [TestMethod]
        public void SendCommand_AddEmptyPath()
        {
            driver.Start();

            AdvAssert.ThrowsArgument(() => driver.SendCommand(COMMAND_TYPE.ADD, string.Empty, 0), "path");
        }

        [TestMethod]
        public void SendCommand_AddNullPath()
        {
            driver.Start();

            AdvAssert.ThrowsArgument(() => driver.SendCommand(COMMAND_TYPE.ADD, null, 0), "path");
        }

        [TestMethod]
        public void SendCommand_AddingSamePaths()
        {
            driver.Start();

            driver.SendCommand(COMMAND_TYPE.ADD, "C:\\test.txt", 1);

            AdvAssert.Throws<PathAlreadyAddedException>(() => driver.SendCommand(COMMAND_TYPE.ADD, "C:\\test.txt", 1),
                "Path", "C:\\test.txt");
        }

        [TestMethod]
        public void SendCommand_AddReallyAddPaths()
        {
            driver.Start();
            driver.SendCommand(COMMAND_TYPE.ADD, "C:\\test.txt", 1);

            Assert.AreEqual(1, fltLib.Paths.Count);
            Assert.IsTrue(fltLib.Paths.ContainsKey("C:\\test.txt".ToUpper()));
            Assert.IsTrue(fltLib.Paths.ContainsValue(1));
        }

        // DEL

        [TestMethod]
        public void SendCommand_DelEmptyPath()
        {
            driver.Start();
            AdvAssert.ThrowsArgument(() => driver.SendCommand(COMMAND_TYPE.DEL, string.Empty, 0), "path");
        }

        [TestMethod]
        public void SendCommand_DelValidPath()
        {
            driver.Start();
            AdvAssert.ThrowsArgument(() => driver.SendCommand(COMMAND_TYPE.DEL, "C:\\test.txt", 0), "path");
        }

        [TestMethod]
        public void SendCommand_DelNotAddedID()
        {
            driver.Start();

            AdvAssert.Throws<IdNotFoundException>(() => driver.SendCommand(COMMAND_TYPE.DEL, null, 33), "ID", 33);
        }

        [TestMethod]
        public void SendCommand_DelReallyRemovesPath()
        {
            driver.Start();
            driver.SendCommand(COMMAND_TYPE.ADD, @"C:\hello1.txt", 1);
            driver.SendCommand(COMMAND_TYPE.ADD, @"C:\hello2.txt", 1);
            driver.SendCommand(COMMAND_TYPE.ADD, @"C:\hello3.txt", 2);

            driver.SendCommand(COMMAND_TYPE.DEL, null, 1);

            Assert.AreEqual(1, fltLib.Paths.Count);
            Assert.IsTrue(fltLib.Paths.ContainsKey(@"C:\hello3.txt".ToUpper()));
        }

        // Sending to stopped driver

        [TestMethod]
        public void SendCommand_ToStopped()
        {
            AdvAssert.ThrowsInvalidOperation(() => driver.SendCommand(COMMAND_TYPE.ADD, @"C:\SomeFolder\somefile.txt", 0));
        }

        [TestMethod]
        public void SendCommand_ToStartedOutside()
        {
            // Start outside
            driverSC.SetStatus(ServiceControllerStatus.Running);

            AdvAssert.ThrowsInvalidOperation(() => driver.SendCommand(COMMAND_TYPE.ADD, @"C:\SomeFolder\somefile.txt", 0));
        }

        [TestMethod]
        public void SendCommand_FilterSendMessageFails()
        {
            fltLib.FilterSendMessageReturnCode = -123;
            driver.Start();
            AdvAssert.Throws<COMException>(() => driver.SendCommand(COMMAND_TYPE.ADD, @"C:\somefile.txt", 22), "ErrorCode", -123);
        }

        #endregion


        #region GetRequest

        [TestMethod]
        public void GetRequest_FromStopped()
        {
            AdvAssert.ThrowsInvalidOperation(() => driver.GetRequest());
        }

        [TestMethod]
        public void GetRequest_ReturnsCorrectRequest()
        {
            var expectedRequest = new ACCESS_REQUEST{MessageId = 1, AccessType = ACCESS_TYPE.REGISTRY, Operation = 133, Path = "SomePath", ProcessID = 10, ReplyLength = 11, RuleID = 122};
            fltLib.SetGetMessageReturn(0, expectedRequest);
            driver.Start();

            var actualRequest = driver.GetRequest();

            Assert.AreEqual(expectedRequest, actualRequest);
        }

        [TestMethod]
        public void GetRequest_ForStoppedOutside()
        {
            driver.Start();
            driverSC.SetStatus(ServiceControllerStatus.Stopped);

            AdvAssert.ThrowsInvalidOperation(() => driver.GetRequest());
        }

        [TestMethod]
        public void GetRequest_FilterGetMessageFails()
        {
            driver.Start();
            fltLib.SetGetMessageReturn(-125, new ACCESS_REQUEST());

            AdvAssert.Throws<COMException>(() => driver.GetRequest(), "ErrorCode", -125);
        }

        #endregion


        #region ReplyRequest

        // TODO: SUCCESS call of ReplyRequest

        [TestMethod]
        public void ReplyRequest_ToStopped()
        {
            AdvAssert.ThrowsInvalidOperation(() => driver.ReplyRequest(new ACCESS_REQUEST(), false));
        }

        [TestMethod]
        public void ReplyRequest_ForStoppedOutside()
        {
            driver.Start();
            driverSC.SetStatus(ServiceControllerStatus.Stopped);

            AdvAssert.ThrowsInvalidOperation(() => driver.ReplyRequest(new ACCESS_REQUEST(), false));
        }

        [TestMethod]
        public void ReplyRequest_FilterReplyMessageFails()
        {
            driver.Start();
            fltLib.FilterReplyMessageReturnCode = -125;

            AdvAssert.Throws<COMException>(() => driver.ReplyRequest(new ACCESS_REQUEST{MessageId = 1}, true), "ErrorCode", -125);

        }


        #endregion


    }
}