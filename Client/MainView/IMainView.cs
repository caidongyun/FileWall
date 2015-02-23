using System;
using System.Drawing;


namespace VitaliiPianykh.FileWall.Client
{
    public interface IMainView
    {
        event EventHandler StartStopClicked;
        event EventHandler ShowRulesClicked;
        event EventHandler ShowEventsClicked;

        bool StartStopEnabled { get; set; }
        string StartStopText { get; set; }
        Color StartStopColor { get; set; }

        uint FilesysBlocks { get; set; }
        uint FilesysPermits { get; set; }
        uint RegistryBlocks { get; set; }
        uint RegistryPermits { get; set; }
    }
}
