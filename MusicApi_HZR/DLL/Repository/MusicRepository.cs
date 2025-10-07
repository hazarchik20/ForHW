using DLL.Models;
using DLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public class MusicRepository : IRepository
    {
        private readonly MusicContext _context;

        public MusicRepository(MusicContext context)
        {
            _context = context;
        }

        public async Task<Music> AddMusic(Music music)
        {
            _context.Add(music);
            await _context.SaveChangesAsync();
            return music;
        }

        public async Task<Music[]> GetAllMusic()
        {
            return await _context.Set<Music>().ToArrayAsync();
        }

        public async Task RemoveMusic(int id)
        {
            _context.Remove(SearchMusicByID(id));
            await _context.SaveChangesAsync();
        }

        public async Task<Music> SearchMusicByID(int id)
        {
            return await _context.Set<Music>().FirstAsync(x => x.id == id);
        }
        public async Task<IEnumerable<Music>> GetMusicBetterTask(int offers, int limit,string name)
        {
            var query = _context.Set<Music>().AsQueryable();
            //наскільки зрозумів, то AsQueryable краще працює з LINQ і робить запити більш оптимізованими і вони виконуються в бд а не в нашому коді

            if (!string.IsNullOrEmpty(name))
                query = query.Where(t => t.name.Contains(name));

            return await query
                .Skip(offers)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Music> UpdateMusic( Music music)
        {
            _context.Update(music);
            await _context.SaveChangesAsync();
            return music;
        }
    }
}
