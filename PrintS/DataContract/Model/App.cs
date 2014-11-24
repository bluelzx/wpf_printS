using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract.Model
{
    /// <summary>
    /// 程序运行状态
    /// </summary>
    public enum AppStatus
    {
        /// <summary>
        /// 故障
        /// </summary>
        warn = 0,
        /// <summary>
        /// 运行
        /// </summary>
        play,
        /// <summary>
        /// 暂停
        /// </summary>
        pause
    }

    public enum MessageCode
    {
        /// <summary>
        /// 无效消息
        /// </summary>
        none = 0,
        /// <summary>
        /// 打印机缺纸
        /// </summary>
        printOutPaper,
        /// <summary>
        /// 打印机补充纸
        /// </summary>
        printPaper
    }

    public class App
    {
    }
}
