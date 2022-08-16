using Dapper;
using SyncEmployService.IRepository;
using SyncEmployService.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.Repository
{
    public class NewDeptRepository: INewDeptRepository
    {
        public async Task<List<NewDept>> GetNewDeptsAsync()
        {
            string sql = $@"SELECT ID,
       DName,
       IsExist,
       IsSalesMan,
       Guid,
       DeptCode
FROM dbo.NewDept WITH (NOLOCK)";
            using (var conn = BaseRepository.ConnectionFactory())
            {
                conn.Open();
                var data = await conn.QueryAsync<NewDept>(sql);
                return data.ToList();
            }
        }

        public async Task InsertNewDeptsAsync(List<NewDept> depts)
        {
            string sql = "";
            DynamicParameters Params = new DynamicParameters();
            for (int i = 0; i < depts.Count; i++)
            {
                sql += $@" INSERT INTO dbo.NewDept
(
    DName,
    IsExist,
    IsSalesMan,
    Guid,
    DeptCode
)
VALUES
(@DName{i}, {depts[i].IsExist}, {depts[i].IsSalesMan}, '{depts[i].Guid.ToString()}', @DeptCode{i});;
";
                Params.Add($"@DName{i}", depts[i].DName);
                Params.Add($"@DeptCode{i}", depts[i].DeptCode);
            }
            using (var conn = BaseRepository.ConnectionFactory())
            {
                conn.Open();
                var data = await conn.ExecuteAsync(sql, Params);
            }
        }
    }
}
