using ModernHttpClient;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NextInpact.Core.Networking
{
    public class Downloader
    {
        static HttpClient httpClient = new HttpClient(new NativeMessageHandler());
       
        public static async Task<String> GetAsync(String uneURL)
        { 
            HttpResponseMessage response = await httpClient.GetAsync(ConvertToUri(uneURL));
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<byte[]> GetAsBytesAsync(String uneURL)
        {
            HttpResponseMessage response = await httpClient.GetAsync(ConvertToUri(uneURL));
            return await response.Content.ReadAsByteArrayAsync();
        }

        public static Uri ConvertToUri(String url)
        {
            int positionSlash = url.LastIndexOf('/');
            int positionParam = url.IndexOf('?');
            String beginningOfUrl = url.JavaSubString(0, positionSlash + 1);
            String page;
            String param = "";

            if (positionParam != -1)
            {
                page = url.JavaSubString(positionSlash + 1, positionParam);
                param = url.Substring(positionParam);
            }
            else
            {
                page = url.Substring(positionSlash + 1);
            }

            return new Uri(beginningOfUrl + WebUtility.UrlEncode(page) + param);
        }
    }

}
