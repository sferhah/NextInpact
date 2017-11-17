using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NextInpact.Core.Helpers;
using NextInpact.Core.Models;
using MvvmCross.Core.ViewModels;
using NextInpact.Core.Data;
using System.Collections.Generic;

namespace NextInpact.Core.ViewModels
{
    public class CommentsViewModel : NextInpactBaseViewModel<int>
    {
        public ObservableRangeCollection<Comment> Items { get; set; }
        public MvxCommand LoadItemsCommand { get; set; }

        private List<Comment> comments;

        int itemId;

        public override void Prepare(int parameter)
        {
            this.itemId = parameter;
        }

        public override async Task Initialize()
        {
            this.comments = await Store.GetArticleComments(itemId);
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
                Items.ReplaceRange(comments);
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