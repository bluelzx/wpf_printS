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

// Conf.dll
using Conf;

/*
 * task表state说明
 * 0：无效的打印任务
 * 1：已入打印任务队列，尚未打印
 * 2：已经打印成功
 */

namespace PrintC
{
    public partial class FrmPrintC : Form
    {
        public FrmPrintC()
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
                    tReload.Start();    // 请求轮播图，二维码
                }
                else
                {
                    // 停止
                    tCode.Stop();      // 请求打印码，同时返回是否请求打印任务，是否开始数据更新
                    tTask.Stop();      // 请求打印任务
                    tPrint.Stop();     // 执行打印
                    tReload.Stop();    // 请求轮播图，二维码
                }
                this.lbApp.Text = "程序状态：" + Convert.ToString(_appSta);
            }
            get { return _appSta; }
        }

        bool _isData;
        /// <summary>
        /// 是否更新数据
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

        bool _isReload;
        /// <summary>
        /// 是否开始下载数据更新
        /// </summary>
        bool isReload
        {
            set
            {
                _isReload = value;
                if (value)
                {
                    this.lbData.Text = "数据更新：start";
                }
                else
                {
                    this.lbData.Text = "数据更新：stop";
                }
            }
            get { return this._isReload; }
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

        private PrinterSStatus _printerSta;
        /// <summary>
        /// 记录上一次打印机的状态
        /// </summary>
        public PrinterSStatus printerSta
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

        PrinterS printer;   // 打印机

        Encoding encoding = Encoding.GetEncoding("utf-8");

        Timer tApp;     // 程序心跳时钟
        Timer tTask;    // 请求打印任务组的时钟
        Timer tPrint;   // 执行打印任务的时钟
        Timer tCode;    // 请求打印码的时钟
        Timer tPrintOT; // 打印机工作超时判断，是否故障
        Timer tReload;     // 请求数据更新时钟（轮播图，二维码，打印底图）

        List<Task> listTask;    // 当前预打印任务表
        List<Adv> listAdv;      // 当前轮播图列表
        List<Ewm> listEwm;      // 当前二维码列表

        void Init()
        {
            // 初始化
            isTask = true;
            isReload = false;
            isData = AppClient.isData;
            // 用于显示列表
            listTask = new List<Task>();
            listAdv = new List<Adv>();
            listEwm = new List<Ewm>();
            // 打印机
            printer = new PrinterS(AppClient.printer);
            printerSta = PrinterSStatus.Ready;
            printLimit = AppClient.pLimit;

            // 显示最近获取的打印码
            this.lsCode.Items.Add(DB.code.getLastCode());

            // 获取打印任务队列
            int count = DB.task.countTask(1);
            if (count > 0)
            {
                listTask.Clear();
                DataRow[] rows = DB.task.getTasks(1);
                foreach (DataRow row in rows)
                {
                    listTask.Add(new Task(row["id"], row["pid"], row["url"], row["pic"], row["state"], row["created"], row["updated"]));
                }
            }
            // 显示打印任务
            this.showList(this.lsTask,
                (from o in listTask select o.pid.ToString()).ToArray());

            // 显示现在显示的轮播图队列
            count = DB.adv.countData();
            if (count > 0)
            {
                this.showList(this.lsAdv,
                    DB.adv.getDatas().Select(x => x["pid"].ToString()).ToArray());
            }

            // 显示现在显示的二维码
            count = DB.ewm.countData();
            if (count > 0)
            {
                this.showList(this.lsEwm,
                    DB.ewm.getData()["pid"].ToString());
            }

            // 显示现在在用的打印底图
            count = DB.bot.countData();
            if (count > 0)
            {
                this.showList(this.lsBot,
                    DB.bot.getData()["pid"].ToString());
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
            tReload = new Timer();
            tReload.Interval = 10 * 1000;
            tReload.Tick += new EventHandler(tReload_Tick);

            // 程序启动
            appSta = AppStatus.play;
        }

        // 请求轮播图图片素材
        void tReload_Tick(object sender, EventArgs e)
        {
            if (!isData)
            {
                // 无需更新
                return;
            }

            if (!isReload)
            {
                return;
            }

            try
            {
                // 网络请求
                JObject result = DB.adv.postUrl();

                // 更新轮播图
                DB.adv.Update(result);

                // 更新二维码
                DB.ewm.Update(result);

                // 更新打印底图
                DB.bot.Update(result);
            }
            catch (Exception ex)
            {
            }

            // 显示现在显示的轮播图队列
            int count = DB.adv.countData();
            if (count > 0)
            {
                this.showList(this.lsAdv,
                    DB.adv.getDatas().Select(x => x["pid"].ToString()).ToArray());
            }

            // 显示现在显示的二维码
            count = DB.ewm.countData();
            if (count > 0)
            {
                this.showList(this.lsEwm,
                    DB.ewm.getData()["pid"].ToString());
            }

            // 显示现在在用的打印底图
            count = DB.bot.countData();
            if (count > 0)
            {
                this.showList(this.lsBot,
                    DB.bot.getData()["pid"].ToString());
            }
        }

        // 心跳时钟
        void tApp_Tick(object sender, EventArgs e)
        {
            // 获取可能有的消息
            Dictionary<string, object> message = DB.printc.getLastMsg();
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
            DB.prints.addMessage(MessageCode.printOutPaper, "打印机缺纸");
        }

        // 请求打印码的时钟
        void tCode_Tick(object sender, EventArgs e)
        {
            try
            {
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("p_id", AppClient.appId);
                JObject result = Json.PostObj(AppClient.urlCode, parameters, null, null, encoding, null);

                // 打印码
                string code = result["code"].ToString();   

                // 是否终止请求任务
                isTask = !Convert.ToBoolean(result["cut"]);

                // 是否开始数据更新
                isReload = Convert.ToBoolean(result["reload"]) & isData;

                // 和上一次打印码不同即可
                if (!DB.code.similarCode(code))
                {
                    DB.code.addCode(code);

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
            PrinterSStatus _printerSta = printer.getStatus();

            if (_printerSta == PrinterSStatus.Warn)
            {
                // 打印机故障
                return;
            }
            if (_printerSta == PrinterSStatus.Other)
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
            if (printerSta == PrinterSStatus.Print && _printerSta == PrinterSStatus.Print)
            {
                // 打印机打印中刚才任务
                printerSta = _printerSta;
                return;
            }

            // 打印机 空闲中->打印中
            if (printerSta == PrinterSStatus.Ready && _printerSta == PrinterSStatus.Print)
            {
                // 打印机打印中刚才任务
                printerSta = _printerSta;
                return;
            }

            // 打印机 打印中->空闲中
            if (printerSta == PrinterSStatus.Print && _printerSta == PrinterSStatus.Ready)
            {
                // 完成刚才的打印任务
                printerSta = _printerSta;

                tPrintOT.Stop();

                // 更新本地数据库对应任务的状态
                DB.task.updateTask(task.id, 2);

                // 刷新打印任务列表
                listTask.RemoveAt(0);

                // 告知服务端打印成功
                try
                {
                    IDictionary<string, string> parameters = new Dictionary<string, string>();
                    parameters.Add("id", task.pid.ToString());
                    parameters.Add("p_id", AppClient.appId);
                    JObject result = Json.PostObj(AppClient.urlPost, parameters, null, null, encoding, null);
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
                    DB.prints.addMessage(MessageCode.printOutPaper, "打印机缺纸");
                }

                return;
            }

            // 打印
            string url = task.url;
            string html = File.ReadAllText(string.Format(@"{0}\Print.html", AppClient.pathPrint));
            // 用户照片
            html = html.Replace("$photo", url);
            // 打印底图
            string logo = string.Format(@"{0}\logo.png", AppClient.pathPrint);
            int count = DB.bot.countData();
            if (count > 0)
            {
                // 使用数据库中的底图
                DataRow row = DB.bot.getData();
                //logo = row["url"].ToString(); // 加载url地址
                logo = string.Format(@"{0}\{1}", AppClient.pathBot, row["pic"]);    // 暂不显示本地的.jpg打印底图
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
                parameters.Add("p_id", AppClient.appId);
                JArray arr = Json.PostArr(AppClient.urlTask, parameters, null, null, encoding, null);
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
                    if (DB.task.existPid(pid))
                    {
                        continue;
                    }

                    // 添加至打印任务表task中
                    int state = 1;
                    string created = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    int id = DB.task.addTask(pid, url, pic, created, state);

                    listTask.Add(new Task(id, pid, url, pic, state, created, created));

                    Http.downloadThread(url, string.Format(@"{0}\{1}", AppClient.pathImg, pic));

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
