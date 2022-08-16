using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.Models.View
{
    [Serializable]
    public class EmployView
    {
        /// <summary>
        /// 员工在喔趣系统的 id， 企业内唯一
        /// </summary>
        public int eid { get; set; }
        /// <summary>
        /// 员工工号
        /// </summary>
        public string employeeCode { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string fullName { get; set; }
        /// <summary>
        /// 离职日期
        /// </summary>
        public string gmtLeave { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        public string dateOfJoin { get; set; }
        /// <summary>
        /// 雇佣状态：Active: 在职；Terminated: 离职
        /// </summary>
        public string hiringStatus { get; set; }
        /// <summary>
        /// 员工所属部门的 id
        /// </summary>
        public int did { get; set; }
        /// <summary>
        /// 员工所属部门编码
        /// </summary>
        public string unitCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string unitName { get; set; }
        ///// <summary>
        ///// 手机号码（暂无）
        ///// </summary>
        //public string phone { get; set; }
    }
}
