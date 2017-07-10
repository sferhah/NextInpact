using Android.App;
using Android.OS;
using V7 = MvvmCross.Droid.Support.V7.AppCompat;
using NextInpact.Core.ViewModels;
using NextInpact.NativeDroid;

namespace NextInpact.Activities.NativeDroid
{
    [Activity(Label = "NextInpact.NativeDroid", MainLauncher = false, Icon = "@drawable/logo_nextinpact")]
    public class CommentsActivity : V7.MvxAppCompatActivity<CommentsViewModel>
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

