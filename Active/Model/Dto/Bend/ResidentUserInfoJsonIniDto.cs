using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.Bend
{
   public class ResidentUserInfoJsonIniDto
    {  /// <summary>
        /// 行政区域
        /// </summary>

        [JsonProperty(PropertyName = "PO_XZQH")]
        public string AdministrativeArea { get; set; }
        /// <summary>
        /// 过程返回值(为1时正常，否则不正常)
        /// </summary>
        [JsonProperty(PropertyName = "PO_FHZ")]

        public string ReturnState { get; set; }
        /// <summary>
        /// 系统错误信息
        /// </summary>
        [JsonProperty(PropertyName = "PO_MSG")]

        public string Msg { get; set; }
    }
}
