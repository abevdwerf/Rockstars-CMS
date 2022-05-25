using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class ArticleTextBlocks
    {
        private Article _article;
        private string _text;

        [Key]
        public int ArticleTextBlockId { get; set; }

        // [ForeignKey(name: "ArticleId")]
        public int ArticleId { get; set; }
        
        public string Text
        {
            get => _text;
            set => _text = value;
        }
    }
}
