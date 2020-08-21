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
    public class PrescriptionUploadDto : IniDto
    {/// <summary>
        /// 批次号
        /// </summary>
        [JsonProperty(PropertyName = "PO_PCH")]
        public string ProjectBatch { get; set; }
    }
}
