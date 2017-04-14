using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using NextInpact.Parsing;
using System.Net;
using System.Reflection;
using Flurl.Http;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod2()
        {
            string result = Task.Run(async () => await "https://m.nextinpact.com/news/103906-le-recap-bons-plans-moment-semaine-13.htm".GetStringAsync()).Result;
            Console.WriteLine(result);
        }

        static async void DownloadPageAsync()
        {
            string result = await "http://www.something.com".GetStringAsync();
            Console.WriteLine(result);
        }


        [TestMethod]
        public void TestMethod1()
        {
            //String urlString = "https://m.nextinpact.com/news/103906-le-recap-bons-plans-moment-semaine-13.htm";
            String urlString = "https://m.nextinpact.com/comment/?newsId=103800&page=1";
            var httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlString);

            HttpResponseMessage response = Task.Run(async () => await httpClient.SendAsync(request)).Result;
            String text = Task.Run(async () => await response.Content.ReadAsStringAsync()).Result;

            //File.WriteAllText(@"c:/test/3.txt", text);

            //string text = File.ReadAllText(@"c:/test/3.txt");
            //var articles = NextInpact.Parsing.ParseurHTML.GetNbCommentaires(text, urlString);
        }

        [TestMethod]
        public void TestMethod()
        {
            var client = new WebClient();

            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            var content = client.DownloadString("https://www.google.fr");
        }
    }
}
