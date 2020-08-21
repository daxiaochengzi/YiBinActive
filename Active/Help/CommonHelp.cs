using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace BenDingActive.Help
{
   public static class CommonHelp
    {
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
    }
}
