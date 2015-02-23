using System;
using System.Runtime.InteropServices;
using DevExpress.XtraEditors;

namespace VitaliiPianykh.FileWall.Client
{
    public partial class FormBugReport : XtraForm, IFormBugReport
    {
        public FormBugReport()
        {
            InitializeComponent();
            DetailsVisible = false;
        }

        private void FormBugReport_Shown(object sender, EventArgs e)
        {
            // Bring the dialog to the foreground.
            SetForegroundWindow(this.Handle);
        }


        #region Implementation of IFormBugReport

        public event EventHandler SendClicked;
        public event EventHandler DontSendClicked;

        void IFormBugReport.ShowDialog()
        {
            base.ShowDialog();
        }

        public string WhatYouDid { get { return editWhatYouDid.Text; } }

        public string Email { get { return editEmail.Text; } }

        public string Details
        {
            get { return editDetails.Text; }
            set { editDetails.Text = value; }
        }

        #endregion

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (SendClicked != null)
                SendClicked(this, EventArgs.Empty);
        }

        private void buttonDontSend_Click(object sender, EventArgs e)
        {
            if (DontSendClicked != null)
                DontSendClicked(this, EventArgs.Empty);
        }

        #region Hide/show details logic

        // NOTE: This logic not in presenter because it's very simple. Hope we don't need to test it.

        private void labelShowDetails_Click(object sender, EventArgs e)
        {
            DetailsVisible = !DetailsVisible;
        }

        private bool _DetailsVisible = true;
        private bool DetailsVisible
        {
            get { return _DetailsVisible; }
            set
            {
                _DetailsVisible = value;
                if (_DetailsVisible)
                {
                    Height += 100;
                    buttonSend.Top += 100;
                    buttonDontSend.Top += 100;
                    labelShowDetails.Text = "To hide the error report, click here.";
                    editDetails.Visible = true;
                }
                else
                {
                    Height -= 100;
                    buttonSend.Top -= 100;
                    buttonDontSend.Top -= 100;
                    labelShowDetails.Text = "To see the error report, click here.";
                    editDetails.Visible = false;
                }
            }
        }

        #endregion

        [DllImport("user32")]
        public static extern int SetForegroundWindow(IntPtr hwnd);
    }
}