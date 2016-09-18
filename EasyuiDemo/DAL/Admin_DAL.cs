using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DBUtity;
using Model;

namespace DAL
{
    public class Admin_DAL
    {
        public static string Login(string uname, string upwd)
        {
            string sql = string.Format("select Admin_Type from Admin where Admin_Uname='{0}' and Admin_Upwd='{1}'", uname, upwd);
            return SqlHelper.GetScalar(sql, CommandType.Text, null).ToString();
        }

        public static DataTable OutTree(int id)
        {
            string sql = string.Format("select * from Menu where Menu_Id in(select Power_Menu_Id from Power where Power_Admin_Type_Id={0})",id);
            return SqlHelper.GetTable(sql);
        }

        public static DataTable InTree(int id)
        {
            string sql = string.Format("select * from Menu where Menu_Parent_Id={0}",id);
            return SqlHelper.GetTable(sql);
        }

        public static string Url(int id)
        {
            string sql = string.Format("select Menu_Url from Menu where Menu_Id={0}",id);
            return SqlHelper.GetScalar(sql, CommandType.Text, null).ToString();
        }
    }
}
