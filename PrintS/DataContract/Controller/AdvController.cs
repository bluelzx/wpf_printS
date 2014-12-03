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
    public class AdvController : DataController
    {
        public AdvController(string _table = "adv")
            : base(_table)
        { }

        /// <summary>
        /// 更新轮播图
        /// </summary>
        /// <param name="result">网络返回的结果</param>
        public void Update(JObject result)
        {
            // 轮播图
            List<string> adv_pid_arr = new List<string>();  // 在用pid集合
            for (int i = 0; i < result["adv"].Count(); i++)
            {
                string adv_pid = result["adv"][i]["id"].ToString();
                string adv_url = result["adv"][i]["url"].ToString();
                string adv_pic = string.Format(@"{0}.{1}", adv_pid, adv_url.Substring(adv_url.LastIndexOf(".") + 1));
                // 接收新的轮播图数据
                if (!base.existPid(adv_pid))
                {
                    // 添加到数据库
                    base.addData(adv_pid, adv_url, adv_pic);
                    // 下载图片
                    Http.downloadThread(adv_url, string.Format(@"{0}\{1}", AppClient.pathAdv, adv_pic));
                }
                adv_pid_arr.Add(adv_pid);
            }
            // 删除不用的数据
            base.delOtherData(adv_pid_arr.ToArray());
        }
    }
}
