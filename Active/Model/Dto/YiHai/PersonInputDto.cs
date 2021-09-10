using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai
{
   public class PersonInputDto
    {
        public PersonInputDataDto data { get; set; }

    }

    public class PersonInputDataDto
    { /// <summary>
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
        /// 开始时间
        /// </summary>
        public string begntime { get; set; }
        /// <summary>
        /// 人员证件类型
        /// </summary>
        public string psn_cert_type { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string certno { get; set; }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string psn_name { get; set; }

        public object expContent { get; set; }
    }

    public class PersonInputCardTokenDto
    {
        public string card_token { get; set; }
    }
}
