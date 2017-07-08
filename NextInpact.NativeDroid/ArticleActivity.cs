using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;
using NextInpact.Core.ViewModels;


namespace NextInpact.NativeDroid
{
    [Activity(Label = "NextInpact.NativeDroid", MainLauncher = false, Icon = "@drawable/logo_nextinpact")]
    public class ArticleActivity : MvxActivity<ArticleDetailViewModel>
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_article);          
        }        
       
    }
}

