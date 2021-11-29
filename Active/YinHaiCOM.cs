using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BenDingActive.Model.Dto.YiHai.comm;
using Newtonsoft.Json;

namespace BenDingActive
{
   public static class YinHaiCOM
    {
        static System.Type yhNew = Type.GetTypeFromProgID("YinHai.CHS.InterfaceSCS");
        static Object yhObject;
        //签到人员id
       public static string SignInUserId = "";
        public static bool Init(out string msg)
        {

            int Appcode = -1;
            msg = string.Empty;
            object[] args = new object[] { Appcode, msg };
            yhObject = System.Activator.CreateInstance(yhNew);
            ParameterModifier pm = new ParameterModifier(2);
            pm[0] = true;
            pm[1] = true;
            ParameterModifier[] pmd = { pm };
            yhNew.InvokeMember("yh_CHS_init", BindingFlags.InvokeMethod, null,
                yhObject, args, pmd, System.Globalization.CultureInfo.CurrentCulture, null);

            string o1 = args[0].ToString();
            string o2 = args[1].ToString();

            if (Convert.ToInt32(o1) < 0)
            {
                msg = o2;
                return false;
            }

            //测试
            //msg = "";
            return true;
        }

        /// <summary>
        /// 获取病人信息
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetPatient()
        {
            //var inpatientBase = new InpatientBaseInfoDto()
            //{
            //    psn_no = "51000051200000512099000007",
            //    psn_cert_type= "90",//    社会保障卡
            //    certno= "511023197201145538",
            //    psn_name= "周雪松",
            //};
            var inpatientBase = new InpatientBaseInfoDto()
            {
                psn_no = "51000051200000512021037964",
                psn_cert_type = "90",//    社会保障卡
                certno = "513922198607153465",
                psn_name = "陈静",
            };
            var insuinfo = new List<InpatientBaseInsuInfo>();
            var insuinfoData = new InpatientBaseInsuInfo()
            {
                insutype="310",
                balc=20,
                insuplc_admdvs= "512000",
                psn_type= "11"//在职
            };
            insuinfo.Add(insuinfoData);
            inpatientBase.insuinfo = insuinfo;
            return JsonConvert.SerializeObject(inpatientBase);
        }

        private static bool yh_interface_destroy()
        {
            try
            {
                yhNew.InvokeMember("yh_CHS_destroy", BindingFlags.InvokeMethod, null,
                         yhObject, null);
                return true;
            }
            catch (Exception ex)
            {

                return false;

            }
        }
        /// <summary>
        /// 主交易
        /// </summary>
        /// <param name="astr_jybh">交易编号</param>
        /// <param name="astr_jykz_xml">控制入参</param>
        /// <param name="astr_jysr_xml">入参数据</param>
        /// <param name="astr_pcbh"></param>
        /// <param name="astr_jylsh">交易流水号</param>
        /// <param name="astr_jyyzm">交易验证码</param>
        /// <param name="astr_jysc_xml"> 交易输出</param>
        /// <param name="along_appcode">交易标志</param>
        /// <param name="astr_appmsg">交易信息</param>
        public static void yh_CHS_call(string infno, string input, ref string output)
        {


            object[] args = new object[] {
                infno,
                input,
                output
             };

            ParameterModifier pm = new ParameterModifier(3);
            pm[0] = false;
            pm[1] = false;
            pm[2] = true;
            //yhObject = System.Activator.CreateInstance(yh);
            ParameterModifier[] pmd = { pm };
            if (yhObject == null) yhObject = System.Activator.CreateInstance(yhNew);
            yhNew.InvokeMember("yh_CHS_call", BindingFlags.InvokeMethod, null,
                yhObject, args, pmd, System.Globalization.CultureInfo.CurrentCulture, null);
            object o0 = args[0].ToString();
            object o1 = args[1].ToString();
            output = args[2] != null ? args[2].ToString() : null;
        }
       /// <summary>
       /// 交易输入
       /// </summary>
       /// <param name="input"></param>
       /// <param name="output"></param>
        public static void yh_CHS_print(string input, ref string output)
        {
            object[] args = new object[] {

                input,
                output
             };
            ParameterModifier pm = new ParameterModifier(3);
            pm[0] = false;
            pm[1] = true;
            //yhObject = System.Activator.CreateInstance(yh);
            ParameterModifier[] pmd = { pm };
            if (yhObject == null) yhObject = System.Activator.CreateInstance(yhNew);
            yhNew.InvokeMember("yh_CHS_print", BindingFlags.InvokeMethod, null,
                yhObject, args, pmd, System.Globalization.CultureInfo.CurrentCulture, null);
            object o0 = args[0].ToString();
            output = args[1] != null ? args[1].ToString() : null;
        }
    }
}
