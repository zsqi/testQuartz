using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.Models.View
{
    [Serializable]
    public class TokenView
    {
        public string token { get; set; }
        /// <summary>
        /// toke 有效期，单位秒：秒；如果在有效期内重复获取，token 不变，token 的有效期会重置为 7200 秒
        /// </summary>
        public int expiresIn { get; set; }
    }
}
