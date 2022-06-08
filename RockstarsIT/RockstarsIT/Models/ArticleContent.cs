using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class ArticleContent
    {
        private string _title;
        private Language _language;
        private Article _article;

        [Key]
        public int ArticleTranslationId { get; set; }
        public string Title 
        { 
            get => _title; 
            set => _title = value; 
        }
        public int? LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language
        {
            get => _language;
            set => _language = value;
        }
        public int? ArticleId { get; set; }
        [ForeignKey("ArticleId")]
        public Article Article
        {
            get => _article;
            set => _article = value;
        }
    }
}
