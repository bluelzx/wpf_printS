using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract.Model
{
    public class Data
    {
        private int _id;
        /// <summary>
        /// 图片资源id
        /// </summary>
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _pid;
        /// <summary>
        /// 服务端id
        /// </summary>
        public int pid
        {
            get { return _pid; }
            set { _pid = value; }
        }

        private string _url;
        /// <summary>
        /// 图片资源的url地址
        /// </summary>
        public string url
        {
            get { return _url; }
            set { _url = value; }
        }

        private string _pic;
        /// <summary>
        /// 图片资源的本地地址
        /// </summary>
        public string pic
        {
            get { return _pic; }
            set { _pic = value; }
        }

        private string _dated;
        /// <summary>
        /// 获取图片资源的时间
        /// </summary>
        public string dated
        {
            get { return _dated; }
            set { _dated = value; }
        }

        /// <summary>
        /// 添加图片资源
        /// </summary>
        /// <param name="id">图片资源id</param>
        /// <param name="pid">服务端id</param>
        /// <param name="url">图片url地址</param>
        /// <param name="pic">图片本地地址</param>
        /// <param name="dated">获取时间</param>
        public Data(object id = null, object pid = null, object url = null, object pic = null, object dated = null)
        {
            this._id = Convert.ToInt32(id);
            this._pid = Convert.ToInt32(pid);
            this._url = Convert.ToString(url);
            this._pic = Convert.ToString(pic);
            this._dated = Convert.ToString(dated);
        }
    }
}
