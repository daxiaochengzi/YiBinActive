
using BenDingActive.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            String[] arr = new String[] { "明泰", "德卡", "德生","大华"};
            for (int i = 0; i < arr.Length; i++)
            {
                comboBox1.Items.Add(arr[i]); // 手动添加值
            }
            comboBox1.SelectedIndex = 0;
            comboBox1.SelectedItem = "明泰";
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
            paramEntity.CardPwd = "890811";
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
    }


}
