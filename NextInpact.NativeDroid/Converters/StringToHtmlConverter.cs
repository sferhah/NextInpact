using System;
using Android.Views;
using System.Globalization;
using MvvmCross.Platform.Converters;
using Android.Text;

namespace NextInpact.NativeDroid.Converters
{
    public class StringToHtmlConverter : MvxValueConverter<String, ISpanned>
    {
        protected override ISpanned Convert(String strHtml, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.Build.VERSION_CODES.N)
                return Html.FromHtml(strHtml, Html.FromHtmlModeLegacy);

            return Html.FromHtml(strHtml);
        }
    }
}