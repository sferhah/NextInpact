using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NextInpact.Core.Helpers;
using NextInpact.Core.Models;
using System.Collections.Generic;
using NextInpact.Core.Data;
using System.Linq;
using NextInpact.Core.Networking;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;

namespace NextInpact.Core.ViewModels
{
    public class ArticlesViewModel : NextInpactBaseViewModel
    {
        public ObservableRangeCollection<Article> Items { get; set; } 
        public MvxCommand LoadItemsCommand { get; set; }

        
        public string LastRefreshDate
        {
            get => Preferences.LastRefreshDateText;            
        }

        public ArticlesViewModel()
        {
            Title = "NextINpact (Unofficial)";
            Items = new ObservableRangeCollection<Article>();
            LoadItemsCommand = new MvxCommand(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommand.Execute(null);
        }

        private MvxCommand<Article> _itemSelectedCommand;

        public ICommand ItemSelectedCommand
        {
            get
            {
                _itemSelectedCommand = _itemSelectedCommand ?? new MvxCommand<Article>(DoSelectItem);
                return _itemSelectedCommand;
            }
        }

        private void DoSelectItem(Article item)
        {   
            ShowViewModel<ArticleDetailViewModel>(new { itemId = item.Id } );
        }

        bool firstAppear = true;

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;            

            try
            {
                if (firstAppear)
                {
                    await InitFromDb();
                    firstAppear = false;

                    if (!Items.Any())
                    {
                        await InitFromHttp();
                    }
                }
                else
                {
                    await InitFromHttp();
                }

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

        public async Task InitFromDb()
        {
            Items.Clear();

            var items_in_db = await Store.GetAll();
            AssignDay(items_in_db);

            Items.ReplaceRange(items_in_db);
        }

        public async Task InitFromHttp()
        {
            var items = await NextInpactClient.GetArticlesAsync(page: 1);
            items = items.OrderByDescending(x => x.PublicationTimeStamp).ToList();

            AssignDay(items);

            Items.Clear();
            Items.ReplaceRange(items);

            // Sets miniatures from cache if availaible
            await Store.SetMiniaturesFromCache(items);
            // Sets miniature from net for articles without miniature
            await NextInpactClient.DownloadMiniaturesAsync(items.Where(x => x.ImageSourceIsDefault));


            // Sets content from db if availaible
            await Store.SyncWithDatabase(items);
            // Sets content from http for articles without Content
            var articles_witout_content = items.Where(x => String.IsNullOrWhiteSpace(x.Content)).ToList();
            await NextInpactClient.DownloadArticlesContent(articles_witout_content);
            await Store.SaveArticlesContent(articles_witout_content);


            await NextInpactClient.DownloadArticlesComs(items);
            await Store.SaveComments(items);

            Preferences.LastRefreshDate = DateTime.Now.Ticks;

            RaisePropertyChanged(()=> LastRefreshDate);            
        }


        public static void AssignDay(IEnumerable<Article> items)
        {
            String currentDay = "";

            foreach (Article article in items)
            {
                if (article.PublicationDate != currentDay)
                {
                    currentDay = article.PublicationDate;
                    article.ShowDateSection = true;
                }
            }
        }
    }
}