using Android.App;
using Android.OS;
using Android.Webkit;
using MvvmCross.Droid.Views;
using NextInpact.Core.ViewModels;
using static Android.Webkit.WebSettings;

namespace NextInpact.NativeDroid
{
    [Activity(Label = "NextInpact.NativeDroid", MainLauncher = false, Icon = "@drawable/logo_nextinpact")]
    public class ArticleActivity : MvxActivity<ArticleDetailViewModel>
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_article);

            var webview = FindViewById<WebView>(Resource.Id.webview);
            webview.Settings.SetLayoutAlgorithm(LayoutAlgorithm.SingleColumn);
            webview.LoadDataWithBaseURL(null, base.ViewModel.Item.Content, "text/html", "utf-8", null);

        }

    }
}

