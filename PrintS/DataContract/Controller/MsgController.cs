using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// CommonLib.dll
using CommonLib;
using CommonLib.DataBase;

// model
using DataContract.Model;

using System.Data;

namespace DataContract.Controller
{
    public class MsgController
    {
        private Sqlite ms;
        private string table;

        public MsgController(string _table)
        {
            this.ms = AppClient.sqlite;
            this.table = _table;
        }

        /// <summary>
        /// 添加一条消息，状态位，默认未读1
        /// </summary>
        /// <param name="_code">消息码</param>
        /// <param name="_msg">消息内容</param>
        /// <returns></returns>
        public int addMessage(MessageCode _code, object _msg)
        {
            string msg = Convert.ToString(_msg);
            int code = Convert.ToInt32(_code);
            string dated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int status = 1;
            string sql = "insert into `" + this.table + "`(`code`,`msg`,`status`,`dated`)values(@code,@msg,@status,@dated)";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@code", code);
            param.Add("@msg", msg);
            param.Add("@status", status);
            param.Add("@dated", dated);
            int id = ms.insert(sql, true, param);
            return id;
        }

        /// <summary>
        /// 获取最近一条未读信息，并修改成已读状态status：2
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> getLastMsg()
        {
            Dictionary<string, object> re = new Dictionary<string, object>();
            re["code"] = MessageCode.none;
            re["msg"] = "";

            string sql = "select count(*) from `" + this.table + "` where `status`=1 limit 1";
            int count = Convert.ToInt32(ms.getOne(sql));
            if (count > 0)
            {
                sql = "select * from `" + this.table + "` where `status`=1 order by `id` desc limit 1";
                DataRow row = ms.getRow(sql);
                int id = Convert.ToInt32(row["id"]);
                sql = "update `" + this.table + "` set `status`=2 where `id`=@id"; // 2 已读
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@id", id);
                ms.query(sql, param);

                re["code"] = (MessageCode)Convert.ToInt32(row["code"]);
                re["msg"] = Convert.ToString(row["msg"]);
            }

            return re;
        }

    }
}
