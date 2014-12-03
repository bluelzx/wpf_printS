using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// CommonLib.dll
using CommonLib.DataBase;
using CommonLib.Http;

using DataContract.Model;

// Newtonsoft.Json.dll
using Newtonsoft.Json.Linq;

namespace DataContract.Controller
{
    public class BotController : DataController
    {
        public BotController(string _table="bot")
            : base(_table)
        { }

        /// <summary>
        /// 更新打印底图
        /// </summary>
        /// <param name="result">网络返回的结果</param>
        public void Update(JObject result)
        {
            // 打印底图
            List<string> bot_pid_arr = new List<string>();  // 在用pid集合
            string bot_pid = result["bot"][0]["id"].ToString();
            string bot_url = result["bot"][0]["url"].ToString();
            //string bot_pic = string.Format(@"{0}.{1}", bot_pid, bot_url.Substring(bot_url.LastIndexOf(".") + 1));
            string bot_pic = string.Format(@"{0}.png", bot_pid);
            // 接收新的打印底图数据
            if (!base.existPid(bot_pid))
            {
                // 添加到数据库
                base.addData(bot_pid, bot_url, bot_pic);
                // 下载图片
                Http.downloadThread(bot_url, string.Format(@"{0}\{1}", AppClient.pathBot, bot_pic));
            }
            bot_pid_arr.Add(bot_pid);
            // 删除不用的数据
            base.delOtherData(bot_pid_arr.ToArray());
        }
    }
}
