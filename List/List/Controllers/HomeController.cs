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
using Newtonsoft.Json;

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

        /// <summary>
        /// 首页绑定
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //查找数据 将DataTable转换成List<T>泛型集合
            List<Model.Stuinfo> stu = GetList<Model.Stuinfo>(BLL.StuinfoBLL.GetStu());
            //将List<T>转换成IEnumerable<T>同样也可以往前台传List<T>。
            IEnumerable<Model.Stuinfo> stus = stu.ToList();
            return View(stus);
        }

        public JsonResult Bind() 
        {
            DataTable dt_data = BLL.StuinfoBLL.GetStu();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string _json = "[";
            for (int i = 0; i < dt_data.Rows.Count; i++)
            {
                dic.Clear();
                dic.Add("s_id", dt_data.Rows[i]["s_id"].ToString());
                dic.Add("s_no", dt_data.Rows[i]["s_no"].ToString());
                dic.Add("s_c_id", dt_data.Rows[i]["s_c_id"].ToString());
                dic.Add("s_name", dt_data.Rows[i]["s_name"].ToString());
                dic.Add("s_state", dt_data.Rows[i]["s_state"].ToString());
                _json += JsonConvert.SerializeObject(dic) + ",";
            }
            _json = _json.Substring(0, _json.Length - 1) + "]";
            return Json(_json, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexLike()
        {
            object Find = Request["id"];//通过a标签进行url传值
            //object Find = Request["txt_find"];//通过submit进行form表单传值 现将方式留下
            List<Model.Stuinfo> stu = GetList<Model.Stuinfo>(BLL.StuinfoBLL.GetLike(Find.ToString()));
            IEnumerable<Model.Stuinfo> stus = stu.ToList();
            return View("Index",stus);//转到对应的view传一个强类型
        }

        public ActionResult Delete()
        {
            return View();
        }

        /// <summary>
        /// 删除的方法(单个删除)
        /// </summary>
        /// <param name="id">要删除的id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //删除返回bool
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

        /// <summary>
        /// 添加显示绑定下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult Insert()
        {
            DataTable dt = BLL.StuinfoBLL.GetClassInfo();
            List<SelectListItem> classinfo = new List<SelectListItem>();
            //循环遍历绑定下拉框
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                    classinfo.Add(new SelectListItem
                    {
                        Value = dt.Rows[i][0].ToString(),
                        Text = dt.Rows[i][1].ToString()
                    });
            }
            //ViewData传到前台
            ViewData["Select"] = new SelectList(classinfo, "Value", "Text");
            return View();
        }

        /// <summary>
        /// 添加的方法
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="no">学号</param>
        /// <param name="select">班级/下拉框</param>
        /// <param name="radio">状态/单选框</param>
        /// <returns></returns>
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

        /// <summary>
        /// 多表删除
        /// </summary>
        /// <param name="val">选中的id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteAll(string val)
        {
            //去掉最后一个逗号
            val = val.Substring(0, val.Length - 1);
            //删除多条 返回bool
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

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">修改绑定的id</param>
        /// <returns></returns>
        public ActionResult Update(int id)
        {
            //查找到一行
            DataTable dt_stu = BLL.StuinfoBLL.FindStuInfo(id);
            Stuinfo ss = new Stuinfo();
            //编号
            ss.S_Id = Convert.ToInt32(dt_stu.Rows[0][0]);
            //姓名
            ss.S_Name = dt_stu.Rows[0][3].ToString();
            //学号
            ss.S_No = dt_stu.Rows[0][1].ToString();
            //班级
            ss.S_C_Id = Convert.ToInt32(dt_stu.Rows[0][2]);
            //状态
            ss.S_state = bool.Parse(dt_stu.Rows[0][4].ToString());
            //查找到绑定下拉框的数据
            DataTable dt = BLL.StuinfoBLL.GetClassInfo();
            //循环遍历绑定下拉框
            List<SelectListItem> classinfo = new List<SelectListItem>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                classinfo.Add(new SelectListItem
                {
                    Value = dt.Rows[i][0].ToString(),
                    Text = dt.Rows[i][1].ToString()
                });
            }
            //ViewData传到前台
            ViewData["Select"] = new SelectList(classinfo, "Value", "Text", ss.S_C_Id);
            return View(ss);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">修改的id</param>
        /// <param name="no">修改的学号</param>
        /// <param name="name">修改的姓名</param>
        /// <param name="select">修改的班级/下拉框</param>
        /// <param name="radio">修改的状态/单选按钮</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(string id, string no, string name, string select, string radio)
        {
            Stuinfo ss = new Stuinfo();
            //编号
            ss.S_Id = Convert.ToInt32(id);
            //姓名
            ss.S_Name = name;
            //学号
            ss.S_No = no;
            //班级
            ss.S_C_Id = Convert.ToInt32(select);
            //状态
            if (radio == "0")
            {
                ss.S_state = false;
            }
            else
            {
                ss.S_state = true;
            }
            //查询结果返回bool
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

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public ActionResult Excel()
        {
            string filename = "学生数据";
            DataTable dt = BLL.StuinfoBLL.GetAll();
            HttpResponse resp = System.Web.HttpContext.Current.Response;
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//格式GB2312格式
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
            //返回字符保存格式
            return Content(sb.ToString(), "application/vnd.ms-exce", resp.ContentEncoding);
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
