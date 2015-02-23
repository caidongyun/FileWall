using System;
using System.Drawing;
using System.Windows.Forms;


namespace VitaliiPianykh.FileWall.Client
{
    public class MainViewPresenter
    {
        private Timer _AutoRefreshTimer;
        private readonly Timer _TimerBugWorkaround = new Timer {Interval = 20000};
        private IMainView _MainView;
        private readonly ServiceGateway _ServiceGateway;

        public event EventHandler ShowRulesClicked;
        public event EventHandler ShowEventsClicked;

        public MainViewPresenter(ServiceGateway serviceGateway)
        {
            if (serviceGateway == null)
                throw new ArgumentNullException("serviceGateway");

            _ServiceGateway = serviceGateway;
            _ServiceGateway.Started += ServiceGateway_Started;
            _ServiceGateway.Stopped += ServiceGateway_Stopped;

            _AutoRefreshTimer = new Timer { Interval = 3000 };
            _AutoRefreshTimer.Tick += AutoRefreshTimer_Tick;
            _AutoRefreshTimer.Enabled = true;

            // BUG workaround "Start/stop bug on service".
            _TimerBugWorkaround.Tick += TimerBugWorkaround_Tick;
        }

        public IMainView MainView
        {
            get { return _MainView; }
            set
            {
                if (_MainView != null)
                {
                    _MainView.StartStopClicked -= MainView_StartStopClicked;
                    _MainView.ShowRulesClicked -= MainView_ShowRulesClicked;
                    _MainView.ShowEventsClicked -= MainView_ShowEventClicked;
                }

                _MainView = value;

                if (_MainView == null)
                    return;

                _MainView.StartStopClicked += MainView_StartStopClicked;
                _MainView.ShowRulesClicked += MainView_ShowRulesClicked;
                _MainView.ShowEventsClicked += MainView_ShowEventClicked;

                if (_ServiceGateway.IsStarted)
                {
                    _MainView.FilesysBlocks = _ServiceGateway.ServiceInterface.FilesysBlocks;
                    _MainView.FilesysPermits = _ServiceGateway.ServiceInterface.FilesysPermits;
                    _MainView.RegistryBlocks = _ServiceGateway.ServiceInterface.RegistryBlocks;
                    _MainView.RegistryPermits = _ServiceGateway.ServiceInterface.RegistryPermits;

                    SetStartStopON();
                }
                else
                {
                    _MainView.FilesysBlocks = 0;
                    _MainView.FilesysPermits = 0;
                    _MainView.RegistryBlocks = 0;
                    _MainView.RegistryPermits = 0;

                    SetStartStopOFF();
                }

                // BUG workaround "Start/stop bug on service".
                if (_TimerBugWorkaround.Enabled)
                {
                    _MainView.StartStopEnabled = false;
                    _MainView.StartStopText = "Stopping";
                    _MainView.StartStopColor = Color.LightSkyBlue;
                }
            }
        }

        public ServiceGateway ServiceGateway { get { return _ServiceGateway; } }

        private void AutoRefreshTimer_Tick(object sender, EventArgs e)
        {
            if (_MainView == null || _ServiceGateway.IsStarted == false)
                return;

            _MainView.FilesysBlocks = _ServiceGateway.ServiceInterface.FilesysBlocks;
            _MainView.FilesysPermits = _ServiceGateway.ServiceInterface.FilesysPermits;
            _MainView.RegistryBlocks = _ServiceGateway.ServiceInterface.RegistryBlocks;
            _MainView.RegistryPermits = _ServiceGateway.ServiceInterface.RegistryPermits;

        }

        private void MainView_StartStopClicked(object sender, EventArgs e)
        {
            if (_ServiceGateway.IsStarted)
            {
                _ServiceGateway.Stop();

                // BUG workaround "Start/stop bug on service".
                _TimerBugWorkaround.Enabled = true;
                _MainView.StartStopEnabled = false;
                _MainView.StartStopText = "Stopping";
                _MainView.StartStopColor = Color.LightSkyBlue;
            }
            else
                _ServiceGateway.Start();
        }

        // Just redirect event.
        private void MainView_ShowRulesClicked(object sender, EventArgs e)
        {
            if (ShowRulesClicked != null)
                ShowRulesClicked(this, EventArgs.Empty);
        }

        // Just redirect event.
        private void MainView_ShowEventClicked(object sender, EventArgs e)
        {
            if (ShowEventsClicked != null)
                ShowEventsClicked(this, EventArgs.Empty);
        }

        private void ServiceGateway_Started(object sender, EventArgs e)
        {
            if(_MainView == null)
                return;

            SetStartStopON();
        }

        private void ServiceGateway_Stopped(object sender, EventArgs e)
        {
            if (_MainView == null)
                return;

            SetStartStopOFF();
        }

        private void SetStartStopON()
        {
            _MainView.StartStopText = "FileWall is ON";
            _MainView.StartStopColor = Color.FromArgb(0, 192, 0);
            _MainView.StartStopEnabled = true;
        }

        private void SetStartStopOFF()
        {
            _MainView.StartStopText = "FileWall is OFF";
            _MainView.StartStopColor = Color.Red;
            _MainView.StartStopEnabled = true;
        }

        private void TimerBugWorkaround_Tick(object sender, EventArgs e)
        {
            _MainView.StartStopEnabled = true;
            _TimerBugWorkaround.Enabled = false;
            SetStartStopOFF();
        }
    }
}