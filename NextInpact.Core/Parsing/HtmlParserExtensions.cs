using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NextInpact.Core.Parsing
{
    public static class HtmlParserExtensions
    {
        public static String GetAbsUrl(this IAttr attr, string origin)
        {
            try
            {
                return new Uri(new Uri(origin), attr.Value).ToString();
            }
            catch
            {
                return attr.Value;
            }
            
        }

        public static void TagName(this IElement el, string value)
        {
            el.OuterHtml = $"<{value}>{el.InnerHtml}</{value}>";
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static IEnumerable<IElement> QuerySelectorAll(this IHtmlCollection<IElement> source, String selector)
        {
            return source.SelectMany(x => x.QuerySelectorAll(selector));
        }

    }
}
