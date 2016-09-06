using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Reflection;
using BLL;
using Model;

namespace List.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            List<Model.Stuinfo> stu = GetList<Model.Stuinfo>(BLL.StuinfoBLL.GetStu());
            IEnumerable<Model.Stuinfo> stus = stu.ToList();
            return View(stus);
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool flag = BLL.StuinfoBLL.Del(id);
            if (flag)
            {
                return Content("true");
            }
            else
            {
                return Content("false");
            }
        }

        public ActionResult Insert()
        {
            DataTable dt = BLL.StuinfoBLL.GetClassInfo();
            List<SelectListItem> classinfo = new List<SelectListItem>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                    classinfo.Add(new SelectListItem
                    {
                        Value = dt.Rows[i][0].ToString(),
                        Text = dt.Rows[i][1].ToString()
                    });
            }
            ViewData["Select"] = new SelectList(classinfo, "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult Insert(string name, string no, string select, string radio)
        {
            Stuinfo ss = new Stuinfo();
            ss.S_Name = name;
            ss.S_No = no;
            ss.S_C_Id = Convert.ToInt32(select);
            if (radio == "0")
            {
                ss.S_state = true;
            }
            else
            {
                ss.S_state = false;
            }
            bool flag = BLL.StuinfoBLL.Insert(ss);
            if (flag)
            {
                return Content("true");
            }
            else
            {
                return Content("false");
            }
        }

        public ActionResult Update(int id)
        {
            DataTable dt_stu = BLL.StuinfoBLL.FindStuInfo(id);
            Stuinfo ss = new Stuinfo();
            ss.S_Id = Convert.ToInt32(dt_stu.Rows[0][0]);
            ss.S_Name = dt_stu.Rows[0][3].ToString();
            ss.S_No = dt_stu.Rows[0][1].ToString();
            ss.S_C_Id = Convert.ToInt32(dt_stu.Rows[0][2]);
            ss.S_state = bool.Parse(dt_stu.Rows[0][4].ToString());
            DataTable dt = BLL.StuinfoBLL.GetClassInfo();
            List<SelectListItem> classinfo = new List<SelectListItem>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                classinfo.Add(new SelectListItem
                {
                    Value = dt.Rows[i][0].ToString(),
                    Text = dt.Rows[i][1].ToString()
                });
            }
            ViewData["Select"] = new SelectList(classinfo, "Value", "Text", ss.S_C_Id);
            return View(ss);
        }

        [HttpPost]
        public ActionResult Update(string id, string no, string name, string select, string radio)
        {
            Stuinfo ss = new Stuinfo();
            ss.S_Id = Convert.ToInt32(id);
            ss.S_Name = name;
            ss.S_No = no;
            ss.S_C_Id = Convert.ToInt32(select);
            if (radio == "0")
            {
                ss.S_state = false;
            }
            else
            {
                ss.S_state = true;
            }
            bool flag = BLL.StuinfoBLL.Update(ss);
            if (flag)
            {
                return Content("true");
            }
            else
            {
                return Content("false");
            }
            return View();
        }

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
