using Dapper;
using Newtonsoft.Json.Linq;
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
    public class EmployRepository: IEmployRepository
    {
        /// <summary>
        /// 获取员工
        /// </summary>
        /// <returns></returns>
        public async Task<List<Employ>> GetEmployeesAsync()
        {
            string sql = $@"SELECT id,
       Name,
       Phone,
       Worked,
       Dept,
       OtherName,
       LeaveTime,
       CStartTime
FROM dbo.Employ WITH (NOLOCK)";
            using (var conn = BaseRepository.ConnectionFactory())
            {
                conn.Open();
                var data=await conn.QueryAsync<Employ>(sql);
                return data.ToList();
            }
        }
        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="employs"></param>
        /// <returns></returns>
        public async Task InsertEmployeesAsync(List<Employ> employs)
        {
            string sql = " DECLARE @newid INT = 0; ";
            DynamicParameters Params = new DynamicParameters();          
            for (int i = 0; i < employs.Count; i++)
            {
                sql += $@" INSERT INTO dbo.Employ
(
    Name,
    Phone,
    Worked,
    Dept,
    OtherName,
    LeaveTime,
    CStartTime
)
VALUES
(@Name{i}, @Phone{i}, '{employs[i].Worked}', @Dept{i}, @OtherName{i}, @LeaveTime{i}, @CStartTime{i}); 
SET @newid = (SELECT @@IDENTITY);
INSERT dbo.Duty
(
    EID,
    DutyName
)
VALUES
(@newid, '生产技工'); 
";
                Params.Add($"@Name{i}", employs[i].Name);
                Params.Add($"@Phone{i}", employs[i].Phone);
                Params.Add($"@Dept{i}", employs[i].Dept);
                Params.Add($"@OtherName{i}", employs[i].OtherName);
                Params.Add($"@LeaveTime{i}", employs[i].LeaveTime);
                Params.Add($"@CStartTime{i}", employs[i].CStartTime);

            }
            using (var conn = BaseRepository.ConnectionFactory())
            {
                conn.Open();
                var data = await conn.ExecuteAsync(sql,Params);
            }
        }
        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="employs"></param>
        /// <returns></returns>
        public async Task UpdateEmployeesAsync(List<Employ> employs)
        {
            string sql = "";
            DynamicParameters Params = new DynamicParameters();
            for (int i = 0; i < employs.Count; i++)
            {
                sql += $@" UPDATE dbo.Employ
SET Name = @Name{i},
    Phone = @Phone{i},
    Worked = '{employs[i].Worked}',
    Dept = @Dept{i},
    OtherName = @OtherName{i},
    LeaveTime = @LeaveTime{i},
    CStartTime = @CStartTime{i}
WHERE id = @ID{i};
";
                Params.Add($"@Name{i}", employs[i].Name);
                Params.Add($"@Phone{i}", employs[i].Phone);
                Params.Add($"@Dept{i}", employs[i].Dept);
                Params.Add($"@OtherName{i}", employs[i].OtherName);
                Params.Add($"@LeaveTime{i}", employs[i].LeaveTime);
                Params.Add($"@CStartTime{i}", employs[i].CStartTime);
                Params.Add($"@ID{i}", employs[i].ID);
            }
            using (var conn = BaseRepository.ConnectionFactory())
            {
                conn.Open();
                var data = await conn.ExecuteAsync(sql, Params);
            }
        }
    }
}
