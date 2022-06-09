using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class PodcastContent
    {
        private string _title;
        private string _description;
        private Language _language;
        private Podcast _podcast;

        [Key]
        public int PodcastContentId { get; set; }
        public string Title
        {
            get => _title;
            set => _title = value;
        }
        public string Description
        {
            get => _description;
            set => _description = value;
        }
        public int? LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language
        {
            get => _language;
            set => _language = value;
        }
        public int? PodcastId { get; set; }
        [ForeignKey("PodcastId")]
        public Podcast Podcast
        {
            get => _podcast;
            set => _podcast = value;
        }
    }
}
