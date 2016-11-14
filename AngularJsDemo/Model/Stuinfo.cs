using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Stuinfo
    {
        public int S_Id { get; set; } //编号
        public string S_No { get; set; } //学号
        public int S_C_Id { get; set; } //班级
        public string S_Name { get; set; } //姓名
        public bool S_state { get; set; } //状态
    }
}
