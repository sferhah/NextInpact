using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Platform;
using MvvmCross.Platform;
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
