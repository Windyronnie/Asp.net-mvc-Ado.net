using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class ClassBLL
    {
        public static string GetClass(int id) 
        {
            return DAL.ClassDAL.GetClass(id);
        }

        public static List<ClassInfo> GetClassInfo() 
        {
            return DAL.ClassDAL.GetClassInfo();
        }
    }
}
