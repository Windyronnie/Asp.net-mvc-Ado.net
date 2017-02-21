using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class StuBLL
    {
        public static List<Stuinfo> GetStu()
        {
            return DAL.StuDAL.GetStu();
        }

        public static int InsertStu(Stuinfo stu)
        {
            return DAL.StuDAL.InsertStu(stu);
        }

        public static int DelStu(string id) 
        {
            return DAL.StuDAL.DelStu(id);
        }

        public static Stuinfo FindStu(int id) 
        {
            return DAL.StuDAL.FindStu(id);
        }

        public static int UpdateStu(Stuinfo stu) 
        {
            return DAL.StuDAL.UpdateStu(stu);
        }

    }
}
