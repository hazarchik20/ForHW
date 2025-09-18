using Client_HZR.Interfaces;
using Client_HZR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_HZR.Services
{
    public class ClientServices : IClientInterfaces
    {
        public Task<Client> AddClient(string path, Client client)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine($"***\n{client.name}|{client.BirthDay}|{client.address.street}|{client.address.city}|{client.address.HouseNumber}|");
            }
            return Task.FromResult(client);
        }

        public async Task<IEnumerable<Client>> GetAll(string path)
        {
            List<Client> client = new List<Client>();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLineAsync().Result) != null)
                {
                    if(line != null && line != "***")
                    {
                        string[] parts = line.Split('|');
                        Client c = new Client
                        {
                            name = parts[0],
                            BirthDay = DateTime.Parse(parts[1]),
                            address = new Addres
                            {
                                street = parts[2],
                                city = parts[3],
                                HouseNumber = Convert.ToInt32(parts[4])
                            }
                        };
                        client.Add(c);
                    }
                }
            }
            return client;
        }

        public Task<Client> GetByName(string name, string path)
        {
            Client client = new Client();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLineAsync().Result) != null)
                {
                    string[] parts = line.Split('|');
                    if(parts[0] == name)
                    client = new Client
                    {
                        name = parts[0],
                        BirthDay = DateTime.Parse(parts[1]),
                        address = new Addres
                        {
                            street = parts[2],
                            city = parts[3],
                            HouseNumber = int.Parse(parts[4])
                        }
                    };
                }
            }
            return Task.FromResult(client);
        }
    }
}
