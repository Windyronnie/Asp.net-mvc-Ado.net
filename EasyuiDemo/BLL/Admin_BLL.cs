using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
using System.Data;

namespace BLL
{
    public class Admin_BLL
    {
        public static string Login(string uname, string upwd)
        {
            return DAL.Admin_DAL.Login(uname, upwd);
        }

        public static DataTable OutTree(int id)
        {
            return DAL.Admin_DAL.OutTree(id);
        }

        public static DataTable InTree(int id)
        {
            return DAL.Admin_DAL.InTree(id);
        }

        public static string Url(int id)
        {
            return DAL.Admin_DAL.Url(id);
        }
    }
}
