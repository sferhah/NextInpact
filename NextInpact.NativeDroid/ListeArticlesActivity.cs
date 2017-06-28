using Android.App;
using Android.Widget;
using Android.OS;
using NextInpact.Core.ViewModels;
using NextInpact.Core.Models;
using Android.Views;
using GalaSoft.MvvmLight.Helpers;
using NextInpact.Core.Data;

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

            global::Xamarin.Forms.Forms.Init(this, bundle);
            
            ThreadSafeSqlite.Instance.Init(typeof(Article), typeof(Comment));

            SetContentView(Resource.Layout.activity_liste_articles);
            List.Adapter = Vm.Items.GetAdapter(GetTaskAdapter);
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


            //articleVH.imageArticle = (ImageView)maView.findViewById(R.id.imageArticle);
            //articleVH.labelAbonne = (TextView)maView.findViewById(R.id.labelAbonne);
            //articleVH.titreArticle = (TextView)maView.findViewById(R.id.titreArticle);
            //articleVH.heureArticle = (TextView)maView.findViewById(R.id.heureArticle);
            //articleVH.sousTitreArticle = (TextView)maView.findViewById(R.id.sousTitreArticle);
            //articleVH.commentairesArticle = (TextView)maView.findViewById(R.id.commentairesArticle);

            //var imageArticle = convertView.FindViewById<ImageView>(Resource.Id.imageArticle);
            

            var titreArticle = convertView.FindViewById<TextView>(Resource.Id.titreArticle);
            titreArticle.Text = model.Title;

            var sousTitreArticle = convertView.FindViewById<TextView>(Resource.Id.sousTitreArticle);
            sousTitreArticle.Text = model.SubTitle;

            return convertView;
        }
    }
}

