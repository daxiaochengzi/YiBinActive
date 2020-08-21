using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDingActive.Model.Dto.Bend
{
   public class ReadCardUserInfoJson
    {    /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string PatientSex { get; set; }
        /// <summary>
        /// 出生日期 Birthday
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 险种类型310:城镇职工基本医疗保险342：城乡居民基本医疗保险根据获取的险种类型，调用对应的职工或者居民接口办理入院。
        /// </summary>
        public string InsuranceType { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCardNo { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string ContactAddress { get; set; }
        /// <summary>
        /// 职工医保账户余额 
        /// </summary>
        public decimal WorkersInsuranceBalance { get; set; }
        /// <summary>
        /// 职工卡号
        /// </summary>
        public  string WorkersCardNo { get; set; }
    }
}
