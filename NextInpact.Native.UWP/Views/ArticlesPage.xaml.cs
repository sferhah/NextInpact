using MvvmCross.Core.ViewModels;
using MvvmCross.Uwp.Views;
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

            ((ArticlesViewModel)ViewModel).ItemSelectedCommand.Execute(item);
        }
    }
}
