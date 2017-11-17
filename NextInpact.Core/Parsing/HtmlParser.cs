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

        public static List<Article> ParseArticleList(string unContenu, string urlPage)
        {
            List<Article> mesArticlesItem = new List<Article>();

            //Won't find some encodings : just press F5 several times...
            var pageNXI = new AngleSharp.Parser.Html.HtmlParser().Parse(unContenu);

            var lesArticles = pageNXI.QuerySelectorAll("article[data-acturowid][data-datepubli]");

            foreach (var unArticle in lesArticles)
            {
                String laDate = unArticle.Attributes["data-datepubli"].Value;
                var image = unArticle.QuerySelectorAll("img[class=ded-image]").First();
                var url = unArticle.QuerySelectorAll("h1 > a[href]").First();
                var sousTitre = unArticle.QuerySelectorAll("span[class=soustitre]").First();
                String monSousTitre = sousTitre.TextContent.Substring(2);
                var commentaires = unArticle.QuerySelectorAll("span[class=nb_comments]").FirstOrDefault();

                int.TryParse(commentaires?.TextContent, out int nbCommentaires);

                var monArticleItem = new Article
                {
                    Id = int.Parse(unArticle.Attributes["data-acturowid"].Value),
                    PublicationTimeStamp = ConvertToTimeStamp(laDate, Constantes.FORMAT_DATE_ARTICLE),
                    UrlIllustration = image.Attributes["data-frz-src"].GetAbsUrl(urlPage),
                    Url = url.Attributes["href"].GetAbsUrl(urlPage),
                    Title = url.TextContent,
                    SubTitle = monSousTitre,
                    TotalCommentsCount = nbCommentaires,
                    HasSubscription = unArticle.QuerySelectorAll("img[alt=badge_abonne]").Count() > 0,
                };

                mesArticlesItem.Add(monArticleItem);
            }

            return mesArticlesItem;
        }


        public static async Task<string> ParseArticleContentAsync(string s, string urlPage)
        {
            return await Task.Run(() => ParseArticleContent(s, urlPage));
        }

        public static string ParseArticleContent(string unContenu, string urlPage)
        {
            var pageNXI = new AngleSharp.Parser.Html.HtmlParser().Parse(unContenu);
            var lArticle = pageNXI.QuerySelectorAll("article");            

            pageNXI.QuerySelectorAll("article > section").FirstOrDefault()?.Remove();
            pageNXI.QuerySelectorAll("article > div[class=thumb-cat-container]").FirstOrDefault()?.Remove();
            pageNXI.QuerySelectorAll("div[class=read-time]").FirstOrDefault()?.Remove();
            pageNXI.QuerySelectorAll("div[class=infos-article] > div > img").FirstOrDefault()?.Remove();

            var lesImagesLiens = lArticle.QuerySelectorAll("a[href] > img");

            HashSet<IElement> baliseA = new HashSet<IElement>();

            foreach (IElement uneImage in lesImagesLiens)
            {
                baliseA.Add((IElement)uneImage.Parent);
            }

            foreach (IElement uneBalise in baliseA)
            {
                foreach (IElement unEnfant in uneBalise.Children)
                {
                    uneBalise.After(unEnfant);
                }
                uneBalise.Remove();
            }

            var lesIframes = lArticle.QuerySelectorAll("iframe");

            String[] schemes = { "https://", "http://", "//" };

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

            //    String idVideo = urlLecteur
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

            var lesLiens = lArticle.QuerySelectorAll("a[href]");


            foreach (IElement unLien in lesLiens)
            {
                unLien.Attributes["href"].Value = unLien.Attributes["href"].GetAbsUrl(urlPage);
            }

            var lesImages = lArticle.QuerySelectorAll("img[src]");

            foreach (IElement uneImage in lesImages)
            {
                uneImage.Attributes["src"].Value = uneImage.Attributes["src"].GetAbsUrl(urlPage);
            }

            return pageNXI.QuerySelector("article").OuterHtml;


        }

        public static async Task<int> GetNbCommentairesAsync(string s, string urlPage)
        {
            return await Task.Run(() => GetNbCommentaires(s, urlPage));
        }

        public static int GetNbCommentaires(string unContenu, string urlPage)
        {
            var pageNXI = new AngleSharp.Parser.Html.HtmlParser().Parse(unContenu);
            var elementNbComms = pageNXI.QuerySelectorAll("span[class=actu_separator_comms]").First();
            String stringNbComms = elementNbComms.TextContent;
            int positionEspace = stringNbComms.IndexOf(" ");
            String valeur = stringNbComms.JavaSubString(0, positionEspace).Trim();
            int nbComms;
            int.TryParse(valeur, out nbComms);
            return nbComms;
        }

        public static async Task<List<Comment>> ParseCommentsAsync(string s, string urlPage)
        {
            return await Task.Run(() => ParseComments(s, urlPage));
        }

        public static List<Comment> ParseComments(string unContenu, string urlPage)
        {
            List<Comment> mesCommentairesItem = new List<Comment>();

            int numeroPage = int.Parse(
                    urlPage.Substring(urlPage.IndexOf("&") + Constantes.NEXT_INPACT_URL_COMMENTAIRES_PARAM_NUM_PAGE.Length + 2));

            var pageNXI = new AngleSharp.Parser.Html.HtmlParser().Parse(unContenu);

            var refArticle = pageNXI.QuerySelectorAll("aside[data-relnews]").First();
            int idArticle = int.Parse(refArticle.Attributes["data-relnews"].Value);
            var lesCommentaires = pageNXI.QuerySelectorAll("div[class~=actu_comm ],div[class~=actu_comm_author]");
            var lesLiensInternes = lesCommentaires.QuerySelectorAll("a[class=link_reply_to], div[class=quote_bloc]>div[class=qname]>a");


            lesLiensInternes.ForEach(x => x.TagName("div"));

            var lesCitations = lesCommentaires.QuerySelectorAll("div[class=link_reply_to], div[class=quote_bloc]");

            lesCitations.ForEach(x => x.TagName("blockquote"));

            var lesLiens = lesCommentaires.QuerySelectorAll("a[href]");

            foreach (IElement unLien in lesLiens)
            {
                unLien.Attributes["href"].Value = unLien.Attributes["href"].GetAbsUrl(urlPage);
            }

            int idCommPrecedent = (numeroPage - 1) * Constantes.NB_COMMENTAIRES_PAR_PAGE;
            int uuidCommPrecedent = 0;

            foreach (IElement unCommentaire in lesCommentaires)
            {
                int monUUID;

                if (!int.TryParse(unCommentaire.Attributes["data-content-id"]?.Value, out monUUID))
                {
                    monUUID = uuidCommPrecedent + 1;
                }

                uuidCommPrecedent = monUUID;

                var monAuteur = unCommentaire.QuerySelectorAll("span[class=author_name]");
                var maDate = unCommentaire.QuerySelectorAll("span[class=date_comm]");

                long timeStampPublication = 0;

                if (maDate.Any())
                {
                    String laDate = maDate.First().TextContent;
                    timeStampPublication = ConvertToTimeStamp(laDate, Constantes.FORMAT_DATE_COMMENTAIRE);
                }

                var monID = unCommentaire.QuerySelectorAll("span[class=actu_comm_num]");
                int id = 0;

                if (monID.Any())
                {
                    String lID = monID.First().TextContent.Substring(1);
                    id = int.Parse(lID);
                    idCommPrecedent = int.Parse(lID);
                }
                else
                {
                    id = idCommPrecedent + 1;
                    idCommPrecedent++;
                }


                var monContenu = unCommentaire.QuerySelectorAll("div[class=actu_comm_content]");
                String commentaire = null;

                if (monContenu.Any())
                {
                    commentaire = monContenu.First().OuterHtml;
                }
                else
                {
                    monContenu = unCommentaire.QuerySelectorAll("div[class~=actu_comm_author]");

                    if (monContenu.Any())
                    {
                        commentaire = monContenu.First().OuterHtml;
                    }
                    else
                    {
                        commentaire = "--- Erreur ---";
                    }
                }


                Comment monCommentaireItem = new Comment
                {
                    Id = id,
                    ArticleId = idArticle,
                    Uuid = monUUID,
                    Author = monAuteur.Any() ? monAuteur.First().TextContent : "-",
                    TimeStampPublication = timeStampPublication,
                    Content = commentaire,
                };


                mesCommentairesItem.Add(monCommentaireItem);
            }

            return mesCommentairesItem;
        }

        public static long ConvertToTimeStamp(string uneDate, string unFormatDate)
        {
            long laDateTS = 0;

            try
            {
                laDateTS = DateTime.ParseExact(uneDate.Trim().Replace("  ", " "), unFormatDate, CultureInfo.InvariantCulture).Ticks;
            }
            catch
            {
                // Pokémon
            }

            return laDateTS;
        }
    }




}
