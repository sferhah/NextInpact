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
    public class ByteArrayToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            try
            {
                var imageBytes = value as byte[];

                if (imageBytes == null || imageBytes.Length == 0)
                {
                    return null;
                }

                using (InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream())
                {
                    using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
                    {
                        writer.WriteBytes(imageBytes);
                        writer.StoreAsync().GetResults();
                    }

                    var image = new BitmapImage();
                    image.SetSource(ms);
                    return image;
                }
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