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
using BenDingActive.Model.Dto.Bend;
using BenDingActive.Model.Dto.YiHai;
using BenDingActive.Model.Dto.YiHai.Hospital;
using BenDingActive.Model.Params.Service;
using BenDingActive.Model.Params.YinHai;
using Newtonsoft.Json;
using BenDingActive.Model.Dto.YiHai.comm;

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
            //string url = "http://10.109.120.206:8080/mss/web/api/fsi/callService";
            //string url = "http://10.109.103.38:8080/mss/web/api/fsi/callService";
            string url = CommonHelp.GetWebServiceUrl();
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
                    ReturnJson = output != null ? resultData.output.ToString() : "",
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
            if (Directory.Exists(scrPath)) //检查路径(目录)是否存在
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
                    long fileSize = fi.Length; //文件大小

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
                var result = RegisterDll();
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
                string dllPath = Path.Combine(savePath, "yh_interface_chs.dll"); //获得要注册的dll的物理路径
                if (!File.Exists(dllPath))
                {
                    MessageBox.Show(string.Format("“{0}”目录下无 yh_interface_chs.dll文件", savePath));
                    //Loger.Write(string.Format("“{0}”目录下无“XXX.dll”文件！", AppDomain.CurrentDomain.BaseDirectory));
                    return false;
                }

                //拼接命令参数
                string startArgs = string.Format("/s \"{0}\"", dllPath);

                Process p = new Process(); //创建一个新进程，以执行注册动作
                p.StartInfo.FileName = "regsvr32";
                p.StartInfo.Arguments = startArgs;

                //以管理员权限注册dll文件
                WindowsIdentity winIdentity = WindowsIdentity.GetCurrent(); //引用命名空间 System.Security.Principal
                WindowsPrincipal winPrincipal = new WindowsPrincipal(winIdentity);
                if (!winPrincipal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    p.StartInfo.Verb = "runas"; //管理员权限运行
                }

                p.Start();
                p.WaitForExit();
                p.Close();
                p.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                result = false; //记录日志，抛出异常
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
                HttpWebRequest webReq = (HttpWebRequest) WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/json";
                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length); //写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse) webReq.GetResponse();
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

        public static string PostWebReq(string PostUrl, string ParamData, Encoding DataEncode)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = DataEncode.GetBytes(ParamData);
                HttpWebRequest webReq = (HttpWebRequest) WebRequest.Create(new Uri(PostUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/json";
                webReq.ContentLength = byteArray.Length;

                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse) webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), DataEncode);
                ret = sr.ReadToEnd();

                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (WebException ex)
            {

            }
            finally
            {

            }

            return ret;
        }

        private YinHaiGetBaseParam GetBaseParam(string infno, string sign_no)
        {
            //string url = "http://10.109.120.206:8080/mss/web/api/fsi/callService";
            //string url = "http://10.109.103.38:8080/mss/web/api/fsi/callService";
            string url = CommonHelp.GetWebServiceUrl();
            var iniParam = new YinHaiGetBaseParam()
            {
                msgid = textBox2.Text + DateTime.Now.ToString("yyyyMMddHHmmss") + "9001",
                infno = infno,
                fixmedins_code = textBox2.Text,
                fixmedins_name = textBox3.Text,
                mdtrtarea_admvs = textBox4.Text,
                insuplc_admdvs = textBox4.Text,
                sign_no = sign_no
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
            //string url = "http://10.109.120.206:8080/mss/web/api/fsi/callService";
            //string url = "http://10.109.103.38:8080/mss/web/api/fsi/callService";

            string url = CommonHelp.GetWebServiceUrl();
            var iniParm = new YinHaiGetBaseParam()
            {
                msgid = textBox2.Text + DateTime.Now.ToString("yyyyMMddHHmmss") + "9001",
                infno = "9001",
                fixmedins_code = textBox2.Text,
                fixmedins_name=textBox3.Text,
                mdtrtarea_admvs= textBox4.Text,
                insuplc_admdvs= textBox4.Text
            };
            //输入参数
            var inputData = new SignInInputDto();
            ////输入数据
            //var data = new SignInInputDataDto()
            //{
            //    opter_no = "01100",
            //    ip = "192.168.71.1",
            //    mac = "08-62-66-0C-D8-47"
            //};

            var data = new SignInInputDataDto()
            {
                opter_no = "1231231232",
                ip = "192.168.7.69",
                mac = "AC-B5-7D-94-C0-C7"
            };

            inputData.signIn = data;
            iniParm.input = inputData;

            var postParam = JsonConvert.SerializeObject(iniParm);
            txt_Input.Text = postParam;
            var resultDataText = PostWebRequest(url, postParam);
            txt_Output.Text = resultDataText;
            var resultData = JsonConvert.DeserializeObject<YinHaiOutBaseParam>(resultDataText);
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

            paramData.input = new {data = dddData};
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
            //2304A
            YinHaiCOM.yh_CHS_call("2207A", secureMediaData, ref msg);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                secureMediaIni = JsonConvert.DeserializeObject<SecureMediaOutputDto>(msg);
                //Logs.LogWriteData(new LogWriteDataParam()
                //{
                //    JoinJson = secureMediaData,
                //    ReturnJson = msg,
                //    OperatorId = "",
                //    TransactionCode = txt_Input.Text.Trim()
                //});
                txt_Output.Text = msg;
            }
        }

        private void button3_Click_3(object sender, EventArgs e)
        {
            //string url = "http://10.109.120.206:8080/mss/web/api/fsi/callService";
            //string url = "http://10.109.122.89:8080/mss/web/api/fsi/callService";
            string url = CommonHelp.GetWebServiceUrl();
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
                expContent = new {card_token = secureMediaIni.data.card_token},
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
                insutype = "310"
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
                expContent = new {card_token = secureMediaIni.data.card_token},

                ipt_otp_no = "51000051150000001013871352",
                psn_no = "51000051150000001013871352",
                mdtrt_id = "511500G0000001040511",


            };

            var data = new {data = registerParam};
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
                main_cond_dscr = "心烦意燥",
                dise_codg = "N05.900x003",
                dise_name = "肾炎",
                expContent = new {card_token = secureMediaIni.data.card_token},
                birctrl_type = "",
            };
            var diseinfo = new List<YinHaiBaseIniDiseinfo>();

            var diseinfoData = new YinHaiBaseIniDiseinfo()
            {
                diag_type = "0",
                diag_code = "N05.900x003",
                diag_name = "肾炎",
                diag_srt_no = 1,
                diag_time = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss"),
                dise_dor_name = "111",
                dise_dor_no = "测试",
                diag_dept = "内科"

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
            var inputData = new List<OutpatientFeeUploadfeedetailInput>();
            var inputFeeData = new OutpatientFeeUploadfeedetailInput()
            {
                feedetl_sn = "452118608000M202104040021",
                mdtrt_id = "512000G0000000382130",
                psn_no = "51000051200000512099000007",
                chrg_bchno = "1111",
                dise_codg = "N05.900x003",
                rxno = "",
                rx_circ_flag = "0",
                fee_ocur_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                med_list_codg = "ZI02AAN0001020103906",
                medins_list_codg = "C30B4454648F4C4098F770C1A6DD0363",
                det_item_fee_sumamt = 15,
                cnt = 10,
                pric = Convert.ToDecimal(1.5),
                sin_dos_dscr = "",
                used_frqu_dscr = "",
                prd_days = 0,
                medc_way_dscr = "",
                bilg_dept_codg = "A03",
                bilg_dept_name = "内科",
                bilg_dr_codg = "1111",
                bilg_dr_name = "444",
                hosp_appr_flag = "1",
                expContent = new {card_token = secureMediaIni.data.card_token}


            };
            inputData.Add(inputFeeData);
            paramData.input = new {feedetail = inputData};
            txt_Input.Text = JsonConvert.SerializeObject(paramData);

        }

        private void button9_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2206A", lab_sign_no.Text);
            var inputData = new OutpatientPreSettlementDataInputDto()
            {
                expContent = new {card_token = secureMediaIni.data.card_token},
                psn_no = "51000051200000512099000007",
                mdtrt_cert_type = secureMediaIni.data.mdtrt_cert_type,
                mdtrt_cert_no = secureMediaIni.data.mdtrt_cert_no,
                med_type = "11",
                medfee_sumamt = 15,
                psn_setlway = "01",
                mdtrt_id = "512000G0000000382130",
                chrg_bchno = "1111",
                acct_used_flag = "0",
                insutype = "310"


            };

            paramData.input = new {data = inputData};
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2207A", lab_sign_no.Text);
            var inputData = new OutpatientSettlementInputDataDto()
            {
                expContent = new {card_token = secureMediaIni.data.card_token},
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
                invono = "K262489735",
                inscp_scp_amt = 0,
                fulamt_ownpay_amt = 15,
                overlmt_selfpay = 0,
                preselfpay_amt = 0

            };

            paramData.input = new {data = inputData};
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2208", lab_sign_no.Text);
            var inputData = new OutpatientCancelSettlementInputDataDto()
            {
                expContent = "",
                psn_no = "51000051150000001015222272",
                mdtrt_id = "511500G0000000228527",
                setl_id = "511500G0000000123150"

            };

            paramData.input = new {data = inputData};
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2401", lab_sign_no.Text);
            //输入参数
            var InputData = new HospitalRegisterInputDto();
            var mdtrtinfo = new HospitalRegisterInputmdtrtinfoDto()
            {
                psn_no = "51000051200000512099001648",
                adm_bed = "33",
                begntime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss"),
                adm_dept_codg = "A03",
                adm_dept_name = "内科",
                atddr_no = "4861632382718291017",
                chfpdr_name = "李茜",
                dscg_maindiag_code = "N05.900x003",
                dscg_maindiag_name = "肾炎",
                //ipt_no = "100120210223011",
                ipt_no = "100210223011",
                adm_diag_dscr = "4861632382718291017",
                main_cond_dscr = "肾炎",
                //birctrl_matn_date = param.birctrl_matn_date,
                //fetts = param.fetts,
                //fetus_cnt = param.fetus_cnt,
                //geso_val = param.geso_val,
                //psn_no = param.psn_no,
                insutype = "310",
                //pret_flag = param.pret_flag,
                mdtrt_cert_type = secureMediaIni.data.mdtrt_cert_type,
                mdtrt_cert_no = secureMediaIni.data.mdtrt_cert_no,
                med_type = "21",
                expContent = new {card_token = secureMediaIni.data.card_token},
            };
            var diseinfo = new List<YinHaiBaseIniDiseinfo>();

            var diseinfoData = new YinHaiBaseIniDiseinfo()
            {
                psn_no = "51000051200000512099001648",
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
            {
                mdtrt_id = "512000G0000000382203",
                psn_no = "51000051200000512099025082",
                insutype = "310",
                endtime = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss"),
                dscg_dept_codg = "A03",
                dscg_dept_name = "内科",

                dscg_way = "1"
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

            paramData.input = new {dscginfo = mdtrtinfo, diseinfo = diseinfo};
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
                med_type = "21",
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
            paramData.input = new {feedetail = inputData};
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2405", lab_sign_no.Text);
            var data = new GetCancelLeaveHospitalInputDataDto()
            {
                mdtrt_id = textBox6.Text,
                psn_no = textBox5.Text,
            };
            paramData.input = new {data = data};
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
                mid_setl_flag = "0",
                acct_used_flag = "0",
                expContent = new {card_token = secureMediaIni.data.card_token},

            };

            paramData.input = new {data = inputData};
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
                expContent = new {card_token = secureMediaIni.data.card_token},
            };

            paramData.input = new {data = inputData};
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2305", lab_sign_no.Text);
            //var data = new GetHospitalCancelSettlementInputDataDto()
            //{
            //    mdtrt_id = "512000G0000000382203",
            //    psn_no = "51000051200000512099025082",
            //    setl_id= "512000G0000000349477"
            //};
            var data = new GetHospitalCancelSettlementInputDataDto()
            {
                mdtrt_id = textBox6.Text,
                psn_no = textBox5.Text,
                setl_id = textBox7.Text
            };

            paramData.input = new {data = data};
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button20_Click(object sender, EventArgs e)
        {


            var paramData = GetBaseParam("9102", lab_sign_no.Text);

            var data33 = new fsDownloadInDto()
            {
                filename = "202109121134028121843524679.txt.zip",
                fixmedins_code = "H51202100005",
                file_qury_no = "fsi/plc/acee38eb5b334652b5d2c7a4c42ea3",
            };
            // var data =
            //new {
            //    type="gend",
            //    parentvalue="",
            //    admdvs= "512000",
            //    date= "2021-09-17",
            //    page_num=1,
            //    vali_flag=1,
            //    ver=0,
            //    page_size = 1000,
            //};
            var data =
                new
                {
                    ver = 0,
                };
            paramData.input = new {data = data33};
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2601", lab_sign_no.Text);
            //var data = new GetHospitalCancelSettlementInputDataDto()
            //{
            //    mdtrt_id = "512000G0000000382203",
            //    psn_no = "51000051200000512099025082",
            //    setl_id= "512000G0000000349477"
            //};
            var data = new RightingDto()
            {
                psn_no = textBox5.Text,
                omsgid = txt_omsgid.Text,
                oinfno = textBox8.Text,
            };

            paramData.input = new {data = data};
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string url = "http://10.109.120.206:8080/mss/web/api/fsi/callService";
            PostWebReq(url, txt_Input.Text, Encoding.UTF8);
        }


        public void download()
        {
            string postString = "id=25811&action=download"; //这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来
            byte[] postData = Encoding.UTF8.GetBytes(postString); //编码，尤其是汉字，事先要看下抓取网页的编码方式
            string url = "http://www.hznymm.com"; //地址
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type",
                "application/x-www-form-urlencoded"); //采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可
            byte[] responseData = webClient.UploadData(url, "POST", postData); //得到返回字符流

            string srcString = Encoding.UTF8.GetString(responseData); //解码

            writeFile(responseData, @"d:\爱情终结.torrent");



        }

        //byte[]转为文件FileStream 保存
        private bool writeFile(byte[] pReadByte, string fileName)
        {

            FileStream pFileStream = null;
            try
            {
                pFileStream = new FileStream(fileName, FileMode.OpenOrCreate);
                pFileStream.Write(pReadByte, 0, pReadByte.Length);
            }
            catch
            {
                return false;
            }

            finally
            {
                if (pFileStream != null)
                    pFileStream.Close();
            }

            return true;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2302", lab_sign_no.Text);
            var data = new List<PrescriptionCancelUploadInputDataDto>();
            var inputFeeData = new PrescriptionCancelUploadInputDataDto()
            {
                feedetl_sn = "0000",
                mdtrt_id = textBox6.Text,
                psn_no = textBox5.Text,



            };
            data.Add(inputFeeData);
            paramData.input = new { data = data };
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void btn_query_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text)==false)
            {
                MessageBox.Show("查询码值不能为空");
            }
            var paramData = GetBaseParam("1901", lab_sign_no.Text);
           
            var Data = new QueryData()
            {
                parentValue= "",
                type= textBox1.Text,
                date =DateTime.Now.ToString("yyyy-MM-dd"),
                admdvs= "511502",
                vali_flag="1"
            };
           
            paramData.input = new { data = Data };
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("5201", lab_sign_no.Text);
            var data = new VisitInformationQuerDto()
            {
                psn_no= textBox5.Text,
                begntime= "2021-10-10 18:57:18.000",
                endtime = "2021-10-28 18:57:18.000",
            };
            paramData.input = new { data = data };
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2404", lab_sign_no.Text);
            var data = new
            {
                mdtrt_id= textBox6.Text,
                psn_no= textBox5.Text,
              
            };
            paramData.input = new { data = data };
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            var paramData = GetBaseParam("2001", lab_sign_no.Text);
            var data = new QueryTreatmentInputDto()
            {
                begntime = DateTime.Now.AddYears(-2).ToString("yyyy-MM-dd HH:mm:ss"),
                psn_no = "51000051150000001001522187",
                fixmedins_code = textBox2.Text,
                insutype = "310",
                med_type = "11",
                endtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),

            };
            paramData.input = new { data = data };
            txt_Input.Text = JsonConvert.SerializeObject(paramData);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}


