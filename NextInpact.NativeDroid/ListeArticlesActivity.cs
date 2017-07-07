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
using Android.Support.V7.App;
using static Android.Support.V7.Widget.ActionMenuView;
using Android.Support.V7.Widget;
using NextInpact.NativeDroid.Converters;

namespace NextInpact.NativeDroid
{
    [Activity(Label = "NextInpact.NativeDroid", MainLauncher = true, Icon = "@drawable/logo_nextinpact")]
    public class ListeArticlesActivity : AppCompatActivity
    {

        //private ListView list;
        //public ListView List
        //{
        //    get
        //    {
        //        return list ?? (list = FindViewById<ListView>(Resource.Id.listeArticles));
        //    }
        //}

        private RecyclerView list;
        public RecyclerView List
        {
            get
            {
                return list ?? (list = FindViewById<RecyclerView>(Resource.Id.listeArticles));
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

            Xamarin.Forms.Forms.Init(this, bundle);

            ThreadSafeSqlite.Instance.Init(typeof(Article), typeof(Comment));

            SetContentView(Resource.Layout.activity_liste_articles);

            bindings.Add(this.SetBinding(() => Vm.LastRefreshDate, () => LastRefreshDate.Text, BindingMode.OneWay));

            SwipeRefresh.Refresh += (sender, e) => { Vm.LoadItemsCommand.Execute(null); };

            bindings.Add(this.SetBinding(() => Vm.IsBusy, () => SwipeRefresh.Refreshing, BindingMode.OneWay));

            //List.Adapter = Vm.Items.GetAdapter(GetTaskAdapter);
            //List.ItemClick += List_ItemClick;

            UpdateRecyclerAdapter(this, List);

            List.SetAdapter(Vm.Items.GetRecyclerAdapter(BindViewHolder, Resource.Layout.liste_articles_item_article));

        }

        private static void UpdateRecyclerAdapter(Context activity, RecyclerView view)
        {   
            int columns = 1;

            RecyclerView.LayoutManager manager = view.GetLayoutManager();

            if ((manager != null)
                && (manager is GridLayoutManager))
            {
                if (((GridLayoutManager)manager).SpanCount != columns)
                {
                    ((GridLayoutManager)manager).SpanCount = columns;
                }
            }
            else
            {
                view.SetLayoutManager(new GridLayoutManager(activity, columns));
            }
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


        private void BindViewHolder(CachingViewHolder holder, Article model, int position)
        {


            var section = holder.FindCachedViewById<TextView>(Resource.Id.titreSection);

            holder.DeleteBinding("1");

            holder.SaveBinding("1", new Binding<string, string>(model,
                                                 () => model.PublicationDate,
                                                 section,
                                                 () => section.Text,
                                                 BindingMode.OneWay));

            holder.DeleteBinding("1.1");
            holder.SaveBinding("1.1", new Binding<bool, ViewStates>(model,
                                                 () => model.ShowDateSection,
                                                 section,
                                                 () => section.Visibility,
                                                 BindingMode.OneWay).ConvertSourceToTarget(BoolToVisibileConverter.Convert));



            var title = holder.FindCachedViewById<TextView>(Resource.Id.titreArticle);
            holder.DeleteBinding("2");


            holder.SaveBinding("2", new Binding<string, string>(model,
                                           () => model.Title,
                                           title,
                                           () => title.Text,
                                           BindingMode.OneWay));

            var subtitle = holder.FindCachedViewById<TextView>(Resource.Id.sousTitreArticle);

            holder.DeleteBinding("3");
            holder.SaveBinding("3", new Binding<string, string>(model,
                                        () => model.SubTitle,
                                        subtitle,
                                        () => subtitle.Text,
                                        BindingMode.OneWay));



            var publicationTime = holder.FindCachedViewById<TextView>(Resource.Id.heureArticle);

            holder.DeleteBinding("4");
            holder.SaveBinding("4", new Binding<string, string>(model,
                                        () => model.PublicationTime,
                                        publicationTime,
                                        () => publicationTime.Text,
                                        BindingMode.OneWay));


            var commentsCount = holder.FindCachedViewById<TextView>(Resource.Id.commentairesArticle);

            holder.DeleteBinding("5");
            holder.SaveBinding("5", new Binding<int, string>(model,
                                        () => model.TotalCommentsCount,
                                        commentsCount,
                                        () => commentsCount.Text,
                                        BindingMode.OneWay));

            var illustration = holder.FindCachedViewById<CustomImageView>(Resource.Id.imageArticle);
            holder.DeleteBinding("6");

            holder.SaveBinding("6", new Binding<byte[], byte[]>(model,
                                     () => model.ImageData,
                                     illustration,
                                     () => illustration.ImageData,
                                     BindingMode.OneWay));            

            holder.SaveBinding("6", new Binding<byte[], byte[]>(model,
                                  () => model.ImageData,
                                  illustration,
                                  () => illustration.ImageData,
                                  BindingMode.OneWay));

        }

    }
}

