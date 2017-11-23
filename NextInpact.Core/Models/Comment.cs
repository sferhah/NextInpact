using System;
using NextInpact.Core.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NextInpact.Core.Models
{

    [Table("Comment")]
    public class Comment : ObservableObject
    {
        [Key]
        public string PrimaryKey { get; set; }   
        public int Position { get; set; }
        public string ArticleId { get; set; }
        public string Author { get; set; } = "";
        public string Content { get; set; } = "";
        public long TimeStampPublication { get; set; }

        public string AuthorWithPostDate
        {
            get
            {
                return this.Author + " " + new DateTime(this.TimeStampPublication).ToString(Constants.FORMAT_DATE_COMMENT);
            }
        }

    }
}
