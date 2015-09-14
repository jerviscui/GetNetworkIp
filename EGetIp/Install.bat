%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe EGetIp.exe
::Copy GetIpConfig.ini c:\GetIpConfig.ini
Net Start EGetIp
sc config EGetIp start= auto
pause