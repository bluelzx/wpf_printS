using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataContract.Controller;

// CommonLib.dll
using CommonLib;
using CommonLib.DataBase;

using System.Data;

namespace DataContract.Controller
{
    public class DB
    {
        private string _dbpath;
        /// <summary>
        /// 数据库路径
        /// </summary>
        public string dbpath
        {
            set { _dbpath = value; }
            get { return _dbpath; }
        }

        private Sqlite _ms;
        /// <summary>
        /// sqlite数据库
        /// </summary>
        public Sqlite ms
        {
            set { _ms = value; }
            get { return _ms; }
        }

        private TaskController _task;
        /// <summary>
        /// task表（打印任务）
        /// </summary>
        public TaskController task
        {
            set { _task = value; }
            get { return _task; }
        }

        private CodeController _code;
        /// <summary>
        /// code表（打印码）
        /// </summary>
        public CodeController code
        {
            set { _code = value; }
            get { return _code; }
        }

        private PrintMsgController _printc;
        /// <summary>
        /// printc表（发送给PrintC程序的消息）
        /// </summary>
        public PrintMsgController printc
        {
            set { _printc = value; }
            get { return _printc; }
        }

        private PrintMsgController _prints;
        /// <summary>
        /// prints表（发送给PrintS程序的消息）
        /// </summary>
        public PrintMsgController prints
        {
            set { _prints = value; }
            get { return _prints; }
        }

        private DataController _adv;
        /// <summary>
        /// adv表（轮播图）
        /// </summary>
        public DataController adv
        {
            set { _adv = value; }
            get { return _adv; }
        }

        private DataController _ewm;
        /// <summary>
        /// ewm表（二维码）
        /// </summary>
        public DataController ewm
        {
            set { _ewm = value; }
            get { return _ewm; }
        }

        private DataController _bot;
        /// <summary>
        /// bot表（打印底图）
        /// </summary>
        public DataController bot
        {
            set { _bot = value; }
            get { return _bot; }
        }

        /// <summary>
        /// 数据库操作
        /// </summary>
        /// <param name="path">数据库文件路径</param>
        public DB(string path)
        {
            this.dbpath = path;
            this.ms = new Sqlite(path);

            // sqlite自动创建的数据库文件，需要初始化表结构
            DataTable dt = ms.GetSchema();
            if (dt.Rows.Count == 0)
            {
                this.init();
            }

            this.task = new TaskController(this.ms);
            this.code = new CodeController(this.ms);
            this.printc = new PrintMsgController(this.ms, "printc");
            this.prints = new PrintMsgController(this.ms, "prints");
            this.adv = new DataController(this.ms, "adv");
            this.ewm = new DataController(this.ms, "ewm");
            this.bot = new DataController(this.ms, "bot");
        }

        /// <summary>
        /// 初始化数据库的表结构
        /// 修改表结构的时候需要同步修改这里的初始化sql
        /// </summary>
        public void init()
        {
            /*
             * code表（打印码）
             * id：主键自增
             * code：打印码
             * dated：获取时间
             */
            string sql = "CREATE TABLE \"code\" (\"id\" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL , \"code\" VARCHAR, \"dated\" DATETIME)";
            this.ms.query(sql);
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
            this.ms.query(sql);
            /*
             * printc表（发送给PrintC程序的消息）
             * id：主键自增
             * code：消息码
             * msg：附加消息
             * status：消息状态（1：已添加未读取；2：已读取；）
             * dated：添加消息时间
             */
            sql = "CREATE TABLE \"printc\" (\"id\" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL , \"code\" INTEGER, \"msg\" VARCHAR, \"status\" INTEGER, \"dated\" DATETIME)";
            this.ms.query(sql);
            /*
             * prints表（发送给PrintS程序的消息）
             * 同printc表
             */
            sql = "CREATE TABLE \"prints\" (\"id\" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL , \"code\" INTEGER, \"msg\" VARCHAR, \"status\" INTEGER, \"dated\" DATETIME)";
            this.ms.query(sql);
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
            this.ms.query(sql);
            /*
             * ewm表（二维码）
             * 同adv表
             */
            sql = "CREATE TABLE \"ewm\" (\"id\" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL , \"pid\" INTEGER, \"url\" VARCHAR, \"pic\" VARCHAR, \"state\" INTEGER, \"dated\" DATETIME)";
            this.ms.query(sql);
            /*
             * bot表（打印底图）
             * 同adv表
             */
            sql = "CREATE TABLE \"bot\" (\"id\" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL , \"pid\" INTEGER, \"url\" VARCHAR, \"pic\" VARCHAR, \"state\" INTEGER, \"dated\" DATETIME)";
            this.ms.query(sql);
        }
    }
}
