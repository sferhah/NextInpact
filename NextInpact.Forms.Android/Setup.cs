using Android.Content;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Core.Views;
using System.Collections.Generic;
using NextInpact.Forms.Views;
using NextInpact.Core.ViewModels;
using MvvmCross.Forms.Droid;
using System.Reflection;
using System.Linq;
using NextInpact.Forms.Converters;
using NextInpact.Core.Data;
using NextInpact.Core.IO;
using NextInpact.PlatformSpecific.Droid;

namespace NextInpact.Droid
{
    public class Setup : MvxFormsAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext) {}

        protected override IMvxApplication CreateApp()
        {
            Mvx.RegisterSingleton<IStringConnectionProvider>(new AndroidConnectionProvider());
            Mvx.RegisterSingleton<ISaveAndLoad>(new AndroidSaveAndLoad());

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

        protected override IEnumerable<Assembly> ValueConverterAssemblies
        {
            get
            {
                var toReturn = base.ValueConverterAssemblies.ToList();
                toReturn.Add(typeof(ByteArrayToImageValueConverter).Assembly);
                return toReturn;
            }
        }

    }
}
