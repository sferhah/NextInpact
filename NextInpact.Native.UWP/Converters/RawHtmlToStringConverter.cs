using System;
using System.Globalization;
using MvvmCross.Platform.Converters;
using System.IO;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Data;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using System.Text.RegularExpressions;

namespace NextInpact.Native.UWP.Converters
{
    public class RawHtmlToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {  
            var html = value as String;

            if (html == null)
            {
                return null;
            }

            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;

        }
        

        public object ConvertBack(object value, System.Type type, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}