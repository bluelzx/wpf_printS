using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// CommonLib.dll
using CommonLib;
using CommonLib.DataBase;

using System.Data;


namespace DataContract.Controller
{
    public class DataController
    {
        private Sqlite ms;
        private string table;

        public DataController(Sqlite _ms, string _table)
        {
            this.ms = _ms;
            this.table = _table;
        }

        /// <summary>
        /// 获取在用数据总数
        /// </summary>
        /// <returns></returns>
        public int countData()
        {
            string sql = "select count(*) from `" + this.table + "` where `state`=1 limit 1";
            int count = Convert.ToInt32(ms.getOne(sql));
            return count;
        }

        /// <summary>
        /// 删除不用的数据（state=2）
        /// </summary>
        /// <param name="pid_arr">在用数据的pid数组</param>
        public void delOtherData(string[] pid_arr)
        {
            if (pid_arr.Count() > 0)
            {
                string pids = string.Join(",", pid_arr);
                string sql = "update `" + this.table + "` set `state`=2 where `pid` not in (" + pids + ")";
                this.ms.query(sql);
            }
            else
            {
                string sql = "update `" + this.table + "` set `state`=2";
                this.ms.query(sql);
            }
        }

        /// <summary>
        /// 插入新数据，默认启用
        /// </summary>
        /// <param name="_pid">服务端id</param>
        /// <param name="_url">图片url地址</param>
        /// <param name="_pic">图片本地地址</param>
        /// <returns></returns>
        public int addData(object _pid, object _url, object _pic)
        {
            int pid = Convert.ToInt32(_pid);
            string url = Convert.ToString(_url);
            string pic = Convert.ToString(_pic);
            string dated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int state = 1;
            string sql = "insert into `" + this.table + "`(`pid`,`url`,`pic`,`state`,`dated`)values(@pid,@url,@pic,@state,@dated)";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@pid", pid);
            param.Add("@url", url);
            param.Add("@pic", pic);
            param.Add("@state", state);
            param.Add("@dated", dated);
            int id = ms.insert(sql, true, param);
            return id;
        }

        /// <summary>
        /// pid是否已存在
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public bool existPid(object _pid)
        {
            int pid = Convert.ToInt32(_pid);
            string sql = "select count(*) from `" + this.table + "` where `pid`=@pid and `state`=1 limit 1";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@pid", pid);
            int count = Convert.ToInt32(ms.getOne(sql, param));
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 返回在用数据数组
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public DataRow[] getDatas(int limit = 100)
        {
            string sql = "select * from `" + this.table + "` where `state`=1 order by `id` desc limit @limit";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@limit", limit);
            DataRow[] rows = ms.getRows(sql, param);
            return rows;
        }

        /// <summary>
        /// 返回在用数据
        /// </summary>
        /// <returns></returns>
        public DataRow getData()
        {
            string sql = "select * from `" + this.table + "` where `state`=1 order by `id` desc limit 1";
            DataRow row = ms.getRow(sql);
            return row;
        }
    }
}
