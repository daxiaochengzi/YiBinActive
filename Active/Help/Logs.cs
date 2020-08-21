using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Help
{
    public static class Logs
    {
        public static void LogWrite(LogParam param)
        {
            //string Is_msg = "";
            //string Is_day = DateTime.Now.Date.ToString("yyyy-MM-dd").Substring(0, 10);
            string path = null;
            var is64Bit = Environment.Is64BitOperatingSystem;
            if (is64Bit)
            {
                path = @"C:\Program Files (x86)\Microsoft\本鼎医保插件\";
            }
            else
            {
                path = @"C:\Program Files\Microsoft\本鼎医保插件\";
            }
            string pathError = path + "\\logs";
            string pathErrorData = path + "\\logs\\"+ DateTime.Now.Date.ToString("yyyy-MM-dd").Substring(0, 10);
            string pathErrorInfo = pathErrorData + "\\log.txt";
            if (!System.IO.Directory.Exists(pathError))
            {
                System.IO.Directory.CreateDirectory(pathError);
            }
            if (!System.IO.Directory.Exists(pathErrorData))
            {
                System.IO.Directory.CreateDirectory(pathErrorData);
            }

            if (!System.IO.File.Exists(pathErrorInfo))
            {
                FileStream fs1 = new FileStream(pathErrorInfo, FileMode.Create, FileAccess.Write); //创建写入文件 
                fs1.Close();
            }

            if (!string.IsNullOrWhiteSpace(param.Msg))
            {
                 string Is_time_fish = null;
                 Is_time_fish = "【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】"+
                              "【OperatorCode】" + param.OperatorCode +"【msg】" +param.Msg;
                if (!string.IsNullOrWhiteSpace(param.Params))
                    Is_time_fish += "【Params】" + param.Params;
                if (!string.IsNullOrWhiteSpace(param.ResultData))
                {
                    Is_time_fish += "【ResultData】" + param.ResultData;
                }

              

                StreamWriter sw = File.AppendText(pathErrorInfo);
                // //获得字节数组
                string data = System.Text.Encoding.Default.GetBytes(Is_time_fish).ToString();
                // //开始写入
                sw.WriteLine(Is_time_fish.ToString());
                // //清空缓冲区、关闭流
                sw.Flush();
                sw.Close();
            }


        }
        public static void LogWriteData(LogWriteDataParam param)
        {
            string sql = $@"INSERT INTO Data (OperatorId, JoinJson, ReturnJson,TransactionCode,CreateTime)
                 VALUES ('{param.OperatorId}', '{param.JoinJson}', '{param.ReturnJson}','{param.TransactionCode}','{ DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
            SqLiteHelper.ExecuteNonQuery(CommonHelp.GetConnStr(), sql, CommandType.Text);
            string path = null;
            var is64Bit = Environment.Is64BitOperatingSystem;
            if (is64Bit)
            {
                path = @"C:\Program Files (x86)\Microsoft\本鼎医保插件\";
            }
            else
            {
                path = @"C:\Program Files\Microsoft\本鼎医保插件\";
            }

            string pathError = path + "\\logs";
            string pathErrorData = path + "\\logs\\" + DateTime.Now.Date.ToString("yyyy-MM-dd").Substring(0, 10);
            string pathErrorInfo = pathErrorData + "\\logData.txt";
            if (!System.IO.Directory.Exists(pathError))
            {
                System.IO.Directory.CreateDirectory(pathError);
            }
            if (!System.IO.Directory.Exists(pathErrorData))
            {
                System.IO.Directory.CreateDirectory(pathErrorData);
            }

            if (!System.IO.File.Exists(pathErrorInfo))
            {
                FileStream fs1 = new FileStream(pathErrorInfo, FileMode.Create, FileAccess.Write); //创建写入文件 
                fs1.Close();
            }

            if (!string.IsNullOrWhiteSpace(param.JoinJson))
            {
                string Is_time_fish = null;
                Is_time_fish = "【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】";
                if (!string.IsNullOrWhiteSpace(param.OperatorId))
                    Is_time_fish += "【OperatorId】" + param.OperatorId;
                if (!string.IsNullOrWhiteSpace(param.JoinJson))
                    Is_time_fish += "【Params】" + param.JoinJson;
                if (!string.IsNullOrWhiteSpace(param.ReturnJson))
                    Is_time_fish += "【ResultData】" + param.ReturnJson;
                StreamWriter sw = File.AppendText(pathErrorInfo);
                // //获得字节数组
                string data = System.Text.Encoding.Default.GetBytes(Is_time_fish).ToString();
                // //开始写入
                sw.WriteLine(Is_time_fish.ToString());
                // //清空缓冲区、关闭流
                sw.Flush();
                sw.Close();
            }


        }
        public static void LogErrorWrite(LogParam param)
        {
            string sql = $@"INSERT INTO DataError (OperatorId, JoinJson, ReturnJson,TransactionCode,CreateTime)
                 VALUES ('{param.OperatorCode}', '{param.Msg}', '{param.ResultData}','{param.TransactionCode}','{ DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
            SqLiteHelper.ExecuteNonQuery(CommonHelp.GetConnStr(), sql, CommandType.Text);
            //string Is_msg = "";
            //string Is_day = DateTime.Now.Date.ToString("yyyy-MM-dd").Substring(0, 10);
            string path = null;
            var is64Bit = Environment.Is64BitOperatingSystem;
            if (is64Bit)
            {
                path = @"C:\Program Files (x86)\Microsoft\本鼎医保插件\";
            }
            else
            {
                path = @"C:\Program Files\Microsoft\本鼎医保插件\";
            }
            string pathError = path + "\\logs";
            string pathErrorData = path + "\\logs\\" + DateTime.Now.Date.ToString("yyyy-MM-dd").Substring(0, 10);
            string pathErrorInfo = pathErrorData + "\\Error.txt";
            if (!System.IO.Directory.Exists(pathError))
            {
                System.IO.Directory.CreateDirectory(pathError);
            }
            if (!System.IO.Directory.Exists(pathErrorData))
            {
                System.IO.Directory.CreateDirectory(pathErrorData);
            }

            if (!System.IO.File.Exists(pathErrorInfo))
            {
                FileStream fs1 = new FileStream(pathErrorInfo, FileMode.Create, FileAccess.Write); //创建写入文件 
                fs1.Close();
            }

            if (!string.IsNullOrWhiteSpace(param.Msg))
            {
                string Is_time_fish = null;
                Is_time_fish = "【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】" +
                             "【OperatorCode】" + param.OperatorCode + "【msg】" + param.Msg;
                if (!string.IsNullOrWhiteSpace(param.Params))
                    Is_time_fish += "【Params】" + param.Params;
                if (!string.IsNullOrWhiteSpace(param.ResultData))
                {
                    Is_time_fish += "【ResultData】" + param.ResultData;
                }



                StreamWriter sw = File.AppendText(pathErrorInfo);
                // //获得字节数组
                string data = System.Text.Encoding.Default.GetBytes(Is_time_fish).ToString();
                // //开始写入
                sw.WriteLine(Is_time_fish.ToString());
                // //清空缓冲区、关闭流
                sw.Flush();
                sw.Close();
            }


        }
        public static string ToJson<T>(T t)
        {
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(t);
            return result;
           
        }
    }
}
