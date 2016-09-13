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

        /// <summary>
        /// 绑定班级
        /// </summary>
        /// <param name="id">通过id进行查询</param>
        /// <returns></returns>
        public static string GetClass(int id)
        {
            return DAL.StuinfoDAL.GetClass(id);
        }

        /// <summary>
        /// 删除的方法
        /// </summary>
        /// <param name="id">通过id进行删除</param>
        /// <returns></returns>
        public static bool Del(int id)
        {
            return DAL.StuinfoDAL.Del(id);
        }

        /// <summary>
        /// 绑定下拉框的查询
        /// </summary>
        /// <returns></returns>
        public static DataTable GetClassInfo()
        {
            return DAL.StuinfoDAL.GetClassInfo();
        }

        /// <summary>
        /// 添加的方法
        /// </summary>
        /// <param name="ss">添加的类</param>
        /// <returns></returns>
        public static bool Insert(Model.Stuinfo ss)
        {
            return DAL.StuinfoDAL.Insert(ss);
        }
        
        /// <summary>
        /// 找到对应一行的用户数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable FindStuInfo(int id)
        {
            return DAL.StuinfoDAL.FindStuInfo(id);
        }

        /// <summary>
        /// 修改的方法
        /// </summary>
        /// <param name="ss">修改的类</param>
        /// <returns></returns>
        public static bool Update(Model.Stuinfo ss)
        {
            return DAL.StuinfoDAL.Update(ss);
        }

        /// <summary>
        /// 多行删除
        /// </summary>
        /// <param name="val">通过查询的数组进行删除</param>
        /// <returns></returns>
        public static bool DelAll(string val)
        {
            return DAL.StuinfoDAL.DelAll(val);
        }

        /// <summary>
        /// 多表联查对应的数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAll()
        {
            return DAL.StuinfoDAL.GetAll();
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="name">通过姓名进行查询</param>
        /// <returns></returns>
        public static DataTable GetLike(string name)
        {
            return DAL.StuinfoDAL.GetLike(name);
        }
    }
}
