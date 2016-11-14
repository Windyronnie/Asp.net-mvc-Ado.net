using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;

namespace AngularWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            List<Stuinfo> stu = GetList<Stuinfo>(BLL.StuinfoBLL.GetStu());
            ViewBag.stu = JsonConvert.SerializeObject(stu);
            return View();
        }

        /// <summary>
        /// DataTable转换成List<T>泛型集合 注:T中的名称要与数据库中保持一致
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns></returns>
        public List<T> GetList<T>(DataTable table)
        {
            List<T> list = new List<T>();
            T t = default(T);
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            foreach (DataRow row in table.Rows)
            {
                t = Activator.CreateInstance<T>();
                propertypes = t.GetType().GetProperties();
                foreach (PropertyInfo pro in propertypes)
                {
                    tempName = pro.Name;
                    if (table.Columns.Contains(tempName))
                    {
                        object value = row[tempName];
                        if (!value.ToString().Equals(""))
                        {
                            pro.SetValue(t, value, null);
                        }
                    }
                }
                list.Add(t);
            }
            return list.Count == 0 ? null : list;
        }

    }
}
