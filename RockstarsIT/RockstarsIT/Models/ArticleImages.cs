using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class ArticleImages
    {
        private Article _article;
        private string imgName;
        private string _url;

        [Key]
        public int ArticleImageId { get; set;  }

        [ForeignKey(name: "ArticleId")]
        public int? ArticleId { get; set; }
        public virtual Article Article
        {
            get => _article;
            set => _article = value;
        }
        //public virtual Article Article { get { return article; } set { article = value; } }
        public string ImageName { get { return imgName; } set { imgName = value; } }
        public string URL { get { return _url; } set { _url = value; } } 

    }
}
