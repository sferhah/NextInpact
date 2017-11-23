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
            var urlPage = Constants.NEXT_INPACT_URL_NUM_PAGE + page;

            String html = await Downloader.GetAsync(urlPage);

            return await HtmlParser.ParseArticleListAsync(html, urlPage);
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

        public static async Task<List<Comment>> DownloadArticlesComs(IEnumerable<Article> items)
        {
            var tasks = items
                        .Select(x => DownloadComments(x))
                        .ToArray();

            var arrayOfListOfComments = await Task.WhenAll(tasks);

            return arrayOfListOfComments.SelectMany(x => x).ToList();
        }

        public static async Task<List<Comment>> DownloadComments(Article article)
        {
            String urlPage = GetUrlComs(article.Id);

            String html = await Downloader.GetAsync(urlPage);

            var comments = await HtmlParser.ParseCommentsAsync(html, urlPage);

            article.TotalCommentsCount = await HtmlParser.GetCommentsCountAsync(html, urlPage);

            article.HasComments = true;

            return comments;
        }

        public static String GetUrlComs(string articleId)
        {
            int maPage = 1;

            String contructedUrl = Constants.NEXT_INPACT_URL_COMMENTS
                + "?"
                + Constants.NEXT_INPACT_URL_COMMENTS_PARAM_ARTICLE_ID
                + "="
                + articleId
                + "&"
                + Constants.NEXT_INPACT_URL_COMMENTS_PARAM_PAGE_NUM
                + "="
                + maPage;

            return contructedUrl;
        }

    }


}
