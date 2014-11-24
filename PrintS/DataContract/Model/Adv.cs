using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract.Model
{
    /// <summary>
    /// 轮播图model，对应数据库表结构
    /// </summary>
    public class Adv
    {
        private int _id;
        /// <summary>
        /// 轮播图id
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
        /// 轮播图的url地址
        /// </summary>
        public string url
        {
            get { return _url; }
            set { _url = value; }
        }

        private string _pic;
        /// <summary>
        /// 轮播图的本地地址
        /// </summary>
        public string pic
        {
            get { return _pic; }
            set { _pic = value; }
        }

        private string _dated;
        /// <summary>
        /// 获取轮播图的时间
        /// </summary>
        public string dated
        {
            get { return _dated; }
            set { _dated = value; }
        }

        /// <summary>
        /// 添加轮播图
        /// </summary>
        /// <param name="id">轮播图id</param>
        /// <param name="pid">服务端id</param>
        /// <param name="url">图片url地址</param>
        /// <param name="pic">图片本地地址</param>
        /// <param name="dated">获取时间</param>
        public Adv(object id = null, object pid = null, object url = null, object pic = null, object dated = null)
        {
            this._id = Convert.ToInt32(id);
            this._pid = Convert.ToInt32(pid);
            this._url = Convert.ToString(url);
            this._pic = Convert.ToString(pic);
            this._dated = Convert.ToString(dated);
        }
    }
}
