using BLL.Models;
using DLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public class ClassRoomRepository : IRepository<ClassRoom>
    {
        private readonly SchooleContext _context;

        public ClassRoomRepository(SchooleContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ClassRoom item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }
        public async Task AddStudentAsync(ClassRoom item, Student student)
        {
            student.ClassRoomId = item.Id;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ClassRoom item)
        {
            _context.Add(GetById(item.Id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClassRoom>> GetAll()
        {
            return await _context.Set<ClassRoom>().ToArrayAsync();
        }

        public async Task<ClassRoom> GetById(int id)
        {
           return await _context.Set<ClassRoom>().FindAsync(id);
        }

        public async Task Update(ClassRoom item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }
    }
}
