using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai
{
   public class VisitInformationQuerDto
    {
        /// <summary>
        /// 个人编码
        /// </summary>
        public string psn_no { get; set; } 
        /// <summary>
        /// 开始时间
        /// </summary>
        public string begntime { get; set; } 
        /// <summary>
        ///结束时间
        /// </summary>
        public string endtime { get; set; } = "2021-10-27 18:57:18.000";
        /// <summary>
        /// 医疗类别
        /// </summary>
        public string med_type { get; set; } = "21";
        
    }
}
