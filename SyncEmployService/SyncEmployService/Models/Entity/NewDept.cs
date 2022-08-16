using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.Models.Entity
{
    /// <summary>
    ///  部门表 
    /// </summary>
    [Serializable]
    public class NewDept
    {
        /// <summary>
        ///  ID 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        ///  部门名称 
        /// </summary>
        public string DName { get; set; }
        /// <summary>
        ///  是否删除 
        /// </summary>
        public int IsExist { get; set; }
        /// <summary>
        ///  是否是业务部 
        /// </summary>
        public int IsSalesMan { get; set; }
        /// <summary>
        ///  Guid 
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 部门编码
        /// </summary>
        public string DeptCode { get; set; }
    }
}
