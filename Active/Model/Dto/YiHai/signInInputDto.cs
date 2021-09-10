using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai
{
   public class SignInInputDto
    {
        public SignInInputDataDto signIn { get; set; }
    }
    public class SignInInputDataDto
    {
        /// <summary>
        /// 
        /// </summary>
       public  string opter_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mac { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ip { get; set; }
    }
}
