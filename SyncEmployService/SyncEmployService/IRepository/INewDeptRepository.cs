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
        Task<List<NewDept>> GetNewDeptsAsync();

        Task InsertNewDeptsAsync(List<NewDept> depts);
    }
}
