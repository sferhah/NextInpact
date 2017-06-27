using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using NextInpact.Core.Parsing;
using System.Net;
using System.Reflection;
using NextInpact.Core.Networking;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public  void TestMethod()
        {   
            var result = Task.Run(async () => await NextInpactClient.GetArticlesAsync(1)).Result;

            var article = result.First();
            Task.Run(async () => await NextInpactClient.DownloadArticleContent(article)).Wait();

        }

    }
}
