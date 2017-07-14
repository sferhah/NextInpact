using MvvmCross.Core.ViewModels;
using MvvmCross.Uwp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextInpact.Native.UWP
{
    public abstract class BaseApplicationMvxPage<TViewModel> : MvxWindowsPage<TViewModel> where TViewModel : MvxViewModel
    {
    }
}
