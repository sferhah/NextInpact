using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NextInpact.Core.Helpers;
using NextInpact.Core.Models;
using Xamarin.Forms;
using MvvmCross.Core.ViewModels;

namespace NextInpact.Core.ViewModels
{
    public class CommentsViewModel : NextInpactBaseViewModel
    {
        public ObservableRangeCollection<Comment> Items { get; set; }
        public MvxCommand LoadItemsCommand { get; set; }

        public Article article { get; set; }
        public static Article StaticItem; // temp solution

        public CommentsViewModel(Article article = null)
        {
            this.article = article ?? StaticItem;
            Title = "NextINpact (Unofficial)";
            Items = new ObservableRangeCollection<Comment>();
            LoadItemsCommand = new MvxCommand(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = article.Comments;
                Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}