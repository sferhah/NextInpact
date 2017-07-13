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

            ThreadSafeSqlite.Instance.Init(typeof(Article), typeof(Comment));
        }

        protected override void OnStart()
        {
            Mvx.Resolve<IMvxAppStart>().Start();            
        }
    }
}
