using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DBUtity;
using Model;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ClassDAL
    {
        static string _strConn = "server=.;database=dianming;uid=sa;pwd=123456;";
        public static string GetClass(int id) 
        {
            string sql = string.Format("select c_name from classInfo where c_id={0}", id);
            string _sex = "";
            using (SqlConnection con=new SqlConnection(_strConn))
            {
                con.Open();
                _sex = con.Query<ClassInfo>(sql).ToList().First().C_Name;
            }
            return _sex;
        }

        public static List<ClassInfo> GetClassInfo() 
        {
            string sql = string.Format("select * from classInfo");
            List<ClassInfo> Class;
            using (SqlConnection con = new SqlConnection(_strConn)) 
            {
                con.Open();
                Class = con.Query<ClassInfo>(sql).ToList();
                con.Close();
            }
            return Class;
        }

    }
}
