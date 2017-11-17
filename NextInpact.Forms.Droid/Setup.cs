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
using MvvmCross.Droid.Views;
using MvvmCross.Forms.Droid.Views;
using MvvmCross.Forms.Droid.Platform;

namespace NextInpact.Forms.Droid
{
    public class Setup : MvxFormsAndroidSetup 
    {
        public Setup(Context applicationContext) : base(applicationContext) {}

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        //MVVMCross 5.1 Forms.Android : Manually create a presenter otherwise MVVMCross won't do it
        //protected override IMvxAndroidViewPresenter CreateViewPresenter()
        //{

        //    var presenter = new MvxFormsAndroidViewPresenter(new List<Assembly> {Assembly.GetExecutingAssembly() })
        //    {
        //        FormsApplication = new App()
        //    };

        //    Mvx.RegisterSingleton<IMvxViewPresenter>(presenter);

        //    return presenter;
        //}

        protected override IEnumerable<Assembly> GetViewAssemblies()
        {
            return base.GetViewAssemblies().Union(new[] { typeof(App).GetTypeInfo().Assembly });
        }

        //MVVMCross all versions : Manually map ViewModels to Views if they are not in the same project otherwise MVVMCross won't do it
        protected override void InitializeViewLookup()
        {   
            Mvx.Resolve<IMvxViewsContainer>().AddAll(App.Mapping);
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
