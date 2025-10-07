using BLL.Models;
using DLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly SchooleContext _context;

        public StudentRepository(SchooleContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Student item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();

        }

        public async Task Delete(Student item)
        {
            _context.Add(GetById(item.Id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _context.Set<Student>().ToArrayAsync();
        }

        public async Task<Student> GetById(int id)
        {
            return await _context.Set<Student>().FindAsync(id);
        }

        public async Task Update(Student item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }
    }
}
