using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BenDingActive.Help;
using BenDingActive.Model;
using BenDingActive.Model.BendParam;
using BenDingActive.Model.Dto;
using BenDingActive.Model.Dto.Bend;
using BenDingActive.Model.Dto.OutpatientDepartment;
using BenDingActive.Model.Params;
using BenDingActive.Model.Params.OutpatientDepartment;
using BenDingActive.ServiceReferenceMedicalInsurance;
using Newtonsoft.Json;

namespace BenDingActive.Service
{/// <summary>
 /// 居民医保接口
 /// </summary>
    public class OutpatientDepartmentService
    {
        /// <summary>
        /// 获取个人基础资料
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        public ApiJsonResultData GetUserInfo(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultData { Success = true };
            var data = new ResidentUserInfoJsonDto();
            try
            {//

                var userInfoParam = new ResidentUserInfoParam()
                {
                    AfferentSign = baseParam.AfferentSign,
                    IdentityMark = baseParam.IdentityMark
                };
                Logs.LogWrite(new LogParam()
                {
                    Params = JsonConvert.SerializeObject(userInfoParam),
                    Msg = JsonConvert.SerializeObject(baseParam)

                });
                var xmlStr = XmlHelp.SaveXmlEntity(userInfoParam);
                if (!xmlStr) throw new Exception("获取个人基础资料保存参数出错");

                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("医保登陆失败!!!");
                int result = MedicalInsuranceDll.CallService_cxjb("CXJB001");

                if (result == 1)
                {
                    data = XmlHelp.DeSerializerModel(new ResidentUserInfoJsonDto(), true);
                    if (data.ReturnState == "1")
                    {
                        var userInfoDto = UserInfoToDto(data);
                        resultData.Data = JsonConvert.SerializeObject(userInfoDto);
                        Logs.LogWrite(new LogParam()
                        {
                            Params = JsonConvert.SerializeObject(param),
                            OperatorCode = baseParam.OperatorId,
                            ResultData = JsonConvert.SerializeObject(userInfoDto),

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
                resultData.Success = false;
                resultData.Message = e.Message;
                Logs.LogErrorWrite(new LogParam()
                {
                    Msg = e.Message + "error:" + e.StackTrace,
                    OperatorCode = baseParam.OperatorId,
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data),
                    TransactionCode = "CXJB001"

                });


            }
            MedicalInsuranceDll.DisConnectAppServer_cxjb("CXJB001");
            return resultData;

        }
        /// <summary>
        /// 读卡获取个人基础资料
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        public ApiJsonResultData ReadCardUserInfo(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultData { Success = true };
            var data = new ResidentUserInfoJsonDto();
            try
            {
                var iniFile = new IniFile("");
                //端口号
                var port = iniFile.GetIni();
                Logs.LogWrite(new LogParam()
                {
                    Params = param,
                    Msg = JsonConvert.SerializeObject(baseParam)
                });
                if (!string.IsNullOrWhiteSpace(param) == false) throw new Exception("密码不能为空!!!");
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("医保登陆失败!!!");
                //int result = MedicalInsuranceDll.ReadCardInfo_cxjb(port, param);
                //if (result == 1)
                //{
                //    data = XmlHelp.DeSerializerModel(new ResidentUserInfoJsonDto(), true);
                //    if (data.ReturnState == "1")
                //    {
                //        var userInfoDto = UserInfoToDto(data);
                //        resultData.Data = JsonConvert.SerializeObject(userInfoDto);
                //        Logs.LogWriteData(new LogWriteDataParam()
                //        {
                //            JoinJson = JsonConvert.SerializeObject(param),
                //            ReturnJson = JsonConvert.SerializeObject(userInfoDto),
                //            OperatorId = baseParam.OperatorId
                //        });
                //    }
                //    else
                //    {
                //        throw new Exception(data.Msg);
                //    }


                //}



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
                    ResultData = Logs.ToJson(data),
                    TransactionCode = "ReadCard"
                });

            }
            return resultData;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ResidentUserInfoJsonDto GetUserInfoEntity(string param, HisBaseParam baseParam)
        {

            var data = new ResidentUserInfoJsonDto();
            try
            {//
                var userInfoParam = new ResidentUserInfoParam()
                {
                    AfferentSign = baseParam.AfferentSign,
                    IdentityMark = baseParam.IdentityMark
                };
                Logs.LogWrite(new LogParam()
                {
                    Params = JsonConvert.SerializeObject(userInfoParam),
                    Msg = JsonConvert.SerializeObject(baseParam)

                });
                var xmlStr = XmlHelp.SaveXmlEntity(userInfoParam);
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
                            JoinJson = JsonConvert.SerializeObject(param),
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

                Logs.LogErrorWrite(new LogParam()
                {
                    Msg = e.Message + "error:" + e.StackTrace,
                    OperatorCode = baseParam.OperatorId,
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data),
                    TransactionCode = "CXJB001"

                });

            }
            MedicalInsuranceDll.DisConnectAppServer_cxjb("CXJB001");
            return data;

        }
        /// <summary>
        /// 普通门诊结算
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData OutpatientDepartmentCostInput(string param, HisBaseParam baseParam)
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
                if (!xmlStr) throw new Exception("保存门诊结算参数出错");
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("医保登陆失败!!!");
                int result = MedicalInsuranceDll.CallService_cxjb("TPYP301");
                if (result == 1)
                {

                    var resultStr = XmlHelp.SerializerModelJson();
                    Logs.LogWriteData(new LogWriteDataParam()
                    {
                        JoinJson = param,
                        ReturnJson = resultStr,
                        OperatorId = baseParam.OperatorId,
                        TransactionCode = "TPYP301"
                    });
                    resultData.Data = resultStr;
                    MedicalInsuranceDll.DisConnectAppServer_cxjb("TPYP301");
                    //获取用余额
                    var userInfo = GetUserInfoEntity("", baseParam);
                    resultData.OtherInfo = userInfo.InsuranceType == "310" ? userInfo.WorkersInsuranceBalance.ToString(CultureInfo.InvariantCulture)
                        : userInfo.ResidentInsuranceBalance.ToString(CultureInfo.InvariantCulture);


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
                    TransactionCode = "TPYP301"

                });

            }

            return resultData;
        }
        /// <summary>
        /// 门诊费取消
        /// </summary>
        public ApiJsonResultData CancelOutpatientDepartmentCost(string param, HisBaseParam baseParam)
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
                if (!xmlStr) throw new Exception("门诊费取消保存参数出错");
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("医保登陆失败!!!");
                int result = MedicalInsuranceDll.CallService_cxjb("TPYP302");

                if (result == 1)
                {
                    var data = XmlHelp.DeSerializerModel(new IniDto(), true);
                    var resultStr = JsonConvert.SerializeObject(data);
                    Logs.LogWriteData(new LogWriteDataParam()
                    {
                        JoinJson = param,
                        ReturnJson = resultStr,
                        OperatorId = baseParam.OperatorId,
                        TransactionCode = "TPYP302"
                    });
                    resultData.Data = resultStr;
                }
                else
                {
                    var data = XmlHelp.DeSerializerModel(new IniDto(), true);
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
                    TransactionCode = "TPYP302"

                });
            }
            MedicalInsuranceDll.DisConnectAppServer_cxjb("TPYP302");
            return resultData;
        }
        /// <summary>
        /// 门诊计划生育预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ApiJsonResultData OutpatientPlanBirthPreSettlement(string param, HisBaseParam baseParam)
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
                if (!xmlStr) throw new Exception("门诊计划生育预结算保存参数出错");
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("医保登陆失败!!!");
                int result = MedicalInsuranceDll.CallService_cxjb("SYBX004");

                if (result == 1)
                {
                    var resultStr = XmlHelp.SerializerModelJson();

                    Logs.LogWriteData(new LogWriteDataParam()
                    {
                        JoinJson = param,
                        ReturnJson = resultStr,
                        OperatorId = baseParam.OperatorId,
                        TransactionCode = "SYBX004"
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
                    TransactionCode = "SYBX004"

                });

            }
            MedicalInsuranceDll.DisConnectAppServer_cxjb("SYBX004");
            return resultData;

        }
        /// <summary>
        /// 门诊计划生育结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ApiJsonResultData OutpatientPlanBirthSettlement(string param, HisBaseParam baseParam)
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
                if (!xmlStr) throw new Exception("门诊计划生育结算保存参数出错");
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("医保登陆失败!!!");
                int result = MedicalInsuranceDll.CallService_cxjb("SYBX005");

                if (result == 1)
                {
                    var resultStr = XmlHelp.SerializerModelJson();
                    Logs.LogWriteData(new LogWriteDataParam()
                    {
                        JoinJson = param,
                        ReturnJson = resultStr,
                        OperatorId = baseParam.OperatorId,
                        TransactionCode = "SYBX005"
                    });
                    resultData.Data = resultStr;
                    MedicalInsuranceDll.DisConnectAppServer_cxjb("SYBX005");
                    //获取用余额
                    var userInfo = GetUserInfoEntity("", baseParam);
                    resultData.OtherInfo = userInfo.InsuranceType == "310" ? userInfo.WorkersInsuranceBalance.ToString(CultureInfo.InvariantCulture)
                        : userInfo.ResidentInsuranceBalance.ToString(CultureInfo.InvariantCulture);
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
                    TransactionCode = "SYBX005"

                });
            }

            return resultData;
        }
        /// <summary>
        /// 门诊计划生育结算取消
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ApiJsonResultData OutpatientPlanBirthSettlementCancel(string param, HisBaseParam baseParam)
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
                if (!xmlStr) throw new Exception("门诊计划生育结算取消保存参数出错");
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("医保登陆失败!!!");
                int result = MedicalInsuranceDll.CallService_cxjb("SYBX006");
                //if (result != 1) throw new Exception("门诊计划生育结算取消执行出错!!!");
                if (result == 1)
                {
                    var resultStr = XmlHelp.SerializerModelJson();
                    Logs.LogWriteData(new LogWriteDataParam()
                    {
                        JoinJson = param,
                        ReturnJson = resultStr,
                        OperatorId = baseParam.OperatorId,
                        TransactionCode = "SYBX006"
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
                    TransactionCode = "SYBX006"

                });
            }
            MedicalInsuranceDll.DisConnectAppServer_cxjb("SYBX006");
            return resultData;
        }
        /// <summary>
        /// 门诊计划生育结算查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ApiJsonResultData OutpatientPlanBirthSettlementQuery(string param, HisBaseParam baseParam)
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
                if (!xmlStr) throw new Exception("门诊计划生育结算查询保存参数出错");
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("医保登陆失败!!!");
                int result = MedicalInsuranceDll.CallService_cxjb("SYBX007");
                //if (result != 1) throw new Exception("门诊计划生育结算查询执行出错!!!");
                if (result == 1)
                {
                    var data = XmlHelp.DeSerializerModel(new WorkerBirthPreSettlementJsonDto(), true);
                    var resultStr = JsonConvert.SerializeObject(data);
                    Logs.LogWriteData(new LogWriteDataParam()
                    {
                        JoinJson = param,
                        ReturnJson = resultStr,
                        OperatorId = baseParam.OperatorId,
                        TransactionCode = "SYBX007"
                    });
                    resultData.Data = resultStr;
                }
                else
                {
                    var data = XmlHelp.DeSerializerModel(new WorkerBirthPreSettlementJsonDto(), true);
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
                    TransactionCode = "SYBX007"

                });
            }
            MedicalInsuranceDll.DisConnectAppServer_cxjb("SYBX007");
            return resultData;
        }
        /// <summary>
        /// 门诊月结汇总
        /// </summary>
        /// <param name="param"></param>
        public ApiJsonResultData MonthlyHospitalization(string param, HisBaseParam baseParam)
        {
            Logs.LogWrite(new LogParam()
            {
                Params = param,
                Msg = JsonConvert.SerializeObject(baseParam)

            });
            var resultData = new ApiJsonResultData { Success = true };
            try
            {
                var iniParam = JsonConvert.DeserializeObject<MonthlyHospitalizationParticipationParam>(param);
                iniParam.StartTime = Convert.ToDateTime(iniParam.StartTime).ToString("yyyyMMdd");
                iniParam.EndTime = Convert.ToDateTime(iniParam.EndTime).ToString("yyyyMMdd");
                var xmlStr = XmlHelp.SaveXmlEntity(iniParam);
                if (!xmlStr) throw new Exception("门诊月结汇总保存参数出错");

                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("医保登陆失败!!!");
                int result = MedicalInsuranceDll.CallService_cxjb("TPYP214");
                //if (result != 1) throw new Exception("门诊月结汇总执行出错!!!");
                if (result == 1)
                {
                    var resultStr = XmlHelp.SerializerModelJson();
                    Logs.LogWriteData(new LogWriteDataParam()
                    {
                        JoinJson = param,
                        ReturnJson = resultStr,
                        OperatorId = baseParam.OperatorId,
                        TransactionCode = "TPYP214"
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
                    TransactionCode = "TPYP214"

                });
            }
            MedicalInsuranceDll.DisConnectAppServer_cxjb("TPYP214");
            return resultData;
        }
        /// <summary>
        /// 取消门诊月结汇总
        /// </summary>
        /// <param name="param"></param>
        public ApiJsonResultData CancelMonthlyHospitalization(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultData { Success = true };
            try
            {
                var xmlStr = XmlHelp.SaveXmlStr(param);
                if (!xmlStr) throw new Exception("取消门诊月结汇总保存参数出错");
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("医保登陆失败!!!");
                int result = MedicalInsuranceDll.CallService_cxjb("TPYP215");
                //if (result != 1) throw new Exception("取消门诊月结汇总执行出错!!!");
                if (result == 1)
                {
                    var resultStr = XmlHelp.SerializerModelJson();
                    Logs.LogWriteData(new LogWriteDataParam()
                    {
                        JoinJson = param,
                        ReturnJson = resultStr,
                        OperatorId = baseParam.OperatorId,
                        TransactionCode = "TPYP215"
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
                    TransactionCode = "TPYP215"

                });
            }
            MedicalInsuranceDll.DisConnectAppServer_cxjb("TPYP215");
            return resultData;
        }
        // <summary>
        // 读卡获取信息
        // </summary>
        ///<param name = "paramStr"></param >
        /// <param name="baseParam"></param>

        public ApiJsonResultData ReadCardInfo(string paramStr, HisBaseParam baseParam)
        {

            //string filename = CommonHelp.GetPathStr() + "\\YiBinActiveClient.exe";
            //string s = "{\"Account\":\"ybx12865\",\"Pwd\":\"aaaaaa\",\"OperatorId\":\"76EDB472F6E544FD8DC8D354BB088BD7\",\"InsuranceType\":null,\"IdentityMark\":\"500233199005223447\",\"AfferentSign\":\"1\"}";

            //Process myprocess = new Process();
            //ProcessStartInfo startInfo = new ProcessStartInfo(filename, s);
            //myprocess.StartInfo = startInfo;
            //myprocess.StartInfo.CreateNoWindow = true;//不显示程序窗口
            //myprocess.StartInfo.UseShellExecute = false;
            //myprocess.StartInfo.RedirectStandardOutput = true;
            //myprocess.StartInfo.RedirectStandardError = true;

            //myprocess.Start();
            //myprocess.WaitForExit();
            //string output = myprocess.StandardOutput.ReadToEnd();

          //  var param = JsonConvert.DeserializeObject<ReadCardInfoParam>(paramStr);
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
            var userData = new GetResidentUserInfoDto();
            try
            {
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("医保登陆失败!!!");
                //居民职工
          
                    var iniFile = new IniFile("");
                    //端口号
                    int port = Convert.ToInt16(iniFile.GetIni());
                    var readCardData = MedicalInsuranceDll.WorkerReadCardInfo(
                        port,
                        paramStr,
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
                        WorkersCardNo = CommonHelp.StrToTransCoding(workersCardNo),
                        PO_SFZH = CommonHelp.StrToTransCoding(idCardNo),
                        PO_JBZHYE= CommonHelp.StrToTransCoding(insuranceBalance),
                      
                    };
                    resultData.Data = JsonConvert.SerializeObject(userData);
                    //userData = XmlHelp.DeSerializerModel(new Model.Dto.GetResidentUserInfoDto(), true);
                   
                    Logs.LogWriteData(new LogWriteDataParam()
                    {
                     
                        ReturnJson = JsonConvert.SerializeObject(userData),
                        OperatorId = baseParam.OperatorId
                    });

                

            }
            catch (Exception e)
            {
                resultData.Success = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = baseParam.OperatorId,
                   
                    TransactionCode = "ReadCard",
                    ResultData = Logs.ToJson(userData)

                });
            }
            return resultData;

        }
        ///// <summary>
        ///// 职工划卡
        ///// </summary>
        ///// <param name="param"></param>
        ///// <param name="baseParam"></param>
        ///// <returns></returns>
        //public ApiJsonResultData WorkersSettlement(WorkersSettlementParam param, HisBaseParam baseParam)
        //{
        //    //流水号
        //    var settlementNo = new byte[1024];
        //    //自付金额
        //    var selfPayment = new byte[1024];
        //    //账户支付
        //    var accountPayment = new byte[1024];
        //    //返回状态
        //    var resultState = new byte[1024];
        //    //消息
        //    var msg = new byte[1024];
        //    var resultData = new ApiJsonResultData {Success = true};

        //    Logs.LogWriteData(new LogWriteDataParam()
        //    {
        //        JoinJson = JsonConvert.SerializeObject(param),
        //        ReturnJson = JsonConvert.SerializeObject(baseParam),
        //        OperatorId = baseParam.OperatorId

        //    });
        //    try
        //    {
        //        if (param == null)
        //            throw new Exception("职工结算入参不能为空!!!");
        //        if (string.IsNullOrWhiteSpace(baseParam.Account))
        //            throw new Exception("医保账户不能为空!!!");
        //        if (string.IsNullOrWhiteSpace(baseParam.Pwd))
        //            throw new Exception("医保账户密码不能为空!!!");
        //        if (string.IsNullOrWhiteSpace(param.CardPwd))
        //            throw new Exception("卡密码不能为空!!!");
        //        if (string.IsNullOrWhiteSpace(param.Operator))
        //            throw new Exception("经办人不能为空!!!");
        //        if (param.AllAmount<=0)
        //            throw new Exception("划卡金额必须大于0!!!");
        //        if (param.MedicalCategory <= 0)
        //            throw new Exception("划卡类别!!!");

        //        var loginData = WorkersMedicalInsurance.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
        //        if (loginData != 1) throw new Exception("医保登陆失败!!!");
        //        //var settlementData = WorkersMedicalInsurance.WorkersSettlement
        //        //(1,
        //        // param.CardPwd,
        //        // param.AllAmount.ToString(),
        //        // param.MedicalCategory.ToString(),
        //        // param.Operator,
        //        // settlementNo,
        //        // accountPayment,
        //        // selfPayment,
        //        // resultState,
        //        // msg
        //        //);
        //        //if (settlementData!=0) throw new Exception("职工划卡失败!!!");
        //        //if (CommonHelp.StrToTransCoding(resultState) != "1") throw new Exception(CommonHelp.StrToTransCoding(msg));
        //        //var data = new WorkersSettlementDto()
        //        //{
        //        //    SettlementNo = CommonHelp.StrToTransCoding(settlementNo),
        //        //    AccountPayment = Convert.ToDecimal(CommonHelp.StrToTransCoding(accountPayment)),
        //        //    SelfPayment = Convert.ToDecimal(CommonHelp.StrToTransCoding(selfPayment)),
        //        //};
        //        var accountPaymentData = param.AllAmount > 0 ? Convert.ToDecimal(0.1) : 0;

        //        //resultData.Data = JsonConvert.SerializeObject(data);
        //        ////数据日志存入
        //        //param.CardPwd = "******";
        //        //Logs.LogWriteData(new LogWriteDataParam()
        //        //{
        //        //    JoinJson = JsonConvert.SerializeObject(param),
        //        //    ReturnJson = JsonConvert.SerializeObject(data),
        //        //    OperatorId = baseParam.OperatorId

        //        //});
        //    }
        //    catch (Exception e)
        //    {
        //        resultData.Success = false;
        //        resultData.Message = e.Message;
        //        Logs.LogWrite(new LogParam()
        //        {
        //            Msg = e.Message,
        //            OperatorCode = baseParam.OperatorId,
        //            Params = Logs.ToJson(param),
        //        });

        //    }

        //    return resultData;
        //}
        private ResidentUserInfoDto UserInfoToDto(ResidentUserInfoJsonDto param)
        {
            var resultData = new ResidentUserInfoDto()
            {
                WorkersInsuranceBalance = param.WorkersInsuranceBalance,
                ResidentInsuranceBalance = param.ResidentInsuranceBalance,
                AdministrativeArea = param.AdministrativeArea,
                Age = param.Age,
                Birthday = param.Birthday,
                CommunityName = param.CommunityName,
                ContactAddress = param.ContactAddress,
                ContactPhone = param.ContactPhone,
                IdCardNo = param.IdCardNo,
                InsuranceType = param.InsuranceType,
                InsuredState = param.InsuredState,
                MentorBalance = param.MentorBalance,
                OverallPaymentBalance = param.OverallPaymentBalance,
                ReturnState = param.ReturnState,
                Msg = param.Msg,
                PatientName = param.PatientName,
                PatientSex = param.PatientSex,
                PersonalCoding = param.PersonalCoding,
                PersonnelClassification = param.PersonnelClassification,
                PoorMark = param.PoorMark,
                PreferentialTreatmentType = param.PreferentialTreatmentType,
                ReimbursementStatus = param.ReimbursementStatus,
                ReimbursementStatusExplain = param.ReimbursementStatusExplain,
                RescueType = param.RescueType,
                SpecialPeopleCognizancePlace = param.SpecialPeopleCognizancePlace
            };
            return resultData;
        }
        /// <summary>
        /// 职工电子凭证
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData NationEcTrans(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultData { Success = true };

            
            string code = "DZPZ001";
            var iniFile = new IniFile("");
            //端口号
            var nationEcTransUrl = iniFile.NationEcTransUrl();

            try
            {
                string url = "";
                string tipsMsg = "电子社保卡支付";

                Logs.LogWrite(new LogParam()
                {
                    Params = param,
                    Msg = JsonConvert.SerializeObject(baseParam)

                });
                //返回状态
                var resultState = new byte[1024];
                //消息
                var msg = new byte[1024];
                var xmlStr = XmlHelp.SaveXmlStr(param);
                if (!xmlStr) throw new Exception(tipsMsg + "保存参数出错!!!");
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception(tipsMsg + "医保执行失败!!!");
                int result = MedicalInsuranceDll.NationEcTrans_call(code, nationEcTransUrl, resultState, msg);
                Logs.LogWrite(new LogParam()
                {
                    Params= "result:"+ result,
                    Msg = "Msg" + CommonHelp.StrToTransCoding(msg)

                });
                var resultStr = XmlHelp.SerializerModelJson();
                resultData.Data = resultStr;
                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = param,
                    ReturnJson = resultStr,
                    OperatorId = baseParam.OperatorId,
                    TransactionCode = code

                });
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

            return resultData;
        }
        /// <summary>
        /// 居民电子凭证
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData NationEcTransResident(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultData { Success = true };

            
            string code = "DZPZ003";
            var iniFile = new IniFile("");
            //端口号
            var nationEcTransUrl = iniFile.NationEcTransUrl();

            try
            {
                string url = "";
                string tipsMsg = "电子社保卡支付";

                Logs.LogWrite(new LogParam()
                {
                    Params = param,
                    Msg = JsonConvert.SerializeObject(baseParam)

                });
                //返回状态
                var resultState = new byte[1024];
                //消息
                var msg = new byte[1024];
                var xmlStr = XmlHelp.SaveXmlStr(param);
                if (!xmlStr) throw new Exception(tipsMsg + "保存参数出错!!!");
                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception(tipsMsg + "医保执行失败!!!");
                int result = MedicalInsuranceDll.NationEcTrans_call(code, nationEcTransUrl, resultState, msg);
                Logs.LogWrite(new LogParam()
                {
                   
                    Msg = "Msg" + CommonHelp.StrToTransCoding(msg)

                });
                var resultStr = XmlHelp.SerializerModelJson();
                resultData.Data = resultStr;
                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = param,
                    ReturnJson = resultStr,
                    OperatorId = baseParam.OperatorId,
                    TransactionCode = code

                });
            }
            catch (Exception e)
            {
                resultData.Success = false;
                resultData.Message = e.Message;
                var resultStr = XmlHelp.SerializerModelJson();
                Logs.LogErrorWrite(new LogParam()
                {
                    Msg = e.Message + "error:" + e.StackTrace,
                    OperatorCode = baseParam.OperatorId,
                    Params = Logs.ToJson(param),
                    ResultData = resultData.Data,
                    TransactionCode = code

                });
            }

            return resultData;
        }
        /// <summary>
        /// 电子凭证身份识别
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData NationEcTransUser(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultData { Success = true };
            //返回状态
            var resultState = new byte[1024];
            try
            {
                //消息
                var msg = new byte[1024];
                var iniFile = new IniFile("");
                //端口号
                var nationEcTransUrl = iniFile.NationEcTransUrl();
                string url = "";
                string tipsMsg = "电子社保卡支付";

                var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception(tipsMsg + "医保执行失败!!!");
                int result = MedicalInsuranceDll.NationEcTrans_call("DZPZ002", nationEcTransUrl, resultState, msg);
       
                var dataIni = XmlHelp.DeSerializerModel(new ResidentUserInfoJsonIniDto(), true);
               // resultData.Data = JsonConvert.SerializeObject(data);
                if (!string.IsNullOrWhiteSpace(dataIni.AdministrativeArea))
                {
                    string areaCode = dataIni.AdministrativeArea.Substring(0, 4);
                    if (areaCode == "5115") //本地
                    {
                        var data = XmlHelp.DeSerializerModel(new ResidentUserInfoJsonDto(), true);
                        resultData.Data = JsonConvert.SerializeObject(data);
                    }
                    if (areaCode != "5115") //异地
                    {
                        var data = XmlHelp.DeSerializerModel(new YdNationEcTransUserInfoJsonDto(), true);
                        //data.InsuranceType 为预设置值310或342者判断，具体需根据实际情况更改
                        //职工
                        if (data.InsuranceType == "310") data.WorkersInsuranceBalance = data.AccountBalance;
                        //居民
                        if (data.InsuranceType == "342") data.ResidentInsuranceBalance = data.AccountBalance;
                         resultData.Data = JsonConvert.SerializeObject(data);

                    }

                }

                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = param,
                    ReturnJson = resultData.Data,
                    OperatorId = baseParam.OperatorId,
                    TransactionCode = "DZPZ002"

                });
            }
            catch (Exception e)
            {
                resultData.Success = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = baseParam.OperatorId,
                    Params = "",
                    TransactionCode = "DZPZ002",


                });
            }

            return resultData;
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        public string Login(string param, HisBaseParam baseParam)
        {
            //Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("sid", "MZBX001");
            //parameters.Add("inXml", "");
            //parameters.Add("operId", baseParam.Account);
            //parameters.Add("operPsw", baseParam.Pwd);
            //parameters.Add("base64Cert", "");

            //WebService(parameters, "callService");

            var paramIni = new ResidentParam()
            {
                Sid = "MZBX001",
                OperatorId = baseParam.Account,
                OperatorPsw = baseParam.Pwd,
                InXml = ""

            };
           var resultData= GetWebServiceData(paramIni);
            
            return resultData;
        }
        /// <summary>
        /// 门诊居民结算
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData ResidentSettlement(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultData { Success = true };
            try
            {
                Logs.LogWrite(new LogParam()
                {
                    Params = param,
                    Msg = JsonConvert.SerializeObject(baseParam)
                });
               Login(param, baseParam);

                
                var paramIni = new ResidentParam()
                {
                    Sid = "MZBX003",
                    OperatorId = baseParam.Account,
                    OperatorPsw = baseParam.Pwd,
                    InXml = param

                };
                var resultString = GetWebServiceData(paramIni);
                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = param,
                    ReturnJson = resultString,
                    OperatorId = baseParam.OperatorId,
                    TransactionCode = "MZBX003"
               });
                resultData.Data = resultString;
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
                    TransactionCode = "MZBX003"

                });

            }

            return resultData;
        }
        /// <summary>
        /// 门诊居民结算取消
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData CancelResidentSettlement(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultData { Success = true };
            try
            {
                Logs.LogWrite(new LogParam()
                {
                    Params = param,
                    Msg = JsonConvert.SerializeObject(baseParam)
                });
                Login(param, baseParam);
                var paramIni = new ResidentParam()
                {
                    Sid = "MZBX004",
                    OperatorId = baseParam.Account,
                    OperatorPsw = baseParam.Pwd,
                    InXml = param

                };
                var resultString = GetWebServiceData(paramIni);
                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = param,
                    ReturnJson = resultString,
                    OperatorId = baseParam.OperatorId,
                    TransactionCode = "MZBX004"
                });
                resultData.Data = resultString;
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
                    TransactionCode = "MZBX004"

                });

            }

            return resultData;
        }
        /// <summary>
        /// 门诊居民划卡
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData ResidentSettlementCard(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultData { Success = true };
            try
            {
                var paramIni = new ResidentParam()
                {
                    Sid = "MZBX013",
                    OperatorId = baseParam.Account,
                    OperatorPsw = baseParam.Pwd,
                    InXml = param

                };
                Logs.LogWrite(new LogParam()
                {
                    Params = JsonConvert.SerializeObject(paramIni),
                    Msg = JsonConvert.SerializeObject(baseParam)
                });
                 Login(param, baseParam);
                //Dictionary<string, string> parameters = new Dictionary<string, string>();
                //parameters.Add("sid", "MZBX013");
                //parameters.Add("inXml", param);
                //parameters.Add("operId", baseParam.Account);
                //parameters.Add("operPsw", baseParam.Pwd);
                //parameters.Add("base64Cert", "");
                var settlementData = GetWebServiceData(paramIni);
                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = param,
                    ReturnJson = settlementData,
                    OperatorId = baseParam.OperatorId,
                    TransactionCode = "MZBX013"
                });
                resultData.Data = settlementData;
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
                    TransactionCode = "MZBX013"

                });

            }
            return resultData;
        }
        /// <summary>
        /// 门诊居民结算查询
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public ApiJsonResultData ResidentSettlementQuery(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultData { Success = true };
            try
            {
                var paramIni = new ResidentParam()
                {
                    Sid = "MZBX005",
                    OperatorId = baseParam.Account,
                    OperatorPsw = baseParam.Pwd,
                    InXml = param

                };
                Logs.LogWrite(new LogParam()
                {
                    Params = JsonConvert.SerializeObject(paramIni),
                    Msg = JsonConvert.SerializeObject(baseParam)
                });
                Login(param, baseParam);
                var settlementData = GetWebServiceData(paramIni);
                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = param,
                    ReturnJson = settlementData,
                    OperatorId = baseParam.OperatorId,
                    TransactionCode = "MZBX005"
                });
                resultData.Data = settlementData;
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
                    TransactionCode = "MZBX005"

                });

            }
            return resultData;
        }
        /// <summary>
        /// 门诊居民结算汇总
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>

        public ApiJsonResultData ResidentSettlementSummary(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultData { Success = true };
            try
            {
                var paramIni = new ResidentParam()
                {
                    Sid = "MZBX006",
                    OperatorId = baseParam.Account,
                    OperatorPsw = baseParam.Pwd,
                    InXml = param

                };
                Logs.LogWrite(new LogParam()
                {
                    Params = JsonConvert.SerializeObject(paramIni),
                    Msg = JsonConvert.SerializeObject(baseParam)
                });
                Login(param, baseParam);
                var settlementData = GetWebServiceData(paramIni);
                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = param,
                    ReturnJson = settlementData,
                    OperatorId = baseParam.OperatorId,
                    TransactionCode = "MZBX006"
                });
                resultData.Data = settlementData;
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
                    TransactionCode = "MZBX006"

                });

            }
            return resultData;
        }
        /// <summary>
        /// 门诊居民结算汇总取消
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>

        public ApiJsonResultData ResidentSettlementSummaryCancel(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultData { Success = true };
            try
            {
                var paramIni = new ResidentParam()
                {
                    Sid = "MZBX007",
                    OperatorId = baseParam.Account,
                    OperatorPsw = baseParam.Pwd,
                    InXml = param

                };
                Logs.LogWrite(new LogParam()
                {
                    Params = JsonConvert.SerializeObject(paramIni),
                    Msg = JsonConvert.SerializeObject(baseParam)
                });
                Login(param, baseParam);
                var settlementData = GetWebServiceData(paramIni);
                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = param,
                    ReturnJson = settlementData,
                    OperatorId = baseParam.OperatorId,
                    TransactionCode = "MZBX007"
                });
                resultData.Data = settlementData;
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
                    TransactionCode = "MZBX007"

                });

            }
            return resultData;
        }
        /// <summary>
        /// 门诊居民结算汇总查询
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>

        public ApiJsonResultData ResidentSettlementSummaryQuery(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultData { Success = true };
            try
            {
                var paramIni = new ResidentParam()
                {
                    Sid = "MZBX008",
                    OperatorId = baseParam.Account,
                    OperatorPsw = baseParam.Pwd,
                    InXml = param

                };
                Logs.LogWrite(new LogParam()
                {
                    Params = JsonConvert.SerializeObject(paramIni),
                    Msg = JsonConvert.SerializeObject(baseParam)
                });
                Login(param, baseParam);
                var settlementData = GetWebServiceData(paramIni);
                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = param,
                    ReturnJson = settlementData,
                    OperatorId = baseParam.OperatorId,
                    TransactionCode = "MZBX008"
                });
                resultData.Data = settlementData;
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
                    TransactionCode = "MZBX008"

                });

            }
            return resultData;
        }

        private string GetWebServiceData(ResidentParam param)
        {//11008
           
            // 创建 HTTP 绑定对象与设置最大传输接受数量
            var binding = new BasicHttpBinding { MaxReceivedMessageSize = 2147483647 };
            // 根据 WebService 的 URL 构建终端点对象
            var iniFile = new IniFile("");
            var urlStr = iniFile.OutpatientResidentUrl();
            //正式
            var endpoint = new EndpointAddress(urlStr);
            // 创建调用接口的工厂，注意这里泛型只能传入接口 添加服务引用时生成的 webservice的接口 一般是 (XXXSoap)
            var factory = new ChannelFactory<YbsiService>(binding, endpoint);
            // 从工厂获取具体的调用实例 
            var callClient = factory.CreateChannel();
        
            var paramIni = new callServiceRequest(
                param.Sid,
                param.InXml, 
                param.OperatorId,
                param.OperatorPsw,
                param.Base64Cert);
          
            //var paramIni = new ExecuteSPRequest(new ExecuteSPRequestBody() {param = param});
            var dataXml=  callClient.callService(paramIni);
            var resultStr = dataXml.callServiceReturn;
            var resultData = XmlHelp.DeSerializer<ResultBaseXmlDto>(resultStr);
            if (resultData.ReturnState!="1") throw  new Exception(resultData.ReturnMsg);
            return resultStr;
        }
    }
}
