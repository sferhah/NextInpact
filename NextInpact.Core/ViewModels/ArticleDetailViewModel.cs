using NextInpact.Core.Models;
using NextInpact.Core.Parsing;

namespace NextInpact.Core.ViewModels
{
    public class ArticleDetailViewModel : BaseViewModel
    {
        public Article Item { get; set; }
        public ArticleDetailViewModel(Article item = null)
        {
            //Title = item.Titre;
            Title = "NextINpact (Unofficial)";
            Item = item;
        }
    }
}