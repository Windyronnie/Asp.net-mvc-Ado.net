using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DBUtity;
using Model;
using Newtonsoft.Json;

namespace DAL
{
    public class StuinfoDAL
    {
        public static DataTable GetStu() 
        {
            string sql = string.Format("select * from stuinfo");
            return SqlHelper.GetTable(sql);
        }
    }
}
