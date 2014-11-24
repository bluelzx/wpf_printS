using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace CommonLib.DataBase
{
    public interface IDataBase
    {
        DataRow[] getRows(string sql, Dictionary<string, object> param = null);

        DataRow getRow(string sql, Dictionary<string, object> param = null);

        Object getOne(string sql, Dictionary<string, object> param = null);

        int insert(string sql, bool result = false, Dictionary<string, object> param = null);

        int query(string sql, Dictionary<string, object> param = null);
    }
}
