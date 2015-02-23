using System;
using System.Drawing;
using DevExpress.XtraEditors;

namespace VitaliiPianykh.FileWall.Client
{
    public partial class MainViewControl : XtraUserControl, IMainView
    {
        public MainViewControl()
        {
            InitializeComponent();
        }

        #region Implementation of IMainView

        public event EventHandler StartStopClicked;
        public event EventHandler ShowRulesClicked;
        public event EventHandler ShowEventsClicked;
        
        public bool StartStopEnabled
        {
            get { return buttonStartStop.Enabled; }
            set { buttonStartStop.Enabled = value; }
        }

        public string StartStopText
        {
            get { return buttonStartStop.Text; }
            set { buttonStartStop.Text = value; }
        }

        public Color StartStopColor
        {
            get { return buttonStartStop.ForeColor; }
            set { buttonStartStop.ForeColor = value; }
        }

        public uint FilesysBlocks
        {
            get { return Convert.ToUInt32(labelFilesysBlocks.Text); }
            set { labelFilesysBlocks.Text = value.ToString(); }
        }

        public uint FilesysPermits
        {
            get { return Convert.ToUInt32(labelFilesysPermits.Text); }
            set { labelFilesysPermits.Text = value.ToString(); }
        }

        public uint RegistryBlocks
        {
            get { return Convert.ToUInt32(labelRegistryBlocks.Text); }
            set { labelRegistryBlocks.Text = value.ToString(); }
        }

        public uint RegistryPermits
        {
            get { return Convert.ToUInt32(labelRegistryPermits.Text); }
            set { labelRegistryPermits.Text = value.ToString(); }
        }

        #endregion

        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            if (StartStopClicked != null)
                StartStopClicked(this, EventArgs.Empty);
        }

        private void buttonShowRules_Click(object sender, EventArgs e)
        {
            if (ShowRulesClicked != null)
                ShowRulesClicked(this, EventArgs.Empty);
        }

        private void buttonViewEvents_Click(object sender, EventArgs e)
        {
            if (ShowEventsClicked != null)
                ShowEventsClicked(this, EventArgs.Empty);
        }
    }
}
