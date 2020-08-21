using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDingActive.Model.BendParam
{
    [XmlRoot("ROW", IsNullable = false)]
    public class ResidentUserInfoParam
    {
        /// <summary>
        /// 身份标志身份证号或个人编号
        /// </summary>
       
        [XmlElement("PI_SFBZ", IsNullable = false)]
        public string IdentityMark { get; set; }
        /// <summary>
        /// 身份标志 1为公民身份号码 2为个人编号
        /// </summary>
       
        [XmlElementAttribute("PI_CRBZ", IsNullable = false)]
        public string AfferentSign { get; set; }


    }
}
