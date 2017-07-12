using MvvmCross.Core.Views;
using MvvmCross.Platform;
using NextInpact.Core.Data;
using NextInpact.Core.IO;
using NextInpact.UWP.PlatformSpecific;

namespace NextInpact.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            

            Mvx.RegisterSingleton<IStringConnectionProvider>(new ConnectionProvider());
            Mvx.RegisterSingleton<ISaveAndLoad>(new WSaveAndLoad());

            LoadApplication(new NextInpact.Forms.App());
        }
    }
}