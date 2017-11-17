using NextInpact.Core.Helpers;
using SQLite;
using System;
using System.Collections.Generic;


namespace NextInpact.Core.Models
{
    [Table("Article")]
    public class Article : ObservableObject
    {
        [PrimaryKey]
        public int Id { get; set; }
        public String Title { get; set; }
        public String SubTitle { get; set; } = "";

        public String Url { get; set; }
        public String UrlIllustration { get; set; } = "";

        private String _Content;
        public String Content
        {
            get => _Content; 
            set
            {  
                _Content = value;
                SyncPercentage += 0.33;
            }
        }

        public bool HasSubscription { get; set; }

        public long PublicationTimeStamp { get; set; }

        private int _TotalCommentsCount;
        public int TotalCommentsCount
        {
            get => _TotalCommentsCount; 
            set
            {
                SetProperty(ref _TotalCommentsCount, value);
            }
        }


        public bool ImageSourceIsDefault = true;

        private byte[] _ImageData = null;


        [Ignore]
        public byte[] ImageData
        {
            get => _ImageData; 
            set
            {
                ImageSourceIsDefault = false;                
                SetProperty(ref _ImageData, value);

                SyncPercentage += 0.33;
            }
        }

        
        private double _SyncPercentage;
        [Ignore]
        public double SyncPercentage
        {
            get { return _SyncPercentage; }
            set { SetProperty(ref _SyncPercentage, value); }
        }


        [Ignore]
        public bool ShowDateSection { get; set; }


        public String PublicationTime
        {
            get => new DateTime(PublicationTimeStamp).ToString(Constants.FORMAT_AFFICHAGE_ARTICLE_HEURE);         
        }

        public String PublicationDate
        {
            get => new DateTime(PublicationTimeStamp).ToString(Constants.FORMAT_AFFICHAGE_SECTION_DATE);            
        }


        private bool _HasComments;
        [Ignore]
        public bool HasComments
        {
            get => _HasComments; 
            set { _HasComments =  value; SyncPercentage += 0.34; }
        }
    }
}
