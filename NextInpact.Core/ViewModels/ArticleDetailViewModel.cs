using MvvmCross.Core.ViewModels;
using NextInpact.Core.Models;
using System.Windows.Input;
using System;
using System.Threading.Tasks;
using NextInpact.Core.Data;

namespace NextInpact.Core.ViewModels
{
    public class ArticleDetailViewModel : NextInpactBaseViewModel
    {   
        public Article Item { get; set; }

        
        public String ArticleContent
        {
            get => Item?.Content ?? "<html>loading...</html>";
        }

        public async void Init(int itemId)
        {   
            this.Item = await Store.GetArticle(itemId);
            RaisePropertyChanged(() => ArticleContent);
        }

        public ArticleDetailViewModel()
        {
            //Title = item.Title;
            Title = "NextINpact (Unofficial)";
        }

        private MvxCommand _ShowCommentsCommand;

        public ICommand ShowCommentsCommand
        {
            get
            {
                _ShowCommentsCommand = _ShowCommentsCommand ?? new MvxCommand(ShowComments);
                return _ShowCommentsCommand;
            }
        }

        private void ShowComments()
        {   
            ShowViewModel<CommentsViewModel>(new { itemId = this.Item.Id });
        }

    }
}