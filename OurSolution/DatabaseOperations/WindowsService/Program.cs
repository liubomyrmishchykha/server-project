using System;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using InstanceSearcher;

namespace WindowsService
{
    static class Program
    {
        /// <summary>
        /// Main app entry point.
        /// </summary>
        static void Main(String[] args)
        {
            // Initialize the service to start
            SearchingService service = new SearchingService();
            // In interactive mode ?
            if (Environment.UserInteractive)
            {
                service.OnStart(args);
                Console.WriteLine("Press any key to stop program");
                Console.Read();
                service.OnStop();
            }
            else
            {
                // Normal service execution
                ServiceBase.Run(service);
            }
        }
    }
}