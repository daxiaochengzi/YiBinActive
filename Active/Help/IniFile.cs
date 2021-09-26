using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BenDingActive.Model.Dto.YiHai;
using Newtonsoft.Json;

namespace BenDingActive.Help
{
  public  class IniFile
    {
        public string path;

        public IniFile(string inIPath)
        {
            path = inIPath;
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);

        [DllImport("kernel32")]

        private static extern int GetPrivateProfileString(string section,
            string key, string def, StringBuilder retVal, int size, string filePath);

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }


        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);

            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);

            return temp.ToString();

        }

        /// <summary>
        /// 获取xml标头
        /// </summary>
        /// <returns></returns>
        public string GetIni()
        {
            var is64Bit = Environment.Is64BitOperatingSystem;
            path = is64Bit ? @"C:\Program Files (x86)\Microsoft\本鼎医保插件\BenDing.ini" : @"C:\Program Files\Microsoft\本鼎医保插件\BenDing.ini";
            IniFile myFile = new IniFile(path);
            var port = myFile.IniReadValue("BenDingSet", "Port");
            //myFile.IniWriteValue("BenDingSet", "Port","'222'");
            return port;
        }
        public void SaveAddress(string mac, string ip)
        {
          
            var is64Bit = Environment.Is64BitOperatingSystem;
            path = is64Bit ? @"C:\Program Files (x86)\Microsoft\本鼎医保插件\hnsi.ini" : @"C:\Program Files\Microsoft\本鼎医保插件\hnsi.ini";
            IniFile myFile = new IniFile(path);
            myFile.IniWriteValue("YinHaiSet", "ip", ip);
            myFile.IniWriteValue("YinHaiSet", "mac", mac);

        }

        public void ReadAddress(out string mac, out string ip)
        {
            mac = "";
            ip = "";
            var is64Bit = Environment.Is64BitOperatingSystem;
            path = is64Bit ? @"C:\Program Files (x86)\Microsoft\本鼎医保插件\hnsi.ini" : @"C:\Program Files\Microsoft\本鼎医保插件\hnsi.ini";
            IniFile myFile = new IniFile(path);
            mac = myFile.IniReadValue("YinHaiSet", "mac");
            ip = myFile.IniReadValue("YinHaiSet", "ip");
        }

        public string SetCardType( string cardType)
        {
            var is64Bit = Environment.Is64BitOperatingSystem;
            path = is64Bit ? @"C:\Program Files (x86)\Microsoft\本鼎医保插件\hnsi.ini" : @"C:\Program Files\Microsoft\本鼎医保插件\hnsi.ini";
            IniFile myFile = new IniFile(path);
            var port = myFile.IniReadValue("DLL", "dll");
            myFile.IniWriteValue("DLL", "dll", cardType);
            return port;
        }
        public string ReadCardType()
        {
            var is64Bit = Environment.Is64BitOperatingSystem;
            path = is64Bit ? @"C:\Program Files (x86)\Microsoft\本鼎医保插件\hnsi.ini" : @"C:\Program Files\Microsoft\本鼎医保插件\hnsi.ini";
            IniFile myFile = new IniFile(path);
            var port = myFile.IniReadValue("DLL", "dll");
          
            return port;
        }
        /// <summary>
        /// 读取密码键盘状态
        /// </summary>
        /// <returns></returns>
        public string ReadKeyPwd()
        {
            var is64Bit = Environment.Is64BitOperatingSystem;
            path = is64Bit ? @"C:\Program Files (x86)\Microsoft\本鼎医保插件\hnsi.ini" : @"C:\Program Files\Microsoft\本鼎医保插件\hnsi.ini";
            IniFile myFile = new IniFile(path);
            var port = myFile.IniReadValue("KEYBORD", "PASS");
            return port;
        }
        /// <summary>
        /// 设置是否使用密码键盘
        /// </summary>
        /// <param name="isUse"></param>
        /// <returns></returns>
        public void SetKeyPwd(int isUse)
        {
            var is64Bit = Environment.Is64BitOperatingSystem;
            path = is64Bit ? @"C:\Program Files (x86)\Microsoft\本鼎医保插件\hnsi.ini" : @"C:\Program Files\Microsoft\本鼎医保插件\hnsi.ini";
            IniFile myFile = new IniFile(path);
            myFile.IniWriteValue("KEYBORD", "PASS", isUse.ToString());

        }
        /// <summary>
        /// 获取电子医保凭证地址
        /// </summary>
        /// <returns></returns>
        public string NationEcTransUrl()
        {
            var is64Bit = Environment.Is64BitOperatingSystem;
            path = is64Bit ? @"C:\Program Files (x86)\Microsoft\本鼎医保插件\BenDing.ini" : @"C:\Program Files\Microsoft\本鼎医保插件\BenDing.ini";
            IniFile myFile = new IniFile(path);
            var port = myFile.IniReadValue("NationEcTrans", "Url");
            return port;
        }
        public string OutpatientResidentUrl()
        {
            var is64Bit = Environment.Is64BitOperatingSystem;
            path = is64Bit ? @"C:\Program Files (x86)\Microsoft\本鼎医保插件\BenDing.ini" : @"C:\Program Files\Microsoft\本鼎医保插件\BenDing.ini";
            IniFile myFile = new IniFile(path);
            var port = myFile.IniReadValue("OutpatientResident", "Url");
            return port;
        }
        public string YinHaiUrl()
        {
            var is64Bit = Environment.Is64BitOperatingSystem;
            path = is64Bit ? @"C:\Program Files (x86)\Microsoft\本鼎医保插件\BenDing.ini" : @"C:\Program Files\Microsoft\本鼎医保插件\BenDing.ini";
            IniFile myFile = new IniFile(path);
            var port = myFile.IniReadValue("YinHaiSet", "Url");
            return port;
        }
        public string YinHaiData( string codeNum)
        {
            var is64Bit = Environment.Is64BitOperatingSystem;
            path = is64Bit ? @"C:\Program Files (x86)\Microsoft\本鼎医保插件\BenDing.ini" : @"C:\Program Files\Microsoft\本鼎医保插件\BenDing.ini";
            IniFile myFile = new IniFile(path);
            var port = myFile.IniReadValue("YinHaiData", codeNum);
            return port;
        }
        public string YinHaiAddress()
        {
            var signInData = new SignInInputDataDto();
            var is64Bit = Environment.Is64BitOperatingSystem;
            path = is64Bit ? @"C:\Program Files (x86)\Microsoft\本鼎医保插件\BenDing.ini" : @"C:\Program Files\Microsoft\本鼎医保插件\BenDing.ini";
            IniFile myFile = new IniFile(path);
            var ip = myFile.IniReadValue("YinHaiSet", "ip");
            var mac = myFile.IniReadValue("YinHaiSet", "mac");
            signInData.ip = ip;
            signInData.mac = mac;
            return JsonConvert.SerializeObject(signInData);
          
        }
    }
}
