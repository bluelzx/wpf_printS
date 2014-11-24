using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace CommonLib.Tools
{
    public class Tools
    {
        /// <summary>
        /// 记录日子好
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="path">日志文件路径</param>
        public static void WriteLog(string msg, string path = "Exception.log")
        {
            StreamWriter log = new StreamWriter(path, true);
            log.WriteLine(string.Format("{0}\t{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg));
            log.Close(); 
        }
    }
}
