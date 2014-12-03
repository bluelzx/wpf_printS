using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// CommonLib.dll
using CommonLib.DataBase;

namespace DataContract.Controller
{
    public class PrintCController : MsgController
    {
        public PrintCController(string _table = "printc")
            : base(_table)
        { }

    }
}
