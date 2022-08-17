using SyncEmployService.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.IRepository
{
    public interface INewDeptRepository
    {
        /// <summary>
        /// 获取部门
        /// </summary>
        /// <returns></returns>
        Task<List<NewDept>> GetNewDeptsAsync();
        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="depts"></param>
        /// <returns></returns>
        Task InsertNewDeptsAsync(List<NewDept> depts);
    }
}
