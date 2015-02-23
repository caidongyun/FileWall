using System;
using System.Windows.Forms;
using VitaliiPianykh.FileWall.Shared;


namespace VitaliiPianykh.FileWall.Client
{
    public class FormBugReportPresenter
    {
        private readonly BugSubmitter _BugSubmitter;
        private readonly IFormBugReport _FormBugReport;
        private BugReport _BugReport;

        public FormBugReportPresenter(BugSubmitter bugSubmitter, IFormBugReport formBugReport)
        {
            _BugSubmitter = bugSubmitter;
            _FormBugReport = formBugReport;
            _FormBugReport.SendClicked += Form_SendClicked;
            _FormBugReport.DontSendClicked += Form_DontSendClicked;
        }

        public void Show(Exception ex)
        {
            try // Yes I know that it's not good practice
            // But still it the solution.
            {
                _BugReport = _BugSubmitter.CollectInfo(ex);
            }
            catch (Exception internalException)
            {
                MessageBox.Show("Error in exception-handling code. Please send us screenshot of this message\r\n" +
                                BugSubmitter.FormatErrorMessage(internalException),
                                "FileWall",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            _FormBugReport.Details = _BugReport.GetUserFriendlyText();
            _FormBugReport.ShowDialog();
        }

        private void Form_SendClicked(object sender, EventArgs e)
        {
            _BugReport.UserActions = _FormBugReport.WhatYouDid;
            _BugReport.Email = _FormBugReport.Email;

            _FormBugReport.Hide();

            try // Yes I know that it's not good practice
                // But still it the solution.
            {
                _BugSubmitter.Submit(_BugReport);
            }
            catch (Exception internalException)
            {
                MessageBox.Show("Error in exception-handling code. Please send us screenshot of this message\r\n" +
                                BugSubmitter.FormatErrorMessage(internalException),
                                "FileWall",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            _BugReport = null;
        }

        private void Form_DontSendClicked(object sender, EventArgs e)
        {
            _FormBugReport.Hide();
        }
    }
}
