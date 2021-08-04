using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDingActive.Help;
using BenDingActive.Model;
using BenDingActive.Model.BendParam;
using BenDingActive.Model.Dto;
using BenDingActive.Model.Dto.Bend;
using BenDingActive.Model.Params.Service;
using Newtonsoft.Json;

namespace BenDingActive.Service
{/// <summary>
 /// 居民服务接口
 /// </summary>
    public class HospitalizationService
    {
        #region 居民住院服务
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ResidentUserInfoJsonDto GetUserInfoEntity(HisBaseParam baseParam)
        {

            var data = new ResidentUserInfoJsonDto();
            try
            {//

                Logs.LogWrite(new LogParam()
                {
                    Params = JsonConvert.SerializeObject(baseParam),


                });
                var xmlStr = XmlHelp.SaveXmlEntity(new ResidentUserInfoParam()
                {
                    IdentityMark = baseParam.IdentityMark,
                    AfferentSign = baseParam.AfferentSign
                });
                if (!xmlStr) throw new Exception("获取个人基础资料保存参数出错");
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("医保登陆失败!!!");
                int result = MedicalInsuranceDll.CallService_cxjb("CXJB001");
                if (result == 1)
                {
                    data = XmlHelp.DeSerializerModel(new ResidentUserInfoJsonDto(), true);
                    if (data.ReturnState == "1")
                    {

                        Logs.LogWriteData(new LogWriteDataParam()
                        {
                            JoinJson = JsonConvert.SerializeObject(baseParam),
                            ReturnJson = JsonConvert.SerializeObject(data),
                            OperatorId = baseParam.OperatorId

                        });
                    }
                    else
                    {
                        throw new Exception(data.Msg);
                    }


                }



            }
            catch (Exception e)
            {

                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message + "error:" + e.StackTrace,
                    OperatorCode = baseParam.OperatorId,
                    Params = Logs.ToJson(baseParam),

                    ResultData = Logs.ToJson(data)

                });

            }
            //MedicalInsuranceDll.DisConnectAppServer_cxjb("CXJB001");
            return data;

        }
        /// <summary>
        /// 入院登记
        /// </summary>
        /// <returns></returns>
        public ApiJsonResultData HospitalizationRegister(string param, HisBaseParam baseParam)
        {

            var resultData = MedicalInsuranceOperation(param, baseParam,
                "CXJB002", "入院登记");
            return resultData;

        }
        /// <summary>
        /// 修改入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        public ApiJsonResultData HospitalizationModify(string param, HisBaseParam baseParam)
        {
            var resultData = MedicalInsuranceOperation(param, baseParam,
                "CXJB003", "修改入院登记");
            return resultData;



        }
        /// <summary>
        /// 项目下载
        /// </summary>
        public ApiJsonResultData ProjectDownload(string param, HisBaseParam baseParam)
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
                if (!xmlStr) throw new Exception("项目下载保存参数出错");
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("项目下载执行出错!!!");
                int result = MedicalInsuranceDll.CallService_cxjb("CXJB019");
                if (result == 1)
                {
                    string strXml = XmlHelp.DeSerializerModelStr("ROWDATA");
                    var data = XmlHelp.DeSerializer<ResidentProjectDownloadDto>(strXml);
                    var resultStr = JsonConvert.SerializeObject(data);
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
            MedicalInsuranceDll.DisConnectAppServer_cxjb("CXJB019");
            return resultData;

        }
        /// <summary>
        /// 项目下载总条数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ApiJsonResultData ProjectDownloadCount(string param, HisBaseParam baseParam)
        {
            var resultData = MedicalInsuranceOperation(param, baseParam,
                "CXJB019", "项目下载总条数");
            return resultData;
        }
        /// <summary>
        /// 住院预结算
        /// </summary>
        /// <param name="param"></param>
        public ApiJsonResultData HospitalizationPreSettlement(string param, HisBaseParam baseParam)
        {
            var resultData = MedicalInsuranceOperation(param, baseParam,
                "CXJB009", "居民住院预结算");
            return resultData;
        }
        /// <summary>
        /// 职工取消刷卡
        /// </summary>
        /// <param name="param"></param>
        public ApiJsonResultData WorkerCancelSettlementCard(string param, HisBaseParam baseParam)
        {


            var resultData = MedicalInsuranceOperation(param, baseParam,
             "CXJB027", "账户划卡回退");
            return resultData;
        }
        /// <summary>
        /// 医保出院费用结算
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData LeaveHospitalSettlement(string param, HisBaseParam baseParam)
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
                if (!xmlStr) throw new Exception("居民住院结算保存参数出错!!!");
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("居民住院结算医保执行失败!!!");
                int result = MedicalInsuranceDll.CallService_cxjb("CXJB010");
                if (result == 1)
                {
                    var resultStr = XmlHelp.SerializerModelJson();
                    MedicalInsuranceDll.DisConnectAppServer_cxjb("CXJB010");
                    var userInfo = GetUserInfoEntity(baseParam);
                    Logs.LogWriteData(new LogWriteDataParam()
                    {
                        JoinJson = param,
                        ReturnJson = resultStr,
                        OperatorId = baseParam.OperatorId,
                        TransactionCode = "CXJB010"
                    });
                    resultData.Data = resultStr;
                    resultData.OtherInfo = userInfo.ResidentInsuranceBalance.ToString();

                }
                else
                {
                    XmlHelp.SerializerModelJson();
                }
            }
            catch (Exception e)
            {
                resultData.Success = false;
                resultData.Message = e.Message;
                Logs.LogErrorWrite(new LogParam()
                {
                    Msg = e.Message + "error:" + e.StackTrace,
                    OperatorCode = baseParam.OperatorId,
                    Params = Logs.ToJson(param),
                    ResultData = resultData.Data,
                    TransactionCode = "CXJB010"

                });
            }

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
        private ApiJsonResultData MedicalInsuranceOperation(string param, HisBaseParam baseParam, string code, string tipsMsg)
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
                        OperatorId = baseParam.OperatorId,
                        TransactionCode = code

                    });
                    resultData.Data = resultStr;


                }
                else
                {
                    XmlHelp.SerializerModelJson();
                }
            }
            catch (Exception e)
            {
                resultData.Success = false;
                resultData.Message = e.Message;
                Logs.LogErrorWrite(new LogParam()
                {
                    Msg = e.Message + "error:" + e.StackTrace,
                    OperatorCode = baseParam.OperatorId,
                    Params = Logs.ToJson(param),
                    ResultData = resultData.Data,
                    TransactionCode = code

                });
            }
            MedicalInsuranceDll.DisConnectAppServer_cxjb(code);
            return resultData;
        }
        /// <summary>
        ///查询医保出院结算信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ApiJsonResultData QueryLeaveHospitalSettlement(string param, HisBaseParam baseParam)
        {
            var resultData = MedicalInsuranceOperation(param, baseParam,
                "CXJB012", "查询医保出院结算");
            return resultData;

        }

        /// <summary>
        /// 取消医保出院结算
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData LeaveHospitalSettlementCancel(string param, HisBaseParam baseParam
         )
        {
            var resultData = MedicalInsuranceOperation(param, baseParam,
                "CXJB011", "取消医保出院结算");
            return resultData;

        }
        /// <summary>
        /// 处方数据上传
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData PrescriptionUploadData(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultData { Success = true };
            try
            {
                Logs.LogWrite(new LogParam()
                {
                    Params = param,
                    Msg = JsonConvert.SerializeObject(baseParam)

                });
                var uploadDataParam = JsonConvert.DeserializeObject<PrescriptionUploadParam>(param);
                var xmlStr = XmlHelp.SaveXmlEntity(uploadDataParam);
            
                if (!xmlStr) throw new Exception("处方数据上传保存参数出错!!!");
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("处方数据上传医保执行失败!!!");
                int result = MedicalInsuranceDll.CallService_cxjb("CXJB004");
                if (result == 1)
                {
                    var resultStr = XmlHelp.DeSerializerModel(new PrescriptionUploadDto(), true);

                    var batchConfirmParam = new BatchConfirmParam()
                    {
                        BatchNumber = resultStr.ProjectBatch,
                        ConfirmType = 1,
                        MedicalInsuranceHospitalizationNo = uploadDataParam.MedicalInsuranceHospitalizationNo,
                        Operators = uploadDataParam.Operators
                    };
                    //批次确认
                    XmlHelp.SaveXmlEntity(batchConfirmParam);
                    MedicalInsuranceDll.CallService_cxjb("CXJB007");
                    var iniResult = XmlHelp.DeSerializerModel(new IniDto(), true);
                    Logs.LogWriteData(new LogWriteDataParam()
                    {
                        JoinJson = param,
                        ReturnJson = JsonConvert.SerializeObject(baseParam),
                        OperatorId = baseParam.OperatorId
                    });
                    resultData.Data = resultStr.ProjectBatch;


                }
                else
                {
                    var resultStr = XmlHelp.DeSerializerModel(new PrescriptionUploadDto(), true);
                }
            }
            catch (Exception e)
            {
                resultData.Success = false;
                resultData.Message = e.Message;
                Logs.LogErrorWrite(new LogParam()
                {
                    Msg = e.Message + "error:" + e.StackTrace,
                    OperatorCode = baseParam.OperatorId,
                    Params = Logs.ToJson(param),
                    ResultData = resultData.Data,
                    TransactionCode = "CXJB007"

                });
            }
            MedicalInsuranceDll.DisConnectAppServer_cxjb("CXJB007");

            return resultData;
        }

        /// <summary>
        /// 批次确认
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private ApiJsonResultData BatchConfirm(string param, HisBaseParam baseParam)
        {

            var resultData = MedicalInsuranceOperation(param, baseParam,
             "CXJB007", "批次号确认");
            return resultData;
        }

        /// <summary>
        /// 取消处方数据上传
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData DeletePrescriptionUpload(string param, HisBaseParam baseParam)
        {
            var resultData = MedicalInsuranceOperation(param, baseParam,
                "CXJB005", "取消处方数据上传");
            return resultData;
        }


        #endregion
        #region 职工住院服务

        /// <summary>
        /// 职工入院登记
        /// </summary>
        /// <param name="paramc"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData WorkerHospitalizationRegister(string paramc, HisBaseParam baseParam)
        {
            var resultValue = new ApiJsonResultData { Success = true };
            try
            {
                Logs.LogWrite(new LogParam()
                {
                    Params = paramc,
                    Msg = JsonConvert.SerializeObject(baseParam)

                });
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
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
                var resultStr = JsonConvert.SerializeObject(resultData);
                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = JsonConvert.SerializeObject(resultData),
                    ReturnJson = resultStr,
                    OperatorId = baseParam.OperatorId,
                    TransactionCode = "WorkerHospitalizationRegister"
                });
                resultValue.Data = resultStr;
            }
            catch (Exception e)
            {
                resultValue.Success = false;
                resultValue.Message = e.Message;
                Logs.LogErrorWrite(new LogParam()
                {
                    Msg = e.Message + "error:" + e.StackTrace,
                    OperatorCode = baseParam.OperatorId,
                    Params = Logs.ToJson(paramc),
                    ResultData = resultValue.Data,
                    TransactionCode = "WorkerHospitalizationRegister"

                });
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
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                var param = JsonConvert.DeserializeObject<ModifyWorkerHospitalizationParam>(paramc);
                //返回状态
                var resultState = new byte[1024];
                //消息
                var msg = new byte[1024];

                MedicalInsuranceDll.ModifyHospitalization
                (param.OrganizationCode,
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
                    OperatorId = baseParam.OperatorId,
                    TransactionCode = "ModifyWorkerHospitalization"
                });
                resultValue.Data = "职工住院登记修改成功";
            }
            catch (Exception e)
            {
                resultValue.Success = false;
                resultValue.Message = e.Message;
                Logs.LogErrorWrite(new LogParam()
                {
                    Msg = e.Message + "error:" + e.StackTrace,
                    OperatorCode = baseParam.OperatorId,
                    Params = Logs.ToJson(paramc),
                    ResultData = resultValue.Data,
                    TransactionCode = "ModifyWorkerHospitalization"

                });
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
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
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
                    OperatorId = baseParam.OperatorId,
                    TransactionCode = "WorkerHospitalizationPreSettlement"
                });
                resultValue.Data = JsonConvert.SerializeObject(resultData);
            }
            catch (Exception e)
            {
                resultValue.Success = false;
                resultValue.Message = e.Message;
                Logs.LogErrorWrite(new LogParam()
                {
                    Msg = e.Message + "error:" + e.StackTrace,
                    OperatorCode = baseParam.OperatorId,
                    Params = Logs.ToJson(paramc),
                    ResultData = resultValue.Data,
                    TransactionCode = "WorkerHospitalizationPreSettlement"

                });
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
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
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
                var infoEntity = GetUserInfoEntity(baseParam);
                resultValue.OtherInfo = infoEntity.WorkersInsuranceBalance.ToString();
                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = JsonConvert.SerializeObject(param),
                    ReturnJson = JsonConvert.SerializeObject(resultData),
                    OperatorId = baseParam.OperatorId,
                    TransactionCode = "WorkerHospitalizationSettlement"
                });
                resultValue.Data = JsonConvert.SerializeObject(resultData);
            }
            catch (Exception e)
            {
                resultValue.Success = false;
                resultValue.Message = e.Message;
                Logs.LogErrorWrite(new LogParam()
                {
                    Msg = e.Message + "error:" + e.StackTrace,
                    OperatorCode = baseParam.OperatorId,
                    Params = Logs.ToJson(paramc),
                    ResultData = resultValue.Data,
                    TransactionCode = "WorkerHospitalizationSettlement"

                });
            }
            return resultValue;
        }
        /// <summary>
        /// 职工划卡
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData WorkerCardSettlement(string param, HisBaseParam baseParam)
        {
            var msg = new byte[1024];
            //返回状态
            var resultState = new byte[1024];
            var resultValue = new ApiJsonResultData { Success = true };
            try
            {
                Logs.LogWrite(new LogParam()
                {
                    Params = param,
                    Msg = JsonConvert.SerializeObject(baseParam)
                });
                var iniFile = new IniFile("");
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("医保登陆失败!!!");
                //端口号
                int port = Convert.ToInt16(iniFile.GetIni());
                var paramIni = JsonConvert.DeserializeObject<WorkerHospitalSettlementCardDto>(param);
                ReadCardInfo(paramIni.CardPwd, baseParam);


                //流水号
                var serialNumber = new byte[1024];

                //账户支付
                var accountPayment = new byte[1024];
                //现金支付
                var cashPayment = new byte[1024];

                MedicalInsuranceDll.WorkerHospitalSettlement
                (port,
                    paramIni.CardPwd,
                    paramIni.TotalAmount,
                    paramIni.UseCardType,
                    paramIni.OperatorName,
                    serialNumber,
                    accountPayment,
                    cashPayment,
                    resultState,
                    msg
                );

                if (CommonHelp.StrToTransCoding(resultState) != "1") throw new Exception(CommonHelp.StrToTransCoding(msg));
                var resultData = new WorkerHospitalSettlementCardBackDto()
                {
                    SerialNumber = CommonHelp.StrToTransCoding(serialNumber),
                    AccountPayment = Convert.ToDecimal(CommonHelp.StrToTransCoding(accountPayment)),
                    CashPayment = Convert.ToDecimal(CommonHelp.StrToTransCoding(cashPayment)),
                };
                var infoEntity = GetUserInfoEntity(baseParam);
                resultData.AccountBalance = infoEntity.WorkersInsuranceBalance;
                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = JsonConvert.SerializeObject(param),
                    ReturnJson = JsonConvert.SerializeObject(resultData),
                    OperatorId = baseParam.OperatorId,
                    TransactionCode = "WorkerHospitalSettlementCard"
                });
                resultValue.Data = JsonConvert.SerializeObject(resultData);



            }
            catch (Exception e)
            {

                resultValue.Success = false;
                resultValue.Message = e.Message;
                Logs.LogErrorWrite(new LogParam()
                {
                    Msg = e.Message + "error:" + e.StackTrace,
                    OperatorCode = baseParam.OperatorId,
                    Params = Logs.ToJson(param),
                    ResultData = resultValue.Data,
                    TransactionCode = "WorkerHospitalSettlementCard"

                });
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
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
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

                    OperatorId = baseParam.OperatorId,
                    TransactionCode = "WorkerSettlementCancel"
                });
                resultValue.Data = "取消成功!!!";
                resultValue.OtherInfo = CommonHelp.StrToTransCoding(yearSign);
            }
            catch (Exception e)
            {
                resultValue.Success = false;
                resultValue.Message = e.Message;
                Logs.LogErrorWrite(new LogParam()
                {
                    Msg = e.Message + "error:" + e.StackTrace,
                    OperatorCode = baseParam.OperatorId,
                    Params = Logs.ToJson(paramc),
                    ResultData = resultValue.Data,
                    TransactionCode = "WorkerSettlementCancel"

                });
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

            var resultData = new ApiJsonResultData { Success = true };
            try
            {
                Logs.LogWrite(new LogParam()
                {
                    Params = param,
                    Msg = JsonConvert.SerializeObject(baseParam)

                });
                var xmlStr = XmlHelp.SaveXmlStr(param);
                if (!xmlStr) throw new Exception("职工生育结算保存参数出错!!!");
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("职工生育结算医保执行失败!!!");
                int result = MedicalInsuranceDll.CallService_cxjb("SYBX003");
                if (result == 1)
                {
                    var resultStr = XmlHelp.SerializerModelJson();
                    MedicalInsuranceDll.DisConnectAppServer_cxjb("SYBX003");
                    var userInfo = GetUserInfoEntity(baseParam);
                    Logs.LogWriteData(new LogWriteDataParam()
                    {
                        JoinJson = param,
                        ReturnJson = resultStr,
                        OperatorId = baseParam.OperatorId,
                        TransactionCode = "SYBX003"
                    });
                    resultData.Data = resultStr;
                    resultData.OtherInfo = userInfo.ResidentInsuranceBalance.ToString();

                }
                else
                {
                    XmlHelp.SerializerModelJson();
                }
            }
            catch (Exception e)
            {
                resultData.Success = false;
                resultData.Message = e.Message;
                Logs.LogErrorWrite(new LogParam()
                {
                    Msg = e.Message + "error:" + e.StackTrace,
                    OperatorCode = baseParam.OperatorId,
                    Params = Logs.ToJson(param),
                    ResultData = resultData.Data,
                    TransactionCode = "SYBX003"

                });
            }

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
                        OperatorId = baseParam.OperatorId,
                        TransactionCode = code
                    });
                    resultData.Data = resultStr;


                }
                else
                {
                    XmlHelp.SerializerModelJson();
                }
            }
            catch (Exception e)
            {
                resultData.Success = false;
                resultData.Message = e.Message;
                Logs.LogErrorWrite(new LogParam()
                {
                    Msg = e.Message + "error:" + e.StackTrace,
                    OperatorCode = baseParam.OperatorId,
                    Params = Logs.ToJson(param),
                    ResultData = resultData.Data,
                    TransactionCode = code

                });
            }
            MedicalInsuranceDll.DisConnectAppServer_cxjb(code);
            return resultData;
        }
        #endregion
        #region 电子医保支付
        public ApiJsonResultData ReadCardInfo(string cardPwd, HisBaseParam baseParam)
        {

            var resultData = new ApiJsonResultData { Success = true };
            //工作单位
            var unitName = new byte[1024];
            //姓名
            var patientName = new byte[1024];
            //性别
            var patientSex = new byte[1024];
            var nation = new byte[1024];
            //出生日期
            var birthDay = new byte[1024];
            //身份证号
            var idCardNo = new byte[1024];
            //联系地址
            var birthPlace = new byte[1024];
            //医保账户余额
            var insuranceBalance = new byte[1024];
            //职工卡号
            var workersCardNo = new byte[1024];
            //返回状态
            var resultState = new byte[1024];
            //消息
            var msg = new byte[1024];

            try
            {
                var userData = new GetResidentUserInfoDto();

                //需要登陆
                //if (paramStr == "1")
                //{
                //    var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                //    if (loginData != 1) throw new Exception("医保登陆失败!!!");
                //}
                //var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                //if (loginData != 1) throw new Exception("医保登陆失败!!!");
                var iniFile = new IniFile("");
                //端口号
                int port = Convert.ToInt16(iniFile.GetIni());
                var readCardData = MedicalInsuranceDll.WorkerReadCardInfo(
                    port,
                    cardPwd,
                    unitName,
                    workersCardNo,
                    idCardNo,
                    patientName,
                    patientSex,
                    nation,
                    birthPlace,
                    birthDay,
                    insuranceBalance,
                    resultState,
                    msg
                    );

                if (CommonHelp.StrToTransCoding(resultState) != "1") throw new Exception(CommonHelp.StrToTransCoding(msg));


                userData = new GetResidentUserInfoDto()
                {
                    PO_XM = CommonHelp.StrToTransCoding(patientName),
                    PO_XB = CommonHelp.StrToTransCoding(patientSex),
                    PO_LXDZ = CommonHelp.StrToTransCoding(birthPlace),
                    PO_ZGZHYE = CommonHelp.StrToTransCoding(insuranceBalance),
                    PO_SFZH = CommonHelp.StrToTransCoding(idCardNo)

                };
                resultData.Data = JsonConvert.SerializeObject(userData);
                //userData = XmlHelp.DeSerializerModel(new Model.Dto.GetResidentUserInfoDto(), true);
                //数据日志存入

                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = "",
                    ReturnJson = JsonConvert.SerializeObject(userData),
                    OperatorId = baseParam.OperatorId
                });
            }
            catch (Exception e)
            {
                resultData.Success = false;
                resultData.Message = e.Message;
                Logs.LogErrorWrite(new LogParam()
                {
                    Msg = e.Message ,
                    OperatorCode = baseParam.OperatorId,
                    TransactionCode = "resultData"

                });
                throw new Exception(e.Message);
            }

            return resultData;

        }
        #endregion

    }
}
