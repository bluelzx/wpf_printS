using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// CommonLib.dll
using CommonLib;
using CommonLib.Printer;
using CommonLib.Http;
using CommonLib.Tools;

// DataContract
using DataContract;
using DataContract.Model;
using DataContract.Controller;

// Newtonsoft.Json.dll
using Newtonsoft.Json.Linq;

// .net System.Configuration
using System.Configuration;

using System.IO;

// .net System.Management
using System.Management;

/*
 * task表state说明
 * 0：无效的打印任务
 * 1：已入打印任务队列，尚未打印
 * 2：已经打印成功
 */

namespace PrintC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private AppStatus _appSta;
        /// <summary>
        /// 程序状态
        /// </summary>
        public AppStatus appSta
        {
            set
            {
                _appSta = value;
                if (_appSta == AppStatus.play)
                {
                    // 启动
                    tCode.Start();      // 请求打印码，同时返回是否请求打印任务，是否开始数据更新
                    tTask.Start();      // 请求打印任务
                    tPrint.Start();     // 执行打印
                    tData.Start();    // 请求轮播图，二维码
                }
                else
                {
                    // 停止
                    tCode.Stop();      // 请求打印码，同时返回是否请求打印任务，是否开始数据更新
                    tTask.Stop();      // 请求打印任务
                    tPrint.Stop();     // 执行打印
                    tData.Stop();    // 请求轮播图，二维码
                }
                this.lbApp.Text = "程序状态：" + Convert.ToString(_appSta);
            }
            get { return _appSta; }
        }

        bool _isData;
        /// <summary>
        /// 是否数据更新
        /// </summary>
        bool isData
        {
            set
            {
                _isData = value;
                if (value)
                {
                    this.lbData.Text = "数据更新：start";
                }
                else
                {
                    this.lbData.Text = "数据更新：stop";
                }
            }
            get { return this._isData; }
        }

        bool _isTask;
        /// <summary>
        /// 是否请求任务
        /// </summary>
        bool isTask
        {
            set
            {
                _isTask = value;
                if (value)
                {
                    this.lbTask.Text = "请求任务状态：start";
                }
                else
                {
                    this.lbTask.Text = "请求任务状态：stop";
                }
            }
            get { return _isTask; }
        }

        private enum_printerS_status _printerSta;
        /// <summary>
        /// 记录上一次打印机的状态
        /// </summary>
        public enum_printerS_status printerSta
        {
            set
            {
                _printerSta = value;
                this.lbPrint.Text = "打印机状态：" + Convert.ToString(_printerSta);
            }
            get { return _printerSta; }
        }

        private int _printLimit;
        /// <summary>
        /// 单轮打印张数限制，剩余打印纸
        /// </summary>
        public int printLimit
        {
            set
            {
                _printLimit = value;
                this.lbPaper.Text = "剩余打印纸：" + Convert.ToString(_printLimit);
            }
            get { return _printLimit; }
        }

        string appid;       // 设备id
        PrinterS printer;   // 打印机

        string urlTask; // 请求打印任务组的url
        string urlPost; // 返回打印结果的url
        string urlCode; // 请求打印码的url
        string urlAdv;  // 请求轮播图图片素材的url

        string pathPrint;   // 打印排版素材的路径
        string pathImg;     // 下载用户照片的路径
        string pathAdv;     // 轮播图下载的路径
        string pathEwm;     // 二维码下载的路径
        string pathBot;     // 打印底图下载的路径

        Encoding encoding;  // 请求编码格式

        DB ms;  // 数据库task表操作

        Timer tApp;     // 程序心跳时钟
        Timer tTask;    // 请求打印任务组的时钟
        Timer tPrint;   // 执行打印任务的时钟
        Timer tCode;    // 请求打印码的时钟
        Timer tPrintOT; // 打印机工作超时判断，是否故障
        Timer tData;     // 请求数据更新时钟（轮播图，二维码，打印底图）

        List<Task> listTask;    // 当前预打印任务表
        List<Adv> listAdv;      // 当前轮播图列表
        List<Ewm> listEwm;      // 当前二维码列表

        void Init()
        {
            // 初始化
            appid = ConfigurationManager.AppSettings["appid"];
            ms = new DB(ConfigurationManager.AppSettings["dbpath"]);
            isTask = true;
            isData = false;
            // 用于显示列表
            listTask = new List<Task>();
            listAdv = new List<Adv>();
            listEwm = new List<Ewm>();
            // 各程序数据路径
            pathPrint = string.Format(@"{0}\Print", System.Windows.Forms.Application.StartupPath);
            pathImg = string.Format(@"{0}\IMG", System.Windows.Forms.Application.StartupPath);
            pathAdv = string.Format(@"{0}\ADV", System.Windows.Forms.Application.StartupPath);
            pathEwm = string.Format(@"{0}\EWM", System.Windows.Forms.Application.StartupPath);
            pathBot = string.Format(@"{0}\BOT", System.Windows.Forms.Application.StartupPath);
            // 打印机
            printer = new PrinterS(ConfigurationManager.AppSettings["printer"]);
            printerSta = enum_printerS_status.ready;
            printLimit = Convert.ToInt32(ConfigurationManager.AppSettings["pLimit"]);
            // 各请求url
            encoding = Encoding.GetEncoding("utf-8");
            urlTask = ConfigurationManager.AppSettings["pTask"];
            urlPost = ConfigurationManager.AppSettings["pPost"];
            urlCode = ConfigurationManager.AppSettings["pCode"];
            urlAdv = ConfigurationManager.AppSettings["pAdv"];

            // 显示最近获取的打印码
            this.lsCode.Items.Add(ms.code.getLastCode());

            // 获取打印任务队列
            int count = ms.task.countTask(1);
            if (count > 0)
            {
                listTask.Clear();
                DataRow[] rows = ms.task.getTasks(1);
                foreach (DataRow row in rows)
                {
                    listTask.Add(new Task(row["id"], row["pid"], row["url"], row["pic"], row["state"], row["created"], row["updated"]));
                }
            }
            // 显示打印任务
            this.showList(this.lsTask,
                (from o in listTask select o.pid.ToString()).ToArray());

            // 显示现在显示的轮播图队列
            count = ms.adv.countData();
            if (count > 0)
            {
                this.showList(this.lsAdv,
                    ms.adv.getDatas().Select(x => x["pid"].ToString()).ToArray());
            }

            // 显示现在显示的二维码
            count = ms.ewm.countData();
            if (count > 0)
            {
                this.showList(this.lsEwm,
                    ms.ewm.getData()["pid"].ToString());
            }

            // 显示现在在用的打印底图
            count = ms.bot.countData();
            if (count > 0)
            {
                this.showList(this.lsBot,
                    ms.bot.getData()["pid"].ToString());
            }

            // 程序心跳时钟
            tApp = new Timer();
            tApp.Interval = 3 * 1000;
            tApp.Tick += new EventHandler(tApp_Tick);
            tApp.Start();

            // 请求打印任务
            tTask = new Timer();
            tTask.Interval = 10 * 1000;
            tTask.Tick += new EventHandler(tTask_Tick);

            // 执行任务
            tPrint = new Timer();
            tPrint.Interval = 5 * 1000;
            tPrint.Tick += new EventHandler(tPrint_Tick);

            // 请求打印码
            tCode = new Timer();
            tCode.Interval = 5 * 1000;
            tCode.Tick += new EventHandler(tCode_Tick);

            // 打印超时
            tPrintOT = new Timer();
            tPrintOT.Interval = 60 * 1000;
            tPrintOT.Tick += new EventHandler(tPrintOT_Tick);

            // 轮播图，二维码
            tData = new Timer();
            tData.Interval = 10 * 1000;
            tData.Tick += new EventHandler(tData_Tick);

            // 程序启动
            appSta = AppStatus.play;
        }

        // 请求轮播图图片素材
        void tData_Tick(object sender, EventArgs e)
        {
            if (!isData)
            {
                return;
            }

            try
            {
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("p_id", appid);
                JObject result = Json.PostObj(urlAdv, parameters, null, null, encoding, null);

                // 删除现在的数据

                // 轮播图
                List<string> adv_pid_arr = new List<string>();  // 在用pid集合
                for (int i = 0; i < result["adv"].Count(); i++)
                {
                    string adv_pid = result["adv"][i]["id"].ToString();
                    string adv_url = result["adv"][i]["url"].ToString();
                    string adv_pic = string.Format(@"{0}.{1}", adv_pid, adv_url.Substring(adv_url.LastIndexOf(".") + 1));
                    // 接收新的轮播图数据
                    if (!ms.adv.existPid(adv_pid))
                    {
                        // 添加到数据库
                        ms.adv.addData(adv_pid, adv_url, adv_pic);
                        // 下载图片
                        Http.downloadThread(adv_url, string.Format(@"{0}\{1}", pathAdv, adv_pic));
                    }
                    adv_pid_arr.Add(adv_pid);
                }
                // 删除不用的数据
                ms.adv.delOtherData(adv_pid_arr.ToArray());

                // 二维码
                List<string> ewm_pid_arr = new List<string>();  // 在用pid集合
                string ewm_pid = result["ewm"][0]["id"].ToString();
                string ewm_url = result["ewm"][0]["url"].ToString();
                string ewm_pic = string.Format(@"{0}.{1}", ewm_pid, ewm_url.Substring(ewm_url.LastIndexOf(".") + 1));
                // 接收新的二维码数据
                if (!ms.ewm.existPid(ewm_pid))
                {
                    // 添加到数据库
                    ms.ewm.addData(ewm_pid, ewm_url, ewm_pic);
                    // 下载图片
                    Http.downloadThread(ewm_url, string.Format(@"{0}\{1}", pathEwm, ewm_pic));
                }
                ewm_pid_arr.Add(ewm_pid);
                // 删除不用的数据
                ms.ewm.delOtherData(ewm_pid_arr.ToArray());

                // 打印底图
                List<string> bot_pid_arr = new List<string>();  // 在用pid集合
                string bot_pid = result["bot"][0]["id"].ToString();
                string bot_url = result["bot"][0]["url"].ToString();
                string bot_pic = string.Format(@"{0}.{1}", bot_pid, bot_url.Substring(bot_url.LastIndexOf(".") + 1));
                // 接收新的打印底图数据
                if (!ms.bot.existPid(bot_pid))
                {
                    // 添加到数据库
                    ms.bot.addData(bot_pid, bot_url, bot_pic);
                    // 下载图片
                    Http.downloadThread(bot_url, string.Format(@"{0}\{1}", pathBot, bot_pic));
                }
                bot_pid_arr.Add(bot_pid);
                // 删除不用的数据
                ms.bot.delOtherData(bot_pid_arr.ToArray());

            }
            catch (Exception ex)
            {
            }

            // 显示现在显示的轮播图队列
            int count = ms.adv.countData();
            if (count > 0)
            {
                this.showList(this.lsAdv,
                    ms.adv.getDatas().Select(x => x["pid"].ToString()).ToArray());
            }

            // 显示现在显示的二维码
            count = ms.ewm.countData();
            if (count > 0)
            {
                this.showList(this.lsEwm,
                    ms.ewm.getData()["pid"].ToString());
            }

            // 显示现在在用的打印底图
            count = ms.bot.countData();
            if (count > 0)
            {
                this.showList(this.lsBot,
                    ms.bot.getData()["pid"].ToString());
            }
        }

        // 心跳时钟
        void tApp_Tick(object sender, EventArgs e)
        {
            // 获取可能有的消息
            Dictionary<string, object> message = ms.printc.getLastMsg();
            switch ((MessageCode)message["code"])
            {
                // 打印机补充纸
                case MessageCode.printPaper:
                    if (appSta == AppStatus.pause)
                    {
                        appSta = AppStatus.play;
                        printLimit = Convert.ToInt32(message["msg"]);
                    }
                    break;
                default:
                    break;
            }
        }

        // 判断打印机工作超时，判断故障
        void tPrintOT_Tick(object sender, EventArgs e)
        {
            // 打印机打印任务超时，故障..
            appSta = AppStatus.pause;
            tPrintOT.Stop();

            // 添加打印机故障的信息
            ms.prints.addMessage(MessageCode.printOutPaper, "打印机缺纸");
        }

        // 请求打印码的时钟
        void tCode_Tick(object sender, EventArgs e)
        {
            try
            {
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("p_id", appid);
                JObject result = Json.PostObj(urlCode, parameters, null, null, encoding, null);
                string code = result["code"].ToString();    // 打印码

                // 是否终止请求任务
                isTask = !Convert.ToBoolean(result["cut"]);

                // 是否开始数据更新
                isData = Convert.ToBoolean(result["reload"]);

                // 和上一次打印码不同即可
                if (!ms.code.similarCode(code))
                {
                    ms.code.addCode(code);

                    this.lsCode.Items.Add(code);
                }
            }
            catch (Exception ex)
            {

            }
        }

        // webbrowser加载完成即打印
        private void wbPrint_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                this.wbPrint.Print();

                // 启用超时时钟，开始计时打印时间
                tPrintOT.Start();
            }
            catch (Exception ex)
            {

            }
        }

        // 执行打印任务的时钟
        void tPrint_Tick(object sender, EventArgs e)
        {
            // 当前轮询打印机的状态
            enum_printerS_status _printerSta = printer.getStatus();

            if (_printerSta == enum_printerS_status.warn)
            {
                // 打印机故障
                return;
            }
            if (_printerSta == enum_printerS_status.other)
            {
                // 打印机其他状态
                return;
            }

            // 没有任务，记录当前打印机状态即可
            if (listTask.Count == 0)
            {
                // 没有打印任务
                printerSta = _printerSta;
                return;
            }

            // 浏览器加载要打印的页面
            Task task = listTask[0];

            // 打印机 打印中->打印中
            if (printerSta == enum_printerS_status.print && _printerSta == enum_printerS_status.print)
            {
                // 打印机打印中刚才任务
                printerSta = _printerSta;
                return;
            }

            // 打印机 空闲中->打印中
            if (printerSta == enum_printerS_status.ready && _printerSta == enum_printerS_status.print)
            {
                // 打印机打印中刚才任务
                printerSta = _printerSta;
                return;
            }

            // 打印机 打印中->空闲中
            if (printerSta == enum_printerS_status.print && _printerSta == enum_printerS_status.ready)
            {
                // 完成刚才的打印任务
                printerSta = _printerSta;

                tPrintOT.Stop();

                // 更新本地数据库对应任务的状态
                ms.task.updateTask(task.id, 2);

                // 刷新打印任务列表
                listTask.RemoveAt(0);

                // 告知服务端打印成功
                try
                {
                    IDictionary<string, string> parameters = new Dictionary<string, string>();
                    parameters.Add("id", task.pid.ToString());
                    parameters.Add("p_id", appid);
                    JObject result = Json.PostObj(this.urlPost, parameters, null, null, encoding, null);
                    // result["data"]=true/false
                }
                catch (Exception ex)
                {

                }

                // 此轮打印任务还可以打印几张
                printLimit--;
                if (printLimit < 0)
                {
                    // 打印纸不足，暂停程序
                    appSta = AppStatus.pause;

                    // 添加打印机故障的信息
                    ms.prints.addMessage(MessageCode.printOutPaper, "打印机缺纸");
                }

                return;
            }

            // 打印
            string url = task.url;
            string html = File.ReadAllText(string.Format(@"{0}\Print.html", pathPrint));
            // 用户照片
            html = html.Replace("$photo", url);
            // 打印底图
            string logo = string.Format(@"{0}\logo.png", pathPrint);
            int count = ms.bot.countData();
            if (count > 0)
            {
                // 使用数据库中的底图
                DataRow row = ms.bot.getData();
                logo = string.Format(@"{0}\{1}", pathBot, row["pic"]);
            }
            html = html.Replace("$logo", logo);
            if (this.wbPrint.DocumentText != html)  // 防止重复打印相同的照片
            {
                this.wbPrint.DocumentText = html;
            }

            // 显示打印任务
            this.showList(this.lsTask,
                (from o in listTask select o.pid.ToString()).ToArray());
        }

        // 询问打印任务时钟
        void tTask_Tick(object sender, EventArgs e)
        {
            if (!isTask)
            {
                // 停止请求任务
                return;
            }

            try
            {
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("p_id", appid);
                JArray arr = Json.PostArr(urlTask, parameters, null, null, encoding, null);
                foreach (var r in arr)
                {
                    string pid = r["id"].ToString();    // 服务端打印任务id
                    string url = r["url"].ToString();   // 图片url
                    string pic = string.Format(@"{0}_{1}.{2}",
                        DateTime.Now.ToString("yyyyMMddHHmmss"),
                        pid,
                        url.Substring(url.LastIndexOf(".") + 1)
                        );

                    // 判断该打印任务pid是否已经在task任务表中
                    if (ms.task.existPid(pid))
                    {
                        continue;
                    }

                    // 添加至打印任务表task中
                    int state = 1;
                    string created = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    int id = ms.task.addTask(pid, url, pic, created, state);

                    listTask.Add(new Task(id, pid, url, pic, state, created, created));

                    Http.downloadThread(url, string.Format(@"{0}\{1}", pathImg, pic));

                }
            }
            catch (Exception ex)
            {

            }

            // 显示打印任务
            this.showList(this.lsTask,
                (from o in listTask select o.pid.ToString()).ToArray());
        }

        /// <summary>
        /// 显示列表数据
        /// </summary>
        /// <param name="listbox">listbox控件</param>
        /// <param name="array">string[]</param>
        void showList(ListBox listbox, string[] array)
        {
            listbox.Items.Clear();
            foreach (string item in array)
            {
                listbox.Items.Add(item);
            }
        }
        /// <summary>
        /// 显示列表数据
        /// </summary>
        /// <param name="listbox">listbox控件</param>
        /// <param name="str">string</param>
        void showList(ListBox listbox, string str)
        {
            listbox.Items.Clear();
            listbox.Items.Add(str);
        }

    }
}
