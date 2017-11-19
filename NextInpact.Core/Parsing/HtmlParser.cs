using AngleSharp.Dom;
using NextInpact.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace NextInpact.Core.Parsing
{
    public class HtmlParser
    {
        public static async Task<List<Article>> ParseArticleListAsync(string s, string urlPage)
        {
            return await Task.Run(() => ParseArticleList(s, urlPage));
        }

        public static List<Article> ParseArticleList(string html, string urlPage)
        {
            List<Article> articles = new List<Article>();

            //Won't find some encodings : just press F5 several times...
            var htmlDocument = new AngleSharp.Parser.Html.HtmlParser().Parse(html);

            foreach (var articleElement in htmlDocument.QuerySelectorAll("article[data-acturowid][data-datepubli]"))
            {
                String date = articleElement.Attributes["data-datepubli"].Value;
                var image = articleElement.QuerySelectorAll("img[class=ded-image]").First().Attributes["data-frz-src"].GetAbsUrl(urlPage);
                var urlElement = articleElement.QuerySelectorAll("h1 > a[href]").First();
                var subTitle = articleElement.QuerySelectorAll("span[class=soustitre]").First().TextContent.Substring(2);
                var commentsCountElement = articleElement.QuerySelectorAll("span[class=nb_comments]").FirstOrDefault();

                int.TryParse(commentsCountElement?.TextContent, out int commentsCount);

                articles.Add(new Article
                {
                    Id = int.Parse(articleElement.Attributes["data-acturowid"].Value),
                    PublicationTimeStamp = ConvertToTimeStamp(date, Constants.FORMAT_DATE_ARTICLE),
                    UrlIllustration = image,
                    Url = urlElement.Attributes["href"].GetAbsUrl(urlPage),
                    Title = urlElement.TextContent,
                    SubTitle = subTitle,
                    TotalCommentsCount = commentsCount,
                    HasSubscription = articleElement.QuerySelectorAll("img[alt=badge_abonne]").Count() > 0,
                });
            }

            return articles;
        }


        public static async Task<string> ParseArticleContentAsync(string s, string urlPage)
        {
            return await Task.Run(() => ParseArticleContent(s, urlPage));
        }

        public static string ParseArticleContent(string html, string urlPage)
        {
            var htmlDocument = new AngleSharp.Parser.Html.HtmlParser().Parse(html);
            var articleElement = htmlDocument.QuerySelectorAll("article");            

            htmlDocument.QuerySelectorAll("article > section").FirstOrDefault()?.Remove();
            htmlDocument.QuerySelectorAll("article > div[class=thumb-cat-container]").FirstOrDefault()?.Remove();
            htmlDocument.QuerySelectorAll("div[class=read-time]").FirstOrDefault()?.Remove();
            htmlDocument.QuerySelectorAll("div[class=infos-article] > div > img").FirstOrDefault()?.Remove();

            foreach (IElement element in articleElement.QuerySelectorAll("a[href] > img").Select(x=> (IElement)x.Parent))
            {
                foreach (IElement childElement in element.Children)
                {
                    element.After(childElement);
                }

                element.Remove();
            }

            var lesIframes = articleElement.QuerySelectorAll("iframe");

            string[] schemes = { "https://", "http://", "//" };

            //foreach (IElement uneIframe in lesIframes)
            //{
            //    //String urlLecteur = uneIframe.attr("src");
            //    String urlLecteur = uneIframe.Attributes["src"].Value;

            //    foreach (String unScheme in schemes)
            //    {
            //        if (urlLecteur.StartsWith(unScheme))
            //        {
            //            urlLecteur = urlLecteur.Substring(unScheme.Length);
            //        }
            //    }

            //    string idVideo = urlLecteur
            //        .Substring(urlLecteur.LastIndexOf("/") + 1)
            //        .Split("\\?")[0]
            //        .Split('#')[0];

            //    //Element monRemplacement = new Element(Tag.valueOf("div"), "");
            //    Element monRemplacement = new Element(Tag.valueOf("div"), "");

            //    if (urlLecteur.StartsWith("www.youtube.com/embed/videoseries"))
            //    {
            //        idVideo = urlLecteur
            //            .Substring(urlLecteur.LastIndexOf("list=") + "list=".Length)
            //            .Split("\\?")[0]
            //            .Split('#')[0];

            //        monRemplacement.html("<a href=\"http://www.youtube.com/playlist?list=" + idVideo + "\"><img src=\"" +
            //                             Constantes.SCHEME_IFRAME_DRAWABLE + R.drawable.iframe_liste_youtube + "\" /></a>");
            //    }
            //    else if (urlLecteur.StartsWith("www.youtube.com/embed/")
            //        || urlLecteur.StartsWith("www.youtube-nocookie.com/embed/"))
            //    {
            //        monRemplacement.html("<a href=\"http://www.youtube.com/watch?v=" + idVideo + "\"><img src=\""
            //                             + Constantes.SCHEME_IFRAME_DRAWABLE + R.drawable.iframe_youtube + "\" /></a>");
            //    }
            //    else if (urlLecteur.StartsWith("www.dailymotion.com/embed/video/"))
            //    {

            //        monRemplacement.html("<a href=\"http://www.dailymotion.com/video/" + idVideo + "\"><img src=\""
            //                             + Constantes.SCHEME_IFRAME_DRAWABLE + R.drawable.iframe_dailymotion + "\" /></a>");
            //    }
            //    else if (urlLecteur.StartsWith("player.vimeo.com/video/"))
            //    {

            //        monRemplacement.html(
            //                "<a href=\"http://www.vimeo.com/" + idVideo + "\"><img src=\"" + Constantes.SCHEME_IFRAME_DRAWABLE
            //                + R.drawable.iframe_vimeo + "\" /></a>");
            //    }
            //    else if (urlLecteur.StartsWith("static.videos.gouv.fr/player/video/"))
            //    {

            //        monRemplacement.html("<a href=\"http://static.videos.gouv.fr/player/video/" + idVideo + "\"><img src=\""
            //                             + Constantes.SCHEME_IFRAME_DRAWABLE + R.drawable.iframe_videos_gouv_fr + "\" /></a>");
            //    }
            //    else if (urlLecteur.StartsWith("vid.me"))
            //    {
            //        monRemplacement.html("<a href=\"https://vid.me/" + idVideo + "\"><img src=\"" + Constantes.SCHEME_IFRAME_DRAWABLE
            //                             + R.drawable.iframe_vidme + "\" /></a>");
            //    }
            //    else if (urlLecteur.StartsWith("w.soundcloud.com/player/"))
            //    {
            //        monRemplacement.html("<a href=\"" + urlLecteur + "\"><img src=\"" + Constantes.SCHEME_IFRAME_DRAWABLE
            //                             + R.drawable.iframe_soundcloud + "\" /></a>");
            //    }
            //    else if (urlLecteur.StartsWith("www.scribd.com/embeds/"))
            //    {
            //        monRemplacement.html("<a href=\"" + urlLecteur + "\"><img src=\"" + Constantes.SCHEME_IFRAME_DRAWABLE
            //                             + R.drawable.iframe_scribd + "\" /></a>");
            //    }
            //    else if (urlLecteur.StartsWith("player.canalplus.fr/embed/"))
            //    {
            //        monRemplacement.html("<a href=\"" + urlLecteur + "\"><img " + "src=\"" + Constantes.SCHEME_IFRAME_DRAWABLE
            //                             + R.drawable.iframe_canalplus + "\" /></a>");
            //    }
            //    else if (urlLecteur.StartsWith("www.arte.tv/"))
            //    {
            //        monRemplacement.html("<a href=\"" + urlLecteur + "\"><img " + "src=\"" + Constantes.SCHEME_IFRAME_DRAWABLE
            //                             + R.drawable.iframe_arte + "\" /></a>");
            //    }
            //    else
            //    {
            //        monRemplacement.html(
            //                "<a href=\"" + uneIframe.absUrl("src") + "\"><img " + "src=\"" + Constantes.SCHEME_IFRAME_DRAWABLE
            //                + R.drawable.iframe_non_supportee + "\" /></a>");
            //    }

            //    // Je remplace l'iframe par mon contenu
            //    uneIframe.replaceWith(monRemplacement);

            //}            

            var linkElements = articleElement.QuerySelectorAll("a[href]");


            foreach (IElement linkElement in linkElements)
            {
                linkElement.Attributes["href"].Value = linkElement.Attributes["href"].GetAbsUrl(urlPage);
            }

            var imageElements = articleElement.QuerySelectorAll("img[src]");

            foreach (IElement imageElement in imageElements)
            {
                imageElement.Attributes["src"].Value = imageElement.Attributes["src"].GetAbsUrl(urlPage);
            }

            return htmlDocument.QuerySelector("article").OuterHtml;


        }

        public static async Task<int> GetCommentsCountAsync(string s, string urlPage)
        {
            return await Task.Run(() => GetCommentsCount(s, urlPage));
        }

        public static int GetCommentsCount(string unContenu, string urlPage)
        {
            var htmlDocument = new AngleSharp.Parser.Html.HtmlParser().Parse(unContenu);
            var stringNbComms = htmlDocument.QuerySelectorAll("span[class=actu_separator_comms]").First().TextContent;
            int spacePosition = stringNbComms.IndexOf(" ");
            string value = stringNbComms.JavaSubString(0, spacePosition).Trim();
            int.TryParse(value, out int nbComms);
            return nbComms;
        }

        public static async Task<List<Comment>> ParseCommentsAsync(string s, string urlPage)
        {
            return await Task.Run(() => ParseComments(s, urlPage));
        }

        public static List<Comment> ParseComments(string html, string urlPage)
        {
            List<Comment> comments = new List<Comment>();

            int pageNumber = int.Parse(urlPage.Substring(urlPage.IndexOf("&") + Constants.NEXT_INPACT_URL_COMMENTAIRES_PARAM_NUM_PAGE.Length + 2));

            var htmlDocument = new AngleSharp.Parser.Html.HtmlParser().Parse(html);

            var articleRef = htmlDocument.QuerySelectorAll("aside[data-relnews]").First();
            int articleId = int.Parse(articleRef.Attributes["data-relnews"].Value);
            var commentElements = htmlDocument.QuerySelectorAll("div[class~=actu_comm ],div[class~=actu_comm_author]");
            var internalLinks = commentElements.QuerySelectorAll("a[class=link_reply_to], div[class=quote_bloc]>div[class=qname]>a");


            internalLinks.ForEach(x => x.TagName("div"));

            var quotes = commentElements.QuerySelectorAll("div[class=link_reply_to], div[class=quote_bloc]");

            quotes.ForEach(x => x.TagName("blockquote"));

            var linkElements = commentElements.QuerySelectorAll("a[href]");

            foreach (IElement linkElement in linkElements)
            {
                linkElement.Attributes["href"].Value = linkElement.Attributes["href"].GetAbsUrl(urlPage);
            }

            int previousIdComm = (pageNumber - 1) * Constants.NB_COMMENTAIRES_PAR_PAGE;
            int previousUuidComm = 0;

            foreach (IElement commentElement in commentElements)
            {
                if (!int.TryParse(commentElement.Attributes["data-content-id"]?.Value, out int uuid))
                {
                    uuid = previousUuidComm + 1;
                }

                previousUuidComm = uuid;

                var authorElement = commentElement.QuerySelectorAll("span[class=author_name]");
                var dateElement = commentElement.QuerySelectorAll("span[class=date_comm]");

                long timeStampPublication = 0;

                if (dateElement.Any())
                {
                    String laDate = dateElement.First().TextContent;
                    timeStampPublication = ConvertToTimeStamp(laDate, Constants.FORMAT_DATE_COMMENTAIRE);
                }

                var idElement = commentElement.QuerySelectorAll("span[class=actu_comm_num]");
                int id = 0;

                if (idElement.Any())
                {
                    String lID = idElement.First().TextContent.Substring(1);
                    id = int.Parse(lID);
                    previousIdComm = int.Parse(lID);
                }
                else
                {
                    id = previousIdComm + 1;
                    previousIdComm++;
                }


                var contentElement = commentElement.QuerySelectorAll("div[class=actu_comm_content]");
                String contentString = null;

                if (contentElement.Any())
                {
                    contentString = contentElement.First().OuterHtml;
                }
                else
                {
                    contentElement = commentElement.QuerySelectorAll("div[class~=actu_comm_author]");

                    if (contentElement.Any())
                    {
                        contentString = contentElement.First().OuterHtml;
                    }
                    else
                    {
                        contentString = "--- Erreur ---";
                    }
                }

                comments.Add(new Comment
                {
                    PrimaryKey = articleId + "-" + uuid,
                    Position = id,
                    ArticleId = articleId,                    
                    Author = authorElement.Any() ? authorElement.First().TextContent : "-",
                    TimeStampPublication = timeStampPublication,
                    Content = contentString,
                });
            }

            return comments;
        }

        public static long ConvertToTimeStamp(string stringValue, string format)
        {
            try
            {
                return DateTime.ParseExact(stringValue.Trim().Replace("  ", " "), format, CultureInfo.InvariantCulture).Ticks;
            }
            catch
            {
                return 0;
            }
         
        }
    }




}
