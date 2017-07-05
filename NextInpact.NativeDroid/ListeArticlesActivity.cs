using Android.App;
using Android.Widget;
using Android.OS;
using NextInpact.Core.ViewModels;
using NextInpact.Core.Models;
using Android.Views;
using GalaSoft.MvvmLight.Helpers;
using NextInpact.Core.Data;
using Android.Graphics;
using Android.Util;
using System.Collections.Generic;
using Android.Support.V4.Widget;
using Android.Content;

namespace NextInpact.NativeDroid
{
    [Activity(Label = "NextInpact.NativeDroid", MainLauncher = true, Icon = "@drawable/logo_nextinpact")]
    public class ListeArticlesActivity : Activity
    {

        private ListView list;
        public ListView List
        {
            get
            {
                return list ?? (list = FindViewById<ListView>(Resource.Id.listeArticles));
            }
        }

        private SwipeRefreshLayout _SwipeRefresh;
        public SwipeRefreshLayout SwipeRefresh
        {
            get
            {
                return _SwipeRefresh ?? (_SwipeRefresh = FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_container));
            }
        }


        private TextView _LastRefreshDate;
        public TextView LastRefreshDate
        {
            get
            {
                return _LastRefreshDate ?? (_LastRefreshDate = FindViewById<TextView>(Resource.Id.header_text));
            }
        }

        ArticlesViewModel vm;
        private ArticlesViewModel Vm
        {
            get
            {
                return vm ?? (vm = new ArticlesViewModel());
            }
        }




        private readonly List<Binding> bindings = new List<Binding>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            ThreadSafeSqlite.Instance.Init(typeof(Article), typeof(Comment));

            SetContentView(Resource.Layout.activity_liste_articles);

            bindings.Add(this.SetBinding(() => Vm.LastRefreshDate, () => LastRefreshDate.Text, BindingMode.OneWay));

            SwipeRefresh.Refresh += (sender, e) => { Vm.LoadItemsCommand.Execute(null); };

            bindings.Add(this.SetBinding(() => Vm.IsBusy, () => SwipeRefresh.Refreshing, BindingMode.OneWay));

            List.Adapter = Vm.Items.GetAdapter(GetTaskAdapter);


            List.ItemClick += List_ItemClick;
        }

        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var article = Vm.Items[e.Position];

            Toast.MakeText(this, article.Title, ToastLength.Long).Show();

//            Intent intent = new Intent(this, null);

        }

        protected override void OnResume()
        {
            base.OnResume();

            if (Vm.Items.Count == 0)
                Vm.LoadItemsCommand.Execute(null);

        }

        private View GetTaskAdapter(int position, Article model, View convertView)
        {
            // Not reusing views here
            convertView = LayoutInflater.Inflate(Resource.Layout.liste_articles_item_article, null);            
            
          


            var SectionArticle = convertView.FindViewById<TextView>(Resource.Id.titreSection);
            SectionArticle.Text = model.PublicationDate;
            SectionArticle.Visibility = model.ShowDateSection? ViewStates.Visible :  ViewStates.Gone;            

            var imgViewer = convertView.FindViewById<ImageView>(Resource.Id.imageArticle);

            
            if(model.ImageData != null)
            {
                Bitmap bm = BitmapFactory.DecodeByteArray(model.ImageData, 0, model.ImageData.Length);
                DisplayMetrics dm = new DisplayMetrics();
                WindowManager.DefaultDisplay.GetMetrics(dm);

                imgViewer.SetMinimumHeight(dm.HeightPixels);
                imgViewer.SetMinimumWidth(dm.WidthPixels);
                imgViewer.SetImageBitmap(bm);
            }

            var titreArticle = convertView.FindViewById<TextView>(Resource.Id.titreArticle);
            titreArticle.Text = model.Title;

            var sousTitreArticle = convertView.FindViewById<TextView>(Resource.Id.sousTitreArticle);
            sousTitreArticle.Text = model.SubTitle;

            var heureArticle = convertView.FindViewById<TextView>(Resource.Id.heureArticle);
            heureArticle.Text = model.PublicationTime;

            var commentairesArticle = convertView.FindViewById<TextView>(Resource.Id.commentairesArticle);
            commentairesArticle.Text = model.TotalCommentsCount.ToString();

            return convertView;
        }
    }
}

