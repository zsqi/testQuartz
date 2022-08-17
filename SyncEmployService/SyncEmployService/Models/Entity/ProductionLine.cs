using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.Models.Entity
{
    [Serializable]
    /// <summary>
    ///  生产线表 
    /// </summary>
    public class ProductionLine
    {
        /// <summary>
        ///  ID 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        ///  生产线名称 
        /// </summary>
        public string LineName { get; set; }
        /// <summary>
        ///  备注 
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        ///  类型 
        /// </summary>
        public int? ProductionLineType { get; set; }
        /// <summary>
        ///  Eid 
        /// </summary>
        public int? Eid { get; set; }
        /// <summary>
        ///  Oid 
        /// </summary>
        public int? Oid { get; set; }
        /// <summary>
        ///  minnum 
        /// </summary>
        public int? Minnum { get; set; }
    }
}
