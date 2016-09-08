using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Reflection;
using BLL;
using Model;
using System.Text;

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

        [HttpPost]
        public ActionResult DeleteAll(string val)
        {
            val = val.Substring(0, val.Length - 1);
            bool flag = BLL.StuinfoBLL.DelAll(val);
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
        }

        public ActionResult Excel()
        {
            string filename = "学生数据";
            DataTable dt = BLL.StuinfoBLL.GetAll();
            HttpResponse resp = System.Web.HttpContext.Current.Response;
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + filename+".xls");
            string colHeaders = "", ls_item = "";
            StringBuilder sb = new StringBuilder();
            ////定义表对象与行对象，同时用DataSet对其值进行初始化
            DataRow[] myRow = dt.Select();//可以类似dt.Select("id>10")之形式达到数据筛选目的
            int i = 0;
            int cl = dt.Columns.Count;

            //取得数据表各列标题，各标题之间以t分割，最后一个列标题后加回车符
            for (i = 0; i < cl; i++)
            {
                if (i == (cl - 1))//最后一列，加n
                {
                    colHeaders += dt.Columns[i].Caption.ToString() + "\n";
                }
                else
                {
                    colHeaders += dt.Columns[i].Caption.ToString() + "\t";
                }

            }
            resp.Write(colHeaders);
            sb.Append(colHeaders);
            //向HTTP输出流中写入取得的数据信息

            //逐行处理数据 
            foreach (DataRow row in myRow)
            {
                //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据   
                for (i = 0; i < cl; i++)
                {
                    if (i == (cl - 1))//最后一列，加n
                    {
                        ls_item += row[i].ToString() + "\n";
                    }
                    else
                    {
                        ls_item += row[i].ToString() + "\t";
                    }

                }
                resp.Write(ls_item);
                sb.Append(ls_item);
                ls_item = "";

            }
            resp.End();
            return Content(sb.ToString(), "application/vnd.ms-exce", resp.ContentEncoding);
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
