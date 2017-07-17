using Android.App;
using Android.OS;
using NextInpact.Core.ViewModels;
using Android.Views;
using V7 = MvvmCross.Droid.Support.V7.AppCompat;


namespace NextInpact.Native.Droid.Activities
{
    [Activity(Label = "NextInpact.Native.Droid", MainLauncher = false, Icon = "@drawable/logo_nextinpact")]
    public class ArticleListActivity : V7.MvxAppCompatActivity<ArticlesViewModel>
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.activity_liste_articles);

        }

        private IMenuItem refreshCommand;
        public IMenuItem RefreshCommand
        {
            get
            {
                return refreshCommand ?? (refreshCommand = this.myMenu.FindItem(Resource.Id.action_refresh));
            }
        }


        private IMenu myMenu;
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            myMenu = menu;
            base.OnCreateOptionsMenu(myMenu);
            MenuInflater.Inflate(Resource.Menu.activity_liste_articles_actions, myMenu);
            return true;
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item == RefreshCommand)
            {
                base.ViewModel.LoadItemsCommand.Execute(null);
                return true;
            }

            return false;
        }
    }
}

