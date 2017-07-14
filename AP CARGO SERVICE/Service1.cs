using System;
using System.Linq;
using System.ServiceProcess;
using System.Timers;

namespace AP_CARGO_SERVICE
{
    public partial class Service1 : ServiceBase
    {
        Synchronization sync;

        private Timer _timer;

        public Service1()
        {
            InitializeComponent();            
        }

        protected override void OnStart(string[] args)
        {            
            sync = new Synchronization();
            _timer = new Timer(60000);
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            _timer.Start();
        }

        protected override void OnStop()
        {
            _timer.Stop();
        }


        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            sync.Synchronize();
        }
    }
}
