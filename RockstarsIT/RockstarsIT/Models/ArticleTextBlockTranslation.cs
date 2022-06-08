using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class ArticleTextBlockTranslation
    {
        private string _text;
        private Language _language;
        private ArticleTextBlocks _articleTextBlocks;

        [Key]
        public int ArtcleTextBlockTranslationId { get; set; }
        public string Text
        {
            get => _text;
            set => _text = value;
        }
        public int? LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language
        {
            get => _language;
            set => _language = value;
        }
        public int? ArticleTextBlockId { get; set; }
        [ForeignKey("ArticleTextBlockId")]
        public ArticleTextBlocks ArticleTextBlocks
        {
            get => _articleTextBlocks;
            set => _articleTextBlocks = value;
        }
    }
}
