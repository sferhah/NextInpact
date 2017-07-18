using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Core.Views;
using MvvmCross.Forms.Droid;
using MvvmCross.Forms.Droid.Presenters;
using MvvmCross.Platform;

namespace NextInpact.Forms.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : MvxFormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);          

            var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsDroidPagePresenter;
            presenter.MvxFormsApp = new App();
            LoadApplication(presenter.MvxFormsApp);            

            //Needs to be set after 'LoadApplication'
            Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0));
        }

    }
}