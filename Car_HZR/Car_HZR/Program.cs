using BLL.Models;
using BLL.Services;
using DLL;
using DLL.Repository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Transactions;

var options = new DbContextOptionsBuilder<CarDataContext>()
           .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CarDB;Integrated Security=True;")
           .Options;
var context = new CarDataContext(options);
var _repository = new CarRepository(context);
CarServices _carServices = new CarServices() ;

Console.WriteLine("Enter 1 for ADD car\nEnter 2 for delete car\nEnter 3 for Update car\nEnter 4 for ShowAll car\nEnter 5 for searh car by ID \nEnter 6 for Show 20 car\nEnter 7 for Update only price car\n 0 - EXT\n");
string str = "";

while (str != "0")
{
    Console.Write("Enter number: ");
    str = Console.ReadLine();
    if (str == "1")
    {
        Console.Write("Enter Name car: ");
        string TempName = Console.ReadLine();
        Console.Write("Enter HP car: ");
        int TempHP = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter Price car: ");
        int TempPrice = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter BirthDay car: ");
        DateTime TempBD = Convert.ToDateTime(Console.ReadLine()); 
        Car c= new Car() { Name = TempName, HourePower = TempHP, Price = TempPrice, YearBirth = TempBD };
        if(_carServices.IsValid(c)!=null)
            await _repository.AddCar(c);
        
    }
    if (str == "2")
    {
        Console.Write("Enter number car for deleted: ");
        Car[] car = await _repository.GetAllCar();
        for(int i =0;i<car.Length;i++)
        {
            Console.WriteLine($"{i}: {car[i].Name} - {car[i].Price} - {car[i].HourePower}");
        }
        int id = Convert.ToInt32(Console.ReadLine());
        await _repository.RemoveCar( await _repository.SearchByIDCar(car[id].Id));
    }
    if (str == "3")
    {
        Console.Write("Enter number car for Update: ");
        Car[] car = await _repository.GetAllCar();
        for (int i = 0; i < car.Length; i++)
        {
            Console.WriteLine($"{i}: {car[i].Name} - {car[i].Price} - {car[i].HourePower}");
        }
        int id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter new Name car: ");
        string TempName = Console.ReadLine();
        Console.Write("Enter new HP car: ");
        int TempHP = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter new Price car: ");
        int TempPrice = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter new BirthDay car: ");
        DateTime TempBD = Convert.ToDateTime(Console.ReadLine());
        await _repository.UpdateCar(car[id].Id, new Car() { Name = TempName, HourePower = TempHP, Price = TempPrice, YearBirth = TempBD } );
    }
    if (str == "4")
    {
        Car[] car = await _repository.GetAllCar();
        for (int i = 0; i < car.Length; i++)
        {
            Console.WriteLine($"{i}: {car[i].Name} - {car[i].Price} - {car[i].HourePower}");
        }
    }
    if (str == "5")
    {
        Console.WriteLine("Enter ID car for search: ");
        int id = Convert.ToInt32(Console.ReadLine());
        await _repository.SearchByIDCar(id);
    }
    if (str == "6")
    {
        Car[] car = await _repository.Show20Car();
        for (int i = 0; i < car.Length; i++)
        {
            Console.WriteLine($"{i}: {car[i].Name} - {car[i].Price} - {car[i].HourePower}");
        }
    }
    if (str == "7")
    {
        Console.WriteLine("Enter New Price car for search: ");
        int price = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter number car for Update: ");
        Car[] car = await _repository.GetAllCar();
        for (int i = 0; i < car.Length; i++)
        {
            Console.WriteLine($"{i}: {car[i].Name} - {car[i].Price} - {car[i].HourePower}");
        }
        int id = Convert.ToInt32(Console.ReadLine());
        await _repository.UpdatePriceCar(id, price);
    }
}
