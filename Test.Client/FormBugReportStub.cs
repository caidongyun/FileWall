using System;
using VitaliiPianykh.FileWall.Client;


namespace Test.Client
{
    class FormBugReportStub: IFormBugReport
    {
        #region Implementation of IFormBugReport

        public event EventHandler SendClicked;
        public event EventHandler DontSendClicked;

        public void ShowDialog()
        {
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
        }

        public string WhatYouDid { get { return "I did something."; } }
        public string Email { get { return "mymail@mail.com"; } }
        public string Details { get; set; }

        #endregion

        public bool Visible { get; private set; }

        public void SendClick()
        {
            if (SendClicked != null)
                SendClicked(this, EventArgs.Empty);
        }

        public void DontSendClick()
        {
            if (DontSendClicked != null)
                DontSendClicked(this, EventArgs.Empty);
        }
    }
}
