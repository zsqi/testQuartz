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
        Task<List<Employ>> GetEmployeesAsync();
        Task InsertEmployeesAsync(List<Employ> employs);
        Task UpdateEmployeesAsync(List<Employ> employs);        
    }
}
