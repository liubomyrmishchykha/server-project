using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using InstanceSearcher;
using LoggerService;
using Models;
using Repository;
using Repository.Interfaces;
using DataCollector;
using Timer = System.Timers.Timer;

namespace WindowsService
{
    public partial class SearchingService : ServiceBase
    {
        private static int _isLock;
        private Timer _executableTimer;
        private Timer _helperTimer;
        private static readonly object _lockObject = new object();

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


        public SearchingService()
        {
            InitializeComponent();
        }


        protected override void OnStart(string[] args)
        {
            Logger.WriteLog(LogLevel.Info, "Service has started");
            _helperTimer = new Timer(120000) { AutoReset = true };
            _helperTimer.Elapsed += HelperTimer_Elapsed;
            _executableTimer = new Timer(120000) { AutoReset = true };
            _executableTimer.Elapsed += ExecutableTimer_Elapsed;
            _helperTimer.Start();
        }


        private void ExecutableTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Checking whether previous executing tasks have finished. If yes, return, else beginning new execution
            if (Interlocked.Exchange(ref _isLock, 1) != 0) return;
            Logger.WriteLog(LogLevel.Info, "Searching instances and collecting data have started");
            lock (_lockObject)
            {
                //Running tasks
                List<Task> serviceTasks = new List<Task>()
                {
                    Task.Factory.StartNew(() => Searcher.InstanceSearch()),
                    Task.Factory.StartNew(() => Collector.CollectData())
                };
                //Waiting for both tasks to finish and handling exceptions if have been raised
                try
                {
                    Task.WaitAll(serviceTasks.ToArray());
                    Logger.WriteLog(LogLevel.Info, "Searching instances and collecting data have successfully finished");
                }
                catch (AggregateException ex)
                {
                    foreach (Exception innerException in ex.Flatten().InnerExceptions)
                    {
                        Logger.WriteLog(LogLevel.Error, "Exception has occurred", innerException);
                    }
                }                
            }
            //Signaling that service can start new execution
            Interlocked.Exchange(ref _isLock, 0);
        }


        private void HelperTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int interval = GetInterval();
            if (interval != 0 & interval * 60000 != _executableTimer.Interval)
            {
                string info = string.Format("New interval will be {0} minutes", interval);
                _executableTimer.Interval = interval * 60000;
                Logger.WriteLog(LogLevel.Info, info);
            }
            if (!_executableTimer.Enabled)
            {
                _executableTimer.Start();
            }
        }


        protected override void OnStop()
        {
            Logger.WriteLog(LogLevel.Info, "Service has stopped");
            _helperTimer.Stop();
            _executableTimer.Stop();
            Interlocked.Exchange(ref _isLock, 0);
        }


        private int GetInterval()
        {
            int interval;
            IRepository<Options> repoInterval = new Repository<Options>();
            try
            {
                interval = repoInterval.SelectByID(1).Interval;
            }
            catch (SqlException ex)
            {
                Logger.WriteLog(LogLevel.Error, "Couldn't get the interval from db. Will try again in 2 minutes", ex);
                return 0;
            }
            if (interval > 0) return interval;
            Logger.WriteLog(LogLevel.Error, "Timer interval can't be less than zero");
            return 0;
        }
    }
}
