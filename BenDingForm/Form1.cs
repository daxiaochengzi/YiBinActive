﻿
using BenDingActive.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
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
using System.Management;
using BenDingActive.Model.Dto.YiHai;

namespace BenDingForm
{
    public partial class Form1 : Form
    {  //安全控件初始化参数
        private string secureMediaData = null;
        private SecureMediaOutputDto secureMediaIni = null;
        private int typeCard = 0;
         HospitalizationService hospitalService = new HospitalizationService();
        OutpatientDepartmentService OutpatientService = new OutpatientDepartmentService();
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
            var secureMedia = new SecureMediaDto()
            {
                data = new SecureMediaDataDto()
            };

            secureMediaData = JsonConvert.SerializeObject(secureMedia);
            String[] arr = new String[] {  "德卡", "华大", "德生","明泰" };
            for (int i = 0; i < arr.Length; i++)
            {
                comboBox1.Items.Add(arr[i]); // 手动添加值
            }
            //卡类型编码
            var iniFile = new  IniFile("");
            var cardTypeCode = iniFile.ReadCardType();
            
            switch (cardTypeCode)
            {
                case "HNSICRW.dll":
                    comboBox1.SelectedIndex = 0;
                    break;
                case "hd.dll":
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
                //lbl_pwd.Visible = true;
                //txtPwd.Visible = true;
            }
            string  mac ;
            string ip ;
            iniFile.ReadAddress( out mac, out ip);
            txt_ip.Text = ip;
            txt_mac.Text = mac;
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
             var baseParam = "{\"OperatorId\":\"E075AC49FCE443778F897CF839F3B924\",\"Account\":\"ybx12865\",\"Pwd\":\"aaaaaa\",\"IdentityMark\":\"512529195202082097\",\"AfferentSign\":\"1\"}";
            var paramEntity = new UserInfoParam();
            paramEntity.PI_CRBZ = "1";
            paramEntity.PI_SFBZ = "51152119810705716X";//513701199002124815
            // JsonConvert.DeserializeObject<HisBaseParam>(baseParam)
            var paramStr = "{\"IdentityMark\":\"51152119810705716X\",\"AfferentSign\":\"1\"}";
                var data = macActiveX.OutpatientMethods(paramStr, baseParam, "GetUserInfo");
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
            var baseParam = "{\"OperatorId\":\"E075AC49FCE443778F897CF839F3B924\",\"Account\":\"cnzzxwsy\",\"Pwd\":\"aaaaaa\"}";
            string param = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            // param = "<ROW xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">";
           param += "<ROW>";
            param += @"<BKC142>1.5</BKC142>
              <HKLB>1</HKLB>
              <NUMS>0</NUMS>
             <DATAROW></DATAROW>
            </ROW>";
            var data = OutpatientService.NationEcTrans(param, JsonConvert.DeserializeObject<HisBaseParam>(baseParam));
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
        
            //var paramEntity = new ReadCardInfoParam();
            //if (CheckPwd.Checked == false)
            //{
            //    if (string.IsNullOrWhiteSpace(txtPwd.Text))
            //    {
            //        MessageBox.Show("密码不能为空!!!");
            //    }
            //    else
            //    {
            //        paramEntity.CardPwd = txtPwd.Text;
            //    }
            //}
            //paramEntity.InsuranceType = 0;
            //// JsonConvert.DeserializeObject<HisBaseParam>(baseParam)
            //var data = macActiveX.OutpatientMethods(txtPwd.Text, baseParam, "ReadCardInfo");
            //textBox1.Text = data;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var macActiveX = new MacActiveX();
            var baseParam = "{\"Account\": \"cnzzxwsy\", 	\"Pwd\": \"aaaaaa\", 	\"OperatorId\": \"76EDB472F6E544FD8DC8D354BB088BD7\", 	\"InsuranceType\": null, 	\"IdentityMark\": \"1001522187\", 	\"AfferentSign\": \"2\" }";
           //var baseParam = "{\"Account\": \"ybx12865\", 	\"Pwd\": \"aaaaaa\", 	\"OperatorId\": \"76EDB472F6E544FD8DC8D354BB088BD7\", 	\"InsuranceType\": null, 	\"IdentityMark\": \"1001522187\", 	\"AfferentSign\": \"2\" }";
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
            var pathNew = is64Bit ? @"C:\Windows\SysWOW64" : @"C:\Windows\System32";
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
                    CopyDirectory(path + "\\dk", pathNew);
                    CopyDirectory(path + "\\dk", path);
                    cardTypeName = "'HNSICRW.dll'";
                    break;
                case 1:
                    CopyDirectory(path + "\\hd", pathNew);
                    CopyDirectory(path + "\\hd", path);
                    cardTypeName = "'hd.dll'";
                    break;
                case 2:
                    CopyDirectory(path + "\\ds", pathNew);
                    CopyDirectory(path + "\\ds", path);
                    cardTypeName = "'LSCard.dll'";
                    break;
                case 3:
                    CopyDirectory(path + "\\mt", pathNew);
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
                //lbl_pwd.Visible = false;
                //txtPwd.Visible = false;
            }
            else
            {
                iniKeyPwd.SetKeyPwd(0);
                //lbl_pwd.Visible = true;
                //txtPwd.Visible = true;

            }
           
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            var baseParam = "{\"OperatorId\":\"E075AC49FCE443778F897CF839F3B924\",\"Account\":\"cnzzxwsy\",\"Pwd\":\"aaaaaa\"}";
            var data = OutpatientService.NationEcTransUser(null, JsonConvert.DeserializeObject<HisBaseParam>(baseParam));
            textBox1.Text = data.Data;
            
        }

        private int SaveDetail()
        {
            int resultData = 0;
            string strConnection = "Provider = Microsoft.ACE.OLEDB.12.0;";  //C#读取Excel的连接字符串  
            strConnection += @"Data Source = D:\xmmx.accdb";

            //创建OleDb连接对象
            try
            {
                OleDbConnection conn = new OleDbConnection(strConnection);
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from xmmx";
                conn.Open();
                OleDbDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                if (dr.HasRows)
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        dt.Columns.Add(dr.GetName(i));
                    }
                    dt.Rows.Clear();
                }
                while (dr.Read())
                {
                    DataRow row = dt.NewRow();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        row[i] = dr[i];
                    }
                    dt.Rows.Add(row);
                }
                cmd.Dispose();
                conn.Close();
                var drugCatalogData = new List<ResidentProjectDownloadRowDataRowDto>();
            
                foreach (DataRow drc in dt.Rows)
                {
                    var item = new ResidentProjectDownloadRowDataRowDto
                    {
                        ProjectCode = CommonHelp.FilterSqlStr(drc["AKA090"].ToString()),
                        ProjectName = CommonHelp.FilterSqlStr(drc["AKA091"].ToString()),
                        ProjectBigType = "1",//CommonHelp.FilterSqlStr(dr["1"].ToString()),// 1 药品 2 诊疗 3 材料 4 其他
                        ProjectLevel = drc["AKA065"].ToString(),
                        ProjectCodeType = CommonHelp.FilterSqlStr(drc["AKA063"].ToString()),
                        WorkersSelfPayProportion = CommonHelp.getNum(drc["AKA069"].ToString()),
                        ResidentSelfPayProportion = CommonHelp.getNum(drc["CKE899"].ToString()),
                        Unit = CommonHelp.FilterSqlStr(drc["AKA067"].ToString()),
                        Specification = drc["AKA074"].ToString(),
                        Formulation = drc["AKA070"].ToString(),
                        QuasiFontSize = CommonHelp.FilterSqlStr(drc["CKA603"].ToString()),
                        RestrictionSign = drc["AKA036"].ToString() == "1" ? "1" : "0",
                        LimitPaymentScope = CommonHelp.FilterSqlStr(drc["CKE599"].ToString()),
                        MnemonicCode = CommonHelp.FilterSqlStr(drc["AKA066"].ToString()),
                        ZeroBlock = CommonHelp.getNum(drc["CKA599"].ToString()),
                        OneBlock = CommonHelp.getNum(drc["CKA578"].ToString()),
                        TwoBlock = CommonHelp.getNum(drc["CKA579"].ToString()),
                        ThreeBlock = CommonHelp.getNum(drc["CKA580"].ToString()),
                        FourBlock = CommonHelp.getNum(drc["CKA560"].ToString()),
                        Manufacturer = drc["AKA098"].ToString(),
                        NewCodeMark = drc["CKE897"].ToString(),
                        EffectiveSign = drc["AAE100"].ToString(),
                        StartTime = drc["AAE030"].ToString(),
                        EndTime = drc["AAE031"].ToString(),
                        NewUpdateTime = drc["AAE036"].ToString(),
                        Remark = drc["AAE013"].ToString(),
                    };

                    drugCatalogData.Add(item);

                    if (drugCatalogData.Count() >= 300)
                    {
                        SaveDrugCatalog(drugCatalogData, "76EDB472F6E544FD8DC8D354BB088BD7");
                        resultData += drugCatalogData.Count();
                        drugCatalogData = new List<ResidentProjectDownloadRowDataRowDto>();
                    }
                }
                //执行剩余的数据
                if (drugCatalogData.Any())
                {
                    SaveDrugCatalog(drugCatalogData, "76EDB472F6E544FD8DC8D354BB088BD7");
                    resultData += drugCatalogData.Count();
                }

                
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
           
            return resultData;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        private void UpdateData(string sql)
        {
            string conStr = $"server={textBox3.Text};database=NFineBase;uid=sa;pwd=BenDingPwd@";
            using (var sqlConnection = new SqlConnection(conStr))
            {
                string insterSql = null;
             
                try
                {//update [dbo].[MedicalInsuranceProject] set [IsDelete]=0 where IsDelete=3;
                    sqlConnection.Open();
                    insterSql = sql;
                    SqlCommand com = new SqlCommand();
                    com.CommandType = CommandType.Text;
                    com.Connection = sqlConnection;
                    com.CommandText = insterSql;
                    com.ExecuteNonQuery();
                    sqlConnection.Close();
                    
                }
                catch (Exception e)
                {
                   
                    throw  new Exception(e.Message);
                }
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            string path = @"D:\xmmx.accdb";
            //string path = @"D:\xmmx.mdb";
            if (!string.IsNullOrWhiteSpace(textBox3.Text.Trim()) == false)
            {
                MessageBox.Show("服务器地址不能为空!!!");
                return;

            }

            if (!File.Exists(path))
            {
                MessageBox.Show(@"D:\xmmx.accdb" + "数据文件不存在!!!");
                return;
            }
            var count = SaveDetail();
            //if (count > 0)
            //{
            //    string updateStr = "update [dbo].[MedicalInsuranceProject] set IsDelete=0 where IsDelete=1";
            //    string deleteStr = "delete [dbo].[MedicalInsuranceProject] where IsDelete=0";
            //    UpdateData(deleteStr);
            //    UpdateData(updateStr);
            //    MessageBox.Show("成功导入:" + count + "条");
            //}



        }


        private void SaveDrugCatalog(List<ResidentProjectDownloadRowDataRowDto> param, string userId)
        {
            string conStr = $"server={textBox3.Text};database=NFineBase;uid=sa;pwd=BenDingPwd@";
            using (var sqlConnection = new SqlConnection(conStr))
            {
                string insterSql = null;
                string insterCount = null;
                try
                {
                    sqlConnection.Open();
                    if (param.Any())
                    {

                        foreach (var item in param)
                        {

                            var projectName = CommonHelp.FilterSqlStr(item.ProjectName);
                            insterSql = $@"INSERT INTO [dbo].[MedicalInsuranceProject]
                               (id,[ProjectCode],[ProjectName] ,[ProjectCodeType] ,[ProjectLevel],[WorkersSelfPayProportion]
                               ,[Unit],[MnemonicCode] ,[Formulation],[ResidentSelfPayProportion],[RestrictionSign]
                               ,[ZeroBlock],[OneBlock],[TwoBlock],[ThreeBlock],[FourBlock],[EffectiveSign],[ResidentOutpatientSign]
                               ,[ResidentOutpatientBlock],[Manufacturer] ,[QuasiFontSize] ,[Specification],[Remark],[NewCodeMark]
                               ,[NewUpdateTime],[StartTime] ,[EndTime],[LimitPaymentScope],[CreateTime],[CreateUserId],[IsDelete],[ProjectBigType]
                               )
                              VALUES('{Guid.NewGuid()}','{item.ProjectCode}','{projectName}','{item.ProjectCodeType}','{item.ProjectLevel}',{CommonHelp.ValueToDecimal(item.WorkersSelfPayProportion)}
                                      ,'{item.Unit}','{item.MnemonicCode}', '{item.Formulation}',{CommonHelp.ValueToDecimal(item.ResidentSelfPayProportion)},'{item.RestrictionSign}'
                                      ,{CommonHelp.ValueToDecimal(item.ZeroBlock)},{CommonHelp.ValueToDecimal(item.OneBlock)},{CommonHelp.ValueToDecimal(item.TwoBlock)},{CommonHelp.ValueToDecimal(item.ThreeBlock)},{CommonHelp.ValueToDecimal(item.FourBlock)},'{item.EffectiveSign}','{item.ResidentOutpatientSign}'
                                      ,{CommonHelp.ValueToDecimal(item.ResidentOutpatientBlock)},'{item.Manufacturer}','{item.QuasiFontSize}','{item.Specification}','{item.Remark}','{item.NewCodeMark}'
                                      ,'{item.NewUpdateTime}','{item.StartTime}','{item.EndTime}','{item.LimitPaymentScope}',GETDATE(),'{userId}',3,'{item.ProjectBigType}'
                                   );";
                            insterCount += insterSql;
                        }
                        SqlCommand com = new SqlCommand();
                        com.CommandType = CommandType.Text;
                        com.Connection = sqlConnection;
                        com.CommandText = insterCount;
                        com.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }



            }
        }
        private DataTable SaveDataBase(string strsql)
        {
            //server=.;database=WR_DATA;uid=wr_zg_sl;pwd=sl@123456
            //server=QUBER-PC-SAING\SQL2012;database=WR_DATA;uid=sa;pwd=123456
            //125.66.152.66
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "server=.;database=Test;uid=sa;pwd=BenDingPwd@";
            con.Open();
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = strsql;
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader dr = com.ExecuteReader();//执行SQL语句
                dt.Load(dr);
                dr.Close();//关闭执行
                con.Close();//关闭数据库

            }
            catch (Exception ex)
            {

                

            }
            return dt;
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            var baseParam = "{\"Account\": \"ybx3105\", 	\"Pwd\": \"bbbb1234\", 	\"OperatorId\": \"76EDB472F6E544FD8DC8D354BB088BD7\", 	\"InsuranceType\": null, 	\"IdentityMark\": \"1001522187\", 	\"AfferentSign\": \"2\" }";
            var data=OutpatientService.Login("",JsonConvert.DeserializeObject<HisBaseParam>(baseParam));
            MessageBox.Show(data);
            //Logs.LogWrite(new LogParam()
            //{
            //    Params = "111",

            //    Msg = "333"
            //});
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var baseParam = "{\"Account\": \"ybx12865\", 	\"Pwd\": \"aaaaaa\", 	\"OperatorId\": \"76EDB472F6E544FD8DC8D354BB088BD7\", 	\"InsuranceType\": null, 	\"IdentityMark\": \"1001522187\", 	\"AfferentSign\": \"2\" }";
            string param = "<?xml version=\"1.0\" encoding=\"GBK\"?>";
            param += @" <ROW>
                    <PI_AKC190>4821307938443769417</PI_AKC190>\r\n  
                    <PI_AAC002>51192320181025982X</PI_AAC002>\r\n  
                    <PI_AAC003>张芯悦</PI_AAC003>\r\n  
                    <PI_AKA131>2</PI_AKA131>\r\n
                    <PI_PSW>123456</PI_PSW>\r\n
                    <PI_ICD10>Z71.900</PI_ICD10>\r\n  
                    <PI_JBMC>咨询</PI_JBMC>\r\n  
                    <PI_NUM>2</PI_NUM>\r\n  
                    <PI_AKB066>0.01</PI_AKB066>\r\n  
                    <PI_XFSJ>20210219162221</PI_XFSJ>\r\n 
                    <ROWDATA>\r\n    
                        <ROW>\r\n      
                            <BKE019>0</BKE019>\r\n      
                            <AKE001>86901815000356</AKE001>\r\n      
                            <AKE002>维生素C片</AKE002>\r\n      
                            <CKE521>0.020</CKE521>\r\n      
                            <AKC226>1</AKC226>\r\n      
                            <CKC526>0.02</CKC526>\r\n    
                        </ROW>\r\n    
                         <ROW>\r\n      
                            <BKE019>1</BKE019>\r\n      
                            <AKE001>C00000002</AKE001>\r\n      
                            <AKE002>居民门诊可报销西药</AKE002>\r\n      
                            <CKE521>0.010</CKE521>\r\n      
                            <AKC226>-1</AKC226>\r\n      
                            <CKC526>-0.010</CKC526>\r\n    
                        </ROW>\r\n 
                    </ROWDATA>\r\n
                </ROW>";
        
          
            var data = OutpatientService.ResidentSettlement(param, JsonConvert.DeserializeObject<HisBaseParam>(baseParam));

            MessageBox.Show(data.Data);
        }


        private void button16_Click_2(object sender, EventArgs e)
        {
            var baseParam = "{\"Account\": \"ybx12865\", 	\"Pwd\": \"aaaaaa\", 	\"OperatorId\": \"76EDB472F6E544FD8DC8D354BB088BD7\", 	\"InsuranceType\": null, 	\"IdentityMark\": \"1001522187\", 	\"AfferentSign\": \"2\" }";
            string param = "<?xml version=\"1.0\" encoding=\"GBK\"?>";
            param += @"<ROW> 
                    <PI_JSLB>1</PI_JSLB>
                    <PI_AAZ216>1013426519</PI_AAZ216>
                    <PI_AAC002>身份证号</PI_AAC002>
                    <PI_AAC003>吴能勇</PI_AAC003>
                    <PI_AKA131>1</PI_AKA131>
                    <PI_CARDID>Y01926189</PI_CARDID>
                </ROW>";


            var data = OutpatientService.ResidentSettlementCard(param, JsonConvert.DeserializeObject<HisBaseParam>(baseParam));
        }

        private void button17_Click(object sender, EventArgs e)
        {
            var baseParam = "{\"Account\": \"jlsqwsy\", 	\"Pwd\": \"sq222222\", 	\"OperatorId\": \"76EDB472F6E544FD8DC8D354BB088BD7\", 	\"InsuranceType\": null, 	\"IdentityMark\": \"1001522187\", 	\"AfferentSign\": \"2\" }";
            string param = "<?xml version=\"1.0\" encoding=\"GBK\"?>";
            param += $@"<ROW> 
                    <PI_AKC600>{textBox2.Text}</PI_AKC600>
                    <PI_AAE013>测试</PI_AAE013>
                </ROW>";


            var data = OutpatientService.CancelResidentSettlement(param, JsonConvert.DeserializeObject<HisBaseParam>(baseParam));
            MessageBox.Show(data.Data);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            var baseParam = "{\"OperatorId\":\"E075AC49FCE443778F897CF839F3B924\",\"Account\":\"xzq17808\",\"Pwd\":\"111111\"}";
            string param = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            // param = "<ROW xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">";
          
            param += @"<ROW> 
                <PI_AKC190>5596002356832624993</PI_AKC190>
                <PI_AAC002>512527197610271213</PI_AAC002>
                <PI_AAC003>程继兵</PI_AAC003>
                <PI_ICD10>Z71.900</PI_ICD10>
                <PI_JBMC>咨询</PI_JBMC>
                <PI_NUM>1</PI_NUM>
                <PI_AKB066>0.10</PI_AKB066>
                <PI_XFSJ>20210122160851</PI_XFSJ>
                <ROWDATA> 
                    <ROW>    
                        <BKE019>0</BKE019>
                        <AKE001>YBYC00010</AKE001>  
                        <AKE002>新筛外检</AKE002>
                        <CKE521>0.10</CKE521>    
                        <AKC226>1</AKC226>    
                        <CKC526>0.10</CKC526>
                    </ROW>
                </ROWDATA>
            </ROW>";
            var data = OutpatientService.NationEcTransResident(param, JsonConvert.DeserializeObject<HisBaseParam>(baseParam));
            MessageBox.Show(data.Data);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {
            Logs.LogErrorWrite(new LogParam()
            {
                OperatorCode="123",
                Msg="1111"
            });
        }

        private void button19_Click_1(object sender, EventArgs e)
        {
            var macActiveX = new MacActiveX();
            var baseParam = "{\"Account\": \"ybx12865\", 	\"Pwd\": \"aaaaaa\", 	\"OperatorId\": \"76EDB472F6E544FD8DC8D354BB088BD7\", 	\"InsuranceType\": null, 	\"IdentityMark\": \"1001522187\", 	\"AfferentSign\": \"2\" }";
            //var data = macActiveX.YdMedicalInsuranceMethods(txtPwd.Text, baseParam, "YdReadCardInfo");
            //var baseParam = "{\"Account\": \"ybx12865\", 	\"Pwd\": \"aaaaaa\", 	\"OperatorId\": \"76EDB472F6E544FD8DC8D354BB088BD7\", 	\"InsuranceType\": null, 	\"IdentityMark\": \"1001522187\", 	\"AfferentSign\": \"2\" }";
            //var paramXml = "<?xml version=\"1.0\" encoding=\"GBK\"?>";
            //paramXml += "<ROW><PI_HKLSH>" + textBox2.Text + "</PI_HKLSH><PI_JBR>医保接口</PI_JBR><PI_AAE013>测试</PI_AAE013> </ROW>";
            //var data = macActiveX.HospitalizationMethods(paramXml, baseParam, "WorkerCancelSettlementCard");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            var macActiveX = new MacActiveX();
            var baseParam = "{\"Account\": \"ybx12865\", 	\"Pwd\": \"aaaaaa\", 	\"OperatorId\": \"76EDB472F6E544FD8DC8D354BB088BD7\", 	\"InsuranceType\": null, 	\"IdentityMark\": \"1001522187\", 	\"AfferentSign\": \"2\" }";
            var paramXml = "<?xml version=\"1.0\" encoding=\"GBK\"?>";
            paramXml += @"<ROW>
                        <pi_xzqh>" + textBox2.Text + "</pi_xzqh>" +
                        "<pi_fyze>0.1</pi_fyze>" +
                        "<pi_cardid>测试</pi_cardid> " +
                        "<pi_hklb>1</pi_hklb> " +
                        "<pi_jbr>1</pi_jbr> " +
                        "</ROW>";
            var data = macActiveX.YdMedicalInsuranceMethods(paramXml, baseParam, "YdReadCardInfo");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            
               var baseParam = "{\"Account\": \"ybx3102\", 	\"Pwd\": \"aaaaaa12\", 	\"OperatorId\": \"76EDB472F6E544FD8DC8D354BB088BD7\", 	\"InsuranceType\": null, 	\"IdentityMark\": \"1025629937\", 	\"AfferentSign\": \"2\" }";
            string param = "<?xml version=\"1.0\" encoding=\"GBK\"?>";
            param += @" <ROW>
                  <PI_AKC190>5486697101372358359</PI_AKC190>
                  <PI_AKA131>2</PI_AKA131>
                  <PI_AAC002>512527194509189123</PI_AAC002>
                  <PI_AAC003>陶天富</PI_AAC003>
                  <PI_PSW>123321</PI_PSW>
                  <PI_ICD10>K29.603</PI_ICD10>
                  <PI_JBMC>糜烂性胃炎</PI_JBMC>
                  <PI_NUM>4</PI_NUM>
                  <PI_AKB066>40.8072</PI_AKB066>
                  <PI_XFSJ>20210502093518</PI_XFSJ>
                  <ROWDATA>
                    <ROW>
                      <BKE019>0</BKE019>
                      <AKE001>86904141000069</AKE001>
                      <AKE002>奥美拉唑肠溶胶囊</AKE002>
                      <CKE521>0.1992</CKE521>
                      <AKC226>8</AKC226>
                      <CKC526>1.5936</CKC526>
                    </ROW>
                    <ROW>
                      <BKE019>1</BKE019>
                      <AKE001>86900290001216</AKE001>
                      <AKE002>硫糖铝口服混悬液</AKE002>
                      <CKE521>18.36</CKE521>
                      <AKC226>1</AKC226>
                      <CKC526>18.36</CKC526>
                    </ROW>
                    <ROW>
                      <BKE019>2</BKE019>
                      <AKE001>86905590000174</AKE001>
                      <AKE002>香砂养胃片（48片/盒）</AKE002>
                      <CKE521>18.90</CKE521>
                      <AKC226>1</AKC226>
                      <CKC526>18.90</CKC526>
                    </ROW>
                    <ROW>
                      <BKE019>3</BKE019>
                      <AKE001>86901162000931</AKE001>
                      <AKE002>维生素B6片</AKE002>
                      <CKE521>0.0814</CKE521>
                      <AKC226>24</AKC226>
                      <CKC526>1.9536</CKC526>
                    </ROW>
                  </ROWDATA>
                </ROW>";
            var data = OutpatientService.ResidentSettlement(param, JsonConvert.DeserializeObject<HisBaseParam>(baseParam));
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            try
            {
                //关闭ie
                KillProcess("iexplore");
                string mac;
                string ip;
                GetActiveIpAndMac1(out mac, out ip);
                if (!string.IsNullOrWhiteSpace(ip) == false)
                {
                    MessageBox.Show("获取ip地址失败,请手动填写后更新地址!!!");
                    return;
                }
                if (!string.IsNullOrWhiteSpace(mac) == false)
                {
                    MessageBox.Show("获取mac地址失败,请手动填写后更新地址!!!");
                    return;
                }

                txt_mac.Text = mac;
                txt_ip.Text = ip;
                var iniFile = new IniFile("");
                iniFile.SaveAddress(mac, ip);
                //获取ip
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

        public void SaveAddress(string mac, string ip)
        {
            var iniFile = new IniFile("");
            //端口号
            int port = Convert.ToInt16(iniFile.GetIni());
        }

        /// <summary>
        /// 获取当前激活网络的MAC地址、IPv4地址、IPv6地址 - 方法1
        /// </summary>
        /// <param name="mac">网卡物理地址</param>
        /// <param name="ipv4">IPv4地址</param>
    
        public static void GetActiveIpAndMac1(out string mac, out string ipv4)
        {
            mac = "";
            ipv4 = "";
           

            //需要引用：System.Management;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["IPEnabled"].ToString() == "True")
                {
                    //获取MAC地址，每两位中间用横线【-】隔开
                    mac = mo["MacAddress"].ToString().Replace(":", "-");
                    string[] ipAddrs = mo["IPAddress"] as string[];
                    if (ipAddrs != null && ipAddrs.Length >= 1)
                    {
                        //获取IPv4地址，4个十进制数字，中间用英文句号【.】隔开
                        ipv4 = ipAddrs[0];
                    }
                    //if (ipAddrs != null && ipAddrs.Length >= 2)
                    //{
                    //    //获取IPv6地址，5个十六进制数字，中间用冒号【:】隔开
                    //    ipv6 = ipAddrs[1];
                    //}
                    break;
                }
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

        private void button22_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_mac.Text) == false)
            {
                MessageBox.Show("mac地址不能为空!!!");
                return;
            }
            if (!string.IsNullOrWhiteSpace(txt_ip.Text) == false)
            {
                MessageBox.Show("ip地址不能为空!!!");
                return;
            }

            
            var iniFile = new IniFile("");
            iniFile.SaveAddress(txt_mac.Text.Trim(), txt_ip.Text.Trim());
        }

        private void button23_Click(object sender, EventArgs e)
        {
            string msg = "";
            var resultData = YinHaiCOM.Init(out msg);
            if (resultData)
            {
                MessageBox.Show("控件注册通过!!!");
            }
        }

        private void btn_jz_Click(object sender, EventArgs e)
        {
            string msg = "";
            string iniMsg = "";
            var resultData = YinHaiCOM.Init(out iniMsg);
            //2304A
            YinHaiCOM.yh_CHS_call("1101", secureMediaData, ref msg);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                secureMediaIni = JsonConvert.DeserializeObject<SecureMediaOutputDto>(msg);

                textBox1.Text = msg;
            }
        }
    }
}



