using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;

namespace DapperDemo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            List<Stuinfo> stu = BLL.StuBLL.GetStu();
            return View(stu);
        }

        public ActionResult Insert() 
        {
            List<ClassInfo> Class = BLL.ClassBLL.GetClassInfo();
            List<SelectListItem> classinfo = new List<SelectListItem>();
            for (int i = 0; i < Class.Count; i++)
            {
                classinfo.Add(new SelectListItem
                {
                    Value = Class[i].C_Id.ToString(),
                    Text=Class[i].C_Name.ToString()
                });
            }
            ViewData["Select"] = new SelectList(classinfo, "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult Insert(string S_Name, string S_No, string Select, string state) 
        {
            Stuinfo stu = new Stuinfo();
            stu.S_Name = S_Name;
            stu.S_No = S_No;
            stu.S_C_Id = Convert.ToInt32(Select);
            if (state == "0")
            {
                stu.S_State = true;
            }
            else 
            {
                stu.S_State = false;
            }
            int flag = BLL.StuBLL.InsertStu(stu);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Delete(string id) 
        {
            int flag = BLL.StuBLL.DelStu(id);
            if (flag == 0)
            {
                return Content("false");
            }
            else
            {
                return Content("true");
            }
        }

        public ActionResult Update(int id) 
        {
            Stuinfo stu = BLL.StuBLL.FindStu(id);
            List<ClassInfo> Class = BLL.ClassBLL.GetClassInfo();
            List<SelectListItem> classinfo = new List<SelectListItem>();
            for (int i = 0; i < Class.Count; i++)
            {
                classinfo.Add(new SelectListItem
                {
                    Value = Class[i].C_Id.ToString(),
                    Text = Class[i].C_Name.ToString()
                });
            }
            ViewData["Select"] = new SelectList(classinfo, "Value", "Text", stu.S_C_Id);
            return View(stu);
        }

        [HttpPost]
        public ActionResult Update(string id, string no, string name, string select, string radio)
        {
            Stuinfo stu = new Stuinfo();
            stu.S_Id = Convert.ToInt32(id);
            stu.S_No = no;
            stu.S_Name = name;
            stu.S_C_Id = Convert.ToInt32(select);
            if (radio == "0")
            {
                stu.S_State = true;
            }
            else
            {
                stu.S_State = false;
            }
            int flag = BLL.StuBLL.UpdateStu(stu);
            if (flag == 0)
            {
                return Content("false");
            }
            else
            {
                return Content("true");
            }
            
        }

    }
}
