using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class ArticleImages
    {
        private Article _article;
        private string _url;

        [Key]
        public int ArticleImageId { get; set;  }
        
        // [ForeignKey(name: "ArticleId")]
        public int ArticleId { get; set; }

        public string URL { get { return _url; } set { _url = value; } }

    }
}
