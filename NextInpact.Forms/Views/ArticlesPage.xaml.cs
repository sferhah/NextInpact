using System;
using NextInpact.Core.Models;
using NextInpact.Core.ViewModels;
using Xamarin.Forms;
using MvvmCross.Forms.Core;

namespace NextInpact.Forms.Views
{
    public partial class ArticlesPage : MvxContentPage<ArticlesViewModel>
    {
        public ArticlesPage()
        {
            InitializeComponent();            
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            base.ViewModel.LoadItemsCommand.Execute(null);
        }
        
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Article;

            if (item == null)
            {
                return;
            }

            base.ViewModel.ItemSelectedCommand.Execute(item);            

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

    }
}
