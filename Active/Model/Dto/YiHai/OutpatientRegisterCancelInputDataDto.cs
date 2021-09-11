using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai
{
 public   class OutpatientRegisterCancelInputDataDto
    {/// <summary>
        /// 就诊 ID
        /// </summary>
        public string mdtrt_id { get; set; }
        /// <summary>
        ///人员编号
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string ipt_otp_no { get; set; }
        /// <summary>
        /// 字段扩展
        /// </summary>
        public object expContent { get; set; } 
    }
}
