using System;
using System.Globalization;
using MvvmCross.Platform.Converters;
using Xamarin.Forms;
using System.IO;


namespace NextInpact.Forms.Converters
{
    public class ByteArrayToImageValueConverter : MvxValueConverter<byte[], ImageSource>
    {
        protected override ImageSource Convert(byte[] value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            if (value == null)
            {   
                return ImageSource.FromFile("default_miniature.png");
            }
            
            return ImageSource.FromStream(() => new MemoryStream(value));
        }
    }
}