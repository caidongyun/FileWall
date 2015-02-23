using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;


namespace VitaliiPianykh.FileWall.Shared
{
    /// <summary>
    /// Provides functionality to retrieve advanced information about the environment.
    /// </summary>
    public static class AdvEnvironment
    {
        /// <summary>
        /// Replaces the name of each environment variable embedded in the specified string with the string equivalent of the value of the variable,
        /// then returns the resulting string.
        /// Advanced feature: %LOCALAPPDATA% = Environment.SpecialFolder.LocalApplicationData
        /// Advanced feature: %HISTORY% = Environment.SpecialFolder.History
        /// Advanced feature: %INET_CACHE% = Environment.SpecialFolder.InternetCache
        /// Advanced feature: %SYSTEMP% = HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment|TEMP
        /// Advanced feature: %SYSTMP% = HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment|TMP
        /// </summary>
        public static String ExpandEnvironmentVariables(String path)
        {
            String result = path;
            result = Environment.ExpandEnvironmentVariables(result);
            result = Regex.Replace(result,
                                   "%LOCALAPPDATA%",
                                   Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                   RegexOptions.Compiled | RegexOptions.IgnoreCase);
            result = Regex.Replace(result,
                                   "%HISTORY%",
                                   Environment.GetFolderPath(Environment.SpecialFolder.History),
                                   RegexOptions.Compiled | RegexOptions.IgnoreCase);
            result = Regex.Replace(result,
                                   "%INET_CACHE%",
                                   Environment.GetFolderPath(Environment.SpecialFolder.InternetCache),
                                   RegexOptions.Compiled | RegexOptions.IgnoreCase);
            result = Regex.Replace(result,
                                   "%RECENT%",
                                   Environment.GetFolderPath(Environment.SpecialFolder.Recent),
                                   RegexOptions.Compiled | RegexOptions.IgnoreCase);
            result = Regex.Replace(result,
                                   "%SYSTEMP%",
                                   (String)AdvRegistry.GetValueData("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment\\\\TEMP", "%SYSTEMP%"),
                                   RegexOptions.Compiled | RegexOptions.IgnoreCase);
            result = Regex.Replace(result,
                                   "%SYSTMP%",
                                   (String)AdvRegistry.GetValueData("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment\\\\TMP", "%SYSTMP%"),
                                   RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return result;
        }

        /// <summary>
        /// Retrieves path to execeutable but without executable name.
        /// EXAMPLE: Environment=c:\tmp\myapp.exe and AdvEnvironment=c:\tmp
        /// </summary>
        public static String ExecutablePath
        {
            get
            {
                return Regex.Replace(Application.ExecutablePath, "(.*)(\\\\.*)", "$1", RegexOptions.Compiled);
            }
        }

        public static string[] GetAllUserSIDs()
        {
            var KeyProfileList = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList");
            if(KeyProfileList == null)
                throw new InvalidOperationException("Can't open subkey.");

            var FilteredList = new List<string>();
            foreach (var keyName in KeyProfileList.GetSubKeyNames())
            {
                switch (keyName)
                {
                    case "S-1-5-18":
                    case "S-1-5-19":
                    case "S-1-5-20":
                        break;
                    default:
                        FilteredList.Add(keyName);
                        break;
                }
            }

            return FilteredList.ToArray();
        }

        /// <summary>Returns default CSV delimiter for current user.</summary>
        public static string CSVDelimiter
        {
            get
            {
                var internationalKey = Registry.CurrentUser.OpenSubKey(@"Control Panel\International");
                if (internationalKey == null)
                    return ",";
                return (string) internationalKey.GetValue("sList", ",");
            }
        }

        /// <summary>Returns localized name of "Everyone" group.</summary>
        public static string EveryoneGroupName
        {
            get
            {
                var sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                var account = sid.Translate(typeof (NTAccount)) as NTAccount;
                return account.Value;
            }
        }
    }
}