using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NextInpact.Native.UWP
{
    public class WebViewProperties
    {   
        public static readonly DependencyProperty RawHtmlProperty
            = DependencyProperty.RegisterAttached("RawHtml", typeof(string), typeof(WebViewProperties), new PropertyMetadata(0, new PropertyChangedCallback(OnRawHtmlChanged)));

        public static string GetRawHtml(DependencyObject obj)
        {
            return (string)obj.GetValue(RawHtmlProperty);
        }

        public static void SetRawHtml(DependencyObject obj, string value)
        {
            obj.SetValue(RawHtmlProperty, value);
        }

        private static void OnRawHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WebView wv)
            {
                wv.NavigateToString((string)e.NewValue);
            }
        }
    }
}
