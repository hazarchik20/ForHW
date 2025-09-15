using BLL.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ICityApiService
    {
        Task<City[]> GetCurrentCityAsync(string region);
    }
}
