using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using Microsoft.Win32;


namespace VitaliiPianykh.FileWall.Shared
{
    public class BugSubmitter
    {
        public virtual void Submit(BugReport bugReport)
        {
            var dataBytes = Encoding.UTF8.GetBytes(bugReport.GetUrlEncodedText());

            var request = WebRequest.Create("http://VitaliiPianykh.dyndns.org:83/submit.php");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = dataBytes.Length;
            using (var requestStream = request.GetRequestStream())
                requestStream.Write(dataBytes, 0, dataBytes.Length);

            var responseText = string.Empty;
            var response = (HttpWebResponse)request.GetResponse();

            using (var responseStream = response.GetResponseStream())
            using (var responseReader = new StreamReader(responseStream))
                responseText = responseReader.ReadToEnd();

            if (responseText != string.Empty)
                throw new InvalidOperationException("Server returned error:\r\n" + responseText);
        }

        public virtual BugReport CollectInfo(Exception ex)
        {
            var osVer = string.Empty;
            var driverVer = string.Empty;
            var serviceVer = string.Empty;
            var clientVer = string.Empty;
            EventLog eventLog;

            osVer = Environment.OSVersion.VersionString + " "  + CultureInfo.InstalledUICulture.TwoLetterISOLanguageName.ToUpper() + " (" + Environment.ProcessorCount + " procs)";

            // Try to get version of driver.
            var driverPath = Environment.ExpandEnvironmentVariables("%windir%\\system32\\drivers\\FileWall.sys");
            if (File.Exists(driverPath))
            {
                var versionInfo = FileVersionInfo.GetVersionInfo(driverPath);
                driverVer = versionInfo.FileVersion ?? "NO_VER_INFO";
            }
            else
                driverVer = "NOT_FOUND";

            // Try to get version of service.
            var apServiceKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\FileWallService");
            if (apServiceKey == null)
                serviceVer = "NOT_REGISTER";
            else
            {
                var servicePath = (string)apServiceKey.GetValue("ImagePath", string.Empty);
                if (servicePath == string.Empty)
                    serviceVer = "NOT_FOUND";
                else
                {
                    // Delete \" at the begining and in the end.
                    servicePath = servicePath.Remove(0, 1);
                    servicePath = servicePath.Remove(servicePath.Length - 1, 1);

                    serviceVer = GetAssemblyVersion(servicePath);
                }
            }
  
            // Try to get version of client.
            clientVer = Assembly.GetEntryAssembly().GetName().Version.ToString();

            // Try to open Aplog.
            if (EventLog.Exists("APLog"))
                eventLog = new EventLog("APLog");
            else
                eventLog = null;

            return new BugReport(osVer,
                                DateTime.Now,
                                 driverVer,
                                 serviceVer,
                                 clientVer,
                                 "NOT YET SUPPORTED",
                                 FormatErrorMessage(ex),
                                 eventLog);
        }

        private static string GetAssemblyVersion(string path)
        {
            string version;
            if (File.Exists(path))
            {
                var assemblyName = AssemblyName.GetAssemblyName(path);
                version = assemblyName.Version.ToString();
            }
            else
                version = "NOT_FOUND";
            return version;
        }

        // NOTE: I'm not testing this method.
        public static string FormatErrorMessage(Exception ex)
        {
            var result = "Exception: " + ex.GetType().Namespace + "." + ex.GetType().Name + "\r\n";

            result += "******Exception message****\r\n";
            result += ex.Message + "\r\n\r\n";

            if (string.IsNullOrEmpty(ex.StackTrace) == false)
            {
                result += "******Stack trace**********\r\n";
                result += ex.StackTrace + "\r\n\r\n";
            }

            if (ex.InnerException != null)
            {
                result += "******Inner exception******\r\n";
                result += FormatErrorMessage(ex.InnerException);
            }

            return result;
        }
    }
}