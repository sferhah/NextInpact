using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Forms.Core;
using MvvmCross.Forms.Droid;
using MvvmCross.Forms.Droid.Presenters;
using MvvmCross.Platform;
using NextInpact.Core.Data;
using NextInpact.Core.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Android.Resource;

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

            Forms.Init(this, bundle);

            var app = new App();

            LoadApplication(app);
        

            Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0));

            var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsDroidPagePresenter;
            presenter.MvxFormsApp = app;


            //base.OnCreate(bundle);

            //Forms.Init(this, bundle);

            //ThreadSafeSqlite.Instance.Init(typeof(Article), typeof(Comment));

            ////var mvxFormsApp = new MvxFormsApplication();
            ////LoadApplication(mvxFormsApp);

            //var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsDroidPagePresenter;
            //presenter.MvxFormsApp = mvxFormsApp;

            //IMvxAppStart resolved = Mvx.Resolve<IMvxAppStart>();
            //resolved.Start();

        }


    }
}