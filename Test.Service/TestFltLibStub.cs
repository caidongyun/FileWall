using System;
using System.Runtime.InteropServices;
using VitaliiPianykh.FileWall.Service.Native;
using VitaliiPianykh.FileWall.Shared;
using AdvTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Test.Service
{
    [TestClass]
    public class TestFltLibStub
    {
        private FltLibStub stub;

        [TestInitialize]
        public void TestInitialize()
        {
            stub = new FltLibStub();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            stub = null;
        }


        #region GetMessage and SetGetMessageReturn

        [TestMethod]
        public void GetMessage_Defaults()
        {
            var request = new ACCESS_REQUEST();

            var hr = stub.FilterGetMessage(IntPtr.Zero, ref request, 0, IntPtr.Zero);

            Assert.AreEqual(0, hr);
            Assert.AreEqual((uint)0, request.ReplyLength);
            Assert.AreEqual((ulong)0, request.MessageId);
            Assert.AreEqual((uint)0, request.ProcessID);
            Assert.AreEqual(ACCESS_TYPE.FILESYSTEM, request.AccessType);
            Assert.AreEqual(0, request.RuleID);
            Assert.AreEqual(null, request.Path);
        }

        [TestMethod]
        public void GetMessage_SetGetMessageReturn()
        {
            var expectedData = new ACCESS_REQUEST();
            expectedData.ReplyLength = (uint)Marshal.SizeOf(typeof(ACCESS_REQUEST));
            expectedData.MessageId = 10;
            expectedData.ProcessID = 11;
            expectedData.AccessType = ACCESS_TYPE.REGISTRY;
            expectedData.RuleID = 144;
            expectedData.Path = "HKCU\\USER\\SOMEKEY";
            stub.SetGetMessageReturn(-125, expectedData);

            var request = new ACCESS_REQUEST();
            var hr = stub.FilterGetMessage(IntPtr.Zero, ref request, 0, IntPtr.Zero);

            Assert.AreNotSame(expectedData, request);
            Assert.AreEqual(-125, hr);
            Assert.AreEqual(expectedData.ReplyLength, request.ReplyLength);
            Assert.AreEqual(expectedData.MessageId, request.MessageId);
            Assert.AreEqual(expectedData.ProcessID, request.ProcessID);
            Assert.AreEqual(expectedData.AccessType, request.AccessType);
            Assert.AreEqual(expectedData.RuleID, request.RuleID);
            Assert.AreEqual(expectedData.Path, request.Path);
        }
        
        #endregion


        #region FilterReplyMessage, LastAllowedMessageID and LastBlockedMessageID

        [TestMethod]
        public void FilterReplyMessage_ZeroMessageID()
        {
            var permission = new PERMISSION();

            AdvAssert.ThrowsInvalidOperation(() => stub.FilterReplyMessage(IntPtr.Zero, ref permission, 0));
        }

        [TestMethod]
        public void FilterReplyMessage_IncorrectAllow()
        {
            var incorrectAllow = new PERMISSION { Allow = 12321, MessageId = 22 };
            AdvAssert.ThrowsArgumentOutOfRange(() => stub.FilterReplyMessage(IntPtr.Zero, ref incorrectAllow, 0), "lpReplyBuffer", null);
        }

        [TestMethod]
        public void LastAllowedMessageID_Default()
        {
            AdvAssert.ThrowsInvalidOperation(() => stub.LastAllowedMessageID.ToString());
        }

        [TestMethod]
        public void LastAllowedMessageID()
        {
            var permission = new PERMISSION { Allow = 1, MessageId = 22 };
            stub.FilterReplyMessage(IntPtr.Zero, ref permission, 0);

            Assert.AreEqual((ulong)22, stub.LastAllowedMessageID);
        }

        [TestMethod]
        public void LastBlockedMessageID_Default()
        {
            AdvAssert.ThrowsInvalidOperation(() => stub.LastBlockedMessageID.ToString());
        }

        [TestMethod]
        public void LastBlockedMessageID()
        {
            var permission = new PERMISSION { Allow = 0, MessageId = 22 };
            stub.FilterReplyMessage(IntPtr.Zero, ref permission, 0);

            Assert.AreEqual((ulong)22, stub.LastBlockedMessageID);
        }

        [TestMethod]
        public void FilterReplyMessage_AllowAndBlock()
        {
            var permissionAllow = new PERMISSION { Allow = 1, MessageId = 22 };
            var permissionBlock = new PERMISSION { Allow = 0, MessageId = 33 };

            stub.FilterReplyMessage(IntPtr.Zero, ref permissionAllow, 0);
            stub.FilterReplyMessage(IntPtr.Zero, ref permissionBlock, 0);

            Assert.AreEqual((ulong)22, stub.LastAllowedMessageID);
            Assert.AreEqual((ulong)33, stub.LastBlockedMessageID);
        }

        #endregion


        [TestMethod]
        public void FilterSendMessage_AddExpandPaths()
        {
            var c = new COMMAND {CommandType = COMMAND_TYPE.ADD, ID = 1, Path = "%windir%\\testfile.txt"};
            uint returnedBytes;
            stub.FilterSendMessage(IntPtr.Zero, ref c, 0, IntPtr.Zero, 0, out returnedBytes);

            var expectedPath = AdvEnvironment.ExpandEnvironmentVariables("%windir%\\testfile.txt").ToUpper();
            Assert.IsTrue(stub.Paths.ContainsKey(expectedPath));
        }

        [TestMethod]
        public void FilterSendMessage_WilFallIfAddingEqualPath()
        {
            var c1 = new COMMAND { CommandType = COMMAND_TYPE.ADD, ID = 1, Path = "%windir%\\testfile.txt" };
            // Note that FilterSendMessage must be case insensitive. (file names are in various cases)
            var c2 = new COMMAND { CommandType = COMMAND_TYPE.ADD, ID = 1, Path = "%SystemRoot%\\TestFile.txt" }; 
            uint returnedBytes;
            int hResult;

            hResult = stub.FilterSendMessage(IntPtr.Zero, ref c1, 0, IntPtr.Zero, 0, out returnedBytes);
            Assert.AreEqual(0, hResult); // This call is successful.

            hResult = stub.FilterSendMessage(IntPtr.Zero, ref c2, 0, IntPtr.Zero, 0, out returnedBytes);
            Assert.AreEqual(0xC000022B, (uint)hResult); // And this call is unsuccessful.
        }
    }
}