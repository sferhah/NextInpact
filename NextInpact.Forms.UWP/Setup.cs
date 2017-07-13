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
using MvvmCross.Forms.Uwp;
using Windows.ApplicationModel.Activation;
using MvvmCross.Platform.Converters;
using NextInpact.Forms.Converters;

namespace NextInpact.Forms.UWP
{
    public class Setup : MvxFormsWindowsSetup
    {
        public Setup(Frame rootFrame, LaunchActivatedEventArgs e) : base(rootFrame, e)
        {
        } 

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
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

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
            var registry = Mvx.Resolve<IMvxValueConverterRegistry>();
            registry.AddOrOverwrite("ByteArrayToImage", new ByteArrayToImageValueConverter());
        }

    }
}
