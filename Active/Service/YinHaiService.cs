using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BenDingActive.Help;
using BenDingActive.Model.Dto.YiHai;
using Newtonsoft.Json;

namespace BenDingActive.Service
{
   public class YinHaiService
    {
        public ApiJsonResultData MedicalInsuranceExecute(string paramData)
        {
            var iniFile = new IniFile("");
            var url = iniFile.YinHaiUrl();
            var resultData = new ApiJsonResultData { Success = true };

            var resultDataText = "";
            try
            {

                 resultDataText = PostWebRequest( paramData);

                var outBaseData = JsonConvert.DeserializeObject<YinHaiOutBaseParam>(resultDataText);
                if (outBaseData.infcode == "0")
                {
                    var output = outBaseData.output;
                    resultData.Data = JsonConvert.SerializeObject(output);
                    resultData.OtherInfo = outBaseData.inf_refmsgid;
                    Logs.LogWriteData(new LogWriteDataParam()
                    {
                        JoinJson = paramData,
                        ReturnJson = resultDataText,
                    });
                }
                else
                {
                   throw  new Exception(outBaseData.err_msg);
                   
                }
            }
            catch (Exception e)
            {
                resultData.Success = false;
                resultData.Message = e.Message;

                Logs.LogErrorWrite(new LogParam()
                {
                    Msg = e.Message + "error:" + e.StackTrace,
                    Params = paramData,
                    ResultData = resultDataText,
                  


                });
            }

            return resultData;
        }
        /// <summary>
        /// Post提交数据
        /// </summary>
        /// <param name="paramData">参数</param>
        /// <returns></returns>
        private string PostWebRequest(string paramData)
        {
            var iniFile = new IniFile("");
            string postUrl= iniFile.YinHaiUrl();
            string ret = string.Empty;
            try
            {
                if (!postUrl.StartsWith("http://"))
                    return "";

                byte[] byteArray = Encoding.UTF8.GetBytes(paramData);
               //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                //webReq.ContentType = "application/json";
                webReq.ContentType = "application/json";
                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return ret;
        }
        /// <summary>
        /// 安全控件
        /// </summary>
        /// <param name="param"></param>
        /// <param name="infno"></param>
        /// <returns></returns>
        public ApiJsonResultData SecurityControl(string param,string  infno)
        {
            var resultDataNew = new ApiJsonResultData { Success = true };
            string msg = "";
            string iniMsg = "";
            var resultData = YinHaiCOM.Init(out iniMsg);
            YinHaiCOM.yh_CHS_call(infno, param, ref msg);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                var msgData = JsonConvert.DeserializeObject<SecureMediaOutputDto>(msg);
                if (msgData.code == "1")
                {
                    resultDataNew.Data = JsonConvert.SerializeObject(msgData.data);
                }
                else
                {
                    resultDataNew.Success = false;
                    resultDataNew.Message = msgData.message;
                    
                }
                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = param,
                    ReturnJson = msg,
                    OperatorId = "",
                    TransactionCode = infno
                });
              
            }

            return resultDataNew;
        }

        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ApiJsonResultData Signin(string param)
        {
            var resultData = new ApiJsonResultData { Success = true };
            var resultDataText= PostWebRequest(param);
            
            var signData = JsonConvert.DeserializeObject<YinHaiOutBaseParam>(resultDataText);
            if (signData.infcode == "0")
            {
                var output = signData.output;
                var outputData = JsonConvert.DeserializeObject<SignInOutputDto>(output.ToString());
                resultData.Data = JsonConvert.SerializeObject(outputData.signinoutb);
                resultData.OtherInfo = signData.inf_refmsgid;
            }
            else
            {
                resultData.Success = false;
                resultData.Message = signData.err_msg;
            }

            return resultData;
        }
    }
}
