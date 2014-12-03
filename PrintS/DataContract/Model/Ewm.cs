using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract.Model
{
    /// <summary>
    /// 二维码model
    /// </summary>
    public class Ewm : Data
    {
        public Ewm(object id = null, object pid = null, object url = null, object pic = null, object dated = null)
            : base(id, pid, url, pic, dated)
        { }
    }
}
