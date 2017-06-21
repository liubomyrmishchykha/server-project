using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using WcfServiceLibrary;
using System.ServiceModel;

namespace WCfWindowsServiceHost
{
    public partial class WCFWinService : ServiceBase
    {
        private ServiceHost WCFHost;     

        public WCFWinService()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {

            if(WCFHost != null)
            {
                WCFHost.Close();
                WCFHost = null;
            }

            WCFHost = new ServiceHost(typeof(WCFService));

            Uri address = new Uri("http://localhost:49679/WcfServiceLibrary/WCFService/");
            WSHttpBinding binding = new WSHttpBinding();
            Type contract = typeof (IWcfService);

            WCFHost.AddServiceEndpoint(contract, binding, address);

            WCFHost.Open();
        }
        protected override void OnStop()
        {

            if(WCFHost != null)
                WCFHost.Close();
        }
    }
}
