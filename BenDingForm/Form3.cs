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
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BenDingActive.Model.Dto.YiHai;
using BenDingActive.Model.Params.Service;
using BenDingActive.Model.Params.YinHai;
using Newtonsoft.Json;

namespace BenDingForm
{
    public partial class Form3 : Form
    {
        //安全控件初始化参数
        private string secureMediaData = null;
        private SecureMediaOutputDto secureMediaIni = null;
        public Form3()
        {
            InitializeComponent();
            textBox2.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            var secureMedia = new SecureMediaDto()
            {
                data = new SecureMediaDataDto()
            };

            secureMediaData = JsonConvert.SerializeObject(secureMedia);
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
            //string url = "http://10.109.120.206:8080/mss/web/api/fsi/callService";
            //string msg = "";
            //var paramData = GetBaseParam("1101", lab_sign_no.Text);
            //var ddd = new PersonInputDto()
            //{
            //    card_sn = "",
            //    certno = "511023197201145538",
            //    psn_name = "周雪松",
            //    mdtrt_cert_no = "511023197201145538",
            //    mdtrt_cert_type = "02",
            //    psn_cert_type = "1",

            //};
            //paramData.input = ddd;
            //var postParam = JsonConvert.SerializeObject(paramData);
            //txt_Input.Text = "";
            //txt_Input.Text = postParam;
            //var resultDataText = PostWebRequest(url, postParam);
            //txt_Output.Text = resultDataText;

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

        private void button3_Click(object sender, EventArgs e)
        {
            Logs.LogWriteData(new LogWriteDataParam()
            {
                JoinJson ="123",
                ReturnJson = "123123",
                OperatorId ="12312",
                TransactionCode = "WorkerHospitalizationSettlement"
            });
        }
        /// <summary>
        /// Post提交数据
        /// </summary>
        /// <param name="postUrl">URL</param>
        /// <param name="paramData">参数</param>
        /// <returns></returns>
        private string PostWebRequest(string postUrl, string paramData)
        {
            string ret = string.Empty;
            try
            {
                if (!postUrl.StartsWith("http://"))
                    return "";

                byte[] byteArray = Encoding.Default.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/json";
                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
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

        private YinHaiGetBaseParam GetBaseParam(string infno,string sign_no)
        {
            string url = "http://10.109.120.206:8080/mss/web/api/fsi/callService";
            var iniParam = new YinHaiGetBaseParam()
            {
                msgid = "H51200200049" + DateTime.Now.ToString("yyyyMMddHHmmss") + "9001",
                infno = infno,
                sign_no= sign_no
            };
            //输入参数
            var inputData = new SignInInputDto();
            //输入数据
            var data = new SignInInputDataDto()
            {
                opter_no = "N511527007263",
                ip = "192.168.71.1",
                mac = "08-62-66-0C-D8-47"
            };
            inputData.signIn = data;
            
            return iniParam;
        }

        private void btn_Signin_Click(object sender, EventArgs e)
        {
            string url = "http://10.109.120.206:8080/mss/web/api/fsi/callService";
            var iniParm = new YinHaiGetBaseParam()
            {
                msgid = "H51200200049" + DateTime.Now.ToString("yyyyMMddHHmmss") + "9001",
                infno = "9001"
            };
            //输入参数
            var inputData = new SignInInputDto();
            //输入数据
            var data = new SignInInputDataDto()
            {
                opter_no = "N511527007263",
                ip = "192.168.71.1",
                mac = "08-62-66-0C-D8-47"
            };
            inputData.signIn = data;
            iniParm.input = inputData;

            var postParam = JsonConvert.SerializeObject(iniParm);
            txt_Input.Text = postParam;
            var resultDataText = PostWebRequest(url, postParam);
            txt_Output.Text = resultDataText;
            var resultData= JsonConvert.DeserializeObject<YinHaiOutBaseParam>(resultDataText);
            if (resultData.infcode == "0")
            {
                var output = resultData.output;
                var outputData = JsonConvert.DeserializeObject<SignInOutputDto>(output.ToString());
                lab_sign_no.Text = outputData.signinoutb.sign_no;
            }
            else
            {
                MessageBox.Show("签到失败!!!");
            }

           
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            var ddd = new OutpatientSettlementInputDto();
            var dddData = new OutpatientSettlementInputDataDto();

            dddData.psn_no = "123123";
            dddData.mdtrt_cert_type = "02";
            dddData.mdtrt_cert_no = "511023197201145538";
            dddData.med_type = "11";
            dddData.medfee_sumamt = 1;
            dddData.psn_setlway = "01";
            dddData.acct_used_flag = "01";
            dddData.card_token = "";
            ddd.data = dddData;
            string url = "http://10.109.120.206:8080/mss/web/api/fsi/callService";
            string msg = "";
            var paramData = GetBaseParam("2207", lab_sign_no.Text);
           
            paramData.input = ddd;
            var postParam = JsonConvert.SerializeObject(paramData);
            txt_Input.Text = "";
            txt_Input.Text = postParam;
            var resultDataText = PostWebRequest(url, postParam);
            txt_Output.Text = resultDataText;
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string msg = "";
            string iniMsg = "";
            var resultData = YinHaiCOM.Init(out iniMsg);
            YinHaiCOM.yh_CHS_call(txt_Transaction_Code.Text, secureMediaData, ref msg);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                 secureMediaIni = JsonConvert.DeserializeObject<SecureMediaOutputDto>(msg);
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

        private void button3_Click_3(object sender, EventArgs e)
        {
            string url = "http://10.109.120.206:8080/mss/web/api/fsi/callService";
            string msg = "";
            var paramData = GetBaseParam("1101", lab_sign_no.Text);
         
            if (secureMediaIni == null)
            {
                MessageBox.Show("安全介质为空!!!");
            }
            else
            {
                var tokenData = new PersonInputCardTokenDto()
                {
                    card_token = secureMediaIni.data.card_token
                };
                paramData.insuplc_admdvs = secureMediaIni.data.insuplc_admdvs;
                var personInput = new PersonInputDto();
               
                var personInputData = new PersonInputDataDto
                {
                    card_sn = secureMediaIni.data.card_sn,
                    certno = secureMediaIni.data.certno,
                    psn_name = "陈静",
                    mdtrt_cert_no = secureMediaIni.data.mdtrt_cert_no,
                    mdtrt_cert_type = secureMediaIni.data.mdtrt_cert_type,
                    psn_cert_type = secureMediaIni.data.psn_cert_type,
                    expContent = tokenData,
                    begntime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                personInput.data = personInputData;
                paramData.input = personInput;
                var postParam = JsonConvert.SerializeObject(paramData);
                txt_Input.Text = "";
                txt_Input.Text = postParam;
                var resultDataText = PostWebRequest(url, postParam);
                txt_Output.Text = resultDataText;
            }

           
        }
    }

}


