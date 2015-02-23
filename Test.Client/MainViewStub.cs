using System;
using System.Drawing;
using VitaliiPianykh.FileWall.Client;
using AdvTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Test.Client
{
    class MainViewStub: IMainView
    {
        #region Implementation of IMainView

        public event EventHandler StartStopClicked;
        public event EventHandler ShowRulesClicked;
        public event EventHandler ShowEventsClicked;
        public bool StartStopEnabled { get; set; }
        public string StartStopText { get; set; }
        public Color StartStopColor { get; set; }
        public uint FilesysBlocks { get; set; }
        public uint FilesysPermits { get; set; }
        public uint RegistryBlocks { get; set; }
        public uint RegistryPermits { get; set; }

        #endregion

        public void ShowRules()
        {
            if (ShowRulesClicked != null)
                ShowRulesClicked(this, EventArgs.Empty);
        }

        public void ShowEvents()
        {
            if (ShowEventsClicked != null)
                ShowEventsClicked(this, EventArgs.Empty);
        }

        public void StartStop()
        {
            if (StartStopClicked != null)
                StartStopClicked(this, EventArgs.Empty);
        }
    }

    [TestClass]
    public class TestMainViewStub
    {
        [TestMethod]
        public void ShowRules_RaisesEvent()
        {
            var mainView = new MainViewStub();
            AdvAssert.Raises<EventArgs>(() => mainView.ShowRules(), mainView, "ShowRulesClicked");
        }

        [TestMethod]
        public void ShowEvents_RaisesEvent()
        {
            var mainView = new MainViewStub();
            AdvAssert.Raises<EventArgs>(() => mainView.ShowEvents(), mainView, "ShowEventsClicked");
        }

        [TestMethod]
        public void StartStop_RaisesEvent()
        {
            var mainView = new MainViewStub();
            AdvAssert.Raises<EventArgs>(() => mainView.StartStop(), mainView, "StartStopClicked");
        }
    }
}
