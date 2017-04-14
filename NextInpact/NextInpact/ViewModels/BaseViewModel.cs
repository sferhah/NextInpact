using NextInpact.Helpers;
using NextInpact.Models;
using NextInpact.Parsing;
using Xamarin.Forms;

namespace NextInpact.ViewModels
{
    public class BaseViewModel : ObservableObject
    {   
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
    }
}

