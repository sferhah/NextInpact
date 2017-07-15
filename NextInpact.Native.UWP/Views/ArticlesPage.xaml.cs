using MvvmCross.Core.ViewModels;
using MvvmCross.Uwp.Views;
using NextInpact.Core.Models;
using NextInpact.Core.ViewModels;
using Windows.UI.Xaml.Controls;

namespace NextInpact.Native.UWP.Views
{
    [MvxViewFor(typeof(ArticlesViewModel))]
    public sealed partial class ArticlesPage : MvxWindowsPage
    {   
        public ArticlesPage()
        {
            this.InitializeComponent();            
        }
      
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as Article;

            if (item == null)
            {
                return;
            }
            
            ItemsListView.SelectionMode = ListViewSelectionMode.None;

            ((ArticlesViewModel)ViewModel).ItemSelectedCommand.Execute(item);
        }
    }
}
