using Client_HZR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_HZR.Interfaces
{
    public interface IClientInterfaces
    {
        Task<Client> AddClient(string path, Client client);
        Task<IEnumerable<Client>> GetAll(string path);
        Task<Client> GetByName(string name, string path);
    }
}
