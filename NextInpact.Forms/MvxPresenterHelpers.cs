﻿using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NextInpact
{
    public static class MvxPresenterHelpers
    {
        public static IMvxViewModel LoadViewModel(MvxViewModelRequest request)
        {
            var viewModelLoader = Mvx.Resolve<IMvxViewModelLoader>();
            var viewModel = viewModelLoader.LoadViewModel(request, null);
            return viewModel;
        }

        public static ContentPage CreatePage(MvxViewModelRequest request)
        {
            var viewModelName = request.ViewModelType.Name;
            var pageName = viewModelName.Replace("ViewModel", "Page");
            var pageType = request.ViewModelType.GetTypeInfo().Assembly.CreatableTypes()
                                  .FirstOrDefault(t => t.Name == pageName);
            if (pageType == null)
            {
                Mvx.Trace("Page not found for {0}", pageName);
                return null;
            }

            var page = Activator.CreateInstance(pageType) as ContentPage;
            if (page == null)
            {
                Mvx.Error("Failed to create ContentPage {0}", pageName);
            }
            return page;
        }
    }
}
