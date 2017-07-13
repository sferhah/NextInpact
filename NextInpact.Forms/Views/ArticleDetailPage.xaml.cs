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

        private async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            Vm.ShowCommentsCommand.Execute(null);
        }

    }
}
