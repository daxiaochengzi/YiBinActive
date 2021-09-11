using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai
{
   public class InformationUploadInputDto
    {/// <summary>
        /// 
        /// </summary>
        public InformationUploadInputMdtrtinfoDto mdtrtinfo { get; set; }
        public List<YinHaiBaseIniDiseinfo> diseinfo { get; set; }
    }
    public class InformationUploadInputMdtrtinfoDto
    {/// <summary>
        /// 就诊 ID
        /// </summary>
        public string mdtrt_id { get; set; }
        /// <summary>
        ///人员编号
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 医疗类别
        /// </summary>
        public string med_type { get; set; }
        /// <summary>
        /// 开始时间 (yyyy-MM-dd)
        /// </summary>
        public string begntime { get; set; }
        /// <summary>
        /// 主要病情描述 len(1000)
        /// </summary>
        public string main_cond_dscr { get; set; } = "";
        /// <summary>
        /// 病种名称
        /// </summary>
        public string dise_name { get; set; }
        /// <summary>
        /// 病种编码 len(30)
        /// </summary>
        public string dise_codg { get; set; }
        /// <summary>
        /// 计划生育手术类别
        /// </summary>
        public string birctrl_type { get; set; } = "";
        /// <summary>
        /// 计划生育手术类别
        /// </summary>
        public string birctrl_matn_date { get; set; }
        /// <summary>
        /// 字段扩展
        /// </summary>
        public object expContent { get; set; } = "";
    }
}
