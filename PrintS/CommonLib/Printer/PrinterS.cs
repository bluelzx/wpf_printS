using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib.Printer
{
    // 打印机可用状态集合
    public enum enum_printerS_status
    {
        /// <summary>
        /// 故障
        /// </summary>
        warn = 0,
        /// <summary>
        /// 打印中
        /// </summary>
        print,
        /// <summary>
        /// 空闲中
        /// </summary>
        ready,
        /// <summary>
        /// 其他状态
        /// </summary>
        other
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
        public enum_printerS_status getStatus()
        {
            enum_printerSys_status statusSys = base.getStatus();
            enum_printerS_status status;
            switch (statusSys)
            {
                // Epson
                case enum_printerSys_status.free:
                    status = enum_printerS_status.ready;
                    break;
                case enum_printerSys_status.print:
                case enum_printerSys_status.printing:
                    status = enum_printerS_status.print;
                    break;
                case enum_printerSys_status.warmup:
                    status = enum_printerS_status.other;
                    break;
                default:
                    status = enum_printerS_status.warn;
                    break;
            }
            return status;
        }
    }
}
