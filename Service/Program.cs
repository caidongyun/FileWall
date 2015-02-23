using System.ServiceProcess;


namespace VitaliiPianykh.FileWall.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase.Run(new ServiceBase[] { new FileWallService() });
        }
    }
}