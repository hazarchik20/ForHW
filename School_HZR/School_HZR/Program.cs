using BLL.Models;
using DLL;
using DLL.Repository;
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder<SchooleContext>()
           .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CarDB;Integrated Security=True;")
           .Options;
var context = new SchooleContext(options);
var _CRrepository = new ClassRoomRepository(context);
var _Srepository = new StudentRepository(context);
var _Trepository = new TeacherRepository(context);

bool exit = false;

while (!exit)
{
    Console.WriteLine("\n=== SCHOOL Console UI ===");
    Console.WriteLine("1. Add ");
    Console.WriteLine("2. Update ");
    Console.WriteLine("3. Delete ");
    Console.WriteLine("4. GetAll ");
    Console.WriteLine("5. Add Sudent (for classroom/teacher) ");
    Console.WriteLine("0. Exit");
    Console.Write("Choose option: ");

    var choice = Console.ReadLine();

    await ShowCryptos(Convert.ToInt32(choice));
}
async Task ShowCryptos(int number)
{
    Console.WriteLine("\n=== SCHOOL Console UI ===");
    Console.WriteLine("1. Student");
    Console.WriteLine("2. Teacher");
    Console.WriteLine("3. Classroom");
    Console.WriteLine("0. Exit");
    Console.Write("Choose option: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            {
                if (number == 1)
                {
                    string name = Console.ReadLine();
                    int grade = Convert.ToInt32(Console.ReadLine());

                    await _Srepository.AddAsync(new Student() { Name= name , Grade = grade});
                }
                else if (number == 2)
                {
                    foreach (var item in await _Srepository.GetAll())
                    {
                        Console.WriteLine($"{item.Id} - {item.Name} - {item.Grade}");
                    }
                    int id = Convert.ToInt32(Console.ReadLine());
                    await _Srepository.Update(await _Srepository.GetById(id));
                }
                else if (number == 3)
                {
                    foreach (var item in await _Srepository.GetAll())
                    {
                        Console.WriteLine($"{item.Id} - {item.Name} - {item.Grade}");
                    }
                    int id = Convert.ToInt32(Console.ReadLine());
                    await _Srepository.Delete(await _Srepository.GetById(id));
                }
                else if (number == 4)
                {
                    var s = await _Srepository.GetAll();
                }

            }
            break;

        case "2":
            {
                if (number == 1)
                {
                    string name = Console.ReadLine();
                    string sub = Console.ReadLine();

                    await _Trepository.AddAsync(new Teacher() { Name = name, Subject= sub });
                }
                else if (number == 2)
                {
                    foreach (var item in await _Trepository.GetAll())
                    {
                        Console.WriteLine($"{item.Id} - {item.Name} - {item.Subject}");
                    }
                    int id = Convert.ToInt32(Console.ReadLine());
                    await _Trepository.Update(await _Trepository.GetById(id));
                }
                else if (number == 3)
                {
                    foreach (var item in await _Trepository.GetAll())
                    {
                        Console.WriteLine($"{item.Id} - {item.Name} - {item.Subject}");
                    }
                    int id = Convert.ToInt32(Console.ReadLine());
                    await _Trepository.Delete(await _Trepository.GetById(id));
                }
                else if (number == 4)
                {
                    var s = await _Trepository.GetAll();
                }

                else if (number == 5)
                {
                    foreach (var item in await _Trepository.GetAll())
                    {
                        Console.WriteLine($"{item.Id} - {item.Name} - {item.Subject}");
                    }
                    int idT = Convert.ToInt32(Console.ReadLine());

                    foreach (var item in await _Srepository.GetAll())
                    {
                        Console.WriteLine($"{item.Id} - {item.Name} - {item.Grade}");
                    }
                    int idS = Convert.ToInt32(Console.ReadLine());
                    await _Trepository.AddStudentAsync(await _Trepository.GetById(idT), await _Srepository.GetById(idS));
                }
            }
            break;

        case "3":
            {
                if (number == 1)
                {
                    string RoomNum = Console.ReadLine();
                    int cap = Convert.ToInt32(Console.ReadLine());

                    await _CRrepository.AddAsync(new ClassRoom() { RoomNumber = RoomNum, Capacity = cap });
                }
                else if (number == 2)
                {
                    foreach (var item in await _CRrepository.GetAll())
                    {
                        Console.WriteLine($"{item.Id} - {item.RoomNumber} - {item.Capacity}");
                    }
                    int id = Convert.ToInt32(Console.ReadLine());
                    await _CRrepository.Update(await _CRrepository.GetById(id));
                }
                else if (number == 3)
                {
                    foreach (var item in await _CRrepository.GetAll())
                    {
                        Console.WriteLine($"{item.Id} - {item.RoomNumber} - {item.Capacity}");
                    }
                    int id = Convert.ToInt32(Console.ReadLine());
                    await _CRrepository.Delete(await _CRrepository.GetById(id));
                }
                else if (number == 4)
                {
                    var s = await _Trepository.GetAll();
                }

                else if (number == 5)
                {
                    foreach (var item in await _CRrepository.GetAll())
                    {
                        Console.WriteLine($"{item.Id} - {item.RoomNumber} - {item.Capacity}");
                    }
                    int idCR = Convert.ToInt32(Console.ReadLine());
                    foreach (var item in await _Srepository.GetAll())
                    {
                        Console.WriteLine($"{item.Id} - {item.Name} - {item.Grade}");
                    }
                    int idS = Convert.ToInt32(Console.ReadLine());
                    await _CRrepository.AddStudentAsync(await _CRrepository.GetById(idCR), await _Srepository.GetById(idS));
                }
            }
            break;
        case "0":
            return;

        default:
            Console.WriteLine("Invalid option");
            break;
    }
}
