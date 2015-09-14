using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace EGetIp.Util
{
    public static class WebHelper
    {
        public static string GetWebContent(string sUrl)
        {
            string strResult = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sUrl);
                //声明一个HttpWebRequest请求
                request.Timeout = 3000000;
                //设置连接超时时间
                request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.ToString() != "")
                {
                    Stream streamReceive = response.GetResponseStream();
                    Encoding encoding = Encoding.GetEncoding("UTF-8");
					encoding = Encoding.GetEncoding("gb2312");
                    StreamReader streamReader = new StreamReader(streamReceive, encoding);
                    strResult = streamReader.ReadToEnd();
                }
            }
            catch (Exception exp)
            {
                strResult = string.Empty;
            }
            return strResult;
        }
    }
}
