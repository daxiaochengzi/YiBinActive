using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai
{
  public  class YinHaiOutBaseParam
    {
        /// <summary>
        /// 交易状态码
        /// </summary>
        public string infcode { get; set; }
        /// <summary>
        /// 接收方报文 ID 接收方返回，接收方医保区划代码(6)+时间(14)+流水号(10)
        /// 时间格式：yyyyMMddHHmmss
        /// </summary>
        public string inf_refmsgid { get; set; }
        /// <summary>
        /// 接收报文时间
        /// </summary>
        public string refmsg_time { get; set; }
        /// <summary>
        /// 接收报文时间
        /// </summary>
        public string respond_time { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string err_msg { get; set; }
        /// <summary>
        /// 交易输出
        /// </summary>
        public object output { get; set; }
    }
}
