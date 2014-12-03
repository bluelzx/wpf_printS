using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataContract.Model;

// CommonLib.dll
using CommonLib;
using CommonLib.DataBase;

using System.Data;

namespace DataContract.Controller
{
    public class DB
    {
        /// <summary>
        /// 数据库操作
        /// </summary>
        public DB()
        {

        }

        static private TaskController _task;
        /// <summary>
        /// task表（打印任务）
        /// </summary>
        static public TaskController task
        {
            get
            {
                if (_task == null)
                {
                    _task = new TaskController();
                }
                return _task;
            }
        }

        static private CodeController _code;
        /// <summary>
        /// code表（打印码）
        /// </summary>
        static public CodeController code
        {
            get
            {
                if (_code == null)
                {
                    _code = new CodeController();
                }
                return _code;
            }
        }

        static private PrintCController _printc;
        /// <summary>
        /// printc表（发送给PrintC程序的消息）
        /// </summary>
        static public PrintCController printc
        {
            get
            {
                if (_printc == null)
                {
                    _printc = new PrintCController();
                }
                return _printc;
            }
        }

        static private PrintSController _prints;
        /// <summary>
        /// prints表（发送给PrintS程序的消息）
        /// </summary>
        static public PrintSController prints
        {
            get
            {
                if (_prints == null)
                {
                    _prints = new PrintSController();
                }
                return _prints;
            }
        }

        static private AdvController _adv;
        /// <summary>
        /// adv表（轮播图）
        /// </summary>
        static public AdvController adv
        {
            get
            {
                if (_adv == null)
                {
                    _adv = new AdvController();
                }
                return _adv;
            }
        }

        static private EwmController _ewm;
        /// <summary>
        /// ewm表（二维码）
        /// </summary>
        static public EwmController ewm
        {
            get 
            {
                if (_ewm == null)
                {
                    _ewm = new EwmController();
                }
                return _ewm; 
            }
        }

        static private BotController _bot;
        /// <summary>
        /// bot表（打印底图）
        /// </summary>
        static public BotController bot
        {
            get 
            {
                if (_bot == null)
                {
                    _bot = new BotController();
                }
                return _bot; 
            }
        }

        /// <summary>
        /// 初始化数据库的表结构
        /// 修改表结构的时候需要同步修改这里的初始化sql
        /// </summary>
        static public void init(Sqlite ms)
        {
            DataTable dt = ms.GetSchema();
            if (dt.Rows.Count > 0)
            {
                return;
            }

            /*
             * code表（打印码）
             * id：主键自增
             * code：打印码
             * dated：获取时间
             */
            string sql = "CREATE TABLE \"code\" (\"id\" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL , \"code\" VARCHAR, \"dated\" DATETIME)";
            ms.query(sql);
            /*
             * task表（打印任务）
             * id：主键自增
             * pid：服务端的打印任务id
             * url：用户照片的url地址
             * pic：用户照片在本地地址
             * state：任务状态（1：已获取未打印；2：已打印；）
             * created：任务获取时间
             * updated：任务更新时间
             */
            sql = "CREATE TABLE \"task\" (\"id\" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL , \"pid\" INTEGER, \"url\" VARCHAR, \"pic\" VARCHAR, \"state\" INTEGER, \"created\" DATETIME, \"updated\" DATETIME)";
            ms.query(sql);
            /*
             * printc表（发送给PrintC程序的消息）
             * id：主键自增
             * code：消息码
             * msg：附加消息
             * status：消息状态（1：已添加未读取；2：已读取；）
             * dated：添加消息时间
             */
            sql = "CREATE TABLE \"printc\" (\"id\" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL , \"code\" INTEGER, \"msg\" VARCHAR, \"status\" INTEGER, \"dated\" DATETIME)";
            ms.query(sql);
            /*
             * prints表（发送给PrintS程序的消息）
             * 同printc表
             */
            sql = "CREATE TABLE \"prints\" (\"id\" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL , \"code\" INTEGER, \"msg\" VARCHAR, \"status\" INTEGER, \"dated\" DATETIME)";
            ms.query(sql);
            /*
             * adv表（轮播图）
             * id：主键自增
             * pid：服务端id
             * url：图片url地址
             * pic：图片本地地址
             * state：状态（1：在用；2：停用；）
             * dated：获取时间
             */
            sql = "CREATE TABLE \"adv\" (\"id\" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL , \"pid\" INTEGER, \"url\" VARCHAR, \"pic\" VARCHAR, \"state\" INTEGER, \"dated\" DATETIME)";
            ms.query(sql);
            /*
             * ewm表（二维码）
             * 同adv表
             */
            sql = "CREATE TABLE \"ewm\" (\"id\" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL , \"pid\" INTEGER, \"url\" VARCHAR, \"pic\" VARCHAR, \"state\" INTEGER, \"dated\" DATETIME)";
            ms.query(sql);
            /*
             * bot表（打印底图）
             * 同adv表
             */
            sql = "CREATE TABLE \"bot\" (\"id\" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL , \"pid\" INTEGER, \"url\" VARCHAR, \"pic\" VARCHAR, \"state\" INTEGER, \"dated\" DATETIME)";
            ms.query(sql);
        }
    }
}
