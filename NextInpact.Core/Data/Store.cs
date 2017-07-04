using NextInpact.Core.IO;
using NextInpact.Core.Models;
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

            foreach (var item_in_db in items_in_db)
            {
                item_in_db.Comments = (await ThreadSafeSqlite.Instance.GetAllAsync<Comment>()).Where(x => x.ArticleId == item_in_db.Id).OrderBy(x => x.TimeStampPublication).ToList();
            }

            return items_in_db;
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

        public static async Task SaveComments(IEnumerable<Article> items)
        {
            foreach (var item in items)
            {
                foreach (Comment comment in item.Comments)
                {
                    Comment comment_int_db = await ThreadSafeSqlite.Instance.GetAsync<Comment>(comment.PrimaryKey);

                    if (comment_int_db != null)
                    {
                        continue;
                    }

                    await ThreadSafeSqlite.Instance.InsertOrReplaceAsync(comment);
                }
            }
        }

    }
}
