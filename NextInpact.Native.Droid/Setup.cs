using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using NextInpact.Core.IO;
using NextInpact.Core.Data;
using MvvmCross.Platform;
using NextInpact.Native.Droid.PlatformSpecific;

namespace NextInpact.Native.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)  { }

        protected override IMvxApplication CreateApp()
        {
            Mvx.RegisterSingleton<IStringConnectionProvider>(new AndroidConnectionProvider());            

            return new Core.App();
        }
    }
}