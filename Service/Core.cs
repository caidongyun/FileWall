using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using VitaliiPianykh.FileWall.Service.Native;
using VitaliiPianykh.FileWall.Shared;


namespace VitaliiPianykh.FileWall.Service
{
    public class Core
    {
        private bool _IsStarted;
        private ServiceInterface _ServiceInterface;
        private readonly Driver _Driver;
        private EventLog _EventLog;


        #region Public Methods

        public Core(Driver driver)
        {
            _Driver = driver;
        }
        
        public virtual void Start(Ruleset ruleset, ServiceInterface serviceInterface, EventLog eventLog)
        {
            if (ruleset == null)
                throw new ArgumentNullException("ruleset", "ruleset must be non null.");
            if (serviceInterface == null)
                throw new ArgumentNullException("serviceInterface", "serviceInterface must be non null.");
            if (_IsStarted)
                throw new InvalidOperationException("Core is already started.");

            _EventLog = eventLog;

            _ServiceInterface = serviceInterface;
            Ruleset = ruleset;

            //if (_Driver.IsRunning == false)// TODO: This is not covered by tests.
            _Driver.Start();

            // Subscribe to ruleset's events.
            Ruleset.Paths.RowChanged    += Paths_RowChanged;
            Ruleset.Paths.ColumnChanged += Paths_ColumnChanged;
            Ruleset.Paths.RowDeleted    += Paths_RowDeleted;
            // Populate driver with all paths.
            foreach (Ruleset.PathsRow row in Ruleset.Paths)
                foreach (var path in row.ExpandedPaths)
                    try
                    {
                        _Driver.SendCommand(COMMAND_TYPE.ADD, path, row.ID);
                    }
                    catch (PathAlreadyAddedException e)
                    {
                        EventLog.WriteEntry("APService", e.Message, EventLogEntryType.Warning);
                    }
                    

            _IsStarted = true;
        }

        public virtual void Stop()
        {
            if (!_IsStarted)
                return;

            _Driver.Stop();
            ServiceInterface.Disconnect(_ServiceInterface);

            Ruleset = null;
            _ServiceInterface = null;
            _IsStarted = false;
            _EventLog = null;
        }

        public void WaitRequest()
        {
            if (!_IsStarted)
                throw new InvalidOperationException("Can't execute while Core is stopped.");

            var request = _Driver.GetRequest();
            var allow = false;

            Ruleset.RulesRow rule;
            if (Ruleset.Processes.FindByPath(request.ProcessPath) != null)
            {
                rule = Ruleset.GetRulesRow(request.RuleID, request.ProcessPath);
                if (rule == null && Ruleset.Processes.FindByPath("*") != null)
                    rule = Ruleset.GetRulesRow(request.RuleID, "*");
            }
            else if (Ruleset.Processes.FindByPath("*") != null)
                rule = Ruleset.GetRulesRow(request.RuleID, "*");
            else
                rule = null;

            if (rule == null) // No rule found - just allow this request.
                allow = true;
            else
            {
                if (rule.Action == RuleAction.Allow)
                    allow = true;
                else if (rule.Action == RuleAction.Block)
// ReSharper disable RedundantAssignment
                    allow = false;
// ReSharper restore RedundantAssignment
                else if (rule.Action == RuleAction.Ask)
                {
                    var ea = new CoreAccessRequestedEventArgs("", request.Path, request.ProcessPath);
                    _ServiceInterface.OnAccessRequested(ea);
                    allow = ea.Allow;

                    if (ea.CreateRule)
                    {
                        var processRow = Ruleset.Processes.FindByPath(request.ProcessPath) ??
                                         Ruleset.Processes.AddProcessesRow(request.ProcessPath);

                        if (rule.ProcessPath == "*")
                        {
                            var newRule = Ruleset.GetRulesRow(request.RuleID, request.ProcessPath);
                            if (newRule == null)
                                Ruleset.Rules.AddRulesRow(
                                    (allow ? "Allow" : "Block") + " access to " + Path.GetFileName(request.Path) +
                                    " for " +
                                    Path.GetFileName(request.ProcessPath),
                                    RuleAction.FromBoolean(allow),
                                    true,
                                    rule.PathsRow,
                                    processRow,
                                    rule.ItemsRow);
                            else
                            {
                                newRule.Action = RuleAction.FromBoolean(allow);
                            }
                        }
                        else
                        {
                            rule.Action = RuleAction.FromBoolean(allow);
                        }
                    }
                }
            }

            IncrementCounter(request.AccessType, allow);

            // Log the data about request to event log.
            if ((rule == null || rule.Log) && _EventLog != null)
            {
                var entryData =
                    new LogEntryData(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString(),
                                     allow, (AccessType) request.AccessType, request.Path, request.ProcessPath);
                _EventLog.WriteEntry("Please use FileWall to view this log.", EventLogEntryType.Information, 0, 0, LogEntryData.Serialize(entryData));
            }

            _Driver.ReplyRequest(request, allow);
        }

        #endregion


        #region Public Properties

        public Ruleset Ruleset { get; private set; }

        #endregion


        #region Ruleset Event Handlers

        // User added path.
        private void Paths_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action != DataRowAction.Add)
                return;

            var row = (Ruleset.PathsRow)e.Row;
            foreach (var path in row.ExpandedPaths)
                _Driver.SendCommand(COMMAND_TYPE.ADD, path, row.ID);
            ((DataTable)sender).AcceptChanges();
        }

        // User changed existing PathsRow.
        private void Paths_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            // This row not added now, it's adding now. 
            // Check TestStaticCore.AddPath_SendCommandDEL_ShouldNotBeCalled
            if (e.Row.RowState == DataRowState.Detached)
                return;

            var row = (Ruleset.PathsRow)e.Row;

            _Driver.SendCommand(COMMAND_TYPE.DEL, null, row.ID); // Delete protected path from driver.
            foreach (var path in row.ExpandedPaths)
                _Driver.SendCommand(COMMAND_TYPE.ADD, path, row.ID); // And add it again.
        }

        // User deleted PathsRow.
        private void Paths_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            var row = (Ruleset.PathsRow)e.Row;
            _Driver.SendCommand(COMMAND_TYPE.DEL, null, (int)row["ID", DataRowVersion.Original]);
        }

        #endregion


        #region Private Functions

        /// <summary>
        /// Increments appropriate counter in <see cref="ServiceInterface"/>
        /// accord to provided data.
        /// </summary>
        private void IncrementCounter(ACCESS_TYPE accessType, bool allow)
        {
            if (allow)
            {
                if (accessType == ACCESS_TYPE.FILESYSTEM)
                    _ServiceInterface.FilesysPermits++;
                else if (accessType == ACCESS_TYPE.REGISTRY)
                    _ServiceInterface.RegistryPermits++;
            }
            else
            {
                if (accessType == ACCESS_TYPE.FILESYSTEM)
                    _ServiceInterface.FilesysBlocks++;
                else if (accessType == ACCESS_TYPE.REGISTRY)
                    _ServiceInterface.RegistryBlocks++;
            }
        }

        #endregion
    }
}
