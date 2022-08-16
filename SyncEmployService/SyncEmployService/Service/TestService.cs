using SyncEmployService.IRepository;
using SyncEmployService.IService;
using SyncEmployService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.Service
{
    public class TestService : ITestService
    {
        ITestRepository test = new TestRepository();
        public int Sum(int i, int j)
        {
            return test.Sum(i, j);
        }
    }
}
