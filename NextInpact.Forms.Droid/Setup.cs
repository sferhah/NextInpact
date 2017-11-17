using Android.Content;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using MvvmCross.Forms.Droid.Platform;
using MvvmCross.Forms.Platform;

namespace NextInpact.Forms.Droid
{
    public class Setup : MvxFormsAndroidSetup 
    {
        public Setup(Context applicationContext) : base(applicationContext) {}

        protected override MvxFormsApplication CreateFormsApplication()
        {
            return new App();
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        } 

        protected override IEnumerable<Assembly> GetViewAssemblies()
        {
            return base.GetViewAssemblies().Union(new[] { typeof(App).GetTypeInfo().Assembly });
        }
    }
}
