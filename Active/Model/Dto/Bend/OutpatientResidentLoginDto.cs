using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDingActive.Model.Dto.Bend
{
    [XmlRoot("ROW", IsNullable = false)]
    public class OutpatientResidentLoginDto
    {/// <summary>
    /// 机构编号
    /// </summary>
        [XmlElement("PO_AAZ107", IsNullable = false)]
        public string OrganizationCode { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        [XmlElement("PO_CKB519", IsNullable = false)]
        public string OrganizationName { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        [XmlElement("PO_OPERID", IsNullable = false)]
        public string OperatorCode { get; set; }
        /// <summary>
        /// 操作员姓名
        /// </summary>
        [XmlElement("PO_OPERNAME", IsNullable = false)]
        public string OperatorName { get; set; }
        /// <summary>
        /// 行政区域
        /// </summary>
        [XmlElement("PO_BAE001", IsNullable = false)]
  
        public string Area { get; set; }
        /// <summary>
        /// 上级医院编码
        /// </summary>
        [XmlElement("PO_CKZ545", IsNullable = false)]

        public string SuperiorOrganizationCode { get; set; }
        /// <summary>
        /// 上级医院名称
        /// </summary>
        [XmlElement("PO_AKB021", IsNullable = false)]

        public string SuperiorOrganizationName { get; set; }

       
    }
}
