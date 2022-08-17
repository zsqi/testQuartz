using Dapper;
using SyncEmployService.IRepository;
using SyncEmployService.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.Repository
{
    public class ProductionLineRepository: IProductionLineRepository
    {
        /// <summary>
        /// 获取生产线
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductionLine>> GetProductionLinesAsync()
        {
            string sql = $@"SELECT ID,
       lineName LineName
FROM dbo.ProductionLine WITH (NOLOCK);";
            using (var conn = BaseRepository.ConnectionFactory())
            {
                conn.Open();
                var data = await conn.QueryAsync<ProductionLine>(sql);
                return data.ToList();
            }
        }
    }
}
