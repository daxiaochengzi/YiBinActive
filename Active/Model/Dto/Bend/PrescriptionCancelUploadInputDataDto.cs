using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.Bend
{
   public class PrescriptionCancelUploadInputDataDto
    {
        /// <summary>
        /// 费用明细流水号
        /// </summary>
        public string feedetl_sn { get; set; }
        /// <summary>
        /// 就诊id
        /// </summary>
        public string mdtrt_id { get; set; }
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
