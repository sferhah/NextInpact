using Microsoft.EntityFrameworkCore;
using NextInpact.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


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

        public static async Task InsertOrReplaceAsync(Article item)
        {
            using (AppDbContext database = new AppDbContext())
            {
                var itemInDatabase = await database.Articles.Where(x => x.Id == item.Id).FirstOrDefaultAsync();

                if (itemInDatabase != null)
                {
                    database.Entry(itemInDatabase).CurrentValues.SetValues(item);
                }
                else
                {
                    await database.AddAsync(item);
                }

                await database.SaveChangesAsync();
            }

        }

    }

}
