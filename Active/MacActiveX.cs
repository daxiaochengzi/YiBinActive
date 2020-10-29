using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using BenDingActive.Help;
using BenDingActive.Model;
using BenDingActive.Model.BendParam;

using BenDingActive.Service;
using Newtonsoft.Json;

namespace BenDingActive
{
    [Guid("67475F7D-57A1-45AD-96F3-428A679B2E6C")]
    public class MacActiveX : ActiveXControl
    {     /// <summary>
          /// 门诊方法集合
          /// </summary>
          /// <param name="param"></param>
          /// <param name="baseParam"></param>
          /// <param name="methodName"></param>
          /// <returns></returns>
        public string OutpatientMethods(string param, string baseParam, string methodName)
        {
            System.IO.Directory.SetCurrentDirectory(CommonHelp.GetPathStr());
            //反射获取 命名空间 + 类名
            string className = "BenDingActive.Service.OutpatientDepartmentService";
            var resultData=  MedicalInsuranceExecute(param, baseParam, methodName, className);
            return resultData;
        }
        /// <summary>
        /// 住院方法集合
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public string HospitalizationMethods(string param, string baseParam, string methodName)
        {
            System.IO.Directory.SetCurrentDirectory(CommonHelp.GetPathStr());
            //反射获取 命名空间 + 类名
            string className = "BenDingActive.Service.HospitalizationService";
            var resultData = MedicalInsuranceExecute(param, baseParam, methodName, className);
            return resultData;
        }
       

        /// <summary>
        /// 医保执行
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <param name="methodName"></param>
        /// <param name="namespaces"></param>
        /// <returns></returns>
        private string MedicalInsuranceExecute(string param, string baseParam, string methodName,string namespaces)
        {
            string resultData = null;
            var baseParams = JsonConvert.DeserializeObject<HisBaseParam>(baseParam);
            try
            {
                
                //反射获取 命名空间 + 类名
                string className = namespaces;
                //传递参数
                Object[] paras = new Object[] { param, baseParams };
                Type t = Type.GetType(className);
                object obj = Activator.CreateInstance(t);
                //直接调用
                MethodInfo method = t.GetMethod(methodName);
                if (method != null)
                {
                    var data = method.Invoke(obj, paras);
                    resultData = JsonConvert.SerializeObject(data);
                    //释放内存
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                else
                {
                    resultData = JsonConvert.SerializeObject(new ApiJsonResultData
                    {
                        Success = false,
                        Message = "当前插件方法不存在!!!"
                    });
                }

            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                {
                    Exception exTmp = ex.InnerException;

                    resultData = JsonConvert.SerializeObject(new ApiJsonResultData
                    {
                        Success = false,
                        Message = exTmp.Message
                    });
                    Logs.LogErrorWrite(new LogParam()
                    {
                        Params = param,
                        ResultData = methodName,
                        Msg = exTmp.Message.ToString(),
                        TransactionCode = "[Exe]" + methodName,
                        OperatorCode= baseParams.OperatorId
                    });
                }
               
            }
            return resultData;
        }

        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <returns></returns>
        public int GetVersionNumber()
        { //生成数据文件夹
            XmlHelp.CheckFolders();
            return 100;
        }
    }
}
