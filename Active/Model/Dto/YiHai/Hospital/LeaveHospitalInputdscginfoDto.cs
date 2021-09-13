using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai.Hospital
{
   public class LeaveHospitalInputDscgInfoDto
    {/// <summary>
     /// 就诊ID
     /// </summary>
        public string mdtrt_id { get; set; }
        /// <summary>
        /// 人员编号 *
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 险种类型 *
        /// </summary>
        public string insutype { get; set; }

        /// <summary>
        ///     出院时间  *  yyyy-MM-dd HH:mm:ss
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
        /// 计划生育服务证号
        /// </summary>
        public string fpsc_no { get; set; } = "";
        /// <summary>
        /// 生育类别
        /// </summary>
        public string matn_type { get; set; } = "";
        /// <summary>
        /// 计划生育手术类别
        /// </summary>
        public string birctrl_type { get; set; } = "";
        /// <summary>
        /// 晚育标志
        /// </summary>
        public string latechb_flag { get; set; } = "";

        /// <summary>
        /// 孕周数
        /// </summary>
        public int geso_val { get; set; }
        /// <summary>
        /// 胎次
        /// </summary>
        public int fetts { get; set; }

        /// <summary>
        /// 胎儿数
        /// </summary>
        public int fetus_cnt { get; set; }
        /// <summary>
        /// 早产标志
        /// </summary>
        public string pret_flag { get; set; } = "";
        /// <summary>
        /// 计划生育手术或生育日期 yyyy-MM-dd
        /// </summary>
        public string birctrl_matn_date { get; set; } = "";
        /// <summary>
        /// 伴有并发症标志
        /// </summary>
        public string cop_flag { get; set; } = "";
        /// <summary>
        /// 出院科室编码 *
        /// </summary>
        public string dscg_dept_codg { get; set; }
        /// <summary>
        /// 出院科室名称 *
        /// </summary>
        public string dscg_dept_name { get; set; }
        /// <summary>
        /// 出院床位
        /// </summary>
        public string dscg_bed { get; set; } = "";
        /// <summary>
        /// 离院方式 *
        /// </summary>
        public string dscg_way { get; set; }
        /// <summary>
        /// 死亡日期 yyyy-MM-dd
        /// </summary>
        public string die_date { get; set; }
        public object expContent { get; set; }
    }
}
