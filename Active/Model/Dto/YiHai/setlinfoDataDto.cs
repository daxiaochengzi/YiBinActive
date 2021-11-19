using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai
{
  

    public class setlinfoDataoutput
    {
      public setlinfoDataDto setlinfo { get; set; }
    }

    public class setlinfoDataDto
    {
        public string fixmedins_code { get; set; }
        public string fixmedins_name { get; set; }
        public string fixmedins_poolarea { get; set; }
    }
}
