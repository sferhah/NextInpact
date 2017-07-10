using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NextInpact.Core.Helpers;
using NextInpact.Core.Models;
using MvvmCross.Core.ViewModels;
using NextInpact.Core.Data;

namespace NextInpact.Core.ViewModels
{
    public class CommentsViewModel : NextInpactBaseViewModel
    {
        public ObservableRangeCollection<Comment> Items { get; set; }
        public MvxCommand LoadItemsCommand { get; set; }

        private Article article;

        public async void Init(int itemId)
        {
            this.article = await Store.GetArticle(itemId);
            IsBusy = false;
            await ExecuteLoadItemsCommand();
        }

        public CommentsViewModel()
        {   
            Title = "NextINpact (Unofficial)";
            Items = new ObservableRangeCollection<Comment>();
            LoadItemsCommand = new MvxCommand(async () => await ExecuteLoadItemsCommand());
            IsBusy = true;
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
                //MessagingCenter.Send(new MessagingCenterAlert
                //{
                //    Title = "Error",
                //    Message = "Unable to load items.",
                //    Cancel = "OK"
                //}, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}