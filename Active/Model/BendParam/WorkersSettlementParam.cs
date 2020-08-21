using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.BendParam
{
   public class WorkersSettlementParam
    {
       
        /// <summary>
        /// 1 门诊划卡 ;2 住院划卡;
        /// </summary>
        public int MedicalCategory { get; set; }
        /// <summary>
        /// 合计金额
        /// </summary>
        public  decimal AllAmount { get; set; }
        /// <summary>
        /// 卡密码
        /// </summary>

        public  string CardPwd { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>

        public string Operator { get; set; }
    }
}
