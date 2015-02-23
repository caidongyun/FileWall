using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using VitaliiPianykh.FileWall.Shared;


namespace VitaliiPianykh.FileWall.Client
{
    public class LogViewModel
    {
        Timer _AutoRefreshTimer;
        private int _LastIndex;

        public LogViewModel(EventLog eventLog)
        {
            // Check parameters.
            if (eventLog == null)
                throw new ArgumentNullException("eventLog");
            EventLog = eventLog;
            Data = new BindingList<LogEntryData>();

            // Setup AutoRefresh timer.
            _AutoRefreshTimer = new Timer();
            _AutoRefreshTimer.Interval = 3000;
            _AutoRefreshTimer.Tick += AutoRefreshTimer_Tick;
        }

        public EventLog EventLog { get; private set; }

        public BindingList<LogEntryData> Data { get; private set; }

        public bool AutoRefresh
        {
            get { return _AutoRefreshTimer.Enabled; }
            set { _AutoRefreshTimer.Enabled = value; }
        }

        /// <summary>Refreshes <see cref="Data"/>.</summary>
        public virtual void Refresh()
        {
            //Data.Clear();

            // Fill the data of view.
            foreach (EventLogEntry entry in EventLog.Entries)
            {
                if (entry.Index <= _LastIndex)
                    continue;

                _LastIndex = entry.Index;

                Data.Add(LogEntryData.Deserialize(entry.Data));
            }
        }

        

        public virtual void Clear()
        {
            Data.Clear();
            EventLog.Clear();
        }

        public string Export()
        {
            var csvDelimiter = AdvEnvironment.CSVDelimiter;
            var csvText = string.Format("Date{0} Action{0} AccessType{0} Path{0} ProcessPath\r\n", csvDelimiter);

            foreach (var logEntryData in Data)
            {
                csvText += string.Format("{0}{5} {1}{5} {2}{5} {3}{5} {4}\r\n",
                                         logEntryData.Date,
                                         logEntryData.IsAllowed ? "Allow" : "Block",
                                         logEntryData.AccessType,
                                         logEntryData.Path,
                                         logEntryData.ProcessPath,
                                         csvDelimiter);
            }
            return csvText;
        }

        // NOTE: AutoRefresh feature cannot be tested with unit tests.
        private void AutoRefreshTimer_Tick(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
