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

        public static Uri ConvertToUri(String uneURL)
        {
            int positionSlash = uneURL.LastIndexOf('/');
            int positionParam = uneURL.IndexOf('?');
            String debutURL = uneURL.JavaSubString(0, positionSlash + 1);
            String page;
            String param = "";

            if (positionParam != -1)
            {
                page = uneURL.JavaSubString(positionSlash + 1, positionParam);
                param = uneURL.Substring(positionParam);
            }
            else
            {
                page = uneURL.Substring(positionSlash + 1);
            }

            return new Uri(debutURL + WebUtility.UrlEncode(page) + param);
        }
    }

}
