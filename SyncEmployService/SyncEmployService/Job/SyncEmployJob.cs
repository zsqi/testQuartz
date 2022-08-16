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
            return Task.Factory.StartNew(() =>
            {
                ITestService testService = new TestService();
                var a= testService.Sum(1, 2);
                Console.WriteLine("Hello Quartz.Net"+$"  a:{a}");
            });
        }
    }
}
