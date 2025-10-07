using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class MusicContext : DbContext
    {
        public MusicContext(DbContextOptions<MusicContext> options)
        : base(options)
        {
        }
        public MusicContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MusicApiDB;Integrated Security=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Music>()
                 .HasKey(x => x.id);
            modelBuilder.Entity<Music>()
                .Property(x => x.countListen)
                .IsRequired();
            modelBuilder.Entity<Music>()
               .Property(x => x.time)
               .IsRequired();
            modelBuilder.Entity<Music>()
                .Property(x => x.singer)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Music>()
               .Property(x => x.name)
               .HasMaxLength(100)
               .IsRequired();
        }
    }
}
