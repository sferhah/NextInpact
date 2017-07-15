using MvvmCross.Core.ViewModels;
using MvvmCross.Uwp.Views;
using NextInpact.Core.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace NextInpact.Native.UWP.Views
{
    [MvxViewFor(typeof(CommentsViewModel))]
    public sealed partial class CommentsPage : MvxWindowsPage
    {
        public CommentsPage()
        {
            this.InitializeComponent();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
     
    }
}
