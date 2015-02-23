using System;
using System.Diagnostics;
using System.Web;


namespace VitaliiPianykh.FileWall.Shared
{
    public sealed class BugReport
    {
        public BugReport(string OSVer, DateTime errorDate, string driverVer, string serviceVer, string clientVer, string errorID, string exceptionDetails, EventLog log)
        {
            this.OSVer = OSVer;
            ErrorDate = errorDate;
            DriverVer = driverVer;
            ServiceVer = serviceVer;
            ClientVer = clientVer;
            ErrorID = errorID;
            ExceptionDetails = exceptionDetails;
            Log = log;
        }

        public string OSVer { get; private set; }
        public DateTime ErrorDate { get; private set; }
        public string DriverVer { get; private set; }
        public string ServiceVer { get; private set; }
        public string ClientVer { get; private set; }
        public string ErrorID { get; private set; }
        public string ExceptionDetails { get; private set; }
        public string UserActions { get; set; }
        public string Email { get; set; }
        public EventLog Log { get; private set; }

        public string GetUserFriendlyText()
        {
            var data =
                "OSVer=" + OSVer +
                "\r\nErrorDate=" + ErrorDate.ToString("yyyy-MM-dd HH:mm:ss") +
                "\r\nDriverVer=" + DriverVer +
                "\r\nServiceVer=" + ServiceVer +
                "\r\nClientVer=" + ClientVer +
                "\r\nErrorID=" + ErrorID +
                "\r\nExceptionDetails=" + ExceptionDetails +
                "\r\nUserActions=Text that you entered goes here." +
                "\r\nemail=email that you entered goes here.";

            if (Log != null)
            {
                var eventNumber = 0;

                for (var i = Log.Entries.Count - 1; i >= 0; i--)
                {
                    var logEntry = Log.Entries[i];

                    // Ommit all warnings.
                    if (logEntry.EntryType != EventLogEntryType.Error &&
                        logEntry.EntryType != EventLogEntryType.Information)
                        continue;

                    data += "\r\nEvent" + eventNumber + "=" +
                            logEntry.TimeGenerated.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" +
                            logEntry.Message;
                    eventNumber++;

                    if (eventNumber == 10)
                        break;
                }
            }

            return data;
        }

        public string GetUrlEncodedText()
        {
            var data =
                "OSVer=" + HttpUtility.UrlEncode(OSVer) +
                "&ErrorDate=" + HttpUtility.UrlEncode(ErrorDate.ToString("yyyy-MM-dd HH:mm:ss")) +
                "&DriverVer=" + HttpUtility.UrlEncode(DriverVer) +
                "&ServiceVer=" + HttpUtility.UrlEncode(ServiceVer) +
                "&ClientVer=" + HttpUtility.UrlEncode(ClientVer) +
                "&ErrorID=" + HttpUtility.UrlEncode(ErrorID) +
                "&ExceptionDetails=" + HttpUtility.UrlEncode(ExceptionDetails) +
                "&UserActions=" + HttpUtility.UrlEncode(UserActions) +
                "&email=" + HttpUtility.UrlEncode(Email);

            if (Log != null)
            {
                var eventNumber = 0;

                for (var i = Log.Entries.Count - 1; i >= 0; i--)
                {
                    var logEntry = Log.Entries[i];

                    // Ommit all warnings.
                    if (logEntry.EntryType != EventLogEntryType.Error &&
                        logEntry.EntryType != EventLogEntryType.Information)
                        continue;

                    data += "&Event" + eventNumber + "=" +
                               HttpUtility.UrlEncode(logEntry.TimeGenerated.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n") +
                               HttpUtility.UrlEncode(logEntry.Message);
                    eventNumber++;

                    if (eventNumber == 10)
                        break;
                }
            }

            return data;
        }
    }
}