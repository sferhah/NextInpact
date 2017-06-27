using NextInpact.Core.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NextInpact.Core.Networking
{
    public class Downloader
    {
        public static async Task<String> GetAsync(String uneURL)
        {
            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(ConvertToUri(uneURL));
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<byte[]> GetAsBytesAsync(String uneURL)
        {
            var httpClient = new HttpClient();
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

            Uri monURL = new Uri(debutURL + WebUtility.UrlEncode(page) + param);
            return monURL;

        }
    }

}
