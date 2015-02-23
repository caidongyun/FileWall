using System;
using System.ServiceProcess;
using VitaliiPianykh.FileWall.Client;
using VitaliiPianykh.FileWall.Shared;
using VitaliiPianykh.FileWall.Testing;
using AdvTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Test.Client
{
    [TestClass]
    public class TestServiceGateway
    {
        private ServiceGateway              _ServiceGateway;
        private ServiceInterfaceManagerStub _ServiceInterfaceManager;

        [TestInitialize]
        public void TestInitialize()
        {
            _ServiceInterfaceManager = new ServiceInterfaceManagerStub();
            _ServiceGateway = new ServiceGateway(new AdvSCStub(), _ServiceInterfaceManager);
        }

        #region Constructor

        [TestMethod]
        public void Constructor_NullSC()
        {
            AdvAssert.ThrowsArgumentNull(()=>new ServiceGateway(null, new ServiceInterfaceManagerStub()), "sc");
        }

        [TestMethod]
        public void Constructor_NullManager()
        {
            AdvAssert.ThrowsArgumentNull(() => new ServiceGateway(new AdvSCStub(), null), "serviceInterfaceManager");
        }

        [TestMethod]
        public void Constructor_IfServiceIsAlreadyStartedConstructorWillConnectToService()
        {
            var sc = new AdvSCStub();
            sc.Start();
            sc.WaitForStatus(ServiceControllerStatus.Running);

            var serviceGateway = new ServiceGateway(sc, new ServiceInterfaceManagerStub());

            Assert.IsNotNull(serviceGateway.ServiceInterface);
        }
        
        #endregion

        [TestMethod]
        public void Start_RaisesStarted()
        {
            AdvAssert.Raises<EventArgs>(() => _ServiceGateway.Start(), _ServiceGateway, "Started");
        }

        [TestMethod]
        public void Stop_RaisesStopped()
        {
            _ServiceGateway.Start();

            AdvAssert.Raises<EventArgs>(() => _ServiceGateway.Stop(), _ServiceGateway, "Stopped");
        }

        #region ServiceInterface Property

        [TestMethod]
        public void Start_GetsServiceInterface()
        {
            _ServiceGateway.Start();
            
            Assert.IsTrue(_ServiceInterfaceManager.IsMarshalled);
            Assert.AreSame(_ServiceInterfaceManager.ServiceInterface, _ServiceGateway.ServiceInterface);
        }

        [TestMethod]
        public void Stop_DisconnectsFromService()
        {
            _ServiceGateway.Start();// Arrange

            _ServiceGateway.Stop();

            Assert.IsFalse(_ServiceInterfaceManager.IsMarshalled);
            Assert.IsNull(_ServiceGateway.ServiceInterface);
        }

        #endregion


        class AdvSCStub2: AdvSC
        {
            private ServiceControllerStatus _Status = ServiceControllerStatus.Stopped;


            public override ServiceControllerStatus Status { get { return _Status; } }

            public void SetStatus(ServiceControllerStatus status)
            {
                _Status = status;
            }
        }

        [TestMethod]
        public void IsStarted_ReturnsTrueIfServiceStartedOtherwiseFalse()
        {
            var sc = new AdvSCStub2();
            var serviceGateway = new ServiceGateway(sc, _ServiceInterfaceManager);

            sc.SetStatus(ServiceControllerStatus.Running);
            Assert.IsTrue(serviceGateway.IsStarted);

            sc.SetStatus(ServiceControllerStatus.StartPending);
            Assert.IsFalse(serviceGateway.IsStarted);

            sc.SetStatus(ServiceControllerStatus.Stopped);
            Assert.IsFalse(serviceGateway.IsStarted);
        }
    }
}
