using BenDingActive;
using BenDingActive.Help;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenDingForm
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void btn_ini_Click(object sender, EventArgs e)
        {
            string msg = "";
            var resultData = YinHaiCOM.Init(out msg);
            if (resultData)
            {
                MessageBox.Show("初始化成功!!!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg = "";
            YinHaiCOM.yh_CHS_call(txt_Transaction_Code.Text, txt_Input.Text.Trim(), ref msg);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = txt_Transaction_Code.Text,
                    ReturnJson = msg,
                    OperatorId = "",
                    TransactionCode = txt_Input.Text.Trim()
                });
                txt_Output.Text = msg;
            }
        }
        public void CopyDirectory(string scrPath, string savePath)
        {
            if (Directory.Exists(scrPath))//检查路径(目录)是否存在
            {
                if (!Directory.Exists(savePath))
                    Directory.CreateDirectory(savePath);
                //string[] sdd = DateTime.Now.GetDateTimeFormats();
                //string subSavePath = savePath + "\\" + DateTime.Now.GetDateTimeFormats()[10];
                string subSavePath = savePath + "\\";
                Directory.CreateDirectory(savePath);

                string[] aFiles = Directory.GetFiles(scrPath);
                string[] aDirectory = Directory.GetDirectories(scrPath);
                for (int i = 0; i < aFiles.Length; i++)
                {
                    FileInfo fi = new FileInfo(aFiles[i]);
                    long fileSize = fi.Length;//文件大小

                    File.Copy(aFiles[i], subSavePath + "\\" + fi.Name);
                }
                if (aDirectory.Length != 0)
                {
                    for (int i = 0; i < aDirectory.Length; i++)
                    {
                        string aName = aDirectory[i].Substring(aDirectory[i].LastIndexOf('\\'));
                        CopyDirectory(aDirectory[i], subSavePath + aName);
                    }
                }
            }
        }
        public void CopyDireToDire(string sourceDire, string destDire, string backupsDire = null)
        {
            if (Directory.Exists(sourceDire) && Directory.Exists(destDire))
            {
                DirectoryInfo sourceDireInfo = new DirectoryInfo(sourceDire);
                FileInfo[] fileInfos = sourceDireInfo.GetFiles();
                foreach (FileInfo fInfo in fileInfos)
                {
                    string sourceFile = fInfo.FullName;
                    string destFile = sourceFile.Replace(sourceDire, destDire);
                    if (backupsDire != null && File.Exists(destFile))
                    {
                        Directory.CreateDirectory(backupsDire);
                        string backFile = destFile.Replace(destDire, backupsDire);
                        File.Copy(destFile, backFile, true);
                    }
                    File.Copy(sourceFile, destFile, true);
                }
                DirectoryInfo[] direInfos = sourceDireInfo.GetDirectories();
                foreach (DirectoryInfo dInfo in direInfos)
                {
                    string sourceDire2 = dInfo.FullName;
                    string destDire2 = sourceDire2.Replace(sourceDire, destDire);
                    string backupsDire2 = null;
                    if (backupsDire != null)
                    {
                        backupsDire2 = sourceDire2.Replace(sourceDire, backupsDire);
                    }
                    Directory.CreateDirectory(destDire2);
                    CopyDireToDire(sourceDire2, destDire2, backupsDire2);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var scrPath = CommonHelp.GetPathStr() + "\\securityDLL";
                var savePath = CommonHelp.GetPathWindowsStr();
                CopyDireToDire(scrPath, savePath);
                var result= RegisterDll();
                if (result == true)
                {
                    MessageBox.Show("程序初始化注册成功!!!");
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
           
        }
        private bool RegisterDll()
        {
            bool result = true;
            try
            {
                var savePath = CommonHelp.GetPathWindowsStr();
                string dllPath = Path.Combine(savePath, "yh_interface_chs.dll");//获得要注册的dll的物理路径
                if (!File.Exists(dllPath))
                {
                    MessageBox.Show(string.Format("“{0}”目录下无 yh_interface_chs.dll文件", savePath));
                    //Loger.Write(string.Format("“{0}”目录下无“XXX.dll”文件！", AppDomain.CurrentDomain.BaseDirectory));
                    return false;
                }
                //拼接命令参数
                string startArgs = string.Format("/s \"{0}\"", dllPath);

                Process p = new Process();//创建一个新进程，以执行注册动作
                p.StartInfo.FileName = "regsvr32";
                p.StartInfo.Arguments = startArgs;

                //以管理员权限注册dll文件
                WindowsIdentity winIdentity = WindowsIdentity.GetCurrent(); //引用命名空间 System.Security.Principal
                WindowsPrincipal winPrincipal = new WindowsPrincipal(winIdentity);
                if (!winPrincipal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    p.StartInfo.Verb = "runas";//管理员权限运行
                }
                p.Start();
                p.WaitForExit();
                p.Close();
                p.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                result = false;　　　　　　　　  //记录日志，抛出异常
            }

            return result;
        }
    }

}


