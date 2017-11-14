using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Forms.Uwp.Presenters;
using MvvmCross.Platform;

namespace NextInpact.Forms.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            //need to force this call otherwise FormsApplication.MainPage is null            
            Mvx.Resolve<IMvxAppStart>().Start();

            var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsUwpPagePresenter;
            LoadApplication(presenter.FormsApplication);
        }
    }
}