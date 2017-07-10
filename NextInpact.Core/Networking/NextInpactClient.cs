using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NextInpact.Core.Parsing;
using NextInpact.Core.Models;
using NextInpact.Core.IO;

namespace NextInpact.Core.Networking
{
    public class NextInpactClient
    {
        public static async Task<List<Article>> GetArticlesAsync(int page)
        {
            var urlPage = Constantes.NEXT_INPACT_URL_NUM_PAGE + page;

            String html = await Downloader.GetAsync(urlPage);

            List<Article> items = await HtmlParser.ParseArticleListAsync(html, urlPage);

            return items;
        }

        public static async Task DownloadMiniaturesAsync(IEnumerable<Article> items)
        {
            var tasks = items
                        .Select(x => DownloadMiniature(x))
                        .ToArray();

            await Task.WhenAll(tasks);
        }


        public static async Task DownloadMiniature(Article article)
        {
            byte[] data = await Downloader.GetAsBytesAsync(article.UrlIllustration);

            await SaveAndLoad.SaveAsync(ImageFolder.MINIATURES, article.Id.ToString(), data);

            article.ImageData = data;
        }

        public static async Task DownloadArticlesContent(IEnumerable<Article> items)
        {
            var tasks = items
                        .Select(x => DownloadArticleContent(x))
                        .ToArray();

            await Task.WhenAll(tasks);
        }

        public static async Task DownloadArticleContent(Article article)
        {
            String html = await Downloader.GetAsync(article.Url);
            article.Content = await HtmlParser.ParseArticleContentAsync(html, article.Url);
        }

        public static async Task DownloadArticlesComs(IEnumerable<Article> items)
        {
            var tasks = items
                        .Select(x => DownloadComments(x))
                        .ToArray();

            await Task.WhenAll(tasks);
        }

        public static async Task<List<Comment>> DownloadComments(Article article)
        {
            String urlPage = GetUrlComs(article.Id);

            String html = await Downloader.GetAsync(urlPage);

            article.Comments = await HtmlParser.ParseCommentsAsync(html, urlPage);
            article.TotalCommentsCount = await HtmlParser.GetNbCommentairesAsync(html, urlPage);

            return article.Comments;
        }

        public static String GetUrlComs(int articleId)
        {
            int maPage = 1;

            String contructedUrl = Constantes.NEXT_INPACT_URL_COMMENTAIRES
                + "?"
                + Constantes.NEXT_INPACT_URL_COMMENTAIRES_PARAM_ARTICLE_ID
                + "="
                + articleId
                + "&"
                + Constantes.NEXT_INPACT_URL_COMMENTAIRES_PARAM_NUM_PAGE
                + "="
                + maPage;

            return contructedUrl;
        }

    }


}
