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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NextInpact.Native.UWP.Views
{
    
    public sealed partial class CommentsPage : Page
    {
        public CommentsViewModel ViewModel { get; set; }


        public CommentsPage()
        {
            this.InitializeComponent();

            DataContext = ViewModel = new CommentsViewModel();

        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is int)
            {
                ViewModel.Init((int)e.Parameter);
            }

            base.OnNavigatedTo(e);
        }
    }
}
