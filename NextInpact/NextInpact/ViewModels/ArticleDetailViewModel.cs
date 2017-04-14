using NextInpact.Models;
using NextInpact.Parsing;

namespace NextInpact.ViewModels
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