using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Platform;
using MvvmCross.Platform;
using NextInpact.Core.Data;
using NextInpact.Core.Models;
using NextInpact.Core.ViewModels;
using NextInpact.Forms.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NextInpact.Forms
{
    public partial class App : MvxFormsApplication
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            var s = Mvx.Resolve<IMvxAppStart>();
            s.Start();            
        }

        public static Dictionary<Type, Type> Mapping
        {
            get
            {
                return new Dictionary<Type, Type>()
                {
                    { typeof(ArticlesViewModel), typeof(ArticlesPage) },
                    { typeof(ArticleDetailViewModel), typeof(ArticleDetailPage) } ,
                    { typeof(CommentsViewModel), typeof(CommentsPage) } ,
                };
            }

        }

    }
}
