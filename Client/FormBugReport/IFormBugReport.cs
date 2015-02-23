using System;


namespace VitaliiPianykh.FileWall.Client
{
    public interface IFormBugReport
    {
        event EventHandler SendClicked;
        event EventHandler DontSendClicked;

        void ShowDialog();
        void Hide();

        string WhatYouDid { get; }
        string Email { get; }
        string Details { get; set; }
    }
}
