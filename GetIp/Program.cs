using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Threading;

namespace GetIp
{
    class Program
    {
        static void Main(string[] args)
        {
            string OriginalIp = string.Empty;

            while (true)
            {
                string myIp = string.Empty;
                try
                {
                    myIp = GetWebContent("http://www.3322.org/dyndns/getip");
                }
                catch (Exception ex)
                {
                    Util.Adr_LogManager.WriteLog(Util.LogType.Warning, "获取IP出错：" + ex.Message);
                    myIp = string.Empty;
                }

                if (myIp != string.Empty && OriginalIp != myIp)
                {
                    OriginalIp = myIp;
                    Console.Write(DateTime.Now.ToString() + " >> 您的IP地址是：" + myIp);
                    SendEmail(myIp);
                }
                Thread.Sleep(1000 * 60);
            }
            ///Console.ReadLine();
        }

        private static string GetWebContent(string sUrl)
        {
            string strResult = "";
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
                    StreamReader streamReader = new StreamReader(streamReceive, encoding);
                    strResult = streamReader.ReadToEnd();
                }
            }
            catch (Exception exp)
            {
                strResult = "";
            }
            return strResult;
        }

        static void SendEmail(string ip)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(
                "heuandmei@qq.com", "成都服务器IP地址", Encoding.UTF8);
            mail.To.Add("heuandmei@qq.com,jekey.sinotop@foxmail.com,271134337@qq.com");
            mail.Subject = "成都服务器IP地址变更" + DateTime.Now.ToString();
            mail.SubjectEncoding = Encoding.Default;
            mail.Body = "IP地址已变更为："+ip;
            mail.BodyEncoding = Encoding.Default;
            mail.IsBodyHtml = false;
            mail.Priority = MailPriority.Normal;

            SmtpClient client = new SmtpClient();
            client.Host = "smtp.qq.com";
            client.Port = 25;
            //是否使用安全套接字层加密连接  
            client.EnableSsl = false;
            //不使用默认凭证，注意此句必须放在 client.Credentials 的上面  
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("heuandmei@qq.com", "meandmei");
            //邮件通过网络直接发送到服务器  
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(mail);
                Util.Adr_LogManager.WriteLog(Util.LogType.Trace, "发信成功");
            }
            catch (SmtpException ex)
            {
                Util.Adr_LogManager.WriteLog(Util.LogType.Trace, "发信失败（1）：" + ex.Message);
            }
            catch (Exception ex)
            {
                Util.Adr_LogManager.WriteLog(Util.LogType.Trace, "发信失败（2）：" + ex.Message);
            }
            finally
            {
                mail.Dispose();
                client = null;
            } 
        }
    }
}
