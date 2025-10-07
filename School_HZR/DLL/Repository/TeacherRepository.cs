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
    public class TeacherRepository : IRepository<Teacher>
    {
        private readonly SchooleContext _context;

        public TeacherRepository(SchooleContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Teacher item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }
        public async Task AddStudentAsync(Teacher item, Student student)
        {
            student.TeacherId = item.Id;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Teacher item)
        {
            _context.Add(GetById( item.Id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Teacher>> GetAll()
        {
            return await _context.Set<Teacher>().ToArrayAsync();
        }

        public async Task<Teacher> GetById(int id)
        {
            return await _context.Set<Teacher>().FindAsync(id);
        }

        public async Task Update(Teacher item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }
    }
}
