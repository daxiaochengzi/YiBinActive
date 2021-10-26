using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai.comm
{
  public  class QueryTreatmentInputDto
    { /// <summary>
      /// 人员编号
      /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 险种类型
        /// </summary>
        public string insutype { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public string fixmedins_code { get; set; }

        /// <summary>
        /// 医疗类别
        /// </summary>
        public string med_type { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string begntime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string endtime { get; set; }
        
        /// <summary>
        /// 病种编码
        /// </summary>
        public string dise_codg { get; set; } = "";
        /// <summary>
        /// 病种名称
        /// </summary>
        public string dise_name { get; set; } = "";
        /// <summary>
        /// 手术操作代码
        /// </summary>
        public string oprn_oprt_code { get; set; } = "";
        /// <summary>
        /// 手术操作名称
        /// </summary>
        public string oprn_oprt_name { get; set; } = "";
        /// <summary>
        /// 生育类别
        /// </summary>
        public string matn_type { get; set; } = "";

        /// <summary>
        /// 计划生育手术类别
        /// </summary>
        public string birctrl_type { get; set; } = "";
        public object expContent { get; set; } = "";
    }
}
