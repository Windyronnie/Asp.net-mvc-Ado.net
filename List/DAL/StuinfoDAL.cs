using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace DAL
{
    public class StuinfoDAL
    {
        /// <summary>
        /// 查询的方法
        /// </summary>
        /// <returns></returns>
        public static DataTable GetStu()
        {
            string sql = string.Format("select * from stuinfo order by s_no");
            return DBUtity.SqlHelper.GetTable(sql);
        }

        public static string GetClass(int id)
        {
            string sql = string.Format("select c_name from classInfo where c_id='{0}'",id);
            return DBUtity.SqlHelper.GetScalar(sql, CommandType.Text, null).ToString();
        }

        public static DataTable FindStuInfo(int id)
        {
            string sql = string.Format("select * from stuinfo where s_id='{0}'",id);
            return DBUtity.SqlHelper.GetTable(sql);
        }

        public static DataTable GetClassInfo()
        {
            string sql = string.Format("select * from classInfo");
            return DBUtity.SqlHelper.GetTable(sql);
        }

        public static bool Del(int id)
        {
            string sql = string.Format("delete from stuinfo where s_id='{0}'",id);
            return DBUtity.SqlHelper.ExeNonQuery(sql,CommandType.Text,null);
        }

        public static bool Insert(Model.Stuinfo ss)
        {
            string sql = string.Format("insert into stuinfo values('{0}','{1}','{2}','{3}')",ss.S_No,ss.S_C_Id,ss.S_Name,ss.S_state);
            return DBUtity.SqlHelper.ExeNonQuery(sql, CommandType.Text, null);
        }

        public static bool Update(Model.Stuinfo ss)
        {
            string sql = string.Format("update stuinfo set s_no='{0}',s_c_id='{1}',s_name='{2}',s_state='{3}' where s_id='{4}'",ss.S_No,ss.S_C_Id,ss.S_Name,ss.S_state,ss.S_Id);
            return DBUtity.SqlHelper.ExeNonQuery(sql, CommandType.Text, null);
        }

    }
}
