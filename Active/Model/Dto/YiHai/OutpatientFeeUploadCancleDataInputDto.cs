using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai
{
   public class OutpatientFeeUploadCancleDataInputDto
    {/// <summary>
     /// 就诊 id
     /// </summary>
        public string mdtrt_id { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string chrg_bchno { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 字段扩展
        /// </summary>
        public string expContent { get; set; } = "";
    }
}
