using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Grade { get; set; }
        public int TeacherId { get; set; }
        public int ClassRoomId { get; set; }
    }
}
