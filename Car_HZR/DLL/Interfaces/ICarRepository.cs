using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Interfaces
{
    internal interface ICarRepository
    {
        Task<Car> AddCar(Car car);
        Task RemoveCar(Car car);
        Task<Car[]> GetAllCar();
        Task<Car> UpdateCar(int id,Car car);
        Task<Car> SearchByIDCar(int id);
        Task<Car> UpdatePriceCar(int id, int price);
        Task<Car[]> Show20Car();
    }
}
