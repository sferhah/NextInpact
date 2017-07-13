using NextInpact.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NextInpact.Forms.Views
{   
    public partial class CommentsPage : ContentPage
    {
        CommentsViewModel Vm
        {
            get
            {
                return ((CommentsViewModel)this.BindingContext);
            }
        }
        
        public CommentsPage()
        {
            InitializeComponent();
        }
    }
   
}
