using MvvmCross.Uwp.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Uwp.Views;
using MvvmCross.Platform;
using MvvmCross.Core.Views;
using Windows.UI.Xaml.Controls;
using MvvmCross.Forms.Uwp;
using Windows.ApplicationModel.Activation;
using MvvmCross.Platform.Converters;
using NextInpact.Forms.Converters;
using MvvmCross.Platform.Logging;
using MvvmCross.Forms.Platform;
using MvvmCross.Forms.Uwp.Presenters;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace NextInpact.Forms.UWP
{
    public class Setup : MvxFormsWindowsSetup
    {
        private readonly LaunchActivatedEventArgs _launchActivatedEventArgs;

        public Setup(Frame rootFrame, LaunchActivatedEventArgs e) : base(rootFrame, e)
        {
            _launchActivatedEventArgs = e;
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
    
        protected override IEnumerable<Assembly> GetViewAssemblies()
        {
            return base.GetViewAssemblies().Union(new[] { typeof(Forms.App).GetTypeInfo().Assembly });
        }

        protected override IMvxWindowsViewPresenter CreateViewPresenter(IMvxWindowsFrame rootFrame)
        {
            Xamarin.Forms.Forms.Init(_launchActivatedEventArgs);


            var xamarinFormsApp = new MvxFormsApplication();
            var presenter = new MvxFormsUwpViewPresenter(rootFrame, xamarinFormsApp);
            Mvx.RegisterSingleton<IMvxViewPresenter>(presenter);

            return presenter;
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
            var registry = Mvx.Resolve<IMvxValueConverterRegistry>();
            registry.AddOrOverwrite("ByteArrayToImage", new ByteArrayToImageValueConverter());
        }

        protected override MvxLogProviderType GetDefaultLogProviderType()
            => MvxLogProviderType.None;
    }
}
