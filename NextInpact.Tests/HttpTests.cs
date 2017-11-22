using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextInpact.Core.Networking;

namespace NextInpact.Tests
{
    [TestClass]
    public class HttpTests
    {

        [TestMethod]
        public  void TestMethod()
        {   
            var result = NextInpactClient.GetArticlesAsync(1).Result;
            var article = result.First();
        }

    }
}
