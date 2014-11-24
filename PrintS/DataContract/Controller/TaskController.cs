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
    public class TaskController
    {
        private Sqlite ms;

        public TaskController(Sqlite _ms)
        {
            this.ms = _ms;
        }

        /// <summary>
        /// 获取任务数
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public int countTask(int state = 1)
        {
            string sql = "select count(*) from `task` where `state`=@state limit 1";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@state", state);
            int count = Convert.ToInt32(ms.getOne(sql, param));
            return count;
        }

        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public DataRow[] getTasks(int state = 1, int limit = 100)
        {
            string sql = "select * from `task` where `state`=@state order by `id` asc limit @limit";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@state", state);
            param.Add("@limit", limit);
            DataRow[] rows = ms.getRows(sql, param);
            return rows;
        }

        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        public void updateTask(int id, int state)
        {
            string updated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); 
            string sql = "update `task` set `state`=@state,`updated`=@updated where `id`=@id";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@state", state);
            param.Add("@updated", updated);
            param.Add("@id", id);
            ms.query(sql, param);
        }

        /// <summary>
        /// pid是否已存在
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public bool existPid(object _pid)
        {
            int pid = Convert.ToInt32(_pid);
            string sql = "select count(*) from `task` where `pid`=@pid limit 1";
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
        /// 添加任务
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="url"></param>
        /// <param name="created"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int addTask(object _pid, object _url, object _pic, object _created, object _state)
        {
            int pid = Convert.ToInt32(_pid);
            string url = Convert.ToString(_url);
            string pic = Convert.ToString(_pic);
            string created = Convert.ToString(_created);
            int state = Convert.ToInt32(_state);
            string sql = "insert into `task`(`pid`,`url`,`pic`,`state`,`created`)values(@pid,@url,@pic,@state,@created)";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@pid", pid);
            param.Add("@url", url);
            param.Add("@pic", pic);
            param.Add("@state", state);
            param.Add("@created", created);
            int id = ms.insert(sql, true, param);
            return id;
        }

        /// <summary>
        /// 重置任务表
        /// </summary>
        public void clearTask()
        {
            string sql = "delete from `task`";
            ms.query(sql);
            sql = "update sqlite_sequence set seq=0 where name='task'";
            ms.query(sql);
        }
    }
}
