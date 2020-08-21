using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.BendParam
{
   public class ReadCardInfoParam
    {
        /// <summary>
        /// 用户密码
        /// </summary>
        public  string CardPwd { get; set; }
        /// <summary>
        /// 0 居民职工 1 异地
        /// </summary>
        public  int InsuranceType { get; set; }
    } 
}
