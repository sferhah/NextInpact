using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using NextInpact.Core.IO;
using NextInpact.PlatformSpecific.NativeDroid;
using NextInpact.Core.Data;
using MvvmCross.Platform;

namespace NextInpact.NativeDroid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)  { }

        protected override IMvxApplication CreateApp()
        {
            Mvx.RegisterSingleton<IStringConnectionProvider>(new AndroidConnectionProvider());
            Mvx.RegisterSingleton<ISaveAndLoad>(new AndroidSaveAndLoad());

            return new Core.App();
        }
    }
}