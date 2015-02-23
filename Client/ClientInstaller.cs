using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Windows.Forms;
using TaskScheduler;


namespace VitaliiPianykh.FileWall.Client
{
    /// <summary>
    /// Lauches FileWallClient after install.
    /// On Uninstall closes client if it's launched.
    /// </summary>
    [RunInstaller(true)]
    public class ClientInstaller : Installer
    {
        public ClientInstaller()
            : base()
        {
            // Add client log installer.
            var clientLogInstaller = new EventLogInstaller();
            clientLogInstaller.Log = "APClientLog";
            clientLogInstaller.Source = "APClient";
            Installers.Add(clientLogInstaller);

            // Subscribe to events.
            AfterInstall += ClientInstaller_AfterInstall;
        }

        private void ClientInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            ScheduleStartup(this.Context.Parameters["assemblypath"]);

            // Start client after setup completed.
            ScheduleAfterInstall(this.Context.Parameters["assemblypath"]);
        }

        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            DeleteSchedule();
            CloseClient();
        }

        /// <summary>Closes FileWallClient if it's launched.</summary>
        private static void CloseClient()
        {
            Process clientProcess = null;

            // Search for FileWallClient process.
            foreach (var process in Process.GetProcesses())
            {
                if (process.ProcessName != "FileWallClient")
                    continue;
                clientProcess = process;
                break;
            }

            if (clientProcess == null)
                return;

            // Close process by sending a close message to its main window.
            clientProcess.CloseMainWindow();

            if (clientProcess.WaitForExit(100) == false)
                clientProcess.Kill();

            // Free resources associated with process.
            clientProcess.Close();
        }

        /// <summary>Schedule FileWall to start on logon.</summary>
        private static void ScheduleStartup(string assemblyPath)
        {
            //Get a ScheduledTasks object for the local computer.
            var scheduledTasks = new ScheduledTasks();

            scheduledTasks.DeleteTask("FileWall Launch");

            // Create a task
            var task = scheduledTasks.CreateTask("FileWall Launch");

            // Fill in the program info
            task.ApplicationName = assemblyPath;
            task.Comment = "Launches FileWall when user logs on.";

            task.Flags |= TaskFlags.RunOnlyIfLoggedOn;

            // Set the account under which the task should run.
            task.SetAccountInformation(Environment.ExpandEnvironmentVariables("%USERNAME%"), (string)null);

            // Create a trigger to start the task every Sunday at 6:30 AM.
            task.Triggers.Add(new OnLogonTrigger());

            // Save the changes that have been made.
            task.Save();
            // Close the task to release its COM resources.
            task.Close();
            // Dispose the ScheduledTasks to release its COM resources.
            scheduledTasks.Dispose();
        }

        /// <summary>Delete on logon schedule.</summary>
        private static void DeleteSchedule()
        {
            //Get a ScheduledTasks object for the local computer.
            var scheduledTasks = new ScheduledTasks();

            try
            {
                scheduledTasks.DeleteTask("FileWall Launch");
            }
            catch { }
        }

        private static void ScheduleAfterInstall(string assemblyPath)
        {
            const string taskName = "FileWall after install launch";
            const string taskComment = "Launches FileWall after install.";

            //Get a ScheduledTasks object for the local computer.
            var scheduledTasks = new ScheduledTasks();

            scheduledTasks.DeleteTask(taskName);

            // Create a task
            var task = scheduledTasks.CreateTask(taskName);

            // Fill in the program info
            task.ApplicationName = assemblyPath;
            task.Comment = taskComment;

            task.Flags |= TaskFlags.RunOnlyIfLoggedOn | TaskFlags.DeleteWhenDone;

            // Set the account under which the task should run.
            task.SetAccountInformation(Environment.ExpandEnvironmentVariables("%USERNAME%"), (string)null);

            // Create a trigger to start the task every Sunday at 6:30 AM.
            task.Triggers.Add(new RunOnceTrigger(DateTime.Now));

            // Save the changes that have been made.
            task.Save();

            task.Run();

            // Close the task to release its COM resources.
            task.Close();
            // Dispose the ScheduledTasks to release its COM resources.
            scheduledTasks.Dispose();
        }
    }
}