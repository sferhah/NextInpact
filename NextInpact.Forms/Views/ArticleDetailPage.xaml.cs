using MvvmCross.Forms.Views;
using NextInpact.Core.ViewModels;

namespace NextInpact.Forms.Views
{
    public partial class ArticleDetailPage : MvxContentPage<ArticleDetailViewModel>
    {
        public ArticleDetailPage()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            base.ViewModel.ShowCommentsCommand.Execute(null);
        }
    }
}
