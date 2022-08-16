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
        readonly ITestRepository _testRepository;
        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task<int> Sum(int i, int j)
        {
            return _testRepository.Sum(i, j);
        }
    }
}
