using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Menu
    {
        public int Menu_Id { get; set; }
        public string Menu_Name { get; set; }
        public int Menu_Parent_Id { get; set; }
        public string Menu_Url { get; set; }
    }
}
