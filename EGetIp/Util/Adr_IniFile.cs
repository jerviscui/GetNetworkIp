using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace EGetIp.Util
{
    /// <summary>
    /// 提供对INI文件的操作帮助
    /// </summary>
    public class Adr_IniFile
    {

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        //读取段里的所有数据
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileSectionW", CharSet = CharSet.Unicode)]
        private extern static int getSectionValues(string Section,
        [MarshalAs(UnmanagedType.LPWStr)] string szBuffer, int nlen, string filename);

        private static readonly char[] sept = { '\0' };	//分隔字符

        /// <summary>
        /// 设置或获取Ini文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// *.ini 文件操作构造函数
        /// </summary>
        /// <param name="IniPath">文件路径</param>
        public Adr_IniFile(string IniPath)
        {
            FilePath = IniPath;
        }

        /// <summary>
        /// 向Ini文件写入一个值
        /// </summary>
        /// <param name="Section">节点名称</param>
        /// <param name="Key">键名称</param>
        /// <param name="Value">写入的值</param>
        public void Write(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.FilePath);
        }

        /// <summary>
        /// 读取Ini文件指定节点的键值
        /// </summary>
        /// <param name="Section">节点名称</param>
        /// <param name="Key">键名称</param>
        public string Read(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(256);
            int i = GetPrivateProfileString(Section, Key, "", temp, 256, this.FilePath);
            return temp.ToString();
        }

        /// <summary>
        /// 读取段里的数据到一个字符串数组
        /// </summary>
        /// <param name="section">段名</param>
        /// <param name="bufferSize">读取的数据大小(字节)</param>
        /// <returns>成功则不为null</returns>
        public string[] GetValues(string section)
        {
            string buffer = new string('\0', 32768);
            int nlen = getSectionValues(section, buffer, 32768, FilePath) - 1;
            if (nlen > 0)
            {
                return buffer.Substring(0, nlen).Split(sept);
            }
            return null;
        }

        /// <summary>
        /// 从一个段中读取其 键-值 数据
        /// </summary>
        /// <param name="section">段名</param>
        /// <param name="bufferSize">读取的数据大小(字节)</param>
        /// <returns>成功则不为null</returns>
        public Dictionary<string, string> GetDictionary(string section)
        {
            string[] sztmp = GetValues(section);
            if (sztmp != null)
            {
                int ArrayLen = sztmp.Length;
                if (ArrayLen > 0)
                {
                    Dictionary<string, string> dtRet = new Dictionary<string, string>();
                    for (int i = 0; i < ArrayLen; i++)
                    {
                        int pos1 = sztmp[i].IndexOf('=');
                        if (pos1 > 1)
                        {
                            int nlen = sztmp[i].Length;
                            //	取键名,键值
                            pos1++;
                            if (pos1 < nlen)
                                dtRet.Add(sztmp[i].Substring(0, pos1 - 1), sztmp[i].Substring(pos1, nlen - pos1));
                        }
                    }
                    return dtRet;
                }
            }
            return null;
        }


    }
}
