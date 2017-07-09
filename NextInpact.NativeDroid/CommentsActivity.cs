using Android.App;
using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;
using NextInpact.Core.ViewModels;

namespace NextInpact.NativeDroid
{
    [Activity(Label = "NextInpact.NativeDroid", MainLauncher = false, Icon = "@drawable/logo_nextinpact")]
    public class CommentsActivity : MvxAppCompatActivity<CommentsViewModel>
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_liste_commentaires);            
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (base.ViewModel.Items.Count == 0)
                base.ViewModel.LoadItemsCommand.Execute(null);
        }
    }
}

