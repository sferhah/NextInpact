using MvvmCross.Core.ViewModels;

namespace NextInpact.Core.ViewModels
{
    public abstract class NextInpactBaseViewModel<T> : MvxViewModel<T>
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

