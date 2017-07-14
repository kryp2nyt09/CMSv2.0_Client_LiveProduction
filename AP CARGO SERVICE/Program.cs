using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AP_CARGO_SERVICE
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            Service1 svc = new Service1();
            
            ServicesToRun = new ServiceBase[]
            {
                svc
            };
            
            ServiceBase.Run(ServicesToRun);            
        }
    }
}
