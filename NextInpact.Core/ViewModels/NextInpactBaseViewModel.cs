using GalaSoft.MvvmLight;
using NextInpact.Core.Helpers;
using NextInpact.Core.Models;
using NextInpact.Core.Parsing;
using Xamarin.Forms;

namespace NextInpact.Core.ViewModels
{
    public class NextInpactBaseViewModel : ViewModelBase
    {
        
        public bool IsBusy
        {
            get;
            set; 
        }

        string title = string.Empty;
        public string Title
        {
            get;
            set;
        }
    }
}

