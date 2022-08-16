using log4net;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.Job
{
    public class ScheduleService
    {
        readonly CancellationTokenSource _cancellationTokenSource;
        readonly ILog _log;
        public ScheduleService(ILog log)
        {
            _log = log;
            _cancellationTokenSource = new CancellationTokenSource();
        }
        public async Task Start()
        {
            try
            {
                if (!_cancellationTokenSource.IsCancellationRequested)
                {
                    IScheduler Scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                    await Scheduler.Start();
                    Console.WriteLine("SyncEmployService Start Succsee");
                }
                _log.Info($"SyncEmployService Start Succsee");
            }
            catch (Exception ex)
            {
                _log.Error($"SyncEmployService Start", ex);
            }

        }

        public async Task Stop()
        {
            try
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _log.Info($"SyncEmployService Stop Succsee");
            }
            catch (Exception ex)
            {
                _log.Error($"SyncEmployService Stop", ex);
            }
        }
    }
}
