using Android.App;
using Android.Widget;
using Android.OS;
using NextInpact.Core.ViewModels;
using NextInpact.Core.Models;
using Android.Views;
using NextInpact.Core.Data;
using Android.Graphics;
using Android.Util;
using System.Collections.Generic;
using Android.Support.V4.Widget;
using Android.Content;
using Android.Support.V7.App;
using static Android.Support.V7.Widget.ActionMenuView;
using Android.Support.V7.Widget;
using NextInpact.NativeDroid.Converters;
using MvvmCross.Droid.Views;
using MvvmCross.Droid.Support.V4;

namespace NextInpact.NativeDroid
{
    [Activity(Label = "NextInpact.NativeDroid", MainLauncher = true, Icon = "@drawable/logo_nextinpact")]
    public class ListeArticlesActivity : MvxActivity
    {
        ArticlesViewModel vm;
        private ArticlesViewModel Vm
        {
            get
            {
                return vm ?? (vm = new ArticlesViewModel());
            }
        }     

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            base.ViewModel = Vm;

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
                Vm.LoadItemsCommand.Execute(null);
                return true;
            }

            return false;
        }
 

        private void List_ItemClick(int x, View y, int z, View w)
        {
            var article = Vm.Items[z];

            Toast.MakeText(this, article.Title, ToastLength.Long).Show();

//            Intent intent = new Intent(this, null);

        }

        protected override void OnResume()
        {
            base.OnResume();

            if (Vm.Items.Count == 0)
                Vm.LoadItemsCommand.Execute(null);

        }

    }
}

