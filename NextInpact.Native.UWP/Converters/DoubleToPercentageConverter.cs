using System;
using System.Globalization;
using MvvmCross.Platform.Converters;
using System.IO;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Data;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace NextInpact.Native.UWP.Converters
{
    public class DoubleToPercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            try
            {
                return (int)(((double)value) * 100);
            }
            catch
            {
                return null;
            }

        }

        public object ConvertBack(object value, System.Type type, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}