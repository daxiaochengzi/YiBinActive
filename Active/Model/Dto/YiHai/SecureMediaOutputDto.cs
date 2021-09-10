using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai
{
  public  class SecureMediaOutputDto
    {
        /// <summary>
        /// 交易状态
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 交易信息
        /// </summary>
        public string message { get; set; }
        public  SecureMediaOutputDataDto data { get; set; }

    }

    public class SecureMediaOutputDataDto
    {
        /// <summary>
        /// 社会保障号
        /// </summary>
        public string card_no { get; set; }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string psn_name { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string certno { get; set; }
        /// <summary>
        /// 就诊凭证类型
        /// </summary>
        public string psn_cert_type { get; set; }
        /// <summary>
        /// 就诊凭证类型
        /// </summary>
        public string mdtrt_cert_type { get; set; }
        /// <summary>
        /// 就诊凭证编号
        /// </summary>
        public string mdtrt_cert_no { get; set; }
        /// <summary>
        /// 卡识别码
        /// </summary>
        public string card_sn { get; set; }
        /// <summary>
        /// 参保地医保区划
        /// </summary>
        public string insuplc_admdvs { get; set; }
        /// <summary>
        /// 校验介质
        /// </summary>
        public string card_token { get; set; }
    }
}
