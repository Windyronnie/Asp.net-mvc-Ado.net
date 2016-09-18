using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;

namespace EasyuiDemo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OutTree()
        {
            int Admin_Type = Convert.ToInt32(Session["Admin_Type"]);
            List<Menu> OutTree = GetList<Menu>(BLL.Admin_BLL.OutTree(Admin_Type));
            string ss="";
            if (OutTree != null)
            {
                ss = JsonConvert.SerializeObject(OutTree);
            }
            return Content(ss);
        }

        [HttpPost]
        public ActionResult InTree(int id)
        {
            List<Menu> InTree = GetList<Menu>(BLL.Admin_BLL.InTree(id));
            string ss = "";
            if (InTree != null)
            {
                ss = JsonConvert.SerializeObject(InTree);
            }
            return Content(ss);
        }

        [HttpPost]
        public ActionResult Url(int id) 
        {
            string ss = BLL.Admin_BLL.Url(id);
            if (ss != null) 
            {
                return Content(ss);
            }
            return Content(ss);
        }

        public ActionResult LoginAdmin()
        {
            string uname = Request["uname"];
            string upwd = Request["upwd"];
            Session["Admin_Type"] = BLL.Admin_BLL.Login(uname, upwd);
            if (Session["Admin_Type"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }            
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
