using Autofac;
using Quartz;
using SyncEmployService.IService;
using SyncEmployService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.Job
{
    public class SyncEmployJob:IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(async () =>
            {
                var container = DependencyConfig.GetContainer();
                var jobService = container.Resolve<JobService>();
                await jobService.SyncEmployJobAsync();
            });
        }
    }
}
