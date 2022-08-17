using SyncEmployService.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.IRepository
{
    public interface IEmployRepository
    {
        /// <summary>
        /// 获取员工
        /// </summary>
        /// <returns></returns>
        Task<List<Employ>> GetEmployeesAsync();
        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="employs"></param>
        /// <returns></returns>
        Task InsertEmployeesAsync(List<Employ> employs);
        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="employs"></param>
        /// <returns></returns>
        Task UpdateEmployeesAsync(List<Employ> employs);
    }
}
