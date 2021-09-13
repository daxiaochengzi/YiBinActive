using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.YiHai.comm
{
   public class InpatientBaseInfoDto
    {
        /// <summary>
        /// 人员编号
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 人员证件类型
        /// </summary>
        public string psn_cert_type { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string certno { get; set; }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string psn_name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>

        public string gend { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string naty { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string brdy { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public string age { get; set; }
        /// <summary>
        /// 字段扩展
        /// </summary>
        public object expContent { get; set; }
        /// <summary>
        /// 参保信息列表
        /// </summary>
        public List<InpatientBaseInsuInfo> insuinfo { get; set; }
    }

    public class InpatientBaseInsuInfo
    {
        /// <summary>
        /// 余额
        /// </summary>
        public decimal balc { get; set; }
        /// <summary>
        /// 险种类型
        /// </summary>
        public string insutype { get; set; }
        /// <summary>
        /// 人员类别
        /// </summary>
        public string psn_type { get; set; }
        /// <summary>
        /// 人员参保状态
        /// </summary>
        public string psn_insu_stas { get; set; }
        /// <summary>
        /// 个人参保日期
        /// </summary>
        public DateTime? psn_insu_date { get; set; }
        /// <summary>
        /// 暂停参保日期
        /// </summary>
        public DateTime? paus_insu_date { get; set; }
        /// <summary>
        /// 公务员标志
        /// </summary>
        public string cvlserv_flag { get; set; }
        /// <summary>
        /// 参保地医保区划
        /// </summary>
        public string insuplc_admdvs { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string emp_name { get; set; }
    }
}
