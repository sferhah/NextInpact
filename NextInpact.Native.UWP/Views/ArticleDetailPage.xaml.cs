using MvvmCross.Core.ViewModels;
using MvvmCross.Uwp.Views;
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
    [MvxViewFor(typeof(ArticleDetailViewModel))]
    public sealed partial class ArticleDetailPage : MvxWindowsPage
    {
        public ArticleDetailPage()
        {
            this.InitializeComponent();         
        }      

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if(Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }    
    }
}
