using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.YinHai
{
  public  class YinHaiGetBaseParam
    {
        /// <summary>
        /// 经办人类别  * 代码标识
        /// </summary>
        public string opter_type { get; set; } = "1";

        /// <summary>
        /// 交易签到流水号 
        /// </summary>
        public string sign_no { get; set; } = "";
        /// <summary>
        /// 发送方报文 ID *
        /// </summary>
        public string msgid { get; set; }
        /// <summary>
        /// 数字签名信息
        /// </summary>
        public string cainfo { get; set; } = "000000000000";

        /// <summary>
        /// 定点医药机构编号 *
        /// </summary>
        public string fixmedins_code { get; set; } = "H51200200049";

        /// <summary>
        /// 按地方要求传入经办人姓名/终端名称
        /// </summary>
        public string opter_name { get; set; } = "510802195209130041";
       
        /// <summary>
        /// 按地方要求传入经办人编号/终端编号 *
        /// </summary>
        public string opter { get; set; } = "23003";
        /// <summary>
        /// 就医地医保区划 *  后台获取
        /// </summary>
        public string mdtrtarea_admvs { get; set; } = "512000";
        /// <summary>
        /// 接收方系统代码* 
        /// </summary>
        public string recer_sys_code { get; set; } = "01";
        /// <summary>
        /// 交易输入  Y
        /// </summary> 
        public object input { get; set; }
        /// <summary>
        /// 定点医药机构名称 *
        /// </summary>

        public string fixmedins_name { get; set; } = "资阳市第一人民医院";
        /// <summary>
        /// 设备编号
        /// </summary>
        public string dev_no { get; set; } = "";

        /// <summary>
        /// 设备安全信息
        /// </summary>
        public string dev_safe_info { get; set; } = "";
        /// <summary>
        /// 交易码 *
        /// </summary>
        public string infno { get; set; }

        /// <summary>
        /// 交易时间 *
        /// </summary>
        public string inf_time { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 参保地医保区划 (如果交易输入中含有人员编号，此项必填)
        /// </summary>
        public string insuplc_admdvs { get; set; } = "512000";
        /// <summary>
        /// 签名类型
        /// </summary>
        public string signtype { get; set; } = "SM3";//SM3
        /// <summary>
        /// 接口版本号 *
        /// </summary>
        public string infver { get; set; } = "1.0";


      
      

        


      
      
    }
}
