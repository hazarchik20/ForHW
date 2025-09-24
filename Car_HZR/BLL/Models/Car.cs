using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Car
    {
        public int Id { get; set; }
        public int? Price { get; set; }
        public string? Name { get; set; }
        public DateTime? YearBirth { get; set; }
        public int? HourePower { get; set; }

    }
}
