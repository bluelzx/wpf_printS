using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract.Model
{
    /// <summary>
    /// 打印任务model，对应数据库表结构
    /// </summary>
    public class Task
    {
        private int _id;
        /// <summary>
        /// 打印任务id
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
        /// 用户照片的url地址
        /// </summary>
        public string url
        {
            get { return _url; }
            set { _url = value; }
        }

        private string _pic;
        /// <summary>
        /// 用户照片的本地地址
        /// </summary>
        public string pic
        {
            get { return _pic; }
            set { _pic = value; }
        }

        private int _state;
        /// <summary>
        /// 打印任务状态
        /// </summary>
        public int state
        {
            get { return _state; }
            set { _state = value; }
        }

        private string _created;
        /// <summary>
        /// 获取任务的时间
        /// </summary>
        public string created
        {
            get { return _created; }
            set { _created = value; }
        }

        private string _updated;
        /// <summary>
        /// 更新任务的时间
        /// </summary>
        public string updated
        {
            get { return _updated; }
            set { _updated = value; }
        }

        /// <summary>
        /// 添加打印任务
        /// </summary>
        /// <param name="id">任务id</param>
        /// <param name="pid">服务端id</param>
        /// <param name="url">用户照片url地址</param>
        /// <param name="pic">用户照片的本地地址</param>
        /// <param name="state">任务状态</param>
        /// <param name="created">任务获取时间</param>
        /// <param name="updated">任务更新时间</param>
        public Task(object id = null, object pid = null, object url = null, object pic = null, object state = null, object created = null, object updated = null)
        {
            this._id = Convert.ToInt32(id);
            this._pid = Convert.ToInt32(pid);
            this._url = Convert.ToString(url);
            this._pic = Convert.ToString(pic);
            this._state = Convert.ToInt32(state);
            this._created = Convert.ToString(created);
            this._updated = Convert.ToString(updated);
        }
    }
}
