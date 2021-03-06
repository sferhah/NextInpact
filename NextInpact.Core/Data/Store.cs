﻿using Microsoft.EntityFrameworkCore;
using NextInpact.Core.IO;
using NextInpact.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextInpact.Core.Data
{
    public class Store
    {
        public static async Task<List<Article>> GetAll()
        {
            using (AppDbContext database = new AppDbContext())
            {
                var items_in_db = await database.Articles.OrderByDescending(x => x.PublicationTimeStamp).ToListAsync();

                await SetMiniaturesFromCache(items_in_db);

                foreach (var item in items_in_db)
                {
                    item.HasComments = true;
                }

                return items_in_db;
            }
        }

        public static async Task<Article> GetArticle(string id)
        {
            using (AppDbContext database = new AppDbContext())
            {
                return await database.Articles.FindAsync(id);
            }
        }

        public static async Task<List<Comment>> GetArticleComments(string articleId)
        {
            using (AppDbContext database = new AppDbContext())
            {
                return await database.Comments.Where(x => x.ArticleId == articleId).OrderBy(x => x.TimeStampPublication).ToListAsync();
            }
        }

        public static async Task SetMiniaturesFromCache(IEnumerable<Article> items)
        {
            foreach (var item in items)
            {
                byte[] datas = await SaveAndLoad.LoadAsync(ImageFolder.MINIATURES, item.Id.ToString());

                if (datas == null)
                {
                    continue;
                }

                item.ImageData = datas;
            }
        }

        public static async Task SaveArticlesContent(IEnumerable<Article> items)
        {
            foreach (var item in items)
            {
                using (AppDbContext database = new AppDbContext())
                {
                    Article db_item = await database.FindAsync<Article>(item.Id);

                    if (db_item != null)
                    {
                        db_item.Content = item.Content;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Article does not exist in db");
                    }

                    await database.SaveChangesAsync();
                }
            }
        }

        public static async Task SyncWithDatabase(IEnumerable<Article> items)
        {
            foreach (var item in items)
            {
                using (AppDbContext database = new AppDbContext())
                {
                    Article db_item = await database.FindAsync<Article>(item.Id);

                    if (db_item != null)
                    {
                        db_item.TotalCommentsCount = item.TotalCommentsCount;
                        db_item.PublicationTimeStamp = item.PublicationTimeStamp;
                    }
                    else
                    {
                        await database.AddAsync(item);
                    }

                    await database.SaveChangesAsync();
                }
      
            }
        }

        public static async Task SaveComments(IEnumerable<Comment> items)
        {
            using (AppDbContext database = new AppDbContext())
            {
                foreach (var item in items)
                {
                    var itemInDatabase = await database.Comments.Where(x => x.PrimaryKey == item.PrimaryKey).FirstOrDefaultAsync();

                    if (itemInDatabase != null)
                    {
                        database.Entry(itemInDatabase).CurrentValues.SetValues(item);
                    }
                    else
                    {
                        await database.AddAsync(item);
                    }
                }

                await database.SaveChangesAsync();
            }
        }

    }
}
