using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// CommonLib.dll
using CommonLib;
using CommonLib.DataBase;
using CommonLib.Http;

using System.Data;
using System.IO;

using DataContract.Model;

// Newtonsoft.Json.dll
using Newtonsoft.Json.Linq;

namespace DataContract.Controller
{
    public class DataController
    {
        private Sqlite ms;
        private string table;

        public DataController(string _table)
        {
            this.ms = AppClient.sqlite;
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

        /// <summary>
        /// 获取图片资源model形式
        /// </summary>
        /// <returns></returns>
        public Data getDataModel()
        {
            Data data = new Data();
            if (this.countData() > 0)
            {
                DataRow row = this.getData();
                data.id = Convert.ToInt32(row["id"]);
                data.pid = Convert.ToInt32(row["pid"]);
                data.url = Convert.ToString(row["url"]);
                data.pic = Convert.ToString(row["pic"]);
                data.dated = Convert.ToString(row["dated"]);
            }
            return data;
        }

        /// <summary>
        /// 获取图片资源组model数组
        /// </summary>
        /// <returns></returns>
        public Data[] getDataModels()
        {
            List<Data> data_arr = new List<Data>();
            if (this.countData() > 0)
            {
                DataRow[] rows = this.getDatas();
                foreach (DataRow row in rows)
                {
                    Data data = new Data();
                    data.id = Convert.ToInt32(row["id"]);
                    data.pid = Convert.ToInt32(row["pid"]);
                    data.url = Convert.ToString(row["url"]);
                    data.pic = Convert.ToString(row["pic"]);
                    data.dated = Convert.ToString(row["dated"]);
                    data_arr.Add(data);
                }
            }
            return data_arr.ToArray();
        }

        /// <summary>
        /// 请求网络
        /// </summary>
        /// <returns></returns>
        public JObject postUrl()
        {
            Encoding encoding = Encoding.GetEncoding("utf-8");
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("p_id", AppClient.appId);
            JObject result = Json.PostObj(AppClient.urlAdv, parameters, null, null, encoding, null);
            return result;
        }

        /// <summary>
        /// 设置本地图片资源
        /// </summary>
        /// <param name="file_path">本地图片资源路径</param>
        public void setLocalFile(string file_path)
        {
            // 添加数据
            string pid = DateTime.Now.ToString("HHmmss");
            string pic = string.Format(@"{0}.png", pid);
            string url = string.Format(@"{0}\{1}", AppClient.pathBot, pic);
            this.addData(pid, url, pic);
            this.delOtherData(new string[] { pid });

            // 复制文件
            File.Copy(file_path, url, true);
        }
    }
}
