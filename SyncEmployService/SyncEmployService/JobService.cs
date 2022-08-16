using log4net;
using SyncEmployService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService
{
    public class JobService
    {
        readonly IEmployService _employService;
        private readonly ILog _log;
        public JobService(IEmployService employService, ILog log)
        {
            _employService = employService;
            _log = log;
        }

        public async Task SyncEmployJobAsync()
        {
            await _employService.SyncEmployAsync();
        }
    }
}
