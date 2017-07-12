using System;
using NextInpact.Core.Models;
using NextInpact.Core.ViewModels;
using Xamarin.Forms;


namespace NextInpact.Forms.Views
{
    public partial class ArticlesPage : ContentPage
    {
        ArticlesViewModel viewModel;

        public ArticlesPage()
        {
            InitializeComponent();        

            BindingContext = viewModel = new ArticlesViewModel();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            viewModel.LoadItemsCommand.Execute(null);
        }
        
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Article;

            if (item == null)
            {
                return;
            }

            switch (Device.RuntimePlatform)
            {
                
                case Device.WinPhone:
                case Device.Windows:
                    var vm = new ArticleDetailViewModel();
                    vm.Init(item.Id);
                    await Navigation.PushAsync(new ArticleDetailPage(vm));
                    break;
                default:
                    viewModel.ItemSelectedCommand.Execute(item);
                    break;
            }

            

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

    }
}
