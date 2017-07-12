using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Core.Views;
using MvvmCross.Forms.Droid;
using MvvmCross.Forms.Droid.Presenters;
using MvvmCross.Platform;
using NextInpact.Core.Data;
using NextInpact.Core.IO;
using NextInpact.Forms;
using NextInpact.PlatformSpecific.Droid;

namespace NextInpact.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : MvxFormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            Mvx.RegisterSingleton<IStringConnectionProvider>(new AndroidConnectionProvider());
            Mvx.RegisterSingleton<ISaveAndLoad>(new AndroidSaveAndLoad());

            var app = new App();

            LoadApplication(app);
        

            Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0));

            var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsDroidPagePresenter;
            presenter.MvxFormsApp = app;


        }


    }
}