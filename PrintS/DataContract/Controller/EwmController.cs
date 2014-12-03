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
    public class EwmController : DataController
    {
        public EwmController(string _table="ewm")
            : base(_table)
        { }

        /// <summary>
        /// 更新二维码
        /// </summary>
        /// <param name="result">网络返回的结果</param>
        public void Update(JObject result)
        {
            // 二维码
            List<string> ewm_pid_arr = new List<string>();  // 在用pid集合
            string ewm_pid = result["ewm"][0]["id"].ToString();
            string ewm_url = result["ewm"][0]["url"].ToString();
            string ewm_pic = string.Format(@"{0}.{1}", ewm_pid, ewm_url.Substring(ewm_url.LastIndexOf(".") + 1));
            // 接收新的二维码数据
            if (!base.existPid(ewm_pid))
            {
                // 添加到数据库
                base.addData(ewm_pid, ewm_url, ewm_pic);
                // 下载图片
                Http.downloadThread(ewm_url, string.Format(@"{0}\{1}", AppClient.pathEwm, ewm_pic));
            }
            ewm_pid_arr.Add(ewm_pid);
            // 删除不用的数据
            base.delOtherData(ewm_pid_arr.ToArray());
        }
    }
}
