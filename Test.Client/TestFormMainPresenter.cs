using System.Diagnostics;
using System.Windows.Forms;
using VitaliiPianykh.FileWall.Client;
using VitaliiPianykh.FileWall.Shared;
using VitaliiPianykh.FileWall.Testing;
using AdvTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Client
{
    class LogViewModelMock : LogViewModel
    {
        public bool IsRefreshed { get; private set; }
        public bool IsCleared { get; private set; }

        public LogViewModelMock() : base(new EventLog("System")) { }

        public override void Refresh() { IsRefreshed = true; }

        public override void Clear() { IsCleared = true; }
    }

    [TestClass]
    public class TestFormMainPresenter
    {
        private FormMainStub        _Form;
        private ServiceGateway      _ServiceGateway;
        private LogViewModelMock    _LogViewModel;
        private readonly ServiceInterface    _ServiceInterface;
        private FormMainPresenter   _Presenter;
        private ServiceInterfaceManagerStub _InterfaceManager;

        [TestInitialize]
        public void TestInitialize()
        {
            _Form = new FormMainStub();
            _InterfaceManager = new ServiceInterfaceManagerStub();

            // Redirect all calls to AdvSCStub and ServiceInterfaceManagerStub to avoid creating accessing system resources.
            _ServiceGateway = new ServiceGateway(new AdvSCStub(), _InterfaceManager);

            _LogViewModel = new LogViewModelMock();
            _Presenter = new FormMainPresenter(_Form, _ServiceGateway, _LogViewModel);

            _ServiceGateway.Start();
        }


        #region Constructor

        [TestMethod]
        public void Constructor()
        {
            Assert.AreSame(_ServiceGateway, _Presenter.ServiceGateway);
            Assert.AreSame(_ServiceGateway, _Presenter.MainViewPresenter.ServiceGateway);
            Assert.AreSame(_LogViewModel, _Presenter.LogViewModel);
            Assert.AreSame(_LogViewModel, _Presenter.LogViewPresenter.LogViewModel);
        }

        [TestMethod]
        public void Constructor_NullForm()
        {
            AdvAssert.ThrowsArgumentNull(() => new FormMainPresenter(null, new ServiceGateway(new AdvSCStub(), new ServiceInterfaceManagerStub()), new LogViewModelMock()), "formMain");
        }
        
        [TestMethod]
        public void Constructor_NullGateway()
        {
            AdvAssert.ThrowsArgumentNull(() => new FormMainPresenter(_Form, null, _LogViewModel), "serviceGateway");
        }

        [TestMethod]
        public void Constructor_NullModel()
        {
            AdvAssert.ThrowsArgumentNull(() => new FormMainPresenter(_Form, _ServiceGateway, null), "logViewModel");
        }

        #endregion


        #region ShowMain

        [TestMethod]
        public void ShowMain()
        {
            _Form.ShowMain();

            Assert.AreEqual(FormMainPage.Main, _Form.SelectedPage);

            Assert.IsTrue(_Form.DisplayedControl is MainViewStub);
            Assert.AreSame(_Form.DisplayedControl, _Presenter.MainViewPresenter.MainView);
        }

        [TestMethod]
        public void CloseAll_After_ShowMain()
        {
            _Form.ShowMain();
            _Form.CloseAll();

            AssertFormIsClean();
            Assert.IsNull(_Presenter.MainViewPresenter.MainView);
        }

        [TestMethod]
        public void ShowRulesClickedOnMainView()
        {
            _Form.ShowMain();

            ((MainViewStub)_Form.DisplayedControl).ShowRules();

            AssertRulesRibbonVisible(true);
        }

        [TestMethod]
        public void ShowEventsClickedOnMainView()
        {
            _Form.ShowMain();

            ((MainViewStub)_Form.DisplayedControl).ShowEvents();

            AssertEventsRibbonVisible(true);
        }

        #endregion


        #region ShowRules

        [TestMethod]
        public void ShowRules()
        {
            _Form.ShowRules();

            Assert.IsTrue(_Form.DisplayedControl is RulesetGrid);
            Assert.IsTrue(((Control)_Form.DisplayedControl).Dock == DockStyle.Fill);
            Assert.AreSame(((RulesetGrid)_Form.DisplayedControl).RuleSet, _ServiceGateway.ServiceInterface.GetRuleset());

            AssertRulesRibbonVisible(true);
        }

        [TestMethod]
        public void ShowRules_WhenServiceStopped()
        {
            _Presenter.ServiceGateway.Stop();

            _Form.ShowRules();

            Assert.IsNotNull(_Form.ShownMessage);
            Assert.IsNull(_Form.DisplayedControl); // After error ShowRules must not display any control.
        }

        [TestMethod]
        public void RefreshRules()
        {
            // Here user clicks "Show rules"
            _Form.ShowRules();
            // This is ruleset that is currently displayed on RulesetGrid.
            var oldRuleset = ((RulesetGrid)_Form.DisplayedControl).RuleSet;

            // ARRANGE
            // We need to replace ruleset
            // To do this we replacing ServiceInterface
            _InterfaceManager.ServiceInterface = new ServiceInterface(new Ruleset());
            var newRuleset = _InterfaceManager.ServiceInterface.GetRuleset();
            // But new ServiceInterface will not be fetched until restart
            _ServiceGateway.Stop();
            _ServiceGateway.Start();

            // User clicks "Refresh"
            _Form.RefreshRules();
            var displayedRuleset = ((RulesetGrid)_Form.DisplayedControl).RuleSet;

            // After clicking "Refresh" user must see fresh results
            Assert.AreNotSame(oldRuleset, newRuleset);
            Assert.AreSame(newRuleset, displayedRuleset);
        }

        [TestMethod]
        public void RefreshRules_WhenMainDisplayed()
        {
            _Form.ShowMain();
            AdvAssert.ThrowsInvalidOperation(() => _Form.RefreshRules());
        }

        [TestMethod]
        public void CloseAll_After_ShowRules()
        {
            _Form.ShowRules();

            _Form.CloseAll();

            AssertFormIsClean();
        }

        #endregion


        #region ShowPreferences

        [TestMethod]
        public void ShowPreferences()
        {
            _Form.ShowPreferences();

            Assert.IsTrue(_Form.DisplayedControl is PreferencesControl);
            Assert.IsTrue(((Control)_Form.DisplayedControl).Dock == DockStyle.Fill);

            AssertPreferencesRibbonVisible(true);
        }

        [TestMethod]
        public void CloseAll_After_ShowPreferences()
        {
            _Form.ShowPreferences();

            _Form.CloseAll();

            AssertFormIsClean();
        }

        #endregion


        #region Events tab

        [TestMethod]
        public void ShowEvents()
        {
            var logViewModelMock = new LogViewModelMock();
            _Form = new FormMainStub();
            _Presenter = new FormMainPresenter(_Form, _ServiceGateway, logViewModelMock);

            _Form.ShowEvents();

            Assert.IsTrue(_Form.DisplayedControl is LogViewControl);
            Assert.IsTrue(((Control)_Form.DisplayedControl).Dock == DockStyle.Fill);
            Assert.AreSame(_Form.DisplayedControl, _Presenter.LogViewPresenter.LogView);

            AssertEventsRibbonVisible(true);
            Assert.IsTrue(logViewModelMock.IsRefreshed);
        }

        [TestMethod]
        public void CloseAll_After_ShowEvents()
        {
            _Form.ShowEvents();
            _Form.CloseAll();

            AssertFormIsClean();
        }

        [TestMethod]
        public void RefreshEvents()
        {
            var logViewModelMock = new LogViewModelMock();
            _Form = new FormMainStub();
            _Presenter = new FormMainPresenter(_Form, _ServiceGateway, logViewModelMock);

            _Form.RefreshEvents();

            Assert.IsTrue(logViewModelMock.IsRefreshed);
        }

        [TestMethod]
        public void ClearEvents()
        {
            _Form.ClearEvents();

            Assert.IsTrue(_LogViewModel.IsCleared);
        }

        [TestMethod]
        public void ExportEventsClicked()
        {
            var csv = _Form.ExportEvents();

            Assert.IsFalse(string.IsNullOrEmpty(csv));
        }

        [TestMethod]
        public void ShowEventDetails()
        {
            var form = new FormMainStub();
            var presenter = new FormMainPresenter(form, _ServiceGateway, _LogViewModel);

            form.ShowEvents();
            form.ShowEventDetails();

            Assert.IsTrue(presenter.LogViewPresenter.DetailsVisible);
        }

        [TestMethod]
        public void AutoRefreshEvents()
        {
            _Form.AutoRefreshEventsSetCheck(true);
            Assert.IsTrue(_LogViewModel.AutoRefresh);

            _Form.AutoRefreshEventsSetCheck(false);
            Assert.IsFalse(_LogViewModel.AutoRefresh);
        }

        #endregion
        

        [TestMethod]
        public void ShowMain_After_ShowRules()
        {
            _Form.ShowRules();
            _Form.ShowMain();

            Assert.IsTrue(_Form.DisplayedControl is MainViewStub);
        }

        [TestMethod]
        public void ShowPreferences_After_ShowRules()
        {
            _Form.ShowRules();
            _Form.ShowPreferences();

            AssertRulesRibbonVisible(false);
            AssertPreferencesRibbonVisible(true);
            Assert.IsTrue(_Form.DisplayedControl is PreferencesControl);
        }

        [TestMethod]
        public void ShowRules_After_ShowPreferences()
        {
            _Form.ShowPreferences();

            _Form.ShowRules();

            AssertPreferencesRibbonVisible(false);
            AssertRulesRibbonVisible(true);
            Assert.IsTrue(_Form.DisplayedControl is RulesetGrid);
        }

        [TestMethod]
        public void ShowEvents_After_ShowRules()
        {
            _Form.ShowRules();
            _Form.ShowEvents();

            AssertRulesRibbonVisible(false);
            AssertEventsRibbonVisible(true);
            Assert.IsTrue(_Form.DisplayedControl is LogViewControl);
        }


        #region ExitAndShutdown
        
        [TestMethod]
        public void ExitAndShutdown()
        {
            _Form.ExitAndShutDown();

            Assert.IsFalse(_ServiceGateway.IsStarted);
        }

        [TestMethod]
        public void ExitAndShutdown_WhenServiceStopped()
        {
            _Form.ExitAndShutDown();

            Assert.IsFalse(_ServiceGateway.IsStarted);
        }

        #endregion


        #region Utilites

        private void AssertRulesRibbonVisible(bool isVisible)
        {
            Assert.AreEqual(isVisible, _Form.SelectedPage == FormMainPage.Rules);
        }

        private void AssertPreferencesRibbonVisible(bool isVisible)
        {
            Assert.AreEqual(isVisible, _Form.SelectedPage == FormMainPage.Preferences);
        }

        private void AssertEventsRibbonVisible(bool isVisible)
        {
            Assert.AreEqual(isVisible, _Form.SelectedPage == FormMainPage.Events);
        }

        private void AssertFormIsClean()
        {
            Assert.IsTrue(_Form.DisplayedControl is MainViewStub);
            Assert.AreEqual(FormMainPage.Main, _Form.SelectedPage);

            // Rules
            AssertRulesRibbonVisible(false);

            // Preferences
            AssertPreferencesRibbonVisible(false);
        }

        #endregion
    }
}
