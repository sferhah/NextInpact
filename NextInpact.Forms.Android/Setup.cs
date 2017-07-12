using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Core.Views;
using MvvmCross.Forms.Droid.Presenters;
using System.Collections.Generic;
using NextInpact.Forms.Views;
using NextInpact.Core.ViewModels;

namespace NextInpact.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new NextInpact.Core.App();
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var presenter = new MvxFormsDroidPagePresenter();
            Mvx.RegisterSingleton<IMvxViewPresenter>(presenter);

            return presenter;
        }


        protected override void InitializeViewLookup()
        {
            var registry = new Dictionary<System.Type, System.Type>()
            {
                { typeof(ArticlesViewModel), typeof(ArticlesPage) },
                { typeof(ArticleDetailViewModel), typeof(ArticleDetailPage) } ,
                { typeof(CommentsViewModel), typeof(CommentsPage) } ,
            };

            var container = Mvx.Resolve<IMvxViewsContainer>();
            container.AddAll(registry);
        }

    }
}
