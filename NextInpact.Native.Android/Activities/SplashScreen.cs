using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace NextInpact.Native.Droid.Activities
{
    [Activity(
        Label = "NextInpact.Native.Droid"
        , MainLauncher = true
        , Icon = "@drawable/logo_nextinpact"
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