using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace BenDingActive.Help
{
   public static class CommonHelp
    { /// <summary>
      /// 
      /// </summary>   
      /// <returns></returns>
        public static string GetWebServiceUrl()
        {
            string resultData = null;

            //宜宾测试
            //resultData = "http://10.109.120.206:8080/mss/web/api/fsi/callService";
            //宜宾正式
            //resultData = "http://10.109.103.38:8080/mss/web/api/fsi/callService";
            //成都测试
             resultData = "http://10.109.255.33:8121/mss/web/api/fsi/callService";


            return resultData;
        }
        public static string StrToTransCoding(byte[] param)
        {
            string resultData = null;
            if (param.Length > 0)
            {
                resultData = Encoding.ASCII.GetString(param, 1, 1023).Replace("\0", "");
            }
            Regex reg = new Regex("^[-+]?(([0-9]+)([.]([0-9]+))?|([.]([0-9]+))?)$");
            if (resultData != null)
            {
                if (reg.IsMatch(resultData) == false)
                {
                    resultData = Encoding.GetEncoding("GBK").GetString(param, 1, 1023).Replace("\0", "");
                    //获取字符是否包含??
                    bool flag = resultData.Contains("??");
                    if (flag) resultData = Encoding.GetEncoding("GBK").GetString(param, 1, 1023).Replace("\0", "");

                }

            }


            return resultData;
        }
        /// <summary>
        /// 获取数据库链接
        /// </summary>
        /// <returns></returns>
        public static string GetConnStr()
        {
            string connStr = null;
            var is64Bit = Environment.Is64BitOperatingSystem;
            if (is64Bit)
            {

                connStr = @"Data Source=C:\Program Files (x86)\Microsoft\本鼎医保插件\xmlData\logData.db; Initial Catalog=logData;Integrated Security=True;Max Pool Size=10";
            }
            else
            {

                connStr = @"Data Source=C:\Program Files\Microsoft\本鼎医保插件\xmlData\logData.db; Initial Catalog=logData;Integrated Security=True;Max Pool Size=10";
            }

            return connStr;
        }
        /// <summary>
        ///  字符串转换数值型
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static decimal ValueToDecimal(string param)
        {
            decimal resultData = 0;
            if (!string.IsNullOrWhiteSpace(param))
            {
                resultData = Convert.ToDecimal(param);
            }

            return resultData;
        }
       
        //入参字符串为空则为0
        public static string getNum(string num)
        {
            string numValue = "0";
            if (!string.IsNullOrWhiteSpace(num))
            {
                numValue = num;
            }

            return numValue;
        }
        /// <summary>
        /// sql过滤不安全字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterSqlStr(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                str = str.Replace("'", "");
                str = str.Replace(" ", "");
                str = str.Replace("?", "");
                str = str.Replace("\"", "");
                str = str.Replace("&", "&amp");
                str = str.Replace("<", "&lt");
                str = str.Replace(">", "&gt");
                str = str.Replace("delete", "");
                str = str.Replace("update", "");
                str = str.Replace("insert", "");
            }

            return str;
        }
        /// <summary>
        /// 获取路径
        /// </summary>
        /// <returns></returns>
        public static string GetPathStr()
        {
            string connStr = null;
            var is64Bit = Environment.Is64BitOperatingSystem;
            if (is64Bit)
            {

                connStr = @"C:\Program Files (x86)\Microsoft\本鼎医保插件";
            }
            else
            {

                connStr = @"C:\Program Files\Microsoft\本鼎医保插件";
            }

            return connStr;
        }
        /// <summary>
        /// 获取路径
        /// </summary>
        /// <returns></returns>
        public static string GetPathWindowsStr()
        {
            string connStr = null;
            var is64Bit = Environment.Is64BitOperatingSystem;
            if (is64Bit)
            {

                connStr = @"C:\Windows\SysWOW64";
            }
            else
            {

                connStr = @"C:\Windows\System32";
            }

            return connStr;
        }
    }
}
