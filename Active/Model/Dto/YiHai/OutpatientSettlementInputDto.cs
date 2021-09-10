using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai
{
    public class OutpatientSettlementInputDto
    {
        public OutpatientSettlementInputDataDto data { get; set; }
    }
    public class OutpatientSettlementInputDataDto
    {

        /// <summary>
        /// 人员编号 *
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 就诊凭证类型 *
        /// </summary>
        public string mdtrt_cert_type { get; set; }
        /// <summary>
        /// 就诊凭证编号 *
        /// </summary>
        public string mdtrt_cert_no { get; set; }
        /// <summary>
        /// 医疗类别 Y
        /// </summary>
        public string med_type { get; set; }
        /// <summary>
        /// 医疗费总额 2位小数
        /// </summary>
        public decimal medfee_sumamt { get; set; }
        /// <summary>
        /// 个人结算方式 y * 01	按项目结算	02	按定额结算
        /// </summary>
        public string psn_setlway { get; set; }
        /// <summary>
        /// 就诊 ID *
        /// </summary>
        public string mdtrt_id { get; set; }
        /// <summary>
        /// 收费批次号 *
        /// </summary>
        public string chrg_bchno { get; set; }
        /// <summary>
        /// 个人账户使用标志  y *
        /// </summary>
        public string acct_used_flag { get; set; }
        /// <summary>
        /// 险种类型 *
        /// </summary>
        public string insutype { get; set; }
        /// <summary>
        /// 字段扩展
        /// </summary>
        public string expContent { get; set; } = "";
        /// <summary>
        /// 发票号
        /// </summary>
        public string invono { get; set; }

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
        /// 校验介质
        /// </summary>

        public string card_token { get; set; }
    }
}
