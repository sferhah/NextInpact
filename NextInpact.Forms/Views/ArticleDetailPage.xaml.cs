using MvvmCross.Forms.Core;
using NextInpact.Core.ViewModels;
using Xamarin.Forms;

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
