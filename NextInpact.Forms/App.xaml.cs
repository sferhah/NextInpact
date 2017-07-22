using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Core;
using MvvmCross.Platform;
using NextInpact.Core.Data;
using NextInpact.Core.Models;
using NextInpact.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NextInpact.Forms
{
    public partial class App : MvxFormsApplication
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            var s = Mvx.Resolve<IMvxAppStart>();
            s.Start();            
        }
    }
}
