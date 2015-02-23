using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using VitaliiPianykh.FileWall.Client.Properties;
using VitaliiPianykh.FileWall.Shared;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using Microsoft.VisualBasic.ApplicationServices;


namespace VitaliiPianykh.FileWall.Client
{
    static class Program
    {
        /// <summary>The main entry point for the application.</summary>
        [STAThread]
        static void Main()
        {
            var app = new FileWallClient();
            app.Run(new string[]{});
        }
        
        class FileWallClient: WindowsFormsApplicationBase
        {
            private FormBugReportPresenter _BugReportPresenter;
            private FormMainPresenter _Presenter;
            private FormMain _FormMain;
            private ServiceGateway _ServiceGateway;

            public FileWallClient()
            {
                // Make this a single-instance application
                IsSingleInstance = true;
                EnableVisualStyles = true;
                StartupNextInstance += App_StartupNextInstance;
            }

            protected override bool OnInitialize(System.Collections.ObjectModel.ReadOnlyCollection<string> commandLineArgs)
            {
                try
                {
                    ChangeUICulture(Settings.Default.UICulture.Name);

                    // Register skins.
                    DevExpress.UserSkins.BonusSkins.Register();
                    DevExpress.UserSkins.OfficeSkins.Register();

                    // Set user skin.
                    UserLookAndFeel.Default.SetSkinStyle(Settings.Default.SkinName);

                    // Initialize automatic error handling.
                    Application.ThreadException += Application_ThreadException;
                    _BugReportPresenter = new FormBugReportPresenter(new BugSubmitter(), new FormBugReport());
                }
                catch (Exception ex)
                {
                    // NOTE: Here we can't use FormBugReport because it may not be initialized.
                    MessageBox.Show(
                        "Unfortunally FileWall failed to initialize. Please send us screenshot of this window.\r\n" +
                        BugSubmitter.FormatErrorMessage(ex), "FileWall", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                try
                {
                    _FormMain = new FormMain();

                    _ServiceGateway = new ServiceGateway(new AdvSC("FileWallService"), new ServiceInterfaceManager());
                    _ServiceGateway.Started += ServiceGateway_Started;

                    _Presenter = new FormMainPresenter(_FormMain,
                                                       _ServiceGateway,
                                                       new LogViewModel(new EventLog("APAccess")));

                    // Start service if it's needed.
                    if (!_Presenter.ServiceGateway.IsStarted)
                        _Presenter.ServiceGateway.Start();
                    else
                    {
                        // Subscribing to events.
                        var accessRequestedWrapper = new AccessRequestedWrapper(_ServiceGateway.ServiceInterface);
                        accessRequestedWrapper.AccessRequested += AccessRequested;
                    }
                }
                catch (Exception ex)
                {
                    _BugReportPresenter.Show(ex);
                    return false;
                }

                return true;
            }

            protected override void OnCreateMainForm()
            {
                MainForm = _FormMain;
            }

            private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
            {
                // Log exception details before showing "error dialog"
                try
                {
                    var submitter = new BugSubmitter();
                    var bugReport = submitter.CollectInfo(e.Exception);

                    EventLog.WriteEntry("APClient", bugReport.GetUserFriendlyText(), EventLogEntryType.Error);
                }
                catch{}

                // Show error dialog.
                _BugReportPresenter.Show(e.Exception);
            }

            private static void App_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
            {
                XtraMessageBox.Show(
                    "One instance of FileWall client is already running. Please check the system tray arrea to find it.",
                    "FileWall", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            private void ServiceGateway_Started(object sender, EventArgs e)
            {
                var accessRequestedWrapper = new AccessRequestedWrapper(_ServiceGateway.ServiceInterface);
                accessRequestedWrapper.AccessRequested += AccessRequested;
            }

            private static void AccessRequested(object sender, CoreAccessRequestedEventArgs ea)
            {
                FormAccessRequest.Show(ea);
            }
        }
    }
}