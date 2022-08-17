using SyncEmployService.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.IRepository
{
    public interface IProductionLineRepository
    {
        /// <summary>
        /// 获取生产线
        /// </summary>
        /// <returns></returns>
        Task<List<ProductionLine>> GetProductionLinesAsync();
    }
}
