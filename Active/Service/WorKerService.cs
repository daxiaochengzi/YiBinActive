using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDingActive.Help;
using BenDingActive.Model;
using BenDingActive.Model.Dto.Bend;
using BenDingActive.Model.Params.Service;
using Newtonsoft.Json;

namespace BenDingActive.Service
{
  public  class WorKerService
    {
       /// <summary>
       /// 职工入院登记
       /// </summary>
       /// <param name="paramc"></param>
       /// <param name="baseParam"></param>
       /// <returns></returns>
        public  ApiJsonResultData WorkerHospitalizationRegister(string paramc, HisBaseParam baseParam)
        {
            var resultValue = new ApiJsonResultData { Success = true };
            try
            {
                Logs.LogWrite(new LogParam()
                {
                    Params = paramc,
                    Msg = JsonConvert.SerializeObject(baseParam)

                });
                var param = JsonConvert.DeserializeObject<WorKerHospitalizationRegisterParam>(paramc);
               WorkerHospitalizationRegisterDto resultData;
                //社保住院号
                var medicalInsuranceHospitalizationNo = new byte[1024];
                //审批编号
                var approvalNumber = new byte[1024];
                //年住院次数
                var yearHospitalizationNumber = new byte[1024];
                //统筹已付金额
                var overallPlanningAlreadyAmount = new byte[1024];
                //统筹可付金额
                var overallPlanningCanAmount = new byte[1024];
                //返回状态
                var resultState = new byte[1024];
                //消息
                var msg = new byte[1024];
                //  param.AdministrativeArea,
                MedicalInsuranceDll.HospitalizationRegister
                    (param.IdentityMark,
                    param.AfferentSign,
                    param.AdministrativeArea,
                    param.OrganizationCode,
                    param.MedicalCategory,
                    param.AdmissionDate,
                    param.AdmissionMainDiagnosisIcd10,
                    param.DiagnosisIcd10Two,
                    param.DiagnosisIcd10Three,
                    param.AdmissionMainDiagnosis,
                    param.InpatientArea,
                    param.BedNumber,
                    param.HospitalizationNo,
                    param.Operators,
                    medicalInsuranceHospitalizationNo,
                    approvalNumber,
                    yearHospitalizationNumber,
                    overallPlanningAlreadyAmount,
                    overallPlanningCanAmount,
                    resultState,
                    msg
                    );
                if (CommonHelp.StrToTransCoding(resultState) != "1") throw new Exception(CommonHelp.StrToTransCoding(msg));
                resultData = new WorkerHospitalizationRegisterDto()
                {
                    MedicalInsuranceHospitalizationNo = CommonHelp.StrToTransCoding(medicalInsuranceHospitalizationNo),
                    ApprovalNumber = CommonHelp.StrToTransCoding(approvalNumber),
                    YearHospitalizationNumber = CommonHelp.StrToTransCoding(yearHospitalizationNumber),
                    OverallPlanningAlreadyAmount = CommonHelp.StrToTransCoding(overallPlanningAlreadyAmount),
                    OverallPlanningCanAmount = CommonHelp.StrToTransCoding(overallPlanningCanAmount),
                };
                    var resultStr = XmlHelp.SerializerModelJson();
                    Logs.LogWriteData(new LogWriteDataParam()
                    {
                        JoinJson = JsonConvert.SerializeObject(resultData),
                        ReturnJson = resultStr,
                        OperatorId = baseParam.OperatorId
                    });
                 resultValue.Data = resultStr;
            }
            catch (Exception e)
            {
                resultValue.Success = false;
                resultValue.Message = e.Message;

            }
            return resultValue;
        }
        /// <summary>
        /// 职工入院登记修改
        /// </summary>
        /// <param name="paramc"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData ModifyWorkerHospitalization(string paramc, HisBaseParam baseParam)
        {
            var resultValue = new ApiJsonResultData { Success = true };
            try
            {
                Logs.LogWrite(new LogParam()
                {
                    Params = paramc,
                    Msg = JsonConvert.SerializeObject(baseParam)

                });
                var param = JsonConvert.DeserializeObject<ModifyWorkerHospitalizationParam>(paramc);
                //返回状态
                var resultState = new byte[1024];
                //消息
                var msg = new byte[1024];

                MedicalInsuranceDll.ModifyHospitalization
                (   param.OrganizationCode,
                    param.MedicalInsuranceHospitalizationNo,
                    param.AdministrativeArea,
                    param.AdmissionDate,
                    param.AdmissionMainDiagnosisIcd10,
                    param.DiagnosisIcd10Two,
                    param.DiagnosisIcd10Three,
                    param.AdmissionMainDiagnosis,
                    param.InpatientArea,
                    param.BedNumber,
                    param.HospitalizationNo,
                    resultState,
                    msg
                );
                if (CommonHelp.StrToTransCoding(resultState) != "1") throw new Exception(CommonHelp.StrToTransCoding(msg));
               
             
                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = JsonConvert.SerializeObject(param),
                    ReturnJson = "职工住院登记修改成功",
                    OperatorId = baseParam.OperatorId
                });
                resultValue.Data =  "职工住院登记修改成功";
            }
            catch (Exception e)
            {
                resultValue.Success = false;
                resultValue.Message = e.Message;

            }
            return resultValue;
        }
        /// <summary>
        /// 职工预结算
        /// </summary>
        /// <param name="paramc"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData WorkerHospitalizationPreSettlement(string paramc, HisBaseParam baseParam)
        {
            var resultValue = new ApiJsonResultData { Success = true };
            try
            {
                Logs.LogWrite(new LogParam()
                {
                    Params = paramc,
                    Msg = JsonConvert.SerializeObject(baseParam)

                });
                var param = JsonConvert.DeserializeObject<WorkerHospitalizationPreSettlementParam>(paramc);
                //社保住院号
                var documentNo = new byte[1024];
                //发生费用金额
                var totalAmount = new byte[1024];
                //基本统筹支付
                var basicOverallPay = new byte[1024];
                //补充医疗保险支付金额
                var supplementPayAmount = new byte[1024];
                //专项基金支付金额
                var specialFundPayAmount = new byte[1024];
                //公务员补贴
                var civilServantsSubsidies = new byte[1024];
                //公务员补助
                var civilServantsSubsidy = new byte[1024];
                //其它支付金额
                var otherPaymentAmount = new byte[1024];
                //账户支付
                var accountPayment = new byte[1024];
                //现金支付
                var cashPayment = new byte[1024];
                //起付金额
                var paidAmount = new byte[1024];
                // 备注
                var remark = new byte[1024];
                //返回状态
                var resultState = new byte[1024];
                //消息
                var msg = new byte[1024];
                MedicalInsuranceDll.WorkerHospitalizationPreSettlement
                    (param.OrganizationCode,
                     param.MedicalInsuranceHospitalizationNo,
                     param.AdministrativeArea,
                     param.IsHospitalizationFrequency,
                     param.Operators,
                     param.LeaveHospitalDate,
                     totalAmount,
                     basicOverallPay,
                     supplementPayAmount,
                     specialFundPayAmount,
                     civilServantsSubsidies,
                     civilServantsSubsidy,
                     otherPaymentAmount,
                     accountPayment,
                     cashPayment,
                     paidAmount,
                     documentNo,
                     remark,
                     resultState,
                     msg
                    );
                if (CommonHelp.StrToTransCoding(resultState) != "1") throw new Exception(CommonHelp.StrToTransCoding(msg));

                var resultData = new WorkerHospitalizationPreSettlementDto()
                {
                    DocumentNo = CommonHelp.StrToTransCoding(documentNo),
                    TotalAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(totalAmount)),
                    BasicOverallPay = Convert.ToDecimal(CommonHelp.StrToTransCoding(basicOverallPay)),
                    SupplementPayAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(supplementPayAmount)),
                    SpecialFundPayAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(specialFundPayAmount)),
                    CivilServantsSubsidies = Convert.ToDecimal(CommonHelp.StrToTransCoding(civilServantsSubsidies)),
                    CivilServantsSubsidy = Convert.ToDecimal(CommonHelp.StrToTransCoding(civilServantsSubsidy)),
                    OtherPaymentAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(otherPaymentAmount)),
                    AccountPayment = Convert.ToDecimal(CommonHelp.StrToTransCoding(accountPayment)),
                    CashPayment = Convert.ToDecimal(CommonHelp.StrToTransCoding(cashPayment)),
                    PaidAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(paidAmount)),
                    Remark = CommonHelp.StrToTransCoding(cashPayment),
                };

             
                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = JsonConvert.SerializeObject(param),
                    ReturnJson = JsonConvert.SerializeObject(resultData),
                    OperatorId = baseParam.OperatorId
                });
                resultValue.Data = JsonConvert.SerializeObject(resultData);
            }
            catch (Exception e)
            {
                resultValue.Success = false;
                resultValue.Message = e.Message;

            }
            return resultValue;
        }
        /// <summary>
        /// 职工结算
        /// </summary>
        /// <param name="paramc"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData WorkerHospitalizationSettlement(string paramc, HisBaseParam baseParam)
        {
            var resultValue = new ApiJsonResultData { Success = true };
            try
            {
                Logs.LogWrite(new LogParam()
                {
                    Params = paramc,
                    Msg = JsonConvert.SerializeObject(baseParam)

                });
                var param = JsonConvert.DeserializeObject<WorkerHospitalizationSettlementParam>(paramc);

                //社保住院号
                var documentNo = new byte[1024];
                //发生费用金额
                var totalAmount = new byte[1024];
                //基本统筹支付
                var basicOverallPay = new byte[1024];
                //补充医疗保险支付金额
                var supplementPayAmount = new byte[1024];
                //专项基金支付金额
                var specialFundPayAmount = new byte[1024];
                //公务员补贴
                var civilServantsSubsidies = new byte[1024];
                //公务员补助
                var civilServantsSubsidy = new byte[1024];
                //其它支付金额
                var otherPaymentAmount = new byte[1024];
                //账户支付
                var accountPayment = new byte[1024];
                //现金支付
                var cashPayment = new byte[1024];
                //起付金额
                var paidAmount = new byte[1024];
                // 备注
                var remark = new byte[1024];
                //返回状态
                var resultState = new byte[1024];
                //消息
                var msg = new byte[1024];
                MedicalInsuranceDll.WorkerHospitalizationSettlement
                    (param.OrganizationCode,
                     param.MedicalInsuranceHospitalizationNo,
                     param.AdministrativeArea,
                     param.IsHospitalizationFrequency,
                     param.Operators,
                     param.LeaveHospitalDate,
                     param.LeaveHospitalState,
                     param.AdmissionMainDiagnosisIcd10,
                     param.DiagnosisIcd10Two,
                     param.DiagnosisIcd10Three,
                     param.LeaveHospitalMainDiagnosis,
                     totalAmount,
                     basicOverallPay,
                     supplementPayAmount,
                     specialFundPayAmount,
                     civilServantsSubsidies,
                     civilServantsSubsidy,
                     otherPaymentAmount,
                     accountPayment,
                     cashPayment,
                     paidAmount,
                     documentNo,
                     remark,
                     resultState,
                     msg
                    );
                if (CommonHelp.StrToTransCoding(resultState) != "1") throw new Exception(CommonHelp.StrToTransCoding(msg));
                var resultData = new WorkerHospitalizationPreSettlementDto()
                {
                    DocumentNo = CommonHelp.StrToTransCoding(documentNo),
                    TotalAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(totalAmount)),
                    BasicOverallPay = Convert.ToDecimal(CommonHelp.StrToTransCoding(basicOverallPay)),
                    SupplementPayAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(supplementPayAmount)),
                    SpecialFundPayAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(specialFundPayAmount)),
                    CivilServantsSubsidies = Convert.ToDecimal(CommonHelp.StrToTransCoding(civilServantsSubsidies)),
                    CivilServantsSubsidy = Convert.ToDecimal(CommonHelp.StrToTransCoding(civilServantsSubsidy)),
                    OtherPaymentAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(otherPaymentAmount)),
                    AccountPayment = Convert.ToDecimal(CommonHelp.StrToTransCoding(accountPayment)),
                    CashPayment = Convert.ToDecimal(CommonHelp.StrToTransCoding(cashPayment)),
                    PaidAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(paidAmount)),
                    Remark = CommonHelp.StrToTransCoding(cashPayment),
                };

                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = JsonConvert.SerializeObject(param),
                    ReturnJson = JsonConvert.SerializeObject(resultData),
                    OperatorId = baseParam.OperatorId
                });
                resultValue.Data = JsonConvert.SerializeObject(resultData);
            }
            catch (Exception e)
            {
                resultValue.Success = false;
                resultValue.Message = e.Message;

            }
            return resultValue;
        }

        /// <summary>
        /// 职工取消结算
        /// </summary>
        /// <param name="paramc"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData WorkerSettlementCancel(string paramc, HisBaseParam baseParam)
        {
            var resultValue = new ApiJsonResultData { Success = true };
            try
            {
                Logs.LogWrite(new LogParam()
                {
                    Params = paramc,
                    Msg = JsonConvert.SerializeObject(baseParam)

                });
                var param = JsonConvert.DeserializeObject<WorkerSettlementCancelParam>(paramc);

                //返回状态
                var resultState = new byte[1024];
                //消息
                var msg = new byte[1024];
                var yearSign = new byte[1024];
                MedicalInsuranceDll.WorkerSettlementCancel
                (param.AdministrativeArea,
                    param.OrganizationCode,
                    param.MedicalInsuranceHospitalizationNo,
                    param.SettlementNo,
                    param.CancelLimit,
                    param.User.UserName,
                    yearSign,
                    resultState,
                    msg
                );
                if (CommonHelp.StrToTransCoding(resultState) != "1")
                {
                    throw new Exception(CommonHelp.StrToTransCoding(msg));
                }

                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = JsonConvert.SerializeObject(param),
                  
                    OperatorId = baseParam.OperatorId
                });
                resultValue.Data = "取消成功!!!";
            }
            catch (Exception e)
            {
                resultValue.Success = false;
                resultValue.Message = e.Message;

            }
            return resultValue;
        }
        /// <summary>
        /// 职工生育入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData WorkerBirthHospitalizationRegister(string param, HisBaseParam baseParam)
        {
            var resultData = WorkerMedicalInsuranceOperation(param, baseParam,
                "SYBX001", "职工生育入院登记");
            return resultData;
        }
        /// <summary>
        /// 职工生育预结算
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData WorkerBirthPreSettlement(string param, HisBaseParam baseParam)
        {
            var resultData = WorkerMedicalInsuranceOperation(param, baseParam,
                "SYBX002", "职工生育预结算");
            return resultData;
        }
        /// <summary>
        /// 职工生育结算
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData WorkerBirthSettlement(string param, HisBaseParam baseParam)
        {
            var resultData = WorkerMedicalInsuranceOperation(param, baseParam,
                "SYBX003", "职工生育结算");
            return resultData;
        }
        /// <summary>
        /// 医保操作
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <param name="code">医保编码</param>
        /// <param name="tipsMsg">提示信息</param>
        /// <returns></returns>
        private ApiJsonResultData WorkerMedicalInsuranceOperation(string param, HisBaseParam baseParam, string code, string tipsMsg)
        {
            var resultData = new ApiJsonResultData { Success = true };
            try
            {
                Logs.LogWrite(new LogParam()
                {
                    Params = param,
                    Msg = JsonConvert.SerializeObject(baseParam)

                });
                var xmlStr = XmlHelp.SaveXmlStr(param);
                if (!xmlStr) throw new Exception(tipsMsg + "保存参数出错!!!");
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception(tipsMsg + "医保执行失败!!!");
                int result = MedicalInsuranceDll.CallService_cxjb(code);
                if (result == 1)
                {
                    var resultStr = XmlHelp.SerializerModelJson();
                    Logs.LogWriteData(new LogWriteDataParam()
                    {
                        JoinJson = param,
                        ReturnJson = resultStr,
                        OperatorId = baseParam.OperatorId
                    });
                    resultData.Data = resultStr;


                }
            }
            catch (Exception e)
            {
                resultData.Success = false;
                resultData.Message = e.Message;

            }
            return resultData;
        }

    }

}
