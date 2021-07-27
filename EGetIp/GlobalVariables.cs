using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using EGetIp.Util;
using System.IO;

namespace EGetIp
{
    /// <summary>
    /// 全局变量类
    /// </summary>
    public static class GlobalVariables
    {
        public static void InitializationConfig()
        {
	        const string file = "GetIpConfig.ini";
	        string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
			//configPath = @"c:/GetIpConfig.ini";
            
            if (File.Exists(configPath) == false)
            {
                SysLog.WriteEntry("EGetIp", "未能在路径中找到配置文件 " + configPath, SysLog.LogType.Error, 404);
                throw new Exception("[EGetIp配置文件错误]未能在路径中找到配置文件 " + configPath);
            }
            Adr_IniFile ini = new Adr_IniFile(configPath);

            Config.SendName = ini.Read("SendMail", "SendName");
            if (string.IsNullOrEmpty(Config.SendName))
            {
                SysLog.WriteEntry("EGetIp", "未能读取到SendMail节SendName项", SysLog.LogType.Error, 404);
                throw new Exception("[EGetIp配置文件错误]未能读取到SendMail节SendName项");
            }

            Config.SendTitle = ini.Read("SendMail", "SendTitle");
            if (string.IsNullOrEmpty(Config.SendTitle))
            {
                SysLog.WriteEntry("EGetIp", "未能读取到SendMail节SendTitle项", SysLog.LogType.Error, 404);
                throw new Exception("[EGetIp配置文件错误]未能读取到SendMail节SendTitle项");
            }
            // Downloads By http://www.veryhuo.com
            Config.SmtpHost = ini.Read("SendMail", "SmtpHost");
            if (string.IsNullOrEmpty(Config.SmtpHost))
            {
                SysLog.WriteEntry("EGetIp", "未能读取到SendMail节SmtpHost项", SysLog.LogType.Error, 404);
                throw new Exception("[EGetIp配置文件错误]未能读取到SendMail节SmtpHost项");
            }

            Config.SmtpPort = int.Parse(ini.Read("SendMail", "SmtpPort"));
            if (Config.SmtpPort < 1 || Config.SmtpPort > 65535)
            {
                SysLog.WriteEntry("EGetIp", "配置文件中SendMail节SmtpPort项的取值范围应该在1 - 65535 之内", SysLog.LogType.Error, 404);
                throw new Exception("[EGetIp配置文件错误]配置文件中SendMail节SmtpPort项的取值范围应该在1 - 65535 之内");
            }

            Config.SmtpLoginUser = ini.Read("SendMail", "SmtpLoginUser");
            if (string.IsNullOrEmpty(Config.SmtpLoginUser))
            {
                SysLog.WriteEntry("EGetIp", "未能读取到SendMail节SmtpLoginUser项", SysLog.LogType.Error, 404);
                throw new Exception("[EGetIp配置文件错误]未能读取到SendMail节SmtpLoginUser项");
            }

            Config.SmtpLoginPwd = ini.Read("SendMail", "SmtpLoginPwd");
            if (string.IsNullOrEmpty(Config.SmtpLoginPwd))
            {
                SysLog.WriteEntry("EGetIp", "未能读取到SendMail节SmtpLoginPwd项", SysLog.LogType.Error, 404);
                throw new Exception("[EGetIp配置文件错误]未能读取到SendMail节SmtpLoginPwd项");
            }

            Config.ReceiveMailList.AddRange(ini.GetValues("ReceiveMailList"));
            if (Config.ReceiveMailList.Count == 0)
            {
                SysLog.WriteEntry("EGetIp", "未能读取ReceiveMailList中有收件人地址列表", SysLog.LogType.Error, 404);
                throw new Exception("[EGetIp配置文件错误]未能读取ReceiveMailList中有收件人地址列表");
            }
            for (int i = 0; i < Config.ReceiveMailList.Count; i++)
            {
                Config.ReceiveMailList[i] = Config.ReceiveMailList[i].Substring(Config.ReceiveMailList[i].IndexOf('=')+1);
            }
        }

        /// <summary>
        /// 配置信息
        /// </summary>
        public static class Config
        {
            static Config()
            {
                ReceiveMailList = new List<string>();
            }

            // 发送者名称
            public static string SendName { get; set; }
            // 邮件标题
            public static string SendTitle { get; set; }
            // SMTP服务器
            public static string SmtpHost { get; set; }
            // SMTP端口号
            public static int SmtpPort { get; set; }
            // SMTP登陆邮箱账号
            public static string SmtpLoginUser { get; set; }
            // SMTP登陆密码
            public static string SmtpLoginPwd { get; set; }

            // 接收邮件的邮箱地址列表
            public static List<string> ReceiveMailList { get; set; }
        }
    }
}
