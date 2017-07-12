using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace NextInpact.Droid
{
    [Activity(
        Label = "NextInpact.Droid"
        , MainLauncher = false
        , Icon = "@mipmap/icon"
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}
