using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDingActive.Help;
using BenDingActive.Model;
using BenDingActive.Model.BendParam;
using BenDingActive.Model.Dto.Bend;
using Newtonsoft.Json;

namespace BenDingActive.Service
{/// <summary>
/// 异地医保服务
/// </summary>
  public  class YdMedicalInsuranceService
    {
        

        /// <summary>
        /// 异地医保操作
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public ApiJsonResultData YdMedicalInsuranceOperation(string param, HisBaseParam baseParam, string code)
        {
            var resultData = new ApiJsonResultData { Success = true };
            try
            {
                Logs.LogWrite(new LogParam()
                {
                    Params = param,
                    Msg = JsonConvert.SerializeObject(baseParam)

                });
                if (!string.IsNullOrWhiteSpace(param))
                {
                    var xmlStr = XmlHelp.SaveXmlStr(param);
                    if (!xmlStr) throw new Exception("异地" + code + "保存参数出错!!!");
                }

               
                var loginData = MedicalInsuranceDll.YdConnectAppServer(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("异地" + code + "医保执行失败!!!");
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

            return resultData;
        }
        /// <summary>
        /// 获取个人基础资料
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>

        public ApiJsonResultData GetUserInfo(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultData { Success = true };

            var data = new YdUserInfoJsonDto();
            try
            {//
                var iniFile = new IniFile("");
                //端口号
                int port = Convert.ToInt16(iniFile.GetIni());
                var loginData = MedicalInsuranceDll.YdConnectAppServer(baseParam.Account, baseParam.Pwd);
                if (loginData != 1) throw new Exception("医保登陆失败!!!");
                int result = MedicalInsuranceDll.YdReadCardInfo(port,param);

                if (result == 1)
                {
                    data = XmlHelp.DeSerializerModel(new YdUserInfoJsonDto(), true);
                    if (data.InsuredState == "0")
                    {
                        throw new Exception(data.UnEnjoyRemark);
                    }
                    else
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
                    TransactionCode = "YdReadCardInfo"

                });


            }
            
            return resultData;

        }
        private ResidentUserInfoDto UserInfoToDto(YdUserInfoJsonDto param)
        {
            var resultData = new ResidentUserInfoDto()
            {
                WorkersInsuranceBalance = param.InsuranceType=="310"? param.InsuranceBalance:0,
                ResidentInsuranceBalance = param.InsuranceType == "310" ? 0 : param.InsuranceBalance,
                AdministrativeArea = param.AdministrativeArea,
                Birthday = param.Birthday,
                IdCardNo = param.IdCardNo,
                InsuranceType = param.InsuranceType,
                InsuredState = param.InsuredState,
                PatientName = param.PatientName,
                PatientSex = param.PatientSex,
                PersonalCoding = param.PersonalCoding,
                CardNo = param.CardNo
            };
            return resultData;
        }
    }
}
