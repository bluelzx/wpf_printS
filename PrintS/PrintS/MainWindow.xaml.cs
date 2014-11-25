using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// CommonLib.dll
using CommonLib;

// DataContract
using DataContract;
using DataContract.Model;
using DataContract.Controller;

// .net System.Configuration
using System.Configuration;

using System.IO;
using System.Data;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace PrintS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        AppStatus appSta;   // 程序状态
        DB ms;  // 数据库task表操作

        DispatcherTimer tData;  // 获取数据时钟

        string pathImg; // 下载图片的路径
        string pathAdv; // 轮播图图片的路径
        string pathEwm; // 二维码图片的路径

        List<Task> listTask;    // 当前预打印任务表 
        List<Adv> listAdv;      // 当前轮播图列表
        List<Ewm> listEwm;      // 当前二维码列表

        void init()
        {
            // 初始化
            //this.Topmost = true;
            appSta = AppStatus.play;
            ms = new DB(ConfigurationManager.AppSettings["dbpath"]);
            this.txtGuide.Text = "使用说明： \r\n 1、打开微信，扫一扫二维码 \r\n 2、关注微信后，发送图片 \r\n 3、输入“随心码”，即可打印照片";
            // 程序用路径
            pathImg = string.Format(@"{0}\IMG", AppDomain.CurrentDomain.BaseDirectory);
            pathAdv = string.Format(@"{0}\ADV", AppDomain.CurrentDomain.BaseDirectory);
            pathEwm = string.Format(@"{0}\EWM", AppDomain.CurrentDomain.BaseDirectory);
            // 显示用数据队列
            listTask = new List<Task>();
            listAdv = new List<Adv>();
            listEwm = new List<Ewm>();

            // 显示二维码
            this.showEwm();

            // 显示轮播图
            this.showAdv();

            // 显示打印码
            this.showCode();

            // 显示打印任务队列
            this.showTask();

            // 刷新界面数据时钟
            tData = new DispatcherTimer();
            tData.Interval = new TimeSpan(0, 0, 3);
            tData.Tick += new EventHandler(tData_Tick);
            tData.Start();

        }

        // 刷新显示数据
        void tData_Tick(object sender, EventArgs e)
        {
            // 获取可能有的消息
            Dictionary<string, object> message = ms.prints.getLastMsg();
            switch ((MessageCode)message["code"])
            {
                // 打印机缺纸
                case MessageCode.printOutPaper:

                    // 显示提示缺纸框
                    this.printing.ShowOTPaper();

                    // 程序暂停
                    appSta = AppStatus.pause;
                    break;
                default:
                    break;
            }

            if (appSta == AppStatus.warn)
            {
                // 应用程序故障
                return;
            }
            if (appSta == AppStatus.pause)
            {
                // 应用程序暂停
                return;
            }

            // 显示二维码
            this.showEwm();

            // 显示轮播图
            this.showAdv();

            // 显示打印码
            this.showCode();

            // 显示打印任务队列
            this.showTask();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.init();
        }

        // 补充纸张按钮
        private void printing_BtnPaperEvent(object sender, EventArgs e)
        {
            int printLimit = 30;

            // 记录操作，告诉PrintC程序
            ms.printc.addMessage(MessageCode.printPaper, printLimit);

            // 启动程序继续运行
            appSta = AppStatus.play;
        }

        /// <summary>
        /// 显示轮播图
        /// </summary>
        void showAdv()
        {
            listAdv.Clear();
            int count = ms.adv.countData();
            if (count > 0)
            {
                DataRow[] rows = ms.adv.getDatas();
                foreach (DataRow row in rows)
                {
                    listAdv.Add(new Adv(row["id"], row["pid"], row["url"], row["pic"], row["dated"]));
                }
            }

            List<BitmapImage> ls_adv_img = new List<BitmapImage>();
            foreach (Adv a in listAdv)
            {
                BitmapImage img;
                try
                {
                    img = new BitmapImage(new Uri(string.Format(@"{0}\{1}", pathAdv, a.pic)));
                }
                catch (Exception ex)
                {
                    img = new BitmapImage();
                }
                ls_adv_img.Add(img);
            }

            this.rollImg.ls_images = ls_adv_img;

            this.rollImg.Begin();

        }

        /// <summary>
        /// 显示二维码
        /// </summary>
        void showEwm()
        {
            // 刷新二维码显示
            listEwm.Clear();

            int count = ms.ewm.countData();
            if (count > 0)
            {
                DataRow row = ms.ewm.getData();
                listEwm.Add(new Ewm(row["id"], row["pid"], row["url"], row["pic"], row["dated"]));
            }

            if (listEwm.Count() > 0)
            {
                BitmapImage img;
                try
                {
                    img = new BitmapImage(
                        new Uri(string.Format(@"{0}\{1}",
                            pathEwm,
                            listEwm[0].pic)
                            ));
                }
                catch (Exception ex)
                {
                    img = new BitmapImage();
                }
                this.imgEwm.Source = img;
            }
        }

        /// <summary>
        /// 显示打印码
        /// </summary>
        void showCode()
        {
            string code = ms.code.getLastCode();
            this.txtCode.Text = code;
        }

        /// <summary>
        /// 显示打印任务队列
        /// </summary>
        void showTask()
        {
            // 缓存上一轮任务的打印中任务
            Task task = new Task();
            if (listTask.Count() > 0)
            {
                task = listTask[0];
            }

            // 刷新打印任务列表
            listTask.Clear();
            int count = ms.task.countTask(1);
            if (count > 0)
            {
                DataRow[] rows = ms.task.getTasks(1);
                foreach (DataRow row in rows)
                {
                    listTask.Add(new Task(row["id"], row["pid"], row["url"], row["pic"], row["state"], row["created"], row["updated"]));
                }
            }

            // 刷新打印任务队列
            List<BitmapImage> ls_task_img = new List<BitmapImage>();
            foreach (Task t in listTask)
            {
                BitmapImage img;
                try
                {
                    img = new BitmapImage(new Uri(string.Format(@"{0}\{1}", pathImg, t.pic)));
                }
                catch (Exception ex)
                {
                    img = new BitmapImage();
                }
                ls_task_img.Add(img);
            }
            this.printTask.ShowTask(ls_task_img);

            // 没有打印任务
            if (listTask.Count() == 0)
            {
                // 隐藏打印中的进度框
                this.printing.Hidden();
            }
            else
            {
                // 打印任务改变
                if (listTask[0].url != task.url)
                {
                    // 显示打印中的进度框
                    this.printing.ShowProBar();
                }
            }
        }

        // 关闭按钮
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
