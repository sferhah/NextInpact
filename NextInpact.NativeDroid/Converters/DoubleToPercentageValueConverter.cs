using System;
using Android.Views;
using System.Globalization;
using MvvmCross.Platform.Converters;
using Android.Text;

namespace NextInpact.NativeDroid.Converters
{
    public class DoubleToPercentageValueConverter : MvxValueConverter<double, int>
    {
        protected override int Convert(double value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            return (int)(value * 100);
        }
    }
}