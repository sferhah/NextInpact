﻿using System;
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
using MvvmCross.Core.Navigation;

namespace NextInpact.Core.ViewModels
{
    public class ArticlesViewModel : NextInpactBaseViewModel<int>
    {


        public ObservableRangeCollection<Article> Items { get; set; } 
        public MvxCommand LoadItemsCommand { get; set; }      

        public string LastRefreshDate
        {
            get => Preferences.LastRefreshDateText;            
        }

        private readonly IMvxNavigationService _navigationService;


        public ArticlesViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
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
            _navigationService.Navigate<ArticleDetailViewModel, string>(item.Id);
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
            var new_items = await NextInpactClient.GetArticlesAsync(page: 1);
            new_items = new_items.OrderByDescending(x => x.PublicationTimeStamp).ToList();

            //Combine
            var delta = new_items.Where(x => !Items.Any(y => x.Id == y.Id));
            List<Article> items  = Items.Concat(delta).OrderByDescending(x => x.PublicationTimeStamp).ToList();            

            AssignDay(items);

            Items.Clear();
            Items.ReplaceRange(items);
            

            // Sets miniatures from cache if availaible
            await Store.SetMiniaturesFromCache(items.Where(x => x.ImageSourceIsDefault));
            // Sets miniature from net for articles without miniature
            await NextInpactClient.DownloadMiniaturesAsync(items.Where(x => x.ImageSourceIsDefault));


            // Sets content from db if availaible
            await Store.SyncWithDatabase(items);
            // Sets content from http for articles without Content
            var articles_witout_content = items.Where(x => String.IsNullOrWhiteSpace(x.Content)).ToList();
            await NextInpactClient.DownloadArticlesContent(articles_witout_content);
            await Store.SaveArticlesContent(articles_witout_content);


            var comments = await NextInpactClient.DownloadArticlesComs(items);
            await Store.SaveComments(comments);

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
                else
                {
                    article.ShowDateSection = false;
                }
            }
        }

        public override void Prepare(int parameter)
        {
            throw new NotImplementedException();
        }
    }
}