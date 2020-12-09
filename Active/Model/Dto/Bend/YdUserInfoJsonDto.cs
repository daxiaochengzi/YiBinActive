using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace BenDingActive.Model.Dto.Bend
{
    [XmlRoot("ROW", IsNullable = false)]
    public  class YdUserInfoJsonDto
    { 
        /// <summary>
        /// 个人编码
        /// </summary>
        [XmlElement("po_grbh", IsNullable = false)]
        [JsonProperty(PropertyName = "po_grbh")]
        public string PersonalCoding { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [XmlElement("po_name", IsNullable = false)]
        [JsonProperty(PropertyName = "po_name")]
        public string PatientName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [XmlElement("po_sex", IsNullable = false)]
        [JsonProperty(PropertyName = "po_sex")]
        public string PatientSex { get; set; }
        /// <summary>
        /// 出生日期 Birthday
        /// </summary>
        [XmlElement("po_birthdate", IsNullable = false)]
        [JsonProperty(PropertyName = "po_birthdate")]
        public string Birthday { get; set; }
        /// <summary>
        /// 险种类型310:城镇职工基本医疗保险342：城乡居民基本医疗保险根据获取的险种类型，调用对应的职工或者居民接口办理入院。
        /// </summary>
        [XmlElement("po_xzlx", IsNullable = false)]
        [JsonProperty(PropertyName = "po_xzlx")]
        public string InsuranceType { get; set; }
        
        /// <summary>
        /// 身份证号
        /// </summary>
        [XmlElement("po_sfzhm", IsNullable = false)]
        [JsonProperty(PropertyName = "po_sfzhm")]
        public string IdCardNo { get; set; }
        /// <summary>
        /// 行政区域
        /// </summary>
        [XmlElement("po_xzqh", IsNullable = false)]
        [JsonProperty(PropertyName = "po_xzqh")]
        public string AdministrativeArea { get; set; }
        /// <summary>
        /// 医保账户余额 
        /// </summary>
        [XmlElement("po_acntbalance", IsNullable = false)]
        [JsonProperty(PropertyName = "po_acntbalance")]
        public decimal InsuranceBalance { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        
       [XmlElement("po_cardid", IsNullable = false)]
       [JsonProperty(PropertyName = "po_cardid")]
        public string CardNo { get; set; }
        /// <summary>
        /// 参保状态
        /// </summary>

        [XmlElement("po_bkc121", IsNullable = false)]
        [JsonProperty(PropertyName = "po_bkc121")]
        public string InsuredState { get; set; }
        /// <summary>
        /// 不享受原因
        /// </summary>

        [XmlElement("po_bkc124", IsNullable = false)]
        [JsonProperty(PropertyName = "po_bkc124")]
        public string UnEnjoyRemark { get; set; }

    }
}
