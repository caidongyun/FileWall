using System;
using System.Windows.Forms;


namespace VitaliiPianykh.FileWall.Client
{
    public sealed class FormMainPresenter
    {
        private readonly IFormMain _FormMain;

        /// <summary>Constructs new presenter for MainForm.</summary>
        public FormMainPresenter(IFormMain formMain, ServiceGateway serviceGateway, LogViewModel logViewModel)
        {
            if(formMain == null)
                throw new ArgumentNullException("formMain");
            if(serviceGateway == null)
                throw new ArgumentNullException("serviceGateway");

            _FormMain = formMain;
            ServiceGateway = serviceGateway;
            LogViewModel = logViewModel;
            MainViewPresenter = new MainViewPresenter(ServiceGateway);
            MainViewPresenter.ShowRulesClicked += MainViewPresenter_ShowRulesClicked;
            MainViewPresenter.ShowEventsClicked += MainViewPresenter_ShowEventsClicked;
            LogViewPresenter = new LogViewPresenter(LogViewModel);

            // Subscribe to form's events.
            _FormMain.ShowMainClicked        += form_ShowMainClicked;
            _FormMain.ShowRulesClicked       += form_ShowRulesClicked;
            _FormMain.ShowPreferencesClicked += form_ShowPreferencesClicked;
            _FormMain.ShowEventsClicked      += form_ShowEventsClicked;
            _FormMain.CloseAllClicked        += form_CloseAllClicked;
            _FormMain.RefreshRulesClicked    += form_RefreshRulesClicked;
            _FormMain.RefreshEventsClicked   += form_RefreshEventsClicked;
            _FormMain.ClearEventsClicked     += form_ClearEventsClicked;
            _FormMain.ExportEventsClicked    += form_ExportEventsClicked;
            _FormMain.ShowEventDetailsClicked       += form_ShowEventDetailsClicked;
            _FormMain.AutoRefreshEventsCheckChanged +=form_AutoRefreshEventsCheckChanged;
            _FormMain.ExitAndShutDownClicked        += form_ExitAndShutDownClicked;
        }

        #region Public properties

        public ServiceGateway ServiceGateway { get; private set; }
        public LogViewModel LogViewModel { get; private set; }

        public LogViewPresenter LogViewPresenter { get; private set; }

        public MainViewPresenter MainViewPresenter { get; private set; }

        #endregion


        #region Form's events handling

        // Main page

        private void form_ShowMainClicked(object sender, EventArgs e)
        {
            CloseAll();
            _FormMain.SelectedPage = FormMainPage.Main;
            MainViewPresenter.MainView = (IMainView) _FormMain.DisplayedControl;
        }

        private void form_ShowPreferencesClicked(object sender, EventArgs e)
        {
            CloseAll();
            //_FormMain.clientPanel.Controls.Clear(); // NOTE: This line is not testable.

            _FormMain.SelectedPage = FormMainPage.Preferences;
        }

        private void form_ShowRulesClicked(object sender, EventArgs e)
        {
            ShowRules();
        }

        private void form_ShowEventsClicked(object sender, EventArgs e)
        {
            ShowEvents();
        }

        // Rules page

        private void form_RefreshRulesClicked(object sender, EventArgs e)
        {
            if (!(_FormMain.DisplayedControl is RulesetGrid))
                throw new InvalidOperationException("RefreshRules will not refresh while RulesetGrid is not displayed. Call ShowRules first.");
            ((RulesetGrid) _FormMain.DisplayedControl).RuleSet = ServiceGateway.ServiceInterface.GetRuleset();
        }

        // Events page

        private void form_RefreshEventsClicked(object sender, EventArgs e)
        {
            LogViewModel.Refresh();
        }

        private void form_ClearEventsClicked(object sender, EventArgs e)
        {
            LogViewModel.Clear();
        }

        private void form_ExportEventsClicked(object sender, ExportEventsEventArgs e)
        {
            e.CSV = LogViewModel.Export();
        }

        private void form_AutoRefreshEventsCheckChanged(object sender, EventArgs e)
        {
            LogViewModel.AutoRefresh = _FormMain.AutoRefreshEvents;
        }

        private void form_ShowEventDetailsClicked(object sender, EventArgs e)
        {
            LogViewPresenter.DetailsVisible = !LogViewPresenter.DetailsVisible;
        }

        // Common functions

        private void form_CloseAllClicked(object sender, EventArgs e)
        {
            CloseAll();
        }

        private void form_ExitAndShutDownClicked(object sender, EventArgs e)
        {
            if (ServiceGateway.IsStarted)
                ServiceGateway.Stop();
            Application.Exit();//This call cannot be tested.
        }

        #endregion


        #region MainView's events handling

        private void MainViewPresenter_ShowRulesClicked(object sender, EventArgs e)
        {
            ShowRules();
        }

        private void MainViewPresenter_ShowEventsClicked(object sender, EventArgs e)
        {
            ShowEvents();
        }

        #endregion


        #region Private Methods

        /// <summary>Closes all tabs and show main tab.</summary>
        private void CloseAll()
        {
            _FormMain.SelectedPage = FormMainPage.Main;
            MainViewPresenter.MainView = null;
        }

        // Obsolete for external use methods

        [Obsolete]
        internal void ShowRules()
        {
            if (!ServiceGateway.IsStarted)
            {
                _FormMain.ShowMessageBox("Sorry but you can't view rules while service is stopped.");
            }
            else
            {
                CloseAll();

                _FormMain.SelectedPage = FormMainPage.Rules;

                ((RulesetGrid)_FormMain.DisplayedControl).RuleSet = ServiceGateway.ServiceInterface.GetRuleset();
            }
        }

        [Obsolete]
        internal void ShowEvents()
        {
            CloseAll();
            _FormMain.SelectedPage = FormMainPage.Events;

            LogViewModel.Refresh();

            LogViewPresenter.LogView = (ILogView)_FormMain.DisplayedControl;
        }

        #endregion
    }
}
