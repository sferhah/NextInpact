using Android.App;
using Android.OS;
using NextInpact.Core.ViewModels;


namespace NextInpact.NativeDroid
{
    [Activity(Label = "NextInpact.NativeDroid", MainLauncher = false, Icon = "@drawable/logo_nextinpact")]
    public class ArticleActivity : Activity
    {

      

        ArticleDetailViewModel vm;
        private ArticleDetailViewModel Vm
        {
            get
            {
                return vm ?? (vm = new ArticleDetailViewModel());
            }
        }
      

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_article);          
        }        
       
    }
}

