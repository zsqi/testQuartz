using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.IService
{
    public interface ITestService
    {
        /// <summary>
        /// 求和
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        int Sum(int i, int j);
    }
}
