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
    
      
        public static Object locker = new Object();
       

        public static async Task<T> GetAsync<T>(Object value) where T : class
        {
            using (AppDbContext database = new AppDbContext())
            {
                return await database.FindAsync<T>(value);
            }
        }

        public static async Task InsertOrReplaceAsync(Article item)
        {
            await Task.Run(() => InsertOrReplace(item));
        }

        private static void InsertOrReplace(Article item) 
        {
            lock (locker)
            {
                using (AppDbContext database = new AppDbContext())
                {
                    var itemInDatabse = database.Articles.Where(x=>x.Id == item.Id).FirstOrDefault();
                    if (itemInDatabse != null)
                    {
                        //database.Articles.Update(item);
                        database.Entry(itemInDatabse).CurrentValues.SetValues(item);
                    }
                    else
                    {
                        database.Add(item);
                    }
                    
                    database.SaveChanges();
                }
            }
        }

        public static async Task InsertOrReplaceAllAsync(IEnumerable<Comment> item) 
        {
            await Task.Run(() => InsertOrReplaceAll(item));
        }

        private static void InsertOrReplaceAll(IEnumerable<Comment> items)
        {
            lock (locker)
            {
                using (AppDbContext database = new AppDbContext())
                {
                    foreach(var item in items)
                    {
                        var itemInDatabse = database.Comments.Where(x => x.Id == item.Id).FirstOrDefault();
                        if (itemInDatabse != null)
                        {
                            //database.Comments.Update(item);
                            database.Entry(itemInDatabse).CurrentValues.SetValues(item);

                        }
                        else
                        {
                            database.Add(item);
                        }                       
                    }

                    database.SaveChanges();
                }
              
            }
        }

    }

}
