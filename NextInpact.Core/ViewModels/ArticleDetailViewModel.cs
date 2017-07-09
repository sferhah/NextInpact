
using MvvmCross.Core.ViewModels;
using NextInpact.Core.Models;
using NextInpact.Core.Parsing;
using System.Windows.Input;

namespace NextInpact.Core.ViewModels
{
    public class ArticleDetailViewModel : NextInpactBaseViewModel
    {
        public static Article StaticItem; // temp solution
        public Article Item { get; set; }

        public ArticleDetailViewModel(Article item = null)
        {
            //Title = item.Titre;
            Title = "NextINpact (Unofficial)";
            Item = item ?? StaticItem;
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
            CommentsViewModel.StaticItem = this.Item;
            ShowViewModel<CommentsViewModel>(this.Item);
        }

    }
}