using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract.Model
{
    /// <summary>
    /// 打印底图model
    /// </summary>
    public class Bot : Data
    {
        public Bot(object id = null, object pid = null, object url = null, object pic = null, object dated = null)
            : base(id, pid, url, pic, dated)
        { }
    }
}
