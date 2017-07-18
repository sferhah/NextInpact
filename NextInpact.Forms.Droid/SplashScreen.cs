using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Droid.Views;
using Xamarin.Forms;

namespace NextInpact.Forms.Droid
{
    [Activity(
        Label = "NextInpact.Droid"
        , MainLauncher = true
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

        private bool isInitializationComplete = false;
        public override void InitializationComplete()
        {
            if (!isInitializationComplete)
            {
                isInitializationComplete = true;
                StartActivity(typeof(MainActivity));
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            Xamarin.Forms.Forms.Init(this, bundle);

            Xamarin.Forms.Forms.ViewInitialized += (object sender, ViewInitializedEventArgs e) =>
            {
                if (!string.IsNullOrWhiteSpace(e.View.StyleId))
                {
                    e.NativeView.ContentDescription = e.View.StyleId;
                }
            };

            base.OnCreate(bundle);
        }
    }
}
