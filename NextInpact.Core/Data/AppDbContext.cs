using Microsoft.EntityFrameworkCore;
using NextInpact.Core.Models;
using System;

namespace NextInpact.Core.Data
{    
    public class AppDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set;  }
        public DbSet<Comment> Comments { get; set; }

        public static void Init()
        {
            using (AppDbContext database = new AppDbContext())
            {
                database.Database.EnsureCreated();
                database.Database.Migrate();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = Plugin.NetStandardStorage.CrossStorage.FileSystem.LocalStorage.FullPath + @"\" + String.Format("{0}.db3", "NextInpact");

            optionsBuilder.UseSqlite($"Filename={path}");
        }
    }
}
