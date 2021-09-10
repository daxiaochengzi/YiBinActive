using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai
{
   public class SecureMediaDto
   {
       public string tran_no { get; set; } = "1101";
        public string tran_time { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");

        public SecureMediaDataDto data { get; set; }

    }

    public class SecureMediaDataDto
    {
        public string fixmedins_code { get; set; } = "H51200200049";
        public string psn_no { get; set; } = "51000051200000512099000007";
        public string mdtrtarea_admvs { get; set; } = "512000";
        public string local_type { get; set; } = "1";
        public string out_type { get; set; } = "1";

    }
}
