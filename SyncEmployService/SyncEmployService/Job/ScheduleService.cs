using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.Job
{
    public class ScheduleService
    {
        public ScheduleService()
        {

        }
        public async Task Start()
        {
            IScheduler Scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await Scheduler.Start();
            Console.WriteLine("任务调度器已启动");
        }

        public async Task Stop()
        {
            Console.WriteLine("SyncEmployService Stoped!");
        }
    }
}
