using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// CommonLib.dll
using CommonLib.DataBase;

namespace DataContract.Controller
{
    public class PrintSController : MsgController
    {
        public PrintSController(string _table = "prints")
            : base(_table)
        { }

    }
}
