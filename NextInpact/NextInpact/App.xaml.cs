using NextInpact.Data;
using NextInpact.Models;
using NextInpact.Parsing;
using NextInpact.Views;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NextInpact
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
