using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;


namespace VitaliiPianykh.FileWall.Service
{
    [RunInstaller(true)]
    public partial class FileWallServiceInstaller : Installer
    {
        public FileWallServiceInstaller()
        {
            InitializeComponent();
        }

        public override void Uninstall(System.Collections.IDictionary savedState)
        {
			// If service is started - stop it first.
            var sc = new ServiceController("FileWallService");
            if(sc.Status == ServiceControllerStatus.Running)
                sc.Stop();

            sc.WaitForStatus(ServiceControllerStatus.Stopped);

            base.Uninstall(savedState);
        }
    }
}