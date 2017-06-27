
using NextInpact.Core.ViewModels;

using Xamarin.Forms;

namespace NextInpact.Views
{
    public partial class ArticleDetailPage : ContentPage
    {
        ArticleDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ArticleDetailPage()
        { 
            InitializeComponent();
        }

        public ArticleDetailPage(ArticleDetailViewModel viewModel)
        {
            InitializeComponent();
       
            BindingContext = this.viewModel = viewModel;
        }

        private async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new CommentsPage(new CommentsViewModel(this.viewModel.Item)));
        }
    }
}
