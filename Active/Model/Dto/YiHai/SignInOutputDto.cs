using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai
{
   public class SignInOutputDto
    {
        public SignInOutputDataDto signinoutb { get; set; }
    }
    public class SignInOutputDataDto
    {
        /// <summary>
        /// 签到时间
        /// </summary>
        public string sign_time { get; set; }
        /// <summary>
        /// 签到编号
        /// </summary>
        public  string sign_no { get; set; }
    }
    
}
