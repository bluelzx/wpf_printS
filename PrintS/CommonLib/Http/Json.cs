using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Security;
using System.IO;
using System.Security.Cryptography.X509Certificates;

// Newtonsoft.Json.dll
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CommonLib.Http
{
    public class Json : Http
    {
        public static JArray PostArr(string url, IDictionary<string, string> parameters, int? timeout, string userAgent, Encoding requestEncoding, CookieCollection cookies)
        {
            JArray ja = new JArray();

            HttpWebResponse response = PostResponse(url, parameters, null, null, requestEncoding, null);
            if (response == null)
            {
                return ja;
            }

            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string result = sr.ReadToEnd();

            ja = (JArray)JsonConvert.DeserializeObject(result);
            return ja;
        }

        /// <summary>
        /// post请求，引用Newtonsoft.Json.dll
        /// 示例：
        /// string postUrl = "http://192.168.0.101/test/PrintServer/post.php";
        /// Encoding encoding = Encoding.GetEncoding("utf-8");
        /// IDictionary<string, string> parameters = new Dictionary<string, string>();
        /// parameters.Add("test", "嗨");
        /// JObject result = Http.Post(postUrl, parameters, null, null, encoding, null);
        /// string test;
        /// try{test=result["test"].ToString();}
        /// catch(Exception ex){test="";}
        /// MessageBox.Show(test);
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>
        /// <returns></returns>
        public static JObject PostObj(string url, IDictionary<string, string> parameters, int? timeout, string userAgent, Encoding requestEncoding, CookieCollection cookies)
        {
            JObject jo = new JObject();

            HttpWebResponse response = PostResponse(url, parameters, null, null, requestEncoding, null);
            if (response == null)
            {
                return jo;
            }

            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string result = sr.ReadToEnd();

            jo = (JObject)JsonConvert.DeserializeObject(result);
            return jo;

            //string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();
        }


    }
}
