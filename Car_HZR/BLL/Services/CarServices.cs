
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CarServices 
    {
        public Task<Car> IsValid(Car car)
        {
            if ((car.Price != null && car.Price <= 0) || (car.HourePower != null && car.HourePower <= 0)|| car.YearBirth == null || string.IsNullOrEmpty(car.Name) || car.Name.Length > 100)
            {
                return null;
            }
            return Task.FromResult(car);
        }
    }
}
