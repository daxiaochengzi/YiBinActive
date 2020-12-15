using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Service
{
    public static class NationEcTrans64Dll
    {
        #region 电子医保支付
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pi_jydm">交易码</param>
        /// <param name="pi_url">地址</param>
        /// <param name="po_fhz"></param>
        /// <param name="po_msg"></param>
        /// <returns></returns>
        [DllImport("yyjks.dll", EntryPoint = "NationEcTrans_call", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NationEcTrans_call(
            string pi_jydm,
            string pi_url,
            byte[] po_fhz, byte[] po_msg
        );
        [DllImport("yyjks.dll", EntryPoint = "ConnectAppServer_cxjb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConnectAppServer_cxjb(string aLoginID, string aUserPwd);
        #endregion

    }
}
