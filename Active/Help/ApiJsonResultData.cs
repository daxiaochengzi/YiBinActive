using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BenDingActive.Help
{
    /// <summary>
    /// http请求返回结果
    /// </summary>
    public class ApiJsonResultData
    {
       

        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>

        public object Data { get; set; }
        /// <summary>
        /// 状态编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 其他信息
        /// </summary>
        public string OtherInfo { get; set; }
    }

}
