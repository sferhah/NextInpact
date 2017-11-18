using Microsoft.EntityFrameworkCore;
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

        public static async Task<Article> GetArticle(int id)
        {
            using (AppDbContext database = new AppDbContext())
            {
                return await database.Articles.FindAsync(id);
            }
        }

        public static async Task<List<Comment>> GetArticleComments(int articleId)
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
                await AppDbContext.InsertOrReplaceAsync(item);
            }
        }

        public static async Task SyncWithDatabase(IEnumerable<Article> items)
        {
            foreach (var item in items)
            {
                Article db_item = await AppDbContext.GetAsync<Article>(item.Id); ;

                if (db_item != null)
                {
                    item.Content = db_item.Content;                    
                }

                //Update info : CommsCount, DatePublication.
                await AppDbContext.InsertOrReplaceAsync(item);
            }
        }

        public static async Task SaveComments(IEnumerable<Comment> items)
        {
            await AppDbContext.InsertOrReplaceAllAsync(items);
        }

    }
}
