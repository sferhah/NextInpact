﻿using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Forms.Uwp;
using MvvmCross.Forms.Uwp.Presenters;
using MvvmCross.Platform;
using NextInpact.Core.Data;
using NextInpact.Core.IO;
using NextInpact.Forms.UWP.PlatformSpecific;

namespace NextInpact.Forms.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();            

            Mvx.RegisterSingleton<IStringConnectionProvider>(new ConnectionProvider());
            Mvx.RegisterSingleton<ISaveAndLoad>(new WSaveAndLoad());

            var start = Mvx.Resolve<IMvxAppStart>();
            start.Start();

            var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsUwpPagePresenter;

            LoadApplication(presenter.MvxFormsApp);
        }
    }
}