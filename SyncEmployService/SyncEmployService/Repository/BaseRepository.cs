using SyncEmployService.Common.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.Repository
{
    public class BaseRepository
    {
        public static class ConnectionString
        {
            /// <summary>
            /// 数据库连接
            /// </summary>
            public static string Connection = ConfigurationHelper.Instance.GetConfiguration(new string[] { "connectionStrings", "connection" });// "Server=.;Database=zq;User Id=sa;Password=123456;";


        }

        /// <summary>
        /// 数据库1
        /// </summary>
        public static Func<SqlConnection> ConnectionFactory = () => new SqlConnection(ConnectionString.Connection);
    }
}
