using NextInpact.Core.Data;
using NextInpact.Core.Models;
using NextInpact.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NextInpact.Forms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ThreadSafeSqlite.Instance.Init(typeof(Article), typeof(Comment));

            Current.MainPage = new NavigationPage(new ArticlesPage());

        }        
    }
}
