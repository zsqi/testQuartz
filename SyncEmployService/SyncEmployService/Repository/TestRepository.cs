using SyncEmployService.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.Repository
{
    public class TestRepository : ITestRepository
    {

        public int Sum(int i, int j)
        {
            return i + j;
        }
    }
}
