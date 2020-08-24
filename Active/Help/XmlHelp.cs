using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using BenDingActive.Model;
using BenDingActive.Model.Dto;
using Newtonsoft.Json;

namespace BenDingActive.Help
{
    public static class XmlHelp
    {
        /// <summary>
        ///  回参
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="isAbnormal"></param>
        /// <returns></returns>
        public static T DeSerializerModel<T>(T t, bool isAbnormal)
        {
            var result = t;
            string pathXml = null;
            string copyPathXml = null;
            var valid = new ValidXmlDto();
            var is64Bit = Environment.Is64BitOperatingSystem;
            if (is64Bit)
            {
                pathXml = @"C:\Program Files (x86)\Microsoft\本鼎医保插件\" + "ResponseParams.xml";
                copyPathXml = @"C:\Program Files (x86)\Microsoft\本鼎医保插件\";
            }
            else
            {
                pathXml = @"C:\Program Files\Microsoft\本鼎医保插件\" + "ResponseParams.xml";
                copyPathXml = @"C:\Program Files\Microsoft\本鼎医保插件\";
            }

            string copyPathXmlNew = copyPathXml + "xmlData\\" +
                                    DateTime.Now.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "\\" +
                                    DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xml";
            File.Copy(pathXml, copyPathXmlNew);

            // pathXml = System.AppDomain.CurrentDomain.BaseDirectory + "ResponseParams.xml";

            if (!System.IO.File.Exists(pathXml))
            {
                throw new Exception("ResponseParams文件不存在!!!");
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(pathXml);
            var fhz = doc.SelectSingleNode("/ROW/PO_FHZ");
            if (fhz != null)
            {
                valid.PO_FHZ = fhz.InnerText;
            }
            else
            {
                var fhzNew = doc.SelectSingleNode("/row/po_fhz");
                valid.PO_FHZ = fhzNew.InnerText;
            }
            var msg = doc.SelectSingleNode("/ROW/PO_MSG");
            if (msg != null)
            {
                valid.PO_MSG = msg.InnerText;
            }
            else
            {
                var msgNew = doc.SelectSingleNode("/row/po_msg");
                valid.PO_MSG = msgNew.InnerText;
            }

            if (isAbnormal == true)
            {
                if (valid.PO_FHZ != "1")
                {
                    throw new Exception(valid.PO_MSG);
                }
            }
            string jsonText = JsonConvert.SerializeXmlNode(doc);

            var resultData = JsonConvert.DeserializeObject<ResultData>(jsonText);
            if (resultData?.Row != null && resultData.Row.ToString() != "")
            {
                var jsonStr = JsonConvert.SerializeObject(resultData.Row);
                result = JsonConvert.DeserializeObject<T>(jsonStr);
            }

            doc = null;
            return result;
        }
        /// <summary>
        ///  返回装换为json
        /// </summary>
        /// <returns></returns>
        public static string SerializerModelJson()
        {
            string jsonStr = null;
            string pathXml = null;
            //string copyPathXml = null;
            var valid = new ValidXmlDto();
            var is64Bit = Environment.Is64BitOperatingSystem;
            if (is64Bit)
            {
                pathXml = @"C:\Program Files (x86)\Microsoft\本鼎医保插件\" + "ResponseParams.xml";
               
            }
            else
            {
                pathXml = @"C:\Program Files\Microsoft\本鼎医保插件\" + "ResponseParams.xml";
              
            }

            //string copyPathXmlNew = copyPathXml + "xmlData\\" +
            //                        DateTime.Now.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "\\" +
            //                        DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xml";
            //File.Copy(pathXml, copyPathXmlNew);

            // pathXml = System.AppDomain.CurrentDomain.BaseDirectory + "ResponseParams.xml";

            if (!System.IO.File.Exists(pathXml))
            {
                throw new Exception("ResponseParams文件不存在!!!");
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(pathXml);
            var fhz = doc.SelectSingleNode("/ROW/PO_FHZ");
            if (fhz != null)
            {
                valid.PO_FHZ = fhz.InnerText;
            }
            else
            {
                var fhzNew = doc.SelectSingleNode("/row/po_fhz");
                valid.PO_FHZ = fhzNew.InnerText;
            }
            var msg = doc.SelectSingleNode("/ROW/PO_MSG");
            if (msg != null)
            {
                valid.PO_MSG = msg.InnerText;
            }
            else
            {
                var msgNew = doc.SelectSingleNode("/row/po_msg");
                valid.PO_MSG = msgNew.InnerText;
            }

            if (valid.PO_FHZ != "1")
            {
                throw new Exception(valid.PO_MSG);
            }
            string jsonText = JsonConvert.SerializeXmlNode(doc);

            var resultData = JsonConvert.DeserializeObject<ResultData>(jsonText);
            if (resultData?.Row != null && resultData.Row.ToString() != "")
            {
                 jsonStr = JsonConvert.SerializeObject(resultData.Row);
               
            }

            doc = null;
            return jsonStr;
        }
        //实体转换
        public static T DeSerializer<T>(string strXml) where T : class
        {
            try
            {
                using (StringReader sr = new StringReader(strXml))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(sr) as T;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                // return null;
            }
        }
        public static string DeSerializerModelStr(string rowsName)
        {

            string resultData = null;
            var valid = new ValidXmlDto();
            string pathXml = null;
            string copyPathXml = null;
            var is64Bit = Environment.Is64BitOperatingSystem;
            if (is64Bit)
            {
                pathXml = @"C:\Program Files (x86)\Microsoft\本鼎医保插件\" + "ResponseParams.xml";
                copyPathXml = @"C:\Program Files (x86)\Microsoft\本鼎医保插件\";
            }
            else
            {
                pathXml = @"C:\Program Files\Microsoft\本鼎医保插件\" + "ResponseParams.xml";
                copyPathXml = @"C:\Program Files\Microsoft\本鼎医保插件\";
            }

            string copyPathXmlNew = copyPathXml + "xmlData\\" +
                                    DateTime.Now.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "\\" +
                                    DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xml";
            File.Copy(pathXml, copyPathXmlNew);
            XmlDocument doc = new XmlDocument();
            doc.Load(pathXml);
            if (!System.IO.File.Exists(pathXml))
            {
                throw new Exception("ResponseParams文件不存在!!!");
            }
            var fhz = doc.SelectSingleNode("/ROW/PO_FHZ");
            if (fhz != null)
            {
                valid.PO_FHZ = fhz.InnerText;
            }
            else
            {
                var fhzNew = doc.SelectSingleNode("/row/po_fhz");
                valid.PO_FHZ = fhzNew.InnerText;
            }
            var msg = doc.SelectSingleNode("/ROW/PO_MSG");
            if (msg != null)
            {
                valid.PO_MSG = msg.InnerText;
            }
            else
            {
                var msgNew = doc.SelectSingleNode("/row/po_msg");
                valid.PO_MSG = msgNew.InnerText;
            }
            if (valid.PO_FHZ == "1")
            {
                var rowNode = doc.SelectSingleNode("/ROW/" + rowsName) ?? doc.SelectSingleNode("/row/" + rowsName);
                string strIni = "xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'";
                resultData = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" + "<" + rowsName + " " + strIni + ">" + rowNode.InnerXml + "</" + rowsName + ">";

            }
            else
            { 
                throw new Exception(valid.PO_MSG);
            }

            doc = null;
            return resultData;
        }
        /// <summary>
        /// 实体转换保存入参
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool SaveXmlEntity<T>(T t)
        {
            string pathXml = null;
            var strXmls = XmlSerialize(t);
            bool result = false;
            if (string.IsNullOrWhiteSpace(strXmls))
            {
                return result;
            }
            else
            {
                var is64Bit = Environment.Is64BitOperatingSystem;
                if (is64Bit)
                {
                    pathXml = @"C:\Program Files (x86)\Microsoft\本鼎医保插件\" + "RequestParams.xml";
                }
                else
                {
                    pathXml = @"C:\Program Files\Microsoft\本鼎医保插件\" + "RequestParams.xml";
                }
                //pathXml = System.AppDomain.CurrentDomain.BaseDirectory + "RequestParams.xml";

                //创建XmlDocument对象
                XmlDocument xmlDocXml = new XmlDocument();
                xmlDocXml.LoadXml(strXmls);

                //创建XmlDocument对象
                XmlDocument xmlDoc = new XmlDocument();
                //XML的声明<?xml version="1.0" encoding="gb2312"?> 
                XmlDeclaration xmlSM = xmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
                //追加xmldecl位置
                xmlDoc.AppendChild(xmlSM);
                //添加一个名为Row的根节点
                XmlElement xml = xmlDoc.CreateElement("", "ROW", "");
                string strXml = xmlDocXml.SelectSingleNode("ROW").InnerXml.ToString();
                xml.InnerXml = strXml;
                xmlDoc.AppendChild(xml);


                if (File.Exists(pathXml))
                {
                    File.Delete(pathXml);
                    xmlDoc.Save(pathXml);
                    result = true;
                }
                else
                {
                    xmlDoc.Save(pathXml);
                    result = true;
                }
                xmlDoc = null;
                return result;
            }
        }
        /// <summary>
        /// 字符串保存入参
        /// </summary>
        /// <param name="strXmls"></param>
        /// <returns></returns>
        public static bool SaveXmlStr(string strXmls)
        {
            string pathXml = null;
         
            bool result = false;
            if (string.IsNullOrWhiteSpace(strXmls))
            {
                return result;
            }
            else
            {
                var is64Bit = Environment.Is64BitOperatingSystem;
                if (is64Bit)
                {
                    pathXml = @"C:\Program Files (x86)\Microsoft\本鼎医保插件\" + "RequestParams.xml";
                }
                else
                {
                    pathXml = @"C:\Program Files\Microsoft\本鼎医保插件\" + "RequestParams.xml";
                }
                //pathXml = System.AppDomain.CurrentDomain.BaseDirectory + "RequestParams.xml";

                //创建XmlDocument对象
                XmlDocument xmlDocXml = new XmlDocument();
                xmlDocXml.LoadXml(strXmls);

                //创建XmlDocument对象
                XmlDocument xmlDoc = new XmlDocument();
                //XML的声明<?xml version="1.0" encoding="gb2312"?> 
                XmlDeclaration xmlSM = xmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
                //追加xmldecl位置
                xmlDoc.AppendChild(xmlSM);
                //添加一个名为Row的根节点
                XmlElement xml = xmlDoc.CreateElement("", "ROW", "");
                string strXml = xmlDocXml.SelectSingleNode("ROW").InnerXml.ToString();
                xml.InnerXml = strXml;
                xmlDoc.AppendChild(xml);


                if (File.Exists(pathXml))
                {
                    File.Delete(pathXml);
                    xmlDoc.Save(pathXml);
                    result = true;
                }
                else
                {
                    xmlDoc.Save(pathXml);
                    result = true;
                }
                xmlDoc = null;
                return result;
            }
        }
        public static string XmlSerialize<T>(T obj)
        {
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    Type t = obj.GetType();
                    XmlSerializer serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(sw, obj);

                    sw.Close();
                    return sw.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("将实体对象转换成XML异常", ex);
            }
        }
        //检查数据文件是否存在
        public static void CheckFolders()
        {
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
        
            string pathErrorData = path + "\\xmlData\\" + DateTime.Now.Date.ToString("yyyy-MM-dd").Substring(0, 10);

            if (!System.IO.Directory.Exists(pathErrorData))
            {
                System.IO.Directory.CreateDirectory(pathErrorData);
            }
        }

        /// <summary>
        /// 获取行xml
        /// </summary>
        /// <param name="rowsObject"></param>
        /// <returns></returns>
        public static string XmlRows(dynamic rowsObject)
        {
            string str = null;
            var count = rowsObject.Count;
            //判断当前行是否存在
            if (Convert.ToInt32(count) > 0)
            {
                foreach (var entityItem in rowsObject)
                {
                    string rowsStr = "<Row>";
                    System.Reflection.PropertyInfo[] rows = entityItem.GetType()
                        .GetProperties(System.Reflection.BindingFlags.Instance |
                                       System.Reflection.BindingFlags.Public);

                    foreach (System.Reflection.PropertyInfo itemRows in rows)
                    {
                        string rowName = itemRows.Name;
                        dynamic rowValue = itemRows.GetValue(entityItem, null);

                        if (rowValue == null)
                        {
                            rowsStr += "<" + rowName + ">" + "</" + rowName + ">";
                        }
                        else
                        {
                            rowsStr += "<" + rowName + ">" + rowValue.ToString() + "</" + rowName + ">";
                        }



                    }
                    rowsStr += "</Row>";
                    str += rowsStr;
                }


            }
            return str;
        }
        /// <summary>
        /// 验证xml返回结果是否正确
        /// </summary>
        /// <param name="rowsName"></param>
        /// <returns></returns>
        public static ValidXmlDto ValidXml(string rowsName)
        {
            var result = new ValidXmlDto();

            string pathXml = System.AppDomain.CurrentDomain.BaseDirectory + "RequestParams.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(pathXml);
            XmlNode po_fhzNode = doc.SelectSingleNode("/ROW/PO_FHZ");
            result.PO_FHZ = po_fhzNode.InnerText;
            XmlNode po_msgNode = doc.SelectSingleNode("/ROW/PO_MSG");
            result.PO_MSG = po_msgNode.InnerText;
            if (result.PO_FHZ == "1")
            {
                var pO_RDXXNode = doc.SelectNodes("/ROW/" + rowsName + "/ROW");
                result.IsRows = pO_RDXXNode.Count > 1 ? true : false;
            }
            return result;

        }
        ///// <summary>
        ///// 特殊疾病认定查询
        ///// </summary>
        ///// <param name="t"></param>
        ///// <returns></returns>
        //public static IdentificationSpecialQueryDto IdentificationSpecialQueryDeSerializerModel(IdentificationSpecialQueryDto t)
        //{
        //    var result = new IdentificationSpecialQueryDto();

        //    string pathXml = System.AppDomain.CurrentDomain.BaseDirectory + "RequestParams.xml";
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(pathXml);

        //    XmlNode po_fhzNode = doc.SelectSingleNode("/ROW/PO_FHZ");
        //    result.PO_FHZ = po_fhzNode.InnerText;
        //    XmlNode po_msgNode = doc.SelectSingleNode("/ROW/PO_MSG");
        //    result.PO_MSG = po_msgNode.InnerText;
        //    if (result.PO_FHZ == "1")
        //    {
        //         var po_RDXXRows= new List<PO_RDXX>(); 
        //         var pO_RDXXNode = doc.SelectNodes("/ROW/PO_RDXX/ROW");
        //        foreach (XmlNode po_rdxxItem in pO_RDXXNode)
        //        {
        //            var po_RDXXRow = DeSerializerModelXmlNode(new PO_RDXXRow(), po_rdxxItem);
        //            var po_RDXXRowRow = po_RDXXRow.Row;
        //            var po_JBXXNode= po_rdxxItem.SelectNodes("PO_JBXX/ROW");
        //            //获取PO_JBXX节点数据
        //            var po_JBXXRows = new List<PO_JBXX>();
        //            foreach (XmlNode po_JBXXItem in po_JBXXNode)
        //            {
        //                var po_JBXXRow = DeSerializerModelXmlNode(new PO_JBXXRow(), po_JBXXItem);
        //                po_JBXXRows.Add(po_JBXXRow.Row);
        //            }

        //            po_RDXXRowRow.PO_JBXXs = po_JBXXRows;
        //            po_RDXXRows.Add(po_RDXXRowRow);
        //        }
        //        result.PO_RDXXs = po_RDXXRows;
        //    }
        //    return result;
        //}
        public static T DeSerializerModelXmlNode<T>(T t, XmlNode xmlNode)
        {

            string jsonTextc = JsonConvert.SerializeXmlNode(xmlNode);
            var resultData = JsonConvert.DeserializeObject<T>(jsonTextc);
            return resultData;

        }
    }
}
