using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextInpact.Parsing;
using NextInpact.Helpers;

namespace NextInpact.Models
{

    [Table("Comment")]
    public class Comment : ObservableObject
    {
        [PrimaryKey]
        public String PrimaryKey
        {
            get
            {
                return this.ArticleId + "-" + this.Uuid;
            }
            set
            {
                var array = value.Split("-");
                this.ArticleId = int.Parse(array[0]);
                this.Uuid = int.Parse(array[1]);
            }
        }

        public int Id { get; set; }
        public int Uuid { get; set; }
        public int ArticleId { get; set; }
        public String Author { get; set; } = "";
        public String Content { get; set; } = "";
        public long TimeStampPublication { get; set; }   

        public String AuthorWithPostDate
        {
            get
            {                              
                return this.Author + " " + new DateTime(this.TimeStampPublication).ToString(Constantes.FORMAT_AFFICHAGE_COMMENTAIRE_DATE_HEURE);
            }
        }
      
    }
}
