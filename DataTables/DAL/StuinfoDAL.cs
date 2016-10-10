using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBUtity;
using Model;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class StuinfoDAL
    {
        /// <summary>
        /// 绑定的方法
        /// </summary>
        /// <returns></returns>
        public static DataTable GetStu(string search,int start,int length)
        {
            string sql = string.Format("select * from(select *, ROW_NUMBER() OVER(order by convert(int,s_id) asc ) as row from stuinfo where s_name Like '%{0}%' ) a where row between {1} and {2}", search, start + 1, start + length);
            return DBUtity.SqlHelper.GetTable(sql);
        }

        /// <summary>
        /// 查询行数
        /// </summary>
        /// <returns></returns>
        public static int GetCount(string search)
        {
            string sql = string.Format("select count(*) from stuinfo where s_name Like '%{0}%'", search);
            return Convert.ToInt32(DBUtity.SqlHelper.GetScalar(sql, CommandType.Text, null));
        }


    }
}
