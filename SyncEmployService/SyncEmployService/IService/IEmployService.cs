using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.IService
{
    public interface IEmployService
    {
        /// <summary>
        /// 同步员工
        /// </summary>
        /// <returns></returns>
        Task SyncEmployAsync();
    }
}
