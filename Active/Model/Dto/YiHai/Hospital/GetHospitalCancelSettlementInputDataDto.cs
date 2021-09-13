using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai.Hospital
{
   public class GetHospitalCancelSettlementInputDataDto
    {
        /// <summary>
        ///  就诊ID
        /// </summary>
        public string mdtrt_id { get; set; }
        /// <summary>
        /// 结算id
        /// </summary>
        public string setl_id { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 字段扩展
        /// </summary>
        public string expContent { get; set; }
    }
}
