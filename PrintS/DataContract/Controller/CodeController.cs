using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// CommonLib.dll
using CommonLib;
using CommonLib.DataBase;

using System.Data;

using DataContract.Model;

namespace DataContract.Controller
{
    public class CodeController
    {
        private Sqlite ms;

        public CodeController()
        {
            this.ms = AppClient.sqlite;
        }

        /// <summary>
        /// 入库一个打印码
        /// </summary>
        /// <param name="_code"></param>
        /// <returns></returns>
        public int addCode(object _code)
        {
            string code = Convert.ToString(_code);
            string dated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string sql = "insert into `code`(`code`,`dated`)values(@code,@dated)";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@code", code);
            param.Add("@dated", dated);
            int id = ms.insert(sql, true, param);
            return id;
        }

        /// <summary>
        /// 判断打印码是否已入库
        /// </summary>
        /// <param name="_code"></param>
        /// <returns></returns>
        public bool existCode(object _code)
        {
            string code = Convert.ToString(_code);
            string dated = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            string sql = "select count(*) from `code` where `code`=@code and `dated`>@dated limit 1";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@code", code);
            param.Add("@dated", dated);
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
        /// 是否与上一个打印码相同
        /// </summary>
        /// <param name="_code">本次打印码</param>
        /// <returns></returns>
        public bool similarCode(object _code)
        {
            string code = Convert.ToString(_code);

            string sql = "select count(*) from `code` limit 1";
            int count = Convert.ToInt32(ms.getOne(sql));
            if (count > 0)
            {
                sql = "select `code` from `code` order by `id` desc limit 1";
                string s_code = Convert.ToString(ms.getOne(sql));
                return (s_code == code) ? true : false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 返回最新的打印码
        /// </summary>
        /// <returns></returns>
        public string getLastCode()
        {
            string sql = "select count(*) from `code` limit 1";
            int count = Convert.ToInt32(ms.getOne(sql));
            if (count > 0)
            {
                sql = "select `code` from `code` order by `id` desc limit 1";
                string code = ms.getOne(sql).ToString();
                return code;
            }
            else
            {
                return "---";
            }
        }

    }
}
