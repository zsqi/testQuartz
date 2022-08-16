using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.Models.Dto
{
    [Serializable]
    public class EmployDto
    {
        /// <summary>
        /// 员工工号，非必填；不填查询所有员工
        /// </summary>
        public List<string> employeeCodes { get; set; }
        /// <summary>
        /// 分页信息：每页数量，非必填；不填默认 30 条每页
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 页码；从 1 开始，非必填；不填默认第一页
        /// </summary>
        public int nowPageIndex { get; set; }
    }
}
