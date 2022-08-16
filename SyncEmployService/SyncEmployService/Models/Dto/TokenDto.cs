using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.Models.Dto
{
    [Serializable]
    public class TokenDto
    {
        public string corpId { get; set; }
        public string appKey { get; set; }
        public string appSecret { get; set; }
    }
}
