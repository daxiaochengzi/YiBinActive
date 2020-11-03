
using BenDingActive.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BenDingActive;
using BenDingActive.Help;
using BenDingActive.Model;
using BenDingActive.Model.BendParam;
using BenDingActive.Model.Dto.Bend;
using BenDingActive.Model.Params;
using BenDingActive.Model.Params.Service;
using Newtonsoft.Json;

namespace BenDingForm
{
    public partial class Form1 : Form
    {
        private int typeCard = 0;
         HospitalizationService hospitalService = new HospitalizationService();
           OutpatientDepartmentService _residentd = new OutpatientDepartmentService();
        //HisBaseParam _hisBase=new HisBaseParam()
        //{

        //    YbOrgCode = "99999",
        //    EmpID = "E075AC49FCE443778F897CF839F3B924",
        //    OrgID = "51072600000000000000000513435964",
        //    BID = "6721F4DA50B349AF9F5F387707C1647A",
        //    BsCode = "23",
        //    TransKey = "6721F4DA50B349AF9F5F387707C1647A"
        //};
        public Form1()
        {
            InitializeComponent();

            String[] arr = new String[] { "华大", "德卡", "德生", "明泰" };
            for (int i = 0; i < arr.Length; i++)
            {
                comboBox1.Items.Add(arr[i]); // 手动添加值
            }
            //卡类型编码
            var iniFile = new  IniFile("");
            var cardTypeCode = iniFile.ReadCardType();
            switch (cardTypeCode)
            {
                case "hd":
                    comboBox1.SelectedIndex = 0;
                    break;
                case "HNSICRW.dll":
                    comboBox1.SelectedIndex = 1;
                    break;
                case "LSCard.dll":
                    comboBox1.SelectedIndex = 2;
                    break;
                case "YB_SSSReaderMT.dll":
                    comboBox1.SelectedIndex = 3;
                    break;

            }

            var codePwd = iniFile.ReadKeyPwd();
            if (codePwd == "1")
            {
                CheckPwd.Checked = true;
            }

            else
            {
                lbl_pwd.Visible = true;
                txtPwd.Visible = true;
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            var macActiveX = new MacActiveX();
             //居民保险
             //string baseParam = JsonConvert.SerializeObject(new HisBaseParam()
             //{
             //    YbOrgCode = "99999",
             //    EmpID = "E075AC49FCE443778F897CF839F3B924",
             //    OrgID = "51072600000000000000000513435964",
             //    BID= "6721F4DA50B349AF9F5F387707C1647A",
             //    BsCode = "23",
             //    TransKey = "6721F4DA50B349AF9F5F387707C1647A"
             //});
             var baseParam = "{\"OperatorId\":\"E075AC49FCE443778F897CF839F3B924\",\"Account\":\"cpq2677\",\"Pwd\":\"888888\"}";
            var paramEntity = new UserInfoParam();
            paramEntity.PI_CRBZ = "1";
            paramEntity.PI_SFBZ = "513701199002124815";
            // JsonConvert.DeserializeObject<HisBaseParam>(baseParam)
            var data = macActiveX.OutpatientMethods(JsonConvert.SerializeObject(paramEntity), baseParam, "GetUserInfo");
           textBox1.Text = data.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var paramEntity = "{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"AfferentSign\":\"3\",\"IdentityMark\":\"1001529930\",\"MedicalCategory\":\"21\",\"AdmissionDate\":\"20200828\",\"AdmissionMainDiagnosisIcd10\":\"K08.801\",\"DiagnosisIcd10Two\":\"R51.x00,Z00.001\",\"DiagnosisIcd10Three\":null,\"AdmissionMainDiagnosis\":\"牙痛,头痛,健康查体\",\"InpatientDepartmentCode\":\"A617F37F4C404C0C90717C195A66F689\",\"BedNumber\":null,\"InpatientArea\":null,\"HospitalizationNo\":\"100120200828003\",\"Operators\":\"医保接口\",\"OrganizationCode\":\"ybx12865\",\"MedicalInsuranceHospitalizationNo\":null,\"AdministrativeArea\":\"511521\",\"BusinessId\":\"2A6B2B404D1C4AE88A521891F19D4B18\",\"User\":null}";
            var baseParam = "{\"OperatorId\":\"E075AC49FCE443778F897CF839F3B924\",\"ybx12865\":\"cpq2677\",\"Pwd\":\"aaaaaa\"}";
            hospitalService.WorkerHospitalizationRegister(paramEntity, JsonConvert.DeserializeObject<HisBaseParam>(baseParam));
        }

        private void button6_Click(object sender, EventArgs e)
        {

            //Logs.LogWriteData(new LogWriteDataParam()
            //{
            //    JoinJson = "45345345",
            //    ReturnJson = "444"
            //});
            //var paramEntity = new UserInfoParam();
            //paramEntity.PI_CRBZ = "1";
            //paramEntity.PI_SFBZ = "513701199002124815";

            var paramEntity = "{\"PI_SFBZ\":\"511521201704210171\",\"PI_CRBZ\":\"1\"}";
            var baseParam = "{\"OperatorId\":\"E075AC49FCE443778F897CF839F3B924\",\"Account\":\"cpq2677\",\"Pwd\":\"888888\"}";

            var data = _residentd.GetUserInfo(paramEntity, JsonConvert.DeserializeObject<HisBaseParam>(baseParam));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var formShow = new Form2();
            formShow.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string sss = "{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"AfferentSign\":\"3\",\"IdentityMark\":\"1001529930\",\"MedicalCategory\":\"21\",\"AdmissionDate\":\"20200828\",\"AdmissionMainDiagnosisIcd10\":\"K08.801\",\"DiagnosisIcd10Two\":\"R51.x00,Z00.001\",\"DiagnosisIcd10Three\":null,\"AdmissionMainDiagnosis\":\"牙痛,头痛,健康查体\",\"InpatientDepartmentCode\":\"A617F37F4C404C0C90717C195A66F689\",\"BedNumber\":null,\"InpatientArea\":null,\"HospitalizationNo\":\"100120200828003\",\"Operators\":\"医保接口\",\"OrganizationCode\":\"ybx12865\",\"MedicalInsuranceHospitalizationNo\":null,\"AdministrativeArea\":\"511521\",\"BusinessId\":\"2A6B2B404D1C4AE88A521891F19D4B18\",\"User\":null}";
            var param = JsonConvert.DeserializeObject<WorKerHospitalizationRegisterParam>(sss);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var baseParam = "{\"OperatorId\":\"E075AC49FCE443778F897CF839F3B924\",\"Account\":\"ybx12865\",\"Pwd\":\"aaaaaa\"}";
            var data = hospitalService.NationEcTrans(null, JsonConvert.DeserializeObject<HisBaseParam>(baseParam));
            textBox1.Text = data.Data;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var macActiveX = new MacActiveX();
            //居民保险
            //string baseParam = JsonConvert.SerializeObject(new HisBaseParam()
            //{
            //    YbOrgCode = "99999",
            //    EmpID = "E075AC49FCE443778F897CF839F3B924",
            //    OrgID = "51072600000000000000000513435964",
            //    BID= "6721F4DA50B349AF9F5F387707C1647A",
            //    BsCode = "23",
            //    TransKey = "6721F4DA50B349AF9F5F387707C1647A"
            //});
            var baseParam = "{\"Account\": \"ybx12865\", 	\"Pwd\": \"aaaaaa\", 	\"OperatorId\": \"76EDB472F6E544FD8DC8D354BB088BD7\", 	\"InsuranceType\": null, 	\"IdentityMark\": \"1001522187\", 	\"AfferentSign\": \"2\" }";
            var paramEntity = new WorkerHospitalSettlementCardDto();
            paramEntity.CardPwd= "";
            paramEntity.TotalAmount = "0.01";
            paramEntity.HospitalLogNo = "12865";
            paramEntity.OperatorName = "医保接口";
            paramEntity.UseCardType = "1";
            // JsonConvert.DeserializeObject<HisBaseParam>(baseParam)
            var data = macActiveX.HospitalizationMethods(JsonConvert.SerializeObject(paramEntity), baseParam, "WorkerCardSettlement");
            textBox1.Text = data;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var macActiveX = new MacActiveX();
            var baseParam = "{\"Account\": \"ybx12865\", 	\"Pwd\": \"aaaaaa\", 	\"OperatorId\": \"76EDB472F6E544FD8DC8D354BB088BD7\", 	\"InsuranceType\": null, 	\"IdentityMark\": \"1001522187\", 	\"AfferentSign\": \"2\" }";
            var paramEntity = new ReadCardInfoParam();
            if (CheckPwd.Checked == false)
            {
                if (string.IsNullOrWhiteSpace(txtPwd.Text))
                {
                    MessageBox.Show("密码不能为空!!!");
                }
                else
                {
                    paramEntity.CardPwd = txtPwd.Text;
                }
            }
            paramEntity.InsuranceType = 0;
            // JsonConvert.DeserializeObject<HisBaseParam>(baseParam)
            var data = macActiveX.OutpatientMethods(JsonConvert.SerializeObject(paramEntity), baseParam, "ReadCardInfo");
            textBox1.Text = data;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var macActiveX = new MacActiveX();
            
            var baseParam = "{\"Account\": \"ybx12865\", 	\"Pwd\": \"aaaaaa\", 	\"OperatorId\": \"76EDB472F6E544FD8DC8D354BB088BD7\", 	\"InsuranceType\": null, 	\"IdentityMark\": \"1001522187\", 	\"AfferentSign\": \"2\" }";
            var paramXml = "<?xml version=\"1.0\" encoding=\"GBK\"?>";
             paramXml += "<ROW><PI_HKLSH>" + textBox2.Text + "</PI_HKLSH><PI_JBR>医保接口</PI_JBR><PI_AAE013>测试</PI_AAE013> </ROW>";
             var data = macActiveX.HospitalizationMethods(paramXml, baseParam, "WorkerCancelSettlementCard");
            textBox1.Text = data;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var iniFile = new IniFile("");
            //端口号
            int port = Convert.ToInt16(iniFile.GetIni());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var is64Bit = Environment.Is64BitOperatingSystem;
            var path = is64Bit ? @"C:\Program Files (x86)\Microsoft\本鼎医保插件" : @"C:\Program Files\Microsoft\本鼎医保插件";
            var iniFile = new IniFile("");
            string cardTypeName = "";
            //   { "明泰", "德卡", "德生","华大"};
            var indexData = comboBox1.SelectedIndex;
            if (typeCard==0)
            {
                typeCard = 1;
            }
            else
            {
                KillProcess("iexplore");
            }
           
            switch (indexData)
            {
                case 0:
                    CopyDirectory(path + "\\hd", path);
                    cardTypeName = "hd";
                    break;
                case 1:
                    CopyDirectory( path+"\\dk", path);
                    cardTypeName = "'HNSICRW.dll'";
                    break;
                case 2:
                    CopyDirectory(path + "\\ds", path);
                    cardTypeName = "'LSCard.dll'";
                    break;
                case 3:
                    CopyDirectory(path + "\\mt", path);
                    cardTypeName = "'YB_SSSReaderMT.dll'";
                    break;
                

            }
           iniFile.SetCardType(cardTypeName);
        }

        public void CopyDirectory(string scrPath, string savePath)
        {
            if (Directory.Exists(scrPath))//检查路径(目录)是否存在
            {
                if (!Directory.Exists(savePath))
                    Directory.CreateDirectory(savePath);
                string subSavePath = savePath;
                string[] aFiles = Directory.GetFiles(scrPath);
                string[] aDirectory = Directory.GetDirectories(scrPath);
                for (int i = 0; i < aFiles.Length; i++)
                {
                    FileInfo fi = new FileInfo(aFiles[i]);
                    long fileSize = fi.Length;//文件大小
                    if (System.IO.File.Exists(subSavePath + "\\" + fi.Name))
                    {
                        File.Delete(subSavePath + "\\" + fi.Name);
                    }
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

        private void button14_Click(object sender, EventArgs e)
        {
            var loginData = MedicalInsuranceDll.ConnectAppServer_cxjb("ybx12865", "aaaaaa");
            if (loginData != 1) throw new Exception("医保登陆失败!!!");
            Logs.LogWrite(new LogParam()
            {
                Params = "0",
                Msg = "登陆成功"

            });
        }
        /// <summary>
        /// 杀掉FoxitReader进程
        /// </summary>
        /// <param name="strProcessesByName"></param>
        public static void KillProcess(string processName)
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName.Contains(processName))
                {
                    try
                    {
                        p.Kill();
                        p.WaitForExit(); // possibly with a timeout
                       
                    }
                    catch (Win32Exception e)
                    {
                        MessageBox.Show(e.Message);
                      
                    }
                    catch (InvalidOperationException e)
                    {
                        MessageBox.Show(e.Message);
                       
                    }
                }

            }
        }

        private void CheckPwd_CheckedChanged(object sender, EventArgs e)
        {
           
          
            var iniKeyPwd = new IniFile("");
            if (this.CheckPwd.Checked)
            {
                iniKeyPwd.SetKeyPwd(1);
                lbl_pwd.Visible = false;
                txtPwd.Visible = false;
            }
            else
            {
                iniKeyPwd.SetKeyPwd(0);
                lbl_pwd.Visible = true;
                txtPwd.Visible = true;

            }
           
        }
    }
}



