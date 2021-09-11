using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai
{
    public class OutpatientFeeUploadfeedetailInput
    { /// <summary>
      /// 费用明细流水号 * len=30
      /// </summary>
        public string feedetl_sn { get; set; }
        /// <summary>
        /// 就诊 ID * len=30
        /// </summary>
        public string mdtrt_id { get; set; }
        /// <summary>
        /// 人员编号 * len=30
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 收费批次号 * len=30
        /// </summary>
        public string chrg_bchno { get; set; }
        /// <summary>
        /// 病种编码 len=30
        /// </summary>
        public string dise_codg { get; set; } = "";
        /// <summary>
        /// 处方号 
        /// </summary>
        public string rxno { get; set; } = "";
        /// <summary>
        /// 外购处方标志 
        /// </summary>
        public string rx_circ_flag { get; set; } = "";
        /// <summary>
        /// 费用发送时间   yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string fee_ocur_time { get; set; }
        /// <summary>
        /// 医保目录编码 * 
        /// </summary>
        public string med_list_codg { get; set; }
        /// <summary>
        /// 医药机构目录编码   * 150
        /// </summary>
        public string medins_list_codg { get; set; }
        /// <summary>
        /// 明细项目费用总额   两位
        /// </summary>
        public decimal det_item_fee_sumamt { get; set; }
        /// <summary>
        /// 数量   4 位
        /// </summary>
        public decimal cnt { get; set; }
        /// <summary>
        /// 单价 6位
        /// </summary>
        public decimal pric { get; set; }
        /// <summary>
        /// 单次剂量描述
        /// </summary>
        public string sin_dos_dscr { get; set; } = "";
        /// <summary>
        /// 使用频次描述
        /// </summary>
        public string used_frqu_dscr { get; set; } = "";
        /// <summary>
        /// 周期天数
        /// </summary>
        public decimal prd_days { get; set; }
        /// <summary>
        /// 用药途径描述
        /// </summary>
        public string medc_way_dscr { get; set; } = "";
        /// <summary>
        /// 开单科室编码 *
        /// </summary>
        public string bilg_dept_codg { get; set; }
        /// <summary>
        /// 开单科室名称 
        /// </summary>
        public string bilg_dept_name { get; set; }
        /// <summary>
        /// 开单医生编码
        /// </summary>
        public string bilg_dr_codg { get; set; }
        /// <summary>
        /// 开单医师姓名
        /// </summary>
        public string bilg_dr_name { get; set; }
        /// <summary>
        /// 受单科室编码
        /// </summary>
        public string acord_dept_codg { get; set; } = "";
        /// <summary>
        /// 受单科室名称
        /// </summary>
        public string acord_dept_name { get; set; } = "";
        /// <summary>
        /// 受单医生编码
        /// </summary>
        public string orders_dr_code { get; set; } = "";
        /// <summary>
        /// 受单医生名称
        /// </summary>
        public string orders_dr_name { get; set; } = "";
        /// <summary>
        /// 医院审批标志 *    0 无须审批  2 审批不通过 1 审批通过
        /// </summary>
        public string hosp_appr_flag { get; set; }
        /// <summary>
        /// 中药使用方式
        /// </summary>
        public string tcmdrug_used_way { get; set; } = "";
        /// <summary>
        /// 外检标志
        /// </summary>
        public string etip_flag { get; set; } = "";
        /// <summary>
        /// 外检医院编码
        /// </summary>
        public string etip_hosp_code { get; set; } = "";
        /// <summary>
        /// 出院带药标志
        /// </summary>
        public string dscg_tkdrug_flag { get; set; } = "";
        /// <summary>
        /// 生育费用标志
        /// </summary>
        public string matn_fee_flag { get; set; } = "";

        /// <summary>
        /// 组套编号
        /// </summary>
        public string comb_no { get; set; } = "";
        /// <summary>
        /// 字段扩展
        /// </summary>
        public object expContent { get; set; } 
    }
}
