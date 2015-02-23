using System;
using VitaliiPianykh.FileWall.Client;
using VitaliiPianykh.FileWall.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Test.Client
{
    [TestClass]
    public class TestFormBugReportPresenter
    {
        [TestMethod]
        public void Show_DisplaysView()
        {
            var form = new FormBugReportStub();
            var bugSubmitter = new BugSubmitterStub();
            var presenter = new FormBugReportPresenter(bugSubmitter, form);

            var exception = new Exception("hello world");
            var expectedDetails = bugSubmitter.CollectInfo(exception).GetUserFriendlyText();
            presenter.Show(exception);

            Assert.IsTrue(form.Visible);
            Assert.AreEqual(expectedDetails, form.Details);
        }

        [TestMethod]
        public void DontSendClicked_WillHide()
        {
            var form = new FormBugReportStub();
            var bugSubmitter = new BugSubmitterStub();
            var presenter = new FormBugReportPresenter(bugSubmitter, form);

            presenter.Show(new Exception());
            form.DontSendClick();

            Assert.IsFalse(form.Visible);
        }

        [TestMethod]
        public void SendClicked_WillSendData()
        {
            var form = new FormBugReportStub();
            var bugSubmitter = new BugSubmitterStub();
            var presenter = new FormBugReportPresenter(bugSubmitter, form);

            presenter.Show(new Exception());
            form.SendClick();

            Assert.IsFalse(form.Visible);
            var expectedReport = bugSubmitter.ReportFake;
            expectedReport.UserActions = form.WhatYouDid;
            expectedReport.Email = form.Email;
            Assert.AreEqual(expectedReport, bugSubmitter.SentData);
        }
    }
}
