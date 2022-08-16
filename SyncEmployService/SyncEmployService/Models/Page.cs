using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.Models
{
    [Serializable]
    public class Page<T>
    {
        /// <summary>
        /// 分页信息：员工总条数
        /// </summary>
        public int totalItem { get; set; }
        /// <summary>
        /// 分页信息：员工总页数
        /// </summary>
        public int totalPage { get; set; }
        /// <summary>
        /// list
        /// </summary>
        public List<T> list { get; set; }
    }
}
