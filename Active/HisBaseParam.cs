using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model
{
  public  class HisBaseParam
    {/// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 操作人员id
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 医保类别
        /// </summary>
        public string InsuranceType { get; set; }
        /// <summary>
        /// 身份标志   身份证号或个人编号
        /// </summary>
        public string IdentityMark { get; set; }
        /// <summary>
        /// 传入标志 1为公民身份号码 2为个人编号
        /// </summary>
        public string AfferentSign { get; set; }
        
    }
}
