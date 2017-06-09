using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DBUtity;
using Model;

namespace DAL
{
    public class StuDAL
    {
        static string _strConn = "server=.;database=dianming;uid=sa;pwd=123456;";
        public static List<Stuinfo> GetStu()
        {
            string sql = string.Format("select * from stuinfo"); 
            List<Stuinfo> stu;
            using (SqlConnection con = new SqlConnection(_strConn))
            {
                con.Open();
                stu = con.Query<Stuinfo>(sql).ToList();
                con.Close();
            }
            return stu;
        }

        public static int InsertStu(Stuinfo stu) 
        {
            string sql = string.Format("insert into stuinfo values(@s_no,@s_c_id,@s_name,@s_state)");
            int flag = 0;
            using (SqlConnection con=new SqlConnection(_strConn))
            {
                con.Open();
                flag = con.Execute(sql, stu);
            }
            return flag;
        }

        public static int DelStu(string id) 
        {
            string sql = string.Format("delete from stuinfo where s_id=@s_id");
            int flag = 0;
            using (IDbConnection con = new SqlConnection(_strConn)) 
            {
                con.Open();
                flag = con.Execute(sql, new { s_id = id });
            }
            return flag;
        }

        public static Stuinfo FindStu(int id) 
        {
            string sql = string.Format("select * from stuinfo where s_id=@id");
            Stuinfo stu = new Stuinfo();
            using (IDbConnection con=new SqlConnection(_strConn))
            {
                con.Open();
                stu = con.Query<Stuinfo>(sql, new { id = id }).ToList().First();
            }
            return stu;
        }

        public static int UpdateStu(Stuinfo stu) 
        {
            string sql = string.Format("update stuinfo set s_no=@s_no,s_c_id=@s_c_id,s_name=@s_name,s_state=@s_state where s_id=@s_id");
            int flag = 0;
            using (IDbConnection con=new SqlConnection(_strConn))
            {
                con.Open();
                flag = con.Execute(sql, stu);
            }
            return flag;
        }
    }
}
