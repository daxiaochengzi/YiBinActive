using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai
{
   public class OutpatientRegisterInputDataParam
    { /// <summary>
        /// 人员编号
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 险种类型
        /// </summary>

        public string insutype { get; set; }
        /// <summary>
        /// 就诊凭证类型
        /// </summary>
        public string mdtrt_cert_type { get; set; }
        /// <summary>
        /// 就诊凭证编号
        /// </summary>
        public string mdtrt_cert_no { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string ipt_otp_no { get; set; }
        /// <summary>
        /// 医师编码
        /// </summary>
        public string atddr_no { get; set; }
        /// <summary>
        /// 医师姓名
        /// </summary>
        public string dr_name { get; set; }
        /// <summary>
        /// 科室编号
        /// </summary>
        public string dept_code { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string dept_name { get; set; }
        /// <summary>
        /// 科别 (码表)
        /// </summary>
        public string caty { get; set; }
        /// <summary>
        /// 校验介质
        /// </summary>
        public object expContent { get; set; }
        public  string begntime { get; set; }
       
    }
}
