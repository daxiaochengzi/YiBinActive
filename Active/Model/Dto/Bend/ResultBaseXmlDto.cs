using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDingActive.Model.Dto.Bend
{
    [XmlRoot("ROW", IsNullable = false)]
    public class ResultBaseXmlDto
    {
        [XmlElementAttribute("PO_FHZ", IsNullable = false)]
        public string ReturnState { get; set; }

        [XmlElementAttribute("PO_MSG", IsNullable = false)]
        public string ReturnMsg { get; set; }
    }
}
