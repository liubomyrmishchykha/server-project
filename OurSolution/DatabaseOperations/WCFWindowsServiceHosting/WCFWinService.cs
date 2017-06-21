using System;
using System.ServiceModel;
using System.ServiceProcess;
using WcfServiceLibrary;

namespace WCFWindowsServiceHosting
{
    public partial class WCFWinService : ServiceBase
    {
        static void Main(string[] args)
        {
            // Initialize the service to start
            WCFWinService service = new WCFWinService();
            // cheacking if debug in interactive mode
            if(Environment.UserInteractive)
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
        private ServiceHost wcfMyHost;

        public WCFWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Validation if wcfMyHost is not null
            if(wcfMyHost != null)
            {
                wcfMyHost.Close();
                wcfMyHost = null;
            }
            // Create host
            wcfMyHost = new ServiceHost(typeof(WCFService));

            //Open host
            wcfMyHost.Open();
        }



        protected override void OnStop()
        {
            // Stop host
            if(wcfMyHost != null)
                wcfMyHost.Close();
        }

    }
}
