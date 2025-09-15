using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Responses
{
    public record List(Weather[] Weather, Wind Wind, Temperature Main, string dt_txt);
}
