using System;

using NextInpact.Models;
using NextInpact.ViewModels;

using Xamarin.Forms;
using NextInpact.Parsing;

namespace NextInpact.Views
{
    public partial class ArticlesPage : ContentPage
    {
        ArticlesViewModel viewModel;

        public ArticlesPage()
        {
            InitializeComponent();        

            BindingContext = viewModel = new ArticlesViewModel();
        }

        private async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            viewModel.LoadItemsCommand.Execute(null);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Article;
            if (item == null)
                return;

            await Navigation.PushAsync(new ArticleDetailPage(new ArticleDetailViewModel(item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }
        

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
