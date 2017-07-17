using Android.App;
using Android.OS;
using V7 = MvvmCross.Droid.Support.V7.AppCompat;
using NextInpact.Core.ViewModels;

namespace NextInpact.Native.Droid.Activities
{
    [Activity(Label = "NextInpact.Native.Droid", MainLauncher = false, Icon = "@drawable/logo_nextinpact")]
    public class CommentsActivity : V7.MvxAppCompatActivity<CommentsViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_liste_commentaires);
        }
    }
}

