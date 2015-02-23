using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.ServiceProcess;
using VitaliiPianykh.FileWall.Shared;


namespace VitaliiPianykh.FileWall.Service
{
    public partial class FileWallService : ServiceBase
    {
        private Ruleset _Ruleset;
        private readonly string _DefaultRulesetPath;

        public FileWallService()
        {
            InitializeComponent();
            _DefaultRulesetPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                               "Ruleset.xml");
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (File.Exists(_DefaultRulesetPath) == false)
                    throw new FileNotFoundException("Ruleset file not found (" + _DefaultRulesetPath + ")");

                _Ruleset = new Ruleset();
                _Ruleset.ReadXml(_DefaultRulesetPath);
                _Ruleset.AcceptChanges();

                var serviceInterface = ServiceInterface.Marshal(_Ruleset);

                AsyncCore.Instance.Start(_Ruleset, serviceInterface, new EventLog("APAccess", ".", "APLogger"));
            }
            catch (RemotingException ex)
            {
                var registeredChannels = string.Format("\r\n******Registered channels({0})*****\r\n", ChannelServices.RegisteredChannels.Length);
                foreach (var channel in ChannelServices.RegisteredChannels)
                    registeredChannels += channel.ChannelName + "\r\n";

                EventLog.WriteEntry("APService", "On start. " + BugSubmitter.FormatErrorMessage(ex) + registeredChannels, EventLogEntryType.Error);
                throw;
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("APService", "On start. " + BugSubmitter.FormatErrorMessage(ex), EventLogEntryType.Error);
                throw;
            }

            EventLog.WriteEntry("APService", "Service successfully started.", EventLogEntryType.Information);
        }

        protected override void OnStop()
        {
            try
            {
                AsyncCore.Instance.Stop();

                // Save the ruleset back to file.
                _Ruleset.AcceptChanges();
                _Ruleset.WriteXml(_DefaultRulesetPath);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("APService", "On stop. " + BugSubmitter.FormatErrorMessage(ex), EventLogEntryType.Error);
                throw;
            }

            EventLog.WriteEntry("APService", "Service successfully stopped.");
        }
    }
}