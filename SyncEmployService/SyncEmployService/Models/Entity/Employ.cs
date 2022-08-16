using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.Models.Entity
{
    /// <summary>
    ///  员工表 
    /// </summary>
    [Serializable]
    public class Employ
    {
        /// <summary>
        ///  id 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        ///  员工工号 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///  电话 
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        ///  是否在职 
        /// </summary>
        public string Worked { get; set; }
        /// <summary>
        ///  部门 
        /// </summary>
        public string Dept { get; set; }
        /// <summary>
        ///  姓名 
        /// </summary>
        public string OtherName { get; set; }
        /// <summary>
        ///  离职时间 
        /// </summary>
        public DateTime? LeaveTime { get; set; }
        /// <summary>
        ///  到岗时间 
        /// </summary>
        public DateTime? CStartTime { get; set; }
    }
}
