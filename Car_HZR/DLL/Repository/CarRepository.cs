using BLL.Models;
using DLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly CarDataContext _context;

        public CarRepository(CarDataContext context)
        {
            _context = context;
        }
        
        public async Task<Car> AddCar(Car car)
        {
            _context.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public Task<Car[]> GetAllCar()
        {
            return _context.Set<Car>().ToArrayAsync();
        }

        public async Task RemoveCar(Car car)
        {
            _context.Remove(car);
            await _context.SaveChangesAsync();
        }

        public Task<Car> SearchByIDCar(int id)
        {
            return _context.Set<Car>().FirstAsync(x => x.Id == id);
        }

        public Task<Car[]> Show20Car()
        {
            return _context.Set<Car>().Take(20).ToArrayAsync();
        }

        public async Task<Car> UpdateCar(int id,Car newCar)
        {
            var car = await _context.Set<Car>().FirstOrDefaultAsync(C => C.Id == id);
            if (car == null) return null;

            car.Name = newCar.Name;
            car.Price = newCar.Price;
            car.HourePower = newCar.HourePower;
            car.YearBirth = newCar.YearBirth;

            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<Car> UpdatePriceCar(int id, int price)
        {
            var car = await _context.Set<Car>().FirstOrDefaultAsync(C => C.Id == id);
            if (car == null) return null;
            car.Price = price;
            await _context.SaveChangesAsync();
            return car;
            
        }
    }
}
