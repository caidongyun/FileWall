using System;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;


namespace VitaliiPianykh.FileWall.Client
{
    public sealed partial class FormMain : DevExpress.XtraBars.Ribbon.RibbonForm, IFormMain
    {
        private FormMainPage _selectedPage;

        public FormMain()
        {
            InitializeComponent();
        }


        #region Buttons events handling

        // Handler for all "close" buttons.
        private void buttonClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            OnShowMainClicked();
        }

        #region Main page

        private void buttonMain_ItemClick(object sender, ItemClickEventArgs e)
        {
            OnShowMainClicked();
        }

        private void buttonShowRules_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ShowRulesClicked != null)
                ShowRulesClicked(this, EventArgs.Empty);
        }

        private void buttonPreferences_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ShowPreferencesClicked != null)
                ShowPreferencesClicked(this, EventArgs.Empty);
        }

        private void buttonShowEvents_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ShowEventsClicked != null)
                ShowEventsClicked(this, EventArgs.Empty);
        }

        private void buttonExitAndShutdown_ItemClick(object sender, ItemClickEventArgs e)
        {
            OnExitAndShutDownClicked();
        }


        #endregion

        #region Rules page

        private void buttonRefreshRules_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (RefreshRulesClicked != null)
                RefreshRulesClicked(this, EventArgs.Empty);
        }

        #endregion

        #region Events Page

        private void buttonRefreshEvents_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (RefreshEventsClicked != null)
                RefreshEventsClicked(this, EventArgs.Empty);
        }

        private void buttonClearEvents_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ClearEventsClicked != null)
                ClearEventsClicked(this, EventArgs.Empty);
        }

        private void checkAutoRefreshEvents_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (AutoRefreshEventsCheckChanged != null)
                AutoRefreshEventsCheckChanged(this, EventArgs.Empty);
        }


        private void buttonShowEventDetail_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ShowEventDetailsClicked != null)
                ShowEventDetailsClicked(this, EventArgs.Empty);
        }

        private void buttonSaveEvents_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (dialogSaveEvents.ShowDialog() == DialogResult.Cancel)
                return;
            var ea = new ExportEventsEventArgs();
            if (ExportEventsClicked != null)
                ExportEventsClicked(this, ea);
            File.WriteAllText(dialogSaveEvents.FileName, ea.CSV);
        }

        #endregion

        #endregion


        #region Mininimizing to tray functionality

        private void FormMain_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
                OnShowMainClicked();
            else
                // Remove all controls from client area.
                clientPanel.Controls.Clear();
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            Hide();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
                return;

            Hide();
            e.Cancel = true;
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            Focus();
            Activate();
        }

        private void toolStripMenuItemShow_Click(object sender, EventArgs e)
        {
            Show();
            Focus();
            Activate();
        }

        private void toolStripMenuItemShutdown_Click(object sender, EventArgs e)
        {
            OnExitAndShutDownClicked();
        }

        #endregion


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

        public object DisplayedControl
        {
            get
            {
                if (clientPanel.Controls.Count == 0)
                    return null;
                return clientPanel.Controls[0];
            }
            private set
            {
                clientPanel.Controls.Clear();
                if (value != null)
                    clientPanel.Controls.Add((Control) value);
            }
        }

        private MainViewControl _MainViewControl;
        private RulesetGrid _RulesetGrid;
        private PreferencesControl _PreferencesControl;
        private LogViewControl _LogViewControl;

        public FormMainPage SelectedPage
        {
            get { return _selectedPage; }
            set
            {
                switch (value)
                {
                    case FormMainPage.Main:
                        if(_MainViewControl == null) // Cache controls.
                            _MainViewControl = new MainViewControl { Dock = DockStyle.Fill };

                        DisplayedControl = _MainViewControl;
                        ribbon.SelectedPage = ribbonPageMain;
                        ribbonPageRules.Visible = false;
                        ribbonPagePreferences.Visible = false;
                        ribbonPageEvents.Visible = false;
                        pageCategoryRules.Visible = false;
                        pageCategoryPreferences.Visible = false;
                        pageCategoryEvents.Visible = false;
                        break;
                    case FormMainPage.Rules:
                        if (_RulesetGrid == null) // Cache controls.
                            _RulesetGrid = new RulesetGrid() { Dock = DockStyle.Fill };

                        DisplayedControl = _RulesetGrid;
                        ribbonPageRules.Visible = true;
                        pageCategoryRules.Visible = true;
                        ribbon.SelectedPage = ribbonPageRules;
                        break;
                    case FormMainPage.Preferences:
                        if (_PreferencesControl == null) // Cache controls.
                            _PreferencesControl = new PreferencesControl { Dock = DockStyle.Fill };

                        DisplayedControl = _PreferencesControl;
                        ribbonPagePreferences.Visible = true;
                        pageCategoryPreferences.Visible = true;
                        ribbon.SelectedPage = ribbonPagePreferences;
                        break;
                    case FormMainPage.Events:
                        if (_LogViewControl == null) // Cache controls.
                            _LogViewControl = new LogViewControl { Dock = DockStyle.Fill };

                        DisplayedControl = _LogViewControl;
                        ribbonPageEvents.Visible = true;
                        pageCategoryEvents.Visible = true;
                        ribbon.SelectedPage = ribbonPageEvents;
                        break;
                }
                _selectedPage = value;
            }
        }

        public bool AutoRefreshEvents { get { return checkAutoRefreshEvents.Checked; } }

        public void ShowMessageBox(string message)
        {
            XtraMessageBox.Show(message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion


        #region Event invokators

        private void OnShowMainClicked()
        {
            if (ShowMainClicked != null)
                ShowMainClicked(this, EventArgs.Empty);
        }

        private void OnExitAndShutDownClicked()
        {
            if (ExitAndShutDownClicked != null)
                ExitAndShutDownClicked(this, EventArgs.Empty);
        }

        #endregion
    }
}
