using MvvmCross.Core.ViewModels;
using NextInpact.Core.Models;
using System.Windows.Input;
using System;
using System.Threading.Tasks;
using NextInpact.Core.Data;
using MvvmCross.Core.Navigation;

namespace NextInpact.Core.ViewModels
{
    public class ArticleDetailViewModel : NextInpactBaseViewModel<string>
    {   
        public Article Item { get; set; }
        
        public String ArticleContent
        {
            get => Item?.Content ?? "<html>loading...</html>";
        }

        string itemId;

        public override void Prepare(string parameter)
        {
            this.itemId = parameter;
        }

        public override async Task Initialize()
        {
            this.Item = await Store.GetArticle(itemId);
            RaisePropertyChanged(() => ArticleContent);
        }

        private readonly IMvxNavigationService _navigationService;


        public ArticleDetailViewModel(IMvxNavigationService navigationService) : this()
        {
            _navigationService = navigationService;
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
            _navigationService.Navigate<CommentsViewModel, string>(this.Item.Id);
        }
    }
}