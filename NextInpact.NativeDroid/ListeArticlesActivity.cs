using Android.App;
using Android.Widget;
using Android.OS;
using NextInpact.Core.ViewModels;
using NextInpact.Core.Models;
using Android.Views;
using NextInpact.Core.Data;
using MvvmCross.Droid.Views;


namespace NextInpact.NativeDroid
{
    [Activity(Label = "NextInpact.NativeDroid", MainLauncher = true, Icon = "@drawable/logo_nextinpact")]
    public class ListeArticlesActivity : MvxActivity<ArticlesViewModel>
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

            ThreadSafeSqlite.Instance.Init(typeof(Article), typeof(Comment));

            SetContentView(Resource.Layout.activity_liste_articles);
           
        }

        private IMenuItem refreshCommand;
        public IMenuItem RefreshCommand
        {
            get
            {
                return refreshCommand ?? (refreshCommand = this.monMenu.FindItem(Resource.Id.action_refresh));
            }
        }


        private IMenu monMenu;
        public override bool OnCreateOptionsMenu(IMenu menu)
        {   
            monMenu = menu;
            base.OnCreateOptionsMenu(monMenu);
            MenuInflater.Inflate(Resource.Menu.activity_liste_articles_actions, monMenu);
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
 

        private void List_ItemClick(int x, View y, int z, View w)
        {
            var article = base.ViewModel.Items[z];

            Toast.MakeText(this, article.Title, ToastLength.Long).Show();

//            Intent intent = new Intent(this, null);

        }

        protected override void OnResume()
        {
            base.OnResume();

            if (base.ViewModel.Items.Count == 0)
                base.ViewModel.LoadItemsCommand.Execute(null);

        }

    }
}

