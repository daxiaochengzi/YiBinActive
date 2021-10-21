using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai.comm
{
    /// <summary>
    /// 字典查询
    /// </summary>
  public  class QueryData
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 父字典键值
        /// </summary>
        public string parentValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string admdvs { get; set; }
        /// <summary>
        /// 查询日期
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        public string vali_flag { get; set; }
    }
}
