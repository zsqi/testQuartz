using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.Models
{
    [Serializable]
    public class CommonResponse<T>
    {
        /// <summary>
        /// 状态码；0 表示查询成功；其他值表示查询失败
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 状态码描述
        /// </summary>
        public string msg { get; set; }

        public T data { get; set; }

        public string lastModifiedTime { get; set; }

        public string details { get; set; }

        public bool success { get; set; }
    }
}
