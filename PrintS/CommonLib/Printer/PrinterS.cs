using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib.Printer
{
    // 打印机可用状态集合
    public enum PrinterSStatus
    {
        /// <summary>
        /// 故障
        /// </summary>
        Warn = 0,
        /// <summary>
        /// 打印中
        /// </summary>
        Print,
        /// <summary>
        /// 空闲中
        /// </summary>
        Ready,
        /// <summary>
        /// 其他状态
        /// </summary>
        Other
    }

    public class PrinterS : PrinterSys
    {
        public PrinterS(string name)
            : base(name)
        { 

        }

        /// <summary>
        /// 获取打印机状态
        /// </summary>
        /// <returns></returns>
        public PrinterSStatus getStatus()
        {
            PrinterSysStatus statusSys = base.getStatus();
            PrinterSStatus status;
            switch (statusSys)
            {
                // Epson
                case PrinterSysStatus.Free:
                    status = PrinterSStatus.Ready;
                    break;
                case PrinterSysStatus.Print:
                case PrinterSysStatus.Printing:
                    status = PrinterSStatus.Print;
                    break;
                case PrinterSysStatus.Warmup:
                    status = PrinterSStatus.Other;
                    break;
                default:
                    status = PrinterSStatus.Warn;
                    break;
            }
            return status;
        }
    }
}
