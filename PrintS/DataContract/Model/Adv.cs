using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract.Model
{
    /// <summary>
    /// 轮播图model
    /// </summary>
    public class Adv : Data
    {
        public Adv(object id = null, object pid = null, object url = null, object pic = null, object dated = null)
            : base(id, pid, url, pic, dated)
        { }

    }
}
