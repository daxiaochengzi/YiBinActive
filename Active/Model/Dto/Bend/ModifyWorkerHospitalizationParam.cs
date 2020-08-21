using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.Bend
{
   public class ModifyWorkerHospitalizationParam
    {/// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 入院日期(格式为YYYYMMDD)
        /// </summary>
        public string AdmissionDate { get; set; }
        /// <summary>
        /// 入院主要诊断疾病ICD-10编码
        /// </summary>
        public string AdmissionMainDiagnosisIcd10 { get; set; }
        /// <summary>
        /// 入院诊断疾病ICD-10编码
        /// </summary>

        public string DiagnosisIcd10Two { get; set; }
        /// <summary>
        /// 入院诊断疾病ICD-10编码three
        /// </summary>

        public string DiagnosisIcd10Three { get; set; }
        /// <summary>
        /// 入院诊断
        /// </summary>

        public string AdmissionMainDiagnosis { get; set; }
        /// <summary>
        /// 病区
        /// </summary>
        public string InpatientArea { get; set; }
        /// <summary>
        /// 科室编号
        /// </summary>
        public string InpatientDepartmentCode { get; set; }

        /// <summary>
        /// 床位号
        /// </summary>

        public string BedNumber { get; set; }
        /// <summary>
        /// 医院住院号
        /// </summary>

        public string HospitalizationNo { get; set; }

        /// <summary>
        /// 经办人
        /// </summary>
        public string Operators { get; set; }
        /// <summary>
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
    }
}
