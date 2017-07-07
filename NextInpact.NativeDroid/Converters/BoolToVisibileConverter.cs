using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace NextInpact.NativeDroid.Converters
{
    public static class BoolToVisibileConverter
    {
        public static bool ConvertBack(ViewStates viewState)
        {
            return viewState == ViewStates.Visible;
        }

        public static ViewStates Convert(bool visible)
        {
            return visible ? ViewStates.Visible : ViewStates.Gone;
        }
    }
}