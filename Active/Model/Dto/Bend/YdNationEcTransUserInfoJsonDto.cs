using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.Bend
{
  public  class YdNationEcTransUserInfoJsonDto
    {
        /// <summary>
        /// 个人编码
        /// </summary>

        [JsonProperty(PropertyName = "AAC001")]
        public string PersonalCoding { get; set; }

        /// <summary>
        /// 行政区域
        /// </summary>

        [JsonProperty(PropertyName = "BAA008")]
        public string AdministrativeArea { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>

        [JsonProperty(PropertyName = "AAC003")]
        public string PatientName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>

        [JsonProperty(PropertyName = "AAC004")]
        public string PatientSex { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>

        [JsonProperty(PropertyName = "AAC002")]
        public string IdCardNo { get; set; }

        /// <summary>
        /// 险种类型310:城镇职工基本医疗保险342：城乡居民基本医疗保险根据获取的险种类型，调用对应的职工或者居民接口办理入院。
        /// </summary>

        [JsonProperty(PropertyName = "AAE140")]
        public string InsuranceType { get; set; }
        /// <summary>
        /// 参保状态
        /// </summary>

        [JsonProperty(PropertyName = "AAC031")]
        public string InsuredState { get; set; }
        /// <summary>
        /// 社区名称
        /// </summary>

        [JsonProperty(PropertyName = "AAB300")]
        public string CommunityName { get; set; }
        /// <summary>
        /// 居民医保账户余额ResidentInsuranceBalance
        /// </summary>


        public decimal ResidentInsuranceBalance { get; set; }
        /// <summary>
        /// 职工医保账户余额 
        /// </summary>


        public decimal WorkersInsuranceBalance { get; set; }
        /// <summary>
        /// 账户余额
        /// </summary>
        [JsonProperty(PropertyName = "AAE240")]
        public decimal AccountBalance { get; set; }
        /// <summary>
        /// 过程返回值(为1时正常，否则不正常)
        /// </summary>
        [JsonProperty(PropertyName = "PO_FHZ")]

        public string ReturnState { get; set; }
        /// <summary>
        /// 系统错误信息
        /// </summary>
        [JsonProperty(PropertyName = "PO_MSG")]

        public string Msg { get; set; }
        /// <summary>
        /// 电子凭证TOKEN
        /// </summary>
        public string ECTOKEN { get; set; }
        /// <summary>
        /// 电子凭证外部索引号
        /// </summary>
        public string ECINDEXNO { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public string IDTYPE { get; set; }
        /// <summary>
        /// 电子凭证二维码值
        /// </summary>
        public string ECQRCODE { get; set; }
    }
}
