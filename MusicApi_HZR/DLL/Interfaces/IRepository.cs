using DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Interfaces
{
    public interface IRepository
    {
        Task<Music> AddMusic(Music music);
        Task RemoveMusic(int id);
        Task<Music[]> GetAllMusic();
        Task<Music> UpdateMusic( Music music);
        Task<Music> SearchMusicByID(int id);
    }
}
