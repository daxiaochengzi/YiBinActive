using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai
{
   public class SecureMediaDto
   {
        /// <summary>
        /// 交易编号
        /// </summary>
        public string tran_no { get; set; } = "2304A";
        /// <summary>
        /// 
        /// </summary>
        public string tran_time { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");

        public SecureMediaDataDto data { get; set; }

    }

    public class SecureMediaDataDto
    {
        /// <summary>
        /// 定点医药机构编号
        /// </summary>
        public string fixmedins_code { get; set; } = "H51202100005";
        /// <summary>
        /// 人员编号
        /// </summary>
        public string psn_no { get; set; } = "";
        public string mdtrtarea_admvs { get; set; } = "512000";
        public string local_type { get; set; } = "1";
        public string out_type { get; set; } = "1";

    }
}
