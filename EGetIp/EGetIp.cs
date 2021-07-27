using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using EGetIp.Util;
using System.Threading;

namespace EGetIp
{
    public partial class EGetIp : ServiceBase
    {
        public EGetIp()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
			Thread.Sleep(1000 * 30);

            //初始化配置文件的参数
            GlobalVariables.InitializationConfig();
            Thread getIpThread = new Thread(GetIp);
            getIpThread.IsBackground = true;
            getIpThread.Start();
        }

        protected override void OnStop()
        {
            SysLog.WriteEntry("EGetIp", "停止了服务" + DateTime.Now, SysLog.LogType.Information, 0);
        }

        private void GetIp()
        {
            string originalIp = string.Empty;

            while (true)
            {
                string myIp = string.Empty;
				//http://www.ip138.com/
				myIp = WebHelper.GetWebContent("http://1111.ip138.com/ic.asp");

				//<center>您的IP是：[183.49.88.234] 来自：广东省深圳市 电信</center>
				int start = myIp.IndexOf("您的IP是：[", StringComparison.CurrentCultureIgnoreCase);
				int end = myIp.IndexOf("</center>", StringComparison.CurrentCultureIgnoreCase);
				
				if (start > -1 && end > -1)
				{
					myIp = myIp.Substring(start, end - start);
					if (myIp != string.Empty && originalIp != myIp)
					{
						originalIp = myIp;
						Console.Write(DateTime.Now.ToString() + " >> " + myIp);
						Util.EMail.SendEmail(myIp);
					}
				}

                Thread.Sleep(1000 * 60);
            }
        }

    }
}
