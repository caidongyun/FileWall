namespace VitaliiPianykh.FileWall.Service
{
    partial class FileWallServiceInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller = new System.ServiceProcess.ServiceInstaller();
            this.eventLogInstaller = new System.Diagnostics.EventLogInstaller();
            this.eventLogInstallerForAccess = new System.Diagnostics.EventLogInstaller();
            // 
            // serviceProcessInstaller
            // 
            this.serviceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller.Password = null;
            this.serviceProcessInstaller.Username = null;
            // 
            // serviceInstaller
            // 
            this.serviceInstaller.ServiceName = "FileWallService";
            this.serviceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // eventLogInstaller
            // 
            this.eventLogInstaller.CategoryCount = 0;
            this.eventLogInstaller.CategoryResourceFile = null;
            this.eventLogInstaller.Log = "APLog";
            this.eventLogInstaller.MessageResourceFile = null;
            this.eventLogInstaller.ParameterResourceFile = null;
            this.eventLogInstaller.Source = "APService";
            // 
            // eventLogInstallerForAccess
            // 
            this.eventLogInstallerForAccess.CategoryCount = 0;
            this.eventLogInstallerForAccess.CategoryResourceFile = null;
            this.eventLogInstallerForAccess.Log = "APAccess";
            this.eventLogInstallerForAccess.MessageResourceFile = null;
            this.eventLogInstallerForAccess.ParameterResourceFile = null;
            this.eventLogInstallerForAccess.Source = "APLogger";
            // 
            // FileWallServiceInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller,
            this.serviceInstaller,
            this.eventLogInstaller,
            this.eventLogInstallerForAccess});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller serviceInstaller;
        private System.Diagnostics.EventLogInstaller eventLogInstaller;
        private System.Diagnostics.EventLogInstaller eventLogInstallerForAccess;
    }
}