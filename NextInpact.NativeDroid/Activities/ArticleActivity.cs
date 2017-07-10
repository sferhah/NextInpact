using Android.App;
using Android.OS;
using Android.Views;
using Android.Webkit;
using V7 = MvvmCross.Droid.Support.V7.AppCompat;
using NextInpact.Core.ViewModels;
using static Android.Webkit.WebSettings;
using NextInpact.NativeDroid;

namespace NextInpact.Activities.NativeDroid
{
    [Activity(Label = "NextInpact.NativeDroid", MainLauncher = false, Icon = "@drawable/logo_nextinpact")]
    public class ArticleActivity : V7.MvxAppCompatActivity<ArticleDetailViewModel>
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_article);

            var webview = FindViewById<WebView>(Resource.Id.webview);
            webview.Settings.SetLayoutAlgorithm(LayoutAlgorithm.SingleColumn);
            webview.LoadDataWithBaseURL(null, base.ViewModel.Item.Content, "text/html", "utf-8", null);

        }


        private IMenuItem _ShowCommentsButton;
        public IMenuItem ShowCommentsButton
        {
            get
            {
                return _ShowCommentsButton ?? (_ShowCommentsButton = this.myMenu.FindItem(Resource.Id.action_comments));
            }
        }


        private IMenu myMenu;
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            myMenu = menu;
            base.OnCreateOptionsMenu(myMenu);
            MenuInflater.Inflate(Resource.Menu.activity_article_actions, myMenu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item == ShowCommentsButton)
            {
                base.ViewModel.ShowCommentsCommand.Execute(null);
                return true;
            }

            return false;
        }

    }
}

