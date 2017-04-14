using Android.App;
using Android.Content.PM;
using Android.OS;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static Android.Resource;

namespace NextInpact.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            ServicePointManager
          .ServerCertificateValidationCallback +=
          (sender, cert, chain, sslPolicyErrors) => true;

            

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            
            LoadApplication(new App());

            Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0));
        }


    }
}