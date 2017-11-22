using NextInpact.Core.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextInpact.Core.Models
{
    [Table("Article")]
    public class Article : ObservableObject
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; } = "";

        public string Url { get; set; }
        public string UrlIllustration { get; set; } = "";

        private string _ContentBackingField;
        public string Content
        {
            get => _ContentBackingField; 
            set
            {  
                _ContentBackingField = value;
                SyncPercentage += 0.33;
            }
        }

        public bool HasSubscription
        {
            get;
            set;
        }

        public long PublicationTimeStamp { get; set; }

        private int _TotalCommentsCountBackingField;
        public int TotalCommentsCount
        {
            get => _TotalCommentsCountBackingField; 
            set
            {
                SetProperty(ref _TotalCommentsCountBackingField, value);
            }
        }


        public bool ImageSourceIsDefault = true;

        private byte[] _ImageData = null;


        [NotMapped]
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
        [NotMapped]
        public double SyncPercentage
        {
            get { return _SyncPercentage; }
            set { SetProperty(ref _SyncPercentage, value); }
        }


        [NotMapped]
        public bool ShowDateSection { get; set; }


        public string PublicationTime
        {
            get => new DateTime(PublicationTimeStamp).ToString(Constants.FORMAT_DISPLAY_ARTICLE_HEURE);         
        }

        public string PublicationDate
        {
            get => new DateTime(PublicationTimeStamp).ToString(Constants.FORMAT_DISPLAY_SECTION_DATE);            
        }


        private bool _HasComments;
        [NotMapped]
        public bool HasComments
        {
            get => _HasComments; 
            set { _HasComments =  value; SyncPercentage += 0.34; }
        }
    }
}
