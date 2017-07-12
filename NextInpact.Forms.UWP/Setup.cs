using MvvmCross.Uwp.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Uwp.Views;
using MvvmCross.Platform;
using MvvmCross.Core.Views;
using MvvmCross.Forms.Uwp.Presenters;
using System.Collections.Generic;
using NextInpact.Forms.Views;
using NextInpact.Core.ViewModels;
using System;
using Windows.UI.Xaml.Controls;

namespace NextInpact.UWP
{
    public class Setup : MvxWindowsSetup
    {
        public Setup(Frame rootFrame, string suspensionManagerSessionStateKey = null) : base(rootFrame, suspensionManagerSessionStateKey)
        {
        }     

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override IMvxWindowsViewPresenter CreateViewPresenter(IMvxWindowsFrame rootFrame)
        {
            var presenter = new MvxWindowsMultiRegionViewPresenter(rootFrame);
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
