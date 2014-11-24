using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// System.Data.SQLite.DLL
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace CommonLib.DataBase
{
    /// <summary>  
    /// 使用方法：  
    /// using System.Data;  
    /// using sqlite;  
    /// Sqlite ms = new Sqlite("test.sqlite");  
    /// string sql = "select * from `posts` where `id`=@id";  
    /// Dictionary<string, object> param = new Dictionary<string, object>();  
    /// param.Add("@id", 1);  
    /// DataRow[] rows = ms.getRows(sql, param);  
    /// </summary>  
    public class Sqlite : IDataBase
    {
        private string _dbpath;

        private SQLiteConnection _conn;
        /// <summary>  
        /// SQLite连接  
        /// </summary>  
        private SQLiteConnection conn
        {
            get
            {
                if (_conn == null)
                {
                    _conn = new SQLiteConnection(
                        string.Format("Data Source={0};Version=3;",
                        this._dbpath
                        ));
                    _conn.Open();
                }
                return _conn;
            }
        }

        /// <summary>  
        /// 构造函数  
        /// </summary>  
        /// <param name="dbpath">sqlite数据库文件路径，相对/绝对路径</param>  
        public Sqlite(string dbpath)
        {
            if (Path.IsPathRooted(dbpath))
            {
                this._dbpath = dbpath;
            }
            else
            {
                this._dbpath = string.Format("{0}/{1}", AppDomain.CurrentDomain.SetupInformation.ApplicationBase, dbpath);
            }
        }

        /// <summary>  
        /// 获取多行  
        /// </summary>  
        /// <param name="sql">执行sql</param>  
        /// <param name="param">sql参数</param>  
        /// <returns>多行结果</returns>  
        public DataRow[] getRows(string sql, Dictionary<string, object> param = null)
        {
            List<SQLiteParameter> sqlite_param = new List<SQLiteParameter>();

            if (param != null)
            {
                foreach (KeyValuePair<string, object> row in param)
                {
                    sqlite_param.Add(new SQLiteParameter(row.Key, row.Value.ToString()));
                }
            }

            DataTable dt = this.ExecuteDataTable(sql, sqlite_param.ToArray());
            return dt.Select();
        }

        /// <summary>  
        /// 获取单行  
        /// </summary>  
        /// <param name="sql">执行sql</param>  
        /// <param name="param">sql参数</param>  
        /// <returns>单行数据</returns>  
        public DataRow getRow(string sql, Dictionary<string, object> param = null)
        {
            DataRow[] rows = this.getRows(sql, param);
            return rows[0];
        }

        /// <summary>  
        /// 获取字段  
        /// </summary>  
        /// <param name="sql">执行sql</param>  
        /// <param name="param">sql参数</param>  
        /// <returns>字段数据</returns>  
        public Object getOne(string sql, Dictionary<string, object> param = null)
        {
            DataRow row = this.getRow(sql, param);
            return row[0];
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int insert(string sql, bool result = false, Dictionary<string, object> param = null)
        {
            int affectedRows = this.query(sql, param);
            if (affectedRows > 0 && result)
            {
                string tsql = "select last_insert_rowid()";
                int lastid = Convert.ToInt32(this.getOne(tsql));
                return lastid;
            }
            else
            {
                return affectedRows;
            }
        }

        /// <summary>  
        /// SQLite增删改  
        /// </summary>  
        /// <param name="sql">要执行的sql语句</param>  
        /// <param name="parameters">所需参数</param>  
        /// <returns>所受影响的行数</returns>  
        public int query(string sql, Dictionary<string, object> param = null)
        {
            List<SQLiteParameter> sqlite_param = new List<SQLiteParameter>();

            if (param != null)
            {
                foreach (KeyValuePair<string, object> row in param)
                {
                    sqlite_param.Add(new SQLiteParameter(row.Key, row.Value.ToString()));
                }
            }

            return this.ExecuteNonQuery(sql, sqlite_param.ToArray());
        }

        /// <summary>  
        /// SQLite增删改  
        /// </summary>  
        /// <param name="sql">要执行的sql语句</param>  
        /// <param name="parameters">所需参数</param>  
        /// <returns>所受影响的行数</returns>  
        private int ExecuteNonQuery(string sql, SQLiteParameter[] parameters)
        {
            int affectedRows = 0;

            System.Data.Common.DbTransaction transaction = conn.BeginTransaction();
            SQLiteCommand command = new SQLiteCommand(conn);
            command.CommandText = sql;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            affectedRows = command.ExecuteNonQuery();
            transaction.Commit();

            return affectedRows;
        }

        /// <summary>  
        /// SQLite查询  
        /// </summary>  
        /// <param name="sql">要执行的sql语句</param>  
        /// <param name="parameters">所需参数</param>  
        /// <returns>结果DataTable</returns>  
        private DataTable ExecuteDataTable(string sql, SQLiteParameter[] parameters)
        {
            DataTable data = new DataTable();

            SQLiteCommand command = new SQLiteCommand(sql, conn);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            adapter.Fill(data);

            return data;
        }

        /// <summary>  
        /// 查询数据库表信息  
        /// </summary>  
        /// <returns>数据库表信息DataTable</returns>  
        public DataTable GetSchema()
        {
            DataTable data = new DataTable();

            data = conn.GetSchema("TABLES");

            return data;
        }
    }
}