﻿using System;

using NextInpact.Core.Models;
using NextInpact.Core.ViewModels;

using Xamarin.Forms;
using NextInpact.Core.Parsing;

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
            {
                return;
            }

            viewModel.ItemSelectedCommand.Execute(item);

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
