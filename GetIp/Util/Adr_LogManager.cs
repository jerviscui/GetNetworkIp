using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;

namespace GetIp.Util
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class Adr_LogManager
    {
        /// <summary>
        /// 日志文件路径（默认为当前程序目录下的Log文件夹）
        /// </summary>
        public static string logPath = @"Log";
        /// <summary>
        /// 保存日志的文件夹
        /// </summary>
        public static string LogPath
        {
            get
            {
                if (logPath == string.Empty)
                {
                    //if (HttpContext.Current == null)
                        // Windows Forms 应用
                        logPath = AppDomain.CurrentDomain.BaseDirectory;
                    //else
                    //    // Web 应用
                    //    logPath = AppDomain.CurrentDomain.BaseDirectory + @"bin\";
                }
                return logPath;
            }
            set { logPath = value; }
        }

        private static string logFielPrefix = "EXC";
        /// <summary>
        /// 日志文件前缀
        /// </summary>
        public static string LogFielPrefix
        {
            get { return logFielPrefix; }
            set { logFielPrefix = value; }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        private static void WriteLog(string logFile, string msg)
        {
            try
            {
                Directory.CreateDirectory(LogPath + "\\" + DateTime.Now.ToString("yyyyMMdd"));
                StreamWriter sw = File.AppendText(LogPath + "\\" +
                    DateTime.Now.ToString("yyyyMMdd") + "\\" +
                    LogFielPrefix + logFile + " " +
                    DateTime.Now.ToString("yyyyMMdd") + ".Log"
                );
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss: ") + msg);
                sw.Close();
            }
            catch
            { }
        }

        /// <summary>
        /// 写入txt日志
        /// </summary>
        /// <param name="logFile">日志类型</param>
        /// <param name="msg">日志信息</param>
        public static void WriteLog(LogType logFile, string msg)
        {
            WriteLog(logFile.ToString(), msg);
        }

        /// <summary>
        /// 写入txt日志
        /// </summary>
        /// <param name="logFile">日志类型</param>
        /// <param name="msg">日志信息</param>
        /// <param name="filePath">指定文件路径，不存在则创建，存在则追加</param>
        public static void WriteLog(LogType logFile, string msg, string filePath)
        {
            try
            {
                Directory.CreateDirectory(filePath);
                StreamWriter sw = File.AppendText(LogPath + "\\" +
                    DateTime.Now.ToString("yyyyMMdd") + "\\" +
                    LogFielPrefix + logFile + " " +
                    DateTime.Now.ToString("yyyyMMdd") + ".Log"
                );
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss: ") + msg);
                sw.Close();
            }
            catch
            { }
        }
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 跟踪日志
        /// </summary>
        Trace,
        /// <summary>
        /// 警告日志
        /// </summary>
        Warning,
        /// <summary>
        /// 错误日志
        /// </summary>
        Error,
        /// <summary>
        /// SQL日志
        /// </summary>
        SQL
    }
}

