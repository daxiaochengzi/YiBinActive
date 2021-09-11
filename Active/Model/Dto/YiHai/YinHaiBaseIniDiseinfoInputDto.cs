using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai
{
   public class YinHaiBaseIniDiseinfoInputDto
    {/// <summary>
        /// 人员编号 --住院,出院 *
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 就诊ID --出院*
        /// </summary>
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 诊断类别 1西医主要诊断 2 西医其他诊断 3 中医主病诊断 4中医其他诊断
        /// </summary>
        public string diag_type { get; set; }
        /// <summary>
        ///诊断排序号
        /// </summary>
        public int diag_srt_no { get; set; }
        /// <summary>
        /// 诊断代码
        /// </summary>
        public string diag_code { get; set; }
        /// <summary>
        /// 诊断名称
        /// </summary>
        public string diag_name { get; set; }
        /// <summary>
        /// 诊断科室
        /// </summary>
        public string diag_dept { get; set; } = "";
        /// <summary>
        /// 诊断医生编码
        /// </summary>
        public string dise_dor_no { get; set; }
        /// <summary>
        /// 诊断医生姓名 
        /// </summary>
        public string dise_dor_name { get; set; }
        /// <summary>
        /// 诊断时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string diag_time { get; set; } = "";
        /// <summary>
        /// 计划生育手术类别
        /// </summary>
        public string birctrl_matn_date { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        public string vali_flag { get; set; } = "1";
    }
}
