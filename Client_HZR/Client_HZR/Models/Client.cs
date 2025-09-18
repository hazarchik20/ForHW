using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client_HZR.Models
{
    public class Client
    {
        public string name { get; set; }
        public DateTime BirthDay { get; set; }
        public Addres address { get; set; }
    }
}
