using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace EGetIp.Util
{
    public static class EMail
    {
        public static void SendEmail(string ip)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(
                GlobalVariables.Config.SmtpLoginUser, GlobalVariables.Config.SendTitle, Encoding.UTF8);
            foreach (var item in GlobalVariables.Config.ReceiveMailList)
            {
                mail.To.Add(item);
            }
            mail.Subject = GlobalVariables.Config.SendTitle;
            mail.SubjectEncoding = Encoding.Default;
            mail.Body = "IP地址已变更：" + ip;
            mail.BodyEncoding = Encoding.Default;
            mail.IsBodyHtml = false;
            mail.Priority = MailPriority.Normal;

            SmtpClient client = new SmtpClient();
            client.Host = GlobalVariables.Config.SmtpHost;
            client.Port = GlobalVariables.Config.SmtpPort;
            //是否使用安全套接字层加密连接  
            client.EnableSsl = false;
            //不使用默认凭证，注意此句必须放在 client.Credentials 的上面  
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(GlobalVariables.Config.SmtpLoginUser, GlobalVariables.Config.SmtpLoginPwd);
            //邮件通过网络直接发送到服务器  
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(mail);
                SysLog.WriteEntry("EGetIp", "发信成功，" + DateTime.Now, SysLog.LogType.Information, 1);
            }
            catch (SmtpException ex)
            {
                SysLog.WriteEntry("EGetIp", ex, 4);
            }
            catch (Exception ex)
            {
                SysLog.WriteEntry("EGetIp", ex, 5);
            }
            finally
            {
                mail.Dispose();
                client = null;
            }
        }
    }
}
