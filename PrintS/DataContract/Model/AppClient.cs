using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// CommonLib.dll
using CommonLib.Config;
using CommonLib.DataBase;

using System.Data;

using DataContract.Controller;

namespace DataContract.Model
{
    /// <summary>
    /// 程序运行状态
    /// </summary>
    public enum AppStatus
    {
        /// <summary>
        /// 故障
        /// </summary>
        warn = 0,
        /// <summary>
        /// 运行
        /// </summary>
        play,
        /// <summary>
        /// 暂停
        /// </summary>
        pause
    }

    public enum MessageCode
    {
        /// <summary>
        /// 无效消息
        /// </summary>
        none = 0,
        /// <summary>
        /// 打印机缺纸
        /// </summary>
        printOutPaper,
        /// <summary>
        /// 打印机补充纸
        /// </summary>
        printPaper
    }

    /// <summary>
    /// 设备
    /// </summary>
    public class AppClient
    {
        /// <summary>
        /// 设备id
        /// </summary>
        static public string appId
        {
            set { Config.setConfig("appId", value); }
            get { return Config.getConfig("appId"); }
        }

        /// <summary>
        /// 数据库地址
        /// </summary>
        static public string dbPath
        {
            set { Config.setConfig("dbPath", value); }
            get { return Config.getConfig("dbPath"); }
        }

        static Sqlite _sqlite;
        /// <summary>
        /// 数据库实例
        /// </summary>
        static public Sqlite sqlite
        {
            get
            {
                if (_sqlite == null)
                {
                    _sqlite = new Sqlite(dbPath);
                    // sqlite自动创建的数据库文件，需要初始化表结构
                    DB.init(_sqlite);
                }
                return _sqlite;
            }
        }

        /// <summary>
        /// 打印机名
        /// </summary>
        static public string printer
        {
            set { Config.setConfig("printer", value); }
            get { return Config.getConfig("printer"); }
        }

        /// <summary>
        /// 打印数预警数
        /// </summary>
        static public int pLimit
        {
            set { Config.setConfig("pLimit", value); }
            get { return Convert.ToInt32(Config.getConfig("pLimit")); }
        }

        /// <summary>
        /// 获取打印任务的url
        /// </summary>
        static public string urlTask
        {
            set { Config.setConfig("urlTask", value); }
            get { return Config.getConfig("urlTask"); }
        }

        /// <summary>
        /// 返回打印结果的url
        /// </summary>
        static public string urlPost
        {
            set { Config.setConfig("urlPost", value); }
            get { return Config.getConfig("urlPost"); }
        }

        /// <summary>
        /// 返回打印码的url
        /// </summary>
        static public string urlCode
        {
            set { Config.setConfig("urlCode", value); }
            get { return Config.getConfig("urlCode"); }
        }

        /// <summary>
        /// 返回数据更新的url
        /// </summary>
        static public string urlAdv
        {
            set { Config.setConfig("urlAdv", value); }
            get { return Config.getConfig("urlAdv"); }
        }

        /// <summary>
        /// 是否开启数据更新
        /// </summary>
        static public bool isData
        {
            set
            {
                int val = value ? 1 : 0;
                Config.setConfig("isData", val);
            }
            get
            {
                int value = Convert.ToInt32(Config.getConfig("isData"));
                return (value > 0) ? true : false;
            }
        }

        /// <summary>
        /// 程序运行路径
        /// </summary>
        static public string pathApp
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        /// <summary>
        /// 下载用户照片的文件夹路径
        /// </summary>
        static public string pathImg
        {
            get
            {
                return string.Format(@"{0}IMG", pathApp);
            }
        }

        /// <summary>
        /// 打印布局html的文件夹路径
        /// </summary>
        static public string pathPrint
        {
            get
            {
                return string.Format(@"{0}Print", pathApp);
            }
        }

        /// <summary>
        /// 轮播图的文件夹路径
        /// </summary>
        static public string pathAdv
        {
            get
            {
                return string.Format(@"{0}ADV", pathApp);
            }
        }

        /// <summary>
        /// 二维码的文件夹路径
        /// </summary>
        static public string pathEwm
        {
            get
            {
                return string.Format(@"{0}EWM", pathApp);
            }
        }

        /// <summary>
        /// 打印底图的文件夹路径
        /// </summary>
        static public string pathBot
        {
            get
            {
                return string.Format(@"{0}BOT", pathApp);
            }
        }
    }
}
