using NextInpact.Core.IO;
using NextInpact.Core.Models;
using NextInpact.Core.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextInpact.Core.Data
{
    public class Store
    {
        public static async Task<List<Article>> GetAll()
        {
            var items_in_db = (await ThreadSafeSqlite.Instance.GetAllAsync<Article>()).OrderByDescending(x => x.PublicationTimeStamp).ToList();

            await Store.SetMiniaturesFromCache(items_in_db);

            foreach (var item in items_in_db)
            {
                item.HasComments = true;
            }

            return items_in_db;

        }

        public static async Task<Article> GetArticle(int id)
        {
            var item = await ThreadSafeSqlite.Instance.GetAsync<Article>(id);

            return item;
        }

        public static async Task<List<Comment>> GetArticleComments(int articleId)
        {
            var comments = (await ThreadSafeSqlite.Instance.GetAllAsync<Comment>()).Where(x => x.ArticleId == articleId).OrderBy(x => x.TimeStampPublication).ToList();

            return comments;
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
                await ThreadSafeSqlite.Instance.InsertOrReplaceAsync(item);
            }
        }

        public static async Task SyncWithDatabase(IEnumerable<Article> items)
        {
            foreach (var item in items)
            {
                Article db_item = await ThreadSafeSqlite.Instance.GetAsync<Article>(item.Id); ;

                if (db_item != null)
                {
                    item.Content = db_item.Content;                    
                }

                //Update info : CommsCount, DatePublication.
                await ThreadSafeSqlite.Instance.InsertOrReplaceAsync(item);
            }
        }

        public static async Task SaveComments(IEnumerable<Comment> items)
        {
            await ThreadSafeSqlite.Instance.InsertOrReplaceAllAsync(items);
        }

    }
}
