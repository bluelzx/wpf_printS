using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib.Printer
{
    // 打印机状态集合
    public enum PrinterSysStatus
    {
        /// <summary>
        /// 其他状态
        /// </summary>
        Other = 1,
        /// <summary>
        /// 未知
        /// </summary>
        Unknown,
        /// <summary>
        /// 空闲
        /// </summary>
        Free,
        /// <summary>
        /// 正在打印
        /// </summary>
        Print,
        /// <summary>
        /// 预热
        /// </summary>
        Warmup,
        /// <summary>
        /// 停止打印
        /// </summary>
        Stop,
        /// <summary>
        /// 打印中
        /// </summary>
        Printing,
        /// <summary>
        /// 离线
        /// </summary>
        Offline,
    }

    public class PrinterSys : Printer
    {
        public PrinterSys(string name)
            : base(name)
        { 
        }

        /// <summary>
        /// 获取打印机状态
        /// </summary>
        /// <returns></returns>
        public PrinterSysStatus getStatus()
        {
            return (PrinterSysStatus)base.getStatus();
        }
    }
}
