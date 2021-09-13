using BenDingActive;
using BenDingActive.Help;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using BenDingActive.Model.Dto.YiHai.Hospital;
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
            string url = "http://10.109.120.206:8080/mss/web/api/fsi/callService";
            string msg = "";
           
           var resultDataText = PostWebRequest(url, txt_Input.Text);
            txt_Output.Text = resultDataText;
            var resultData = JsonConvert.DeserializeObject<YinHaiOutBaseParam>(resultDataText);
            if (resultData.infcode == "0")
            {
                var output = resultData.output;
                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = txt_Input.Text.Trim(),
                    ReturnJson = output!=null?resultData.output.ToString():"",
                    OperatorId = "",
                    TransactionCode = txt_Transaction_Code.Text 
                });
            }
            else
            {
                MessageBox.Show("操作失败!!!");
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
                msgid = "H51202100005" + DateTime.Now.ToString("yyyyMMddHHmmss") + "9001",
                infno = "9001"
            };
            //输入参数
            var inputData = new SignInInputDto();
            //输入数据
            var data = new SignInInputDataDto()
            {
                opter_no = "01100",
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



        private void button3_Click_2(object sender, EventArgs e)
        {
            
            var dddData = new OutpatientSettlementInputDataDto();

            dddData.psn_no = "123123";
            dddData.mdtrt_cert_type = "02";
            dddData.mdtrt_cert_no = "511023197201145538";
            dddData.med_type = "11";
            dddData.medfee_sumamt = 1;
            dddData.psn_setlway = "01";
            dddData.acct_used_flag = "01";
            dddData.card_token = "";
            string url = "http://10.109.120.206:8080/mss/web/api/fsi/callService";
            string msg = "";
            var paramData = GetBaseParam("2207", lab_sign_no.Text);
           
            paramData.input = new {data= dddData };
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
            YinHaiCOM.yh_CHS_call("2304A", secureMediaData, ref msg);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                 secureMediaIni = JsonConvert.DeserializeObject<SecureMediaOutputDto>(msg);
                Logs.LogWriteData(new LogWriteDataParam()
                {
                    JoinJson = secureMediaData,
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
                    psn_name = txt_PatientName.Text,
                    mdtrt_cert_no = secureMediaIni.data.mdtrt_cert_no,
                    mdtrt_cert_type = secureMediaIni.data.mdtrt_cert_type,
                    psn_cert_type = secureMediaIni.data.psn_cert_type,
                    expContent = tokenData,
                    begntime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
             
                personInput.data = personInputData;
                paramData.input = new {data = personInputData};
                var postParam = JsonConvert.SerializeObject(paramData);
                txt_Input.Text = "";
                txt_Input.Text = postParam;
                var resultDataText = PostWebRequest(url, postParam);
                txt_Output.Text = resultDataText;
            }

           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2201", lab_sign_no.Text);
            var registerParam = new OutpatientRegisterInputDataParam()
            {
                expContent =new { card_token = secureMediaIni .data.card_token},
                mdtrt_cert_no = secureMediaIni.data.mdtrt_cert_no,
                dept_code = "A03",
                caty = "A03",
                mdtrt_cert_type = secureMediaIni.data.mdtrt_cert_type,
                atddr_no = "777",
                dr_name = "6666",
                dept_name = "内科",
                ipt_otp_no = "12323",
                psn_no = "51000051200000512099000007",
                begntime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                insutype  = "310"
            };

            var data = new {data = registerParam};
            paramData.input = data;
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
          
          
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            var paramData = GetBaseParam("2202", lab_sign_no.Text);
            var registerParam = new OutpatientRegisterCancelInputDataDto()
            {
                expContent = new { card_token = secureMediaIni.data.card_token },
               
                ipt_otp_no = "12323",
                psn_no = "51000051200000512099000007",
                mdtrt_id= "512000G0000000382104",


            };

            var data = new { data = registerParam };
            paramData.input = data;
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2203A", lab_sign_no.Text);
            //输入参数
            var inputData = new InformationUploadInputDto();
            var mdtrtinfo = new InformationUploadInputMdtrtinfoDto()
            { 
                mdtrt_id = "512000G0000000382130",
                psn_no = "51000051200000512099000007",
                med_type = "11",
                begntime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                main_cond_dscr="心烦意燥",
                dise_codg= "N05.900x003",
                dise_name = "肾炎",
                expContent = new { card_token = secureMediaIni.data.card_token },
                birctrl_type="",
            };
            var diseinfo = new List<YinHaiBaseIniDiseinfo>();

            var diseinfoData = new YinHaiBaseIniDiseinfo()
            {   diag_type="0",
                diag_code = "N05.900x003",
                diag_name = "肾炎",
                diag_srt_no = 1,
                diag_time = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss"),
                dise_dor_name ="111",
                dise_dor_no = "测试",
                diag_dept= "内科"

            };
            diseinfo.Add(diseinfoData);
            inputData.mdtrtinfo = mdtrtinfo;
            inputData.diseinfo = diseinfo;
            paramData.input = inputData;
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2204", lab_sign_no.Text);
            var inputData=new List<OutpatientFeeUploadfeedetailInput>();
            var inputFeeData = new OutpatientFeeUploadfeedetailInput()
            {
                feedetl_sn = "452118608000M202104040021",
                mdtrt_id= "512000G0000000382130",
                psn_no= "51000051200000512099000007",
                chrg_bchno="1111",
                dise_codg= "N05.900x003",
                rxno="",
                rx_circ_flag="0",
                fee_ocur_time=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                med_list_codg= "ZI02AAN0001020103906",
                medins_list_codg = "C30B4454648F4C4098F770C1A6DD0363",
                det_item_fee_sumamt=15,
                cnt=10,
                pric=Convert.ToDecimal(1.5) ,
                sin_dos_dscr="",
                used_frqu_dscr="",
                prd_days=0,
                medc_way_dscr="",
               
                bilg_dept_codg="A03",
                bilg_dept_name="内科",
                bilg_dr_codg="1111",
                bilg_dr_name="444",
                hosp_appr_flag="1",
                expContent= new { card_token = secureMediaIni.data.card_token }


            };
            inputData.Add(inputFeeData);
            paramData.input = new { feedetail = inputData };
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2206A", lab_sign_no.Text);
            var inputData = new OutpatientPreSettlementDataInputDto()
            {
                expContent = new { card_token = secureMediaIni.data.card_token },
                psn_no= "51000051200000512099000007",
                mdtrt_cert_type= secureMediaIni.data.mdtrt_cert_type,
                mdtrt_cert_no = secureMediaIni.data.mdtrt_cert_no,
                med_type="11",
                medfee_sumamt=15,
                psn_setlway ="01",
                mdtrt_id= "512000G0000000382130",
                chrg_bchno= "1111",
                acct_used_flag="0",
                insutype="310"


            };
           
            paramData.input = new { data = inputData };
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2207A", lab_sign_no.Text);
            var inputData = new OutpatientSettlementInputDataDto()
            {
                expContent = new { card_token = secureMediaIni.data.card_token },
                psn_no = "51000051200000512099000007",
                mdtrt_cert_type = secureMediaIni.data.mdtrt_cert_type,
                mdtrt_cert_no = secureMediaIni.data.mdtrt_cert_no,
                med_type = "11",
                medfee_sumamt = 15,
                psn_setlway = "01",
                mdtrt_id = "512000G0000000382130",
                chrg_bchno = "1111",
                acct_used_flag = "0",
                insutype = "310",
                invono="K262489735",
                inscp_scp_amt=0,
                fulamt_ownpay_amt=15,
                overlmt_selfpay=0,
                preselfpay_amt=0

            };

            paramData.input = new { data = inputData };
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2208", lab_sign_no.Text);
            var inputData = new OutpatientCancelSettlementInputDataDto()
            {
                expContent = new { card_token = secureMediaIni.data.card_token },
                psn_no = "51000051200000512099000007",
                mdtrt_id = "512000G0000000382130",
                setl_id= "512000G0000000349439"

            };

            paramData.input = new { data = inputData };
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2401", lab_sign_no.Text);
            //输入参数
            var InputData = new HospitalRegisterInputDto();
            var mdtrtinfo = new HospitalRegisterInputmdtrtinfoDto()
            {   psn_no= "51000051200000512099025082",
                adm_bed = "33",
                begntime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss"),
                adm_dept_codg ="A03",
                adm_dept_name = "内科",
                atddr_no = "4861632382718291017",
                chfpdr_name ="李茜",
                dscg_maindiag_code = "N05.900x003",
                dscg_maindiag_name = "肾炎",
                ipt_no = "100120210223011",
                adm_diag_dscr= "4861632382718291017",
                main_cond_dscr= "肾炎",
                //birctrl_matn_date = param.birctrl_matn_date,
                //fetts = param.fetts,
                //fetus_cnt = param.fetus_cnt,
                //geso_val = param.geso_val,
                //psn_no = param.psn_no,
                insutype ="310",
                //pret_flag = param.pret_flag,
                mdtrt_cert_type = secureMediaIni.data.mdtrt_cert_type,
                mdtrt_cert_no = secureMediaIni.data.mdtrt_cert_no,
                med_type = "21",
                expContent = new { card_token = secureMediaIni.data.card_token },
            };
            var diseinfo = new List<YinHaiBaseIniDiseinfo>();

            var diseinfoData = new YinHaiBaseIniDiseinfo()
            {   psn_no= "51000051200000512099025082",
                diag_type = "1",
                diag_code = "N05.900x003",
                diag_name = "肾炎",
                diag_srt_no = 0,
                diag_time = Convert.ToDateTime(DateTime.Now.AddDays(-1)).ToString("yyyy-MM-dd HH:mm:ss"),
                dise_dor_name = "李茜",
                dise_dor_no = "4861632382718291017",
                diag_dept = "内科",
                maindiag_flag = "1",
            };
            diseinfo.Add(diseinfoData);
            InputData.diseinfo = diseinfo;
            InputData.mdtrtinfo = mdtrtinfo;
            paramData.input = InputData;
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var iniFile = new IniFile("");
            var url = iniFile.YinHaiData("9001");
        }

        private void btn_SigninQuery_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2402", lab_sign_no.Text);
            var mdtrtinfo = new LeaveHospitalInputDscgInfoDto()
            {   mdtrt_id= "512000G0000000382203",
                psn_no = "51000051200000512099025082",
                insutype = "310",
                endtime= Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss"),
                dscg_dept_codg="A03",
                dscg_dept_name="内科",
              
                dscg_way="1"
            };
            var diseinfo = new List<YinHaiBaseIniDiseinfo>();
            var diseinfoData = new YinHaiBaseIniDiseinfo()
            {
                mdtrt_id = "512000G0000000382203",
                psn_no = "51000051200000512099025082",
                diag_type = "1",
                diag_code = "N05.900x003",
                diag_name = "肾炎",
                diag_srt_no = 0,
                diag_time = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss"),
                dise_dor_name = "李茜",
                dise_dor_no = "4861632382718291017",
                diag_dept = "内科",
                maindiag_flag = "1",
            };
            diseinfo.Add(diseinfoData);
           
            paramData.input = new { dscginfo= mdtrtinfo , diseinfo = diseinfo };
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2301", lab_sign_no.Text);
            var inputData = new List<UploadHospitalFeeInputRowDto>();
            var inputFeeData = new UploadHospitalFeeInputRowDto()
            {
                feedetl_sn = "202105161100262",
                mdtrt_id = "512000G0000000382203",
                psn_no = "51000051200000512099025082",
                med_type="21",
                fee_ocur_time = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss"),
                med_list_codg = "ZI02AAN0001020103906",
                medins_list_codg = "0997F11C9A6243BBB8E05204C29B7218",
                det_item_fee_sumamt = 15,
                cnt = 10,
                pric = Convert.ToDecimal(1.5),
                bilg_dept_codg = "A03",
                bilg_dept_name = "内科",
                bilg_dr_codg = "4861632382718291017",
                bilg_dr_name = "李茜",
                hosp_appr_flag = "1"
             


            };
            inputData.Add(inputFeeData);
            paramData.input = new { feedetail = inputData };
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2405", lab_sign_no.Text);
            var data = new GetCancelLeaveHospitalInputDataDto()
            {
                mdtrt_id= "512000G0000000382203",
                psn_no = "51000051200000512099025082"
            };
            paramData.input = new { data = data };
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2303", lab_sign_no.Text);
            var inputData = new HospitalPreSettlementInputDataDto()
            {
                psn_no = "51000051200000512099025082",
                mdtrt_cert_type = secureMediaIni.data.mdtrt_cert_type,
                mdtrt_cert_no = secureMediaIni.data.mdtrt_cert_no,
                medfee_sumamt = 15,
                psn_setlway = "01",
                mdtrt_id = "512000G0000000382203",
                insutype = "310",
                mid_setl_flag="0",
                acct_used_flag="0",
                expContent = new { card_token = secureMediaIni.data.card_token },
  
            };

            paramData.input = new { data = inputData };
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2304", lab_sign_no.Text);
            var inputData = new HospitalPreSettlementInputDataDto()
            {
                psn_no = "51000051200000512099025082",
                mdtrt_cert_type = secureMediaIni.data.mdtrt_cert_type,
                mdtrt_cert_no = secureMediaIni.data.mdtrt_cert_no,
                medfee_sumamt = 15,
                psn_setlway = "01",
                mdtrt_id = "512000G0000000382203",
                insutype = "310",
                mid_setl_flag = "0",
                acct_used_flag = "0",
                expContent = new { card_token = secureMediaIni.data.card_token },
            };

            paramData.input = new { data = inputData };
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2305", lab_sign_no.Text);
            var data = new GetHospitalCancelSettlementInputDataDto()
            {
                mdtrt_id = "512000G0000000382203",
                psn_no = "51000051200000512099025082",
                setl_id= "512000G0000000349477"
            };
            paramData.input = new { data = data };
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            string url = "http://10.109.120.206:8080/mss/web/api/fsi/callService";
           
          
                try
                {
                    HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                    HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                    Stream st = myrp.GetResponseStream();
                    Stream so = new System.IO.FileStream("www", System.IO.FileMode.Create);
                    byte[] by = new byte[1024];
                    int osize = st.Read(by, 0, (int)by.Length);
                    while (osize > 0)
                    {
                        so.Write(by, 0, osize);
                        osize = st.Read(by, 0, (int)by.Length);
                    }
                    so.Close();
                    st.Close();
                    myrp.Close();
                    Myrq.Abort();
                    
                }
                catch (System.Exception exx)
                {
                    
                }
            
        }
    }

}


