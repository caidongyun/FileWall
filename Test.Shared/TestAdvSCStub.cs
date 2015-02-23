using System.ServiceProcess;
using System.Threading;
using VitaliiPianykh.FileWall.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Test.Shared
{
    [TestClass]
    public class TestAdvSCStub
    {
        [TestMethod]
        public void Status_Default()
        {
            var stub = new AdvSCStub();
            Assert.AreEqual(ServiceControllerStatus.Stopped, stub.Status);
        }

        [TestMethod]
        public void Start_SetsStatusAfterShortDelay()
        {
            var advSC = new AdvSCStub();

            advSC.Start();
            Assert.AreEqual(ServiceControllerStatus.Stopped, advSC.Status);

            Thread.Sleep(150);
            Assert.AreEqual(ServiceControllerStatus.Running, advSC.Status);
        }

//        [TestMethod]
//        public void Stop_SetsStatusAfterShortDelay()
//        {
//            var advSC = new AdvSCStub();
//            advSC.Start(false); // Start without delay.
//
//            advSC.Stop();
//            Assert.AreEqual(ServiceControllerStatus.Running, advSC.Status);
//
//            Thread.Sleep(150);
//            Assert.AreEqual(ServiceControllerStatus.Stopped, advSC.Status);
//        }

        // TODO: Доработать все тесты для AdvSCStub!
    }
}