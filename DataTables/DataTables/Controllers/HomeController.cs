using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Model;
using BLL;
using System.Data.SqlClient;
using System.Reflection;

namespace DataTables.Controllers
{
    public class HomeController : Controller
    {

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        //
        // GET: /Home/周

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string ss) 
        {
            Dictionary<String, Object> obj = new Dictionary<String, Object>();
            try
            {
                int start = Convert.ToInt32(Request["start"]), length = Convert.ToInt32(Request["length"]), draw = Convert.ToInt32(Request["draw"]);
                String search = Request["search[value]"];
                List<Stuinfo> stu = GetList<Stuinfo>(BLL.StuinfoBLL.GetStu(search, start, length));
                int count = BLL.StuinfoBLL.GetCount(search);
                obj.Add("draw", draw);
                obj.Add("recordsTotal", count);
                obj.Add("recordsFiltered", count);
                obj.Add("data", stu);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return Json(obj);
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
