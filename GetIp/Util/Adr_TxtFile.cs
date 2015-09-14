using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GetIp.Util
{
    /// <summary>
    /// *.txt 文本帮助类
    /// </summary>
    public class Adr_TxtFile
    {
        /// <summary>
        /// 读取指定路径的Txt文件内容
        /// </summary>
        /// <param name="filePath">txt文件物理路径</param>
        /// <returns></returns>
        public static string ReadAsString(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
            {
                string strContent = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
                return strContent;
            };
        }

        /// <summary>
        /// <para>读取指定路径的Txt文件内容</para>
        /// <para>将文本中的每一行字符串保存在List中</para>
        /// </summary>
        /// <param name="filePath">txt文件物理路径</param>
        /// <returns></returns>
        public static List<string> ReadAsList(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
            {
                List<string> list = new List<string>();
                while (!sr.EndOfStream)
                {
                    list.Add(sr.ReadLine());
                }
                return list;
            };
        }

        /// <summary>
        /// <para>在指定的文件中写入文本内容</para>
        /// <para>如果指定的路径文件不存在，则自动创建该文件</para>
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">文本内容</param>
        public static void Write(string filePath, string content)
        {
            using (StreamWriter sw = new StreamWriter(filePath,false,Encoding.UTF8))
            {
                sw.Write(content);
                sw.Close();
                sw.Dispose();
            }
        }

        /// <summary>
        /// <para>在置顶的文件中追加文本内容</para>
        /// <para>如果指定的路径文件不存在，则自动创建该文件</para>
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">要追加的文本内容</param>
        public static void WriteAppend(string filePath, string content)
        {
            using (StreamWriter sw = new StreamWriter(filePath,true,Encoding.UTF8))
            {
                sw.Write(content);
                sw.Close();
                sw.Dispose();
            }
        }
    }
}
