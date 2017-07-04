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
            get { return _Content; }
            set
            {
                SyncPercentage += 0.33;
                _Content = value;
            }
        }

        public bool HasSubscription { get; set; }

        public long PublicationTimeStamp { get; set; }

        private int _TotalCommentsCount;
        public int TotalCommentsCount
        {
            get { return _TotalCommentsCount; }
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
            get { return _ImageData; }
            set
            {
                ImageSourceIsDefault = false;
                SyncPercentage += 0.33;
                SetProperty(ref _ImageData, value);
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
            get
            {
                return new DateTime(PublicationTimeStamp).ToString(Constantes.FORMAT_AFFICHAGE_ARTICLE_HEURE);
            }
        }

        public String PublicationDate
        {
            get
            {
                return new DateTime(PublicationTimeStamp).ToString(Constantes.FORMAT_AFFICHAGE_SECTION_DATE);
            }
        }


        private List<Comment> _Comments;
        [Ignore]
        public List<Comment> Comments
        {
            get
            {
                if (_Comments == null)
                {
                    _Comments = new List<Comment>();
                }

                return _Comments;
            }
            set
            {
                SyncPercentage += 0.34;
                _Comments = value ?? new List<Comment>();
            }
        }


    }
}
