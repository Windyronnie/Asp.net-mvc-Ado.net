using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    public class StuinfoBLL
    {
        /// <summary>
        /// 绑定的方法
        /// </summary>
        /// <returns></returns>
        public static DataTable GetStu(string search,int start, int length)
        {
            return DAL.StuinfoDAL.GetStu(search, start, length);
        }

        /// <summary>
        /// 查询行数
        /// </summary>
        /// <returns></returns>
        public static int GetCount(string search)
        {
            return DAL.StuinfoDAL.GetCount(search);
        }

    }
}
