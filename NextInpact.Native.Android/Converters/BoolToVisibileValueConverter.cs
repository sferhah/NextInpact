using System;
using Android.Views;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace NextInpact.Native.Droid.Converters
{
    public class BoolToVisibileValueConverter : MvxValueConverter<bool, ViewStates>
    {
        protected override ViewStates Convert(bool value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            return value ? ViewStates.Visible : ViewStates.Gone;
        }
    }
}