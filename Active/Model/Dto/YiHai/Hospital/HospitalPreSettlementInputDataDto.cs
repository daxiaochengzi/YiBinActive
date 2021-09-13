using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai.Hospital
{
   public class HospitalPreSettlementInputDataDto
    {/// <summary>
     /// 人员编号
     /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 就诊凭证类型 *
        /// </summary>
        public string mdtrt_cert_type { get; set; }
        /// <summary>
        /// 就诊凭证编号  *
        /// </summary>
        public string mdtrt_cert_no { get; set; }
        /// <summary>
        /// 医疗费总额 *
        /// </summary>
        public decimal medfee_sumamt { get; set; }
        /// <summary>
        /// 个人结算方式 *  01 按项目结算  02 按定额结算
        /// </summary>
        public string psn_setlway { get; set; } = "01";
        /// <summary>
        /// 个人账户使用标志 0	否	1	是
        /// </summary>
        public string acct_used_flag { get; set; }
        /// <summary>
        /// 就诊ID * 
        /// </summary>
        public string mdtrt_id { get; set; }
       
        /// <summary>
        /// 险种类型 *
        /// </summary>
        public string insutype { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string invono { get; set; } = "";
        /// <summary>
        /// 中途结算标志 *  0否 1 是
        /// </summary>
        public string mid_setl_flag { get; set; }
        /// <summary>
        /// 全自费金额
        /// </summary>
        public decimal fulamt_ownpay_amt { get; set; }
        /// <summary>
        /// 超限价金额
        /// </summary>
        public decimal overlmt_selfpay { get; set; }
        /// <summary>
        /// 先行自付金额
        /// </summary>
        public decimal preselfpay_amt { get; set; }
        /// <summary>
        /// 符合政策范围金额
        /// </summary>
        public decimal inscp_scp_amt { get; set; }
        /// <summary>
        /// 出院时间  * yyyy-MM-dd
        /// </summary>
        public string dscgTime { get; set; }
        /// <summary>
        /// 就诊凭证识别码
        /// </summary>
        public string mdtrt_cert_sn { get; set; } = "";

        /// <summary>
        /// 病种编码
        /// </summary>
        public string dise_codg { get; set; } = "";
        /// <summary>
        /// 字段扩展
        /// </summary>
        public object expContent { get; set; }
       
    }
}
