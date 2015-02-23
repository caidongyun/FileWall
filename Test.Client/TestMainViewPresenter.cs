using System;
using System.Drawing;
using VitaliiPianykh.FileWall.Client;
using VitaliiPianykh.FileWall.Testing;
using AdvTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Test.Client
{
    [TestClass]
    public class TestMainViewPresenter
    {
        [TestMethod]
        public void Constructor_NullGateway()
        {
            AdvAssert.ThrowsArgumentNull(() => new MainViewPresenter(null), "serviceGateway");
        }

        [TestMethod]
        public void Constructor_InitializesGateway()
        {
            var gateway = new ServiceGateway(new AdvSCStub(), new ServiceInterfaceManagerStub());
            var presenter = new MainViewPresenter(gateway);

            Assert.AreSame(gateway, presenter.ServiceGateway);
        }

        [TestMethod]
        public void ShowRulesClicked_Redirected()
        {
            var mainView = new MainViewStub();
            var presenter = new MainViewPresenter(new ServiceGateway(new AdvSCStub(), new ServiceInterfaceManagerStub())) { MainView = mainView };

            AdvAssert.Raises<EventArgs>(() => mainView.ShowRules(), presenter, "ShowRulesClicked");
        }

        [TestMethod]
        public void ShowEventsClicked_Redirected()
        {
            var mainView = new MainViewStub();
            var presenter = new MainViewPresenter(new ServiceGateway(new AdvSCStub(), new ServiceInterfaceManagerStub())) {MainView = mainView};

            AdvAssert.Raises<EventArgs>(() => mainView.ShowEvents(), presenter, "ShowEventsClicked");
        }

        [TestMethod]
        public void StartStopClicked_WillStartServiceIfItWasStopped()
        {
            var mainView = new MainViewStub();
            var serviceGateway = new ServiceGateway(new AdvSCStub(), new ServiceInterfaceManagerStub());
            new MainViewPresenter(serviceGateway) { MainView = mainView };

            mainView.StartStop();

            Assert.IsTrue(serviceGateway.IsStarted);
        }

        [TestMethod]
        public void StartStopClicked_WillStopServiceIfItWasStarted()
        {
            var mainView = new MainViewStub();
            var serviceGateway = new ServiceGateway(new AdvSCStub(), new ServiceInterfaceManagerStub());
            serviceGateway.Start();
            new MainViewPresenter(serviceGateway) { MainView = mainView };

            mainView.StartStop();

            Assert.IsFalse(serviceGateway.IsStarted);
        }

        //[TestMethod]
        //public void StartStopClicked_WillSetStartStopEnabledToFalse()
        //{
        //    var mainView = new MainViewStub();
        //    new MainViewPresenter(mainView, new ServiceGateway(new AdvSCStub(), new ServiceInterfaceManagerStub()));

        //    mainView.StartStop();

        //    Assert.AreEqual("Wait...", mainView.StartStopText);
        //    Assert.AreEqual(Color.LightSkyBlue, mainView.StartStopColor);
        //    Assert.IsFalse(mainView.StartStopEnabled);
        //}

        [TestMethod]
        public void StartedEventRaisedOnServiceGateway()
        {
            var mainView = new MainViewStub();
            var serviceGateway = new ServiceGateway(new AdvSCStub(), new ServiceInterfaceManagerStub());
            new MainViewPresenter(serviceGateway) { MainView = mainView };

            serviceGateway.Start();

            Assert.AreEqual("FileWall is ON", mainView.StartStopText);
            Assert.AreEqual(Color.FromArgb(0, 192, 0), mainView.StartStopColor);
            Assert.IsTrue(mainView.StartStopEnabled);
        }

        [TestMethod]
        public void StoppedEventRaisedOnServiceGateway()
        {
            var mainView = new MainViewStub();
            var serviceGateway = new ServiceGateway(new AdvSCStub(), new ServiceInterfaceManagerStub());
            new MainViewPresenter(serviceGateway) { MainView = mainView };
            serviceGateway.Start();
            mainView.StartStopEnabled = false;

            serviceGateway.Stop();

            Assert.AreEqual("FileWall is OFF", mainView.StartStopText);
            Assert.AreEqual(Color.Red, mainView.StartStopColor);
            Assert.IsTrue(mainView.StartStopEnabled);
        }

        [TestMethod]
        public void StartedEventRaisedOnServiceGatewayViewIsNull()
        {
            var serviceGateway = new ServiceGateway(new AdvSCStub(), new ServiceInterfaceManagerStub());
            new MainViewPresenter(serviceGateway);

            serviceGateway.Start();
        }

        [TestMethod]
        public void StoppedEventRaisedOnServiceGatewayViewIsNull()
        {
            var serviceGateway = new ServiceGateway(new AdvSCStub(), new ServiceInterfaceManagerStub());
            var presenter = new MainViewPresenter(serviceGateway) {MainView = new MainViewStub()};
            serviceGateway.Start();
            presenter.MainView = null;

            serviceGateway.Stop();
        }


        #region MainView

        [TestMethod]
        public void MainView_Default()
        {
            var presenter = new MainViewPresenter(new ServiceGateway(new AdvSCStub(), new ServiceInterfaceManagerStub()));
            Assert.IsNull(presenter.MainView);
        }

        [TestMethod]
        public void MainView_SetGet()
        {
            var presenter = new MainViewPresenter(new ServiceGateway(new AdvSCStub(), new ServiceInterfaceManagerStub()));
            var mainView = new MainViewStub();
            presenter.MainView = mainView;

            Assert.AreSame(mainView, presenter.MainView);
        }

        [TestMethod]
        public void MainView_SetNull()
        {
            var presenter = new MainViewPresenter(new ServiceGateway(new AdvSCStub(), new ServiceInterfaceManagerStub()));
            presenter.MainView = null;
            Assert.IsNull(presenter.MainView);
        }

        [TestMethod]
        public void MainView_AllCountersWillSetWhenGatewayStarted()
        {
            var mainView = new MainViewStub();
            var serviceGateway = new ServiceGateway(new AdvSCStub(), new ServiceInterfaceManagerStub());
            serviceGateway.Start();
            serviceGateway.ServiceInterface.FilesysBlocks   = 1;
            serviceGateway.ServiceInterface.FilesysPermits  = 2;
            serviceGateway.ServiceInterface.RegistryBlocks  = 3;
            serviceGateway.ServiceInterface.RegistryPermits = 4;
            var presenter = new MainViewPresenter(serviceGateway);

            // If gateway is started, presenter must fetch counters values and refresh view.
            presenter.MainView = mainView;

            Assert.AreEqual(1u, mainView.FilesysBlocks);
            Assert.AreEqual(2u, mainView.FilesysPermits);
            Assert.AreEqual(3u, mainView.RegistryBlocks);
            Assert.AreEqual(4u, mainView.RegistryPermits);

            Assert.AreEqual("FileWall is ON", mainView.StartStopText);
            Assert.AreEqual(Color.FromArgb(0, 192, 0), mainView.StartStopColor);
            Assert.IsTrue(mainView.StartStopEnabled);
        }

        [TestMethod]
        public void MainView_AllCountersWillSetZeroWhenGatewayStopped()
        {
            var mainView = new MainViewStub
                               {
                                   FilesysBlocks = 1,
                                   FilesysPermits = 2,
                                   RegistryBlocks = 3,
                                   RegistryPermits = 4
                               };
            var presenter = new MainViewPresenter(new ServiceGateway(new AdvSCStub(), new ServiceInterfaceManagerStub()));

            // If gateway is stopped, presenter must zero all counters in view.
            presenter.MainView = mainView;

            Assert.AreEqual(0u, mainView.FilesysBlocks);
            Assert.AreEqual(0u, mainView.FilesysPermits);
            Assert.AreEqual(0u, mainView.RegistryBlocks);
            Assert.AreEqual(0u, mainView.RegistryPermits);

            Assert.AreEqual("FileWall is OFF", mainView.StartStopText);
            Assert.AreEqual(Color.Red, mainView.StartStopColor);
            Assert.IsTrue(mainView.StartStopEnabled);

        }

        #endregion
    }
}
