using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDingActive.Model.Dto;
using BenDingActive.Model.Dto.Bend;

namespace BenDingActive.Model.Params.Service
{
   public class WorkerBaseParam
    {/// <summary>
        /// 医疗机构号
        /// </summary>
        public string OrganizationCode { get; set; }
        /// <summary>
        /// 医保住院号
        /// </summary>
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 行政区域
        /// </summary>
        public string AdministrativeArea { get; set; }

        public UserInfoDto User { get; set; }


    }
}
