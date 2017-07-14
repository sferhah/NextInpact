using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Uwp.Platform;
using NextInpact.Core.Data;
using NextInpact.Core.IO;
using NextInpact.Native.UWP.PlatformSpecific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace NextInpact.Native.UWP
{
    public class Setup : MvxWindowsSetup
    {
        public Setup(Frame rootFrame) : base(rootFrame) { }

        protected override IMvxApplication CreateApp()
        {
            Mvx.RegisterSingleton<IStringConnectionProvider>(new ConnectionProvider());
            Mvx.RegisterSingleton<ISaveAndLoad>(new WSaveAndLoad());

            return new Core.App();
        }
    }
}
