using System;
using System.Globalization;
using MvvmCross.Platform.Converters;
using Xamarin.Forms;
using System.IO;


namespace NextInpact.Forms.Converters
{
    public class StringToHtmlValueConverter : MvxValueConverter<String, HtmlWebViewSource>
    {
        protected override HtmlWebViewSource Convert(String value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            return new HtmlWebViewSource()
            {
                Html = value ?? "<html></html>",
            };
        }
    }
}