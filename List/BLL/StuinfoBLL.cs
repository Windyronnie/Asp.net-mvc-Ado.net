using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;
using Model;

namespace BLL
{
    public class StuinfoBLL
    {
        /// <summary>
        /// 查询的方法
        /// </summary>
        /// <returns></returns>
        public static DataTable GetStu()
        {
            return DAL.StuinfoDAL.GetStu();
        }

        public static string GetClass(int id)
        {
            return DAL.StuinfoDAL.GetClass(id);
        }

        public static bool Del(int id)
        {
            return DAL.StuinfoDAL.Del(id);
        }

        public static DataTable GetClassInfo()
        {
            return DAL.StuinfoDAL.GetClassInfo();
        }

        public static bool Insert(Model.Stuinfo ss)
        {
            return DAL.StuinfoDAL.Insert(ss);
        }

        public static DataTable FindStuInfo(int id)
        {
            return DAL.StuinfoDAL.FindStuInfo(id);
        }

        public static bool Update(Model.Stuinfo ss)
        {
            return DAL.StuinfoDAL.Update(ss);
        }

        public static bool DelAll(string val)
        {
            return DAL.StuinfoDAL.DelAll(val);
        }

    }
}
