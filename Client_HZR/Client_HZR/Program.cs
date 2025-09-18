using Client_HZR.Models;
using Client_HZR.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace Client_HZR
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string path = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName+ "\\Client_HZR\\DBFoolder\\DBtxt";
            
           
            ClientServices clientServices = new ClientServices();
            Console.WriteLine("enter 1 for ADD, 2 for GETALL, 3 for GETBYNAME 0 -> Exit");
            int choice = Convert.ToInt32(Console.ReadLine());
            while (choice != 0)
            {
                switch (choice)
                {
                    case 0: { Console.WriteLine("See you soon :)");} return;
                    case 1:
                        {
                            Console.WriteLine("Create Client");
                            Console.Write("Enter Name ->");
                            string name = Console.ReadLine();
                            Console.Write("Enter Birthday ->");
                            DateTime date = Convert.ToDateTime(Console.ReadLine());
                            Console.Write("Enter City ->");
                            string City = Console.ReadLine();
                            Console.Write("Enter street ->");
                            string street = Console.ReadLine();
                            Console.Write("Enter house number ->");
                            int house = Convert.ToInt32(Console.ReadLine());
                           
                            Client client = new Client()
                            {
                                name = name,
                                BirthDay = date,
                                address = new Addres()
                                {
                                    city = City,
                                    street = street,
                                    HouseNumber = house
                                }
                            };
                            await clientServices.AddClient(path, client);
                            Console.WriteLine("ALL GOOD!");
                        } break;
                    case 2: 
                        {
                            List<Client> client = (await clientServices.GetAll(path)).ToList(); 
                            foreach (var item in client)
                            {
                                Console.WriteLine($"Name: {item.name}, BirthDay: {item.BirthDay}, City: {item.address.city}, Street: {item.address.street}, HouseNumber: {item.address.HouseNumber}");
                            }
                        } break;
                    case 3: 
                        {
                            Console.Write("Enter Name ->");
                            string name = Console.ReadLine();
                            Client client = await clientServices.GetByName(name, path);
                            if(client.name ==null)
                            {
                                Console.WriteLine("Not Found");
                                break;
                            }
                            Console.WriteLine($"Name: {client.name}, BirthDay: {client.BirthDay}, City: {client.address.city}, Street: {client.address.street}, HouseNumber: {client.address.HouseNumber}");
                            
                        } break;
                }

                Console.WriteLine("enter 1 for ADD, 2 for GETALL, 3 for GETBYNAME 0 -> Exit");
                choice = Convert.ToInt32(Console.ReadLine());
            }

        }
    }
}
