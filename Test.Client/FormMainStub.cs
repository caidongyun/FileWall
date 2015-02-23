using System;
using System.Windows.Forms;
using VitaliiPianykh.FileWall.Client;
using AdvTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Client
{
    sealed class FormMainStub: IFormMain
    {
        public string ShownMessage { get; set; }

        #region Implementation of IFormMain

        public event EventHandler ShowMainClicked;
        public event EventHandler ShowRulesClicked;
        public event EventHandler ShowPreferencesClicked;
        public event EventHandler ShowEventsClicked;
        public event EventHandler CloseAllClicked;
        public event EventHandler RefreshRulesClicked;
        public event EventHandler RefreshEventsClicked;
        public event EventHandler ShowEventDetailsClicked;
        public event EventHandler ClearEventsClicked;
        public event EventHandler AutoRefreshEventsCheckChanged;
        public event EventHandler<ExportEventsEventArgs> ExportEventsClicked;
        public event EventHandler ExitAndShutDownClicked;
        public object DisplayedControl { get;  private set; }
        private FormMainPage _SelectedPage;
        public FormMainPage SelectedPage
        {
            get { return _SelectedPage; }
            set
            {
                switch (value)
                {
                    case FormMainPage.Main:
                        DisplayedControl = new MainViewStub();
                        break;
                    case FormMainPage.Rules:
                        DisplayedControl = new RulesetGrid { Dock = DockStyle.Fill };
                        break;
                    case FormMainPage.Preferences:
                        DisplayedControl = new PreferencesControl { Dock = DockStyle.Fill };
                        break;
                    case FormMainPage.Events:
                        DisplayedControl = new LogViewControl { Dock = DockStyle.Fill };
                        break;
                }

                _SelectedPage = value;
            }
        }

        public bool AutoRefreshEvents { get; private set; }

        public void ShowMessageBox(string message)
        {
            ShownMessage = message;
        }

        #endregion

        #region Event Invokatros

        public void ShowMain()
        {
            if (ShowMainClicked != null)
                ShowMainClicked(this, EventArgs.Empty);
        }

        public void ShowRules()
        {
            if (ShowRulesClicked != null)
                ShowRulesClicked(this, EventArgs.Empty);
        }

        public void ShowPreferences()
        {
            if (ShowPreferencesClicked != null)
                ShowPreferencesClicked(this, EventArgs.Empty);
        }

        public void ShowEvents()
        {
            if (ShowEventsClicked != null)
                ShowEventsClicked(this, EventArgs.Empty);
        }

        public void CloseAll()
        {
            if (CloseAllClicked != null)
                CloseAllClicked(this, EventArgs.Empty);
        }

        public void RefreshRules()
        {
            if (RefreshRulesClicked != null)
                RefreshRulesClicked(this, EventArgs.Empty);
        }

        public void RefreshEvents()
        {
            if (RefreshEventsClicked != null)
                RefreshEventsClicked(this, EventArgs.Empty);
        }

        public void ExitAndShutDown()
        {
            if (ExitAndShutDownClicked != null)
                ExitAndShutDownClicked(this, EventArgs.Empty);
        }

        public void ClearEvents()
        {
            if (ClearEventsClicked != null)
                ClearEventsClicked(this, EventArgs.Empty);
        }

        public string ExportEvents()
        {
            var ea = new ExportEventsEventArgs();
            if (ExportEventsClicked != null)
                ExportEventsClicked(this, ea);
            return ea.CSV;
        }

        public void AutoRefreshEventsSetCheck(bool isChecked)
        {
            AutoRefreshEvents = isChecked;
            if (AutoRefreshEventsCheckChanged != null)
                AutoRefreshEventsCheckChanged(this, EventArgs.Empty);
        }


        #endregion

        public void ShowEventDetails()
        {
            if (ShowEventDetailsClicked != null)
                ShowEventDetailsClicked(this, EventArgs.Empty);
        }
    }

    [TestClass]
    public class TestFormMainStub
    {
        [TestMethod]
        public void TestEventInvokators()
        {
            var stub = new FormMainStub();
            AdvAssert.Raises<EventArgs>(() => stub.ShowMain(), stub, "ShowMainClicked");
            AdvAssert.Raises<EventArgs>(() => stub.ShowRules(), stub, "ShowRulesClicked");
            AdvAssert.Raises<EventArgs>(() => stub.ShowPreferences(), stub, "ShowPreferencesClicked");
            AdvAssert.Raises<EventArgs>(() => stub.ShowEvents(), stub, "ShowEventsClicked");
            AdvAssert.Raises<EventArgs>(() => stub.CloseAll(), stub, "CloseAllClicked");
            AdvAssert.Raises<EventArgs>(() => stub.RefreshRules(), stub, "RefreshRulesClicked");
            AdvAssert.Raises<EventArgs>(() => stub.RefreshEvents(), stub, "RefreshEventsClicked");
            AdvAssert.Raises<EventArgs>(() => stub.ExitAndShutDown(), stub, "ExitAndShutDownClicked");
            AdvAssert.Raises<EventArgs>(() => stub.ClearEvents(), stub, "ClearEventsClicked");
            AdvAssert.Raises<EventArgs>(() => stub.ShowEventDetails(), stub, "ShowEventDetailsClicked");
            AdvAssert.Raises<ExportEventsEventArgs>(() => stub.ExportEvents(), stub, "ExportEventsClicked");
            AdvAssert.Raises<EventArgs>(() => stub.AutoRefreshEventsSetCheck(true), stub, "AutoRefreshEventsCheckChanged");
            Assert.IsTrue(stub.AutoRefreshEvents);
        }

        [TestMethod]
        public void ShowMessageBox()
        {
            var stub = new FormMainStub();

            Assert.IsNull(stub.ShownMessage);

            stub.ShowMessageBox("hello world!");
            Assert.AreEqual("hello world!", stub.ShownMessage);
        }

        [TestMethod]
        public void SelectedPage_CreatesControls()
        {
            var mainForm = new FormMainStub();

            mainForm.SelectedPage = FormMainPage.Main;
            Assert.IsTrue(mainForm.DisplayedControl is MainViewStub);

            mainForm.SelectedPage = FormMainPage.Rules;
            Assert.IsTrue(mainForm.DisplayedControl is RulesetGrid);
            Assert.AreEqual(DockStyle.Fill, ((Control)mainForm.DisplayedControl).Dock);

            mainForm.SelectedPage = FormMainPage.Events;
            Assert.IsTrue(mainForm.DisplayedControl is LogViewControl);
            Assert.AreEqual(DockStyle.Fill, ((Control)mainForm.DisplayedControl).Dock);

            mainForm.SelectedPage = FormMainPage.Preferences;
            Assert.IsTrue(mainForm.DisplayedControl is PreferencesControl);
            Assert.AreEqual(DockStyle.Fill, ((Control)mainForm.DisplayedControl).Dock);
        }
    }
}
