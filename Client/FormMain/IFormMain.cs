using System;
using System.Windows.Forms;

namespace VitaliiPianykh.FileWall.Client
{
    public interface IFormMain
    {
        event EventHandler ShowMainClicked;
        event EventHandler ShowRulesClicked;
        event EventHandler ShowPreferencesClicked;
        event EventHandler ShowEventsClicked;
        event EventHandler CloseAllClicked;
        event EventHandler RefreshRulesClicked;
        event EventHandler RefreshEventsClicked;
        event EventHandler ShowEventDetailsClicked;
        event EventHandler ClearEventsClicked;
        event EventHandler AutoRefreshEventsCheckChanged;
        event EventHandler<ExportEventsEventArgs> ExportEventsClicked;
        event EventHandler ExitAndShutDownClicked;

        object DisplayedControl { get; }
        FormMainPage SelectedPage { get; set; }
        bool AutoRefreshEvents { get; }

        void ShowMessageBox(string message);
    }

    public enum FormMainPage
    {
        Main,
        Rules,
        Preferences,
        Events
    }

    public class ExportEventsEventArgs : EventArgs
    {
        public string CSV { get; set; }
    }
}
