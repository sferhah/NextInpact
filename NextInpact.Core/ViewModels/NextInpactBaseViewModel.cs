using GalaSoft.MvvmLight;
using NextInpact.Core.Helpers;
using NextInpact.Core.Models;
using NextInpact.Core.Parsing;
using Xamarin.Forms;

namespace NextInpact.Core.ViewModels
{
    public class NextInpactBaseViewModel : ViewModelBase
    {   
        private bool _IsBusy;
        public bool IsBusy
        {
            get => _IsBusy;
            set
            {
                _IsBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        string title = string.Empty;
        public string Title
        {
            get;
            set;
        }
    }
}

