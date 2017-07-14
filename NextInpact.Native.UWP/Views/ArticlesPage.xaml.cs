using NextInpact.Core.Models;
using NextInpact.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace NextInpact.Native.UWP.Views
{
    public sealed partial class ArticlesPage : Page
    {
        public ArticlesViewModel ViewModel { get; set; }


        public ArticlesPage()
        {
            this.InitializeComponent();

            DataContext = ViewModel = new ArticlesViewModel();

        }
      
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as Article;

            if (item == null)
            {
                return;
            }

            this.Frame.Navigate(typeof(ArticleDetailPage), item.Id);
        }
    }
}
