using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Interfaces
{
    public interface IRepository<T>
    {
        Task AddAsync(T item);
        Task Update(T item);
        Task Delete(T item);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
    }
}
