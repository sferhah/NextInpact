using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NextInpact.Helpers;
using NextInpact.Models;
using Xamarin.Forms;


namespace NextInpact.ViewModels
{
    public class CommentsViewModel : BaseViewModel
    {      
        public ObservableRangeCollection<Comment> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public Article article { get; set; }

        public CommentsViewModel(Article article)
        {
            this.article = article;
            Title = "NextINpact (Unofficial)";
            Items = new ObservableRangeCollection<Comment>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());           
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