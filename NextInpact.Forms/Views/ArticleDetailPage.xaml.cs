using NextInpact.Core.ViewModels;
using Xamarin.Forms;

namespace NextInpact.Forms.Views
{
    public partial class ArticleDetailPage : ContentPage
    {
        ArticleDetailViewModel Vm
        {
            get
            {
                return ((ArticleDetailViewModel)this.BindingContext);
            }
        }

        public ArticleDetailPage()
        {
            InitializeComponent();
        }

        //Constructor For UWP
        public ArticleDetailPage(ArticleDetailViewModel viewModel)
        {
            InitializeComponent();
            
            BindingContext = viewModel;
        }

        private async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {

            switch (Device.RuntimePlatform)
            {
                case Device.WinPhone:
                case Device.Windows:
                    var vm = new CommentsViewModel();
                    vm.Init(Vm.Item.Id);
                    await Navigation.PushAsync(new CommentsPage(vm));
                    break;
                default:
                    Vm.ShowCommentsCommand.Execute(null);
                    break;
            }
           
        }

    }
}
