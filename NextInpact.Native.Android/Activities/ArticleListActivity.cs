using Android.App;
using Android.Widget;
using Android.OS;
using NextInpact.Core.ViewModels;
using NextInpact.Core.Models;
using Android.Views;
using NextInpact.Core.Data;
using V7 = MvvmCross.Droid.Support.V7.AppCompat;
using NextInpact.NativeDroid;


namespace NextInpact.Activities.NativeDroid
{
    [Activity(Label = "NextInpact.NativeDroid", MainLauncher = false, Icon = "@drawable/logo_nextinpact")]
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

