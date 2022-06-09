using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class PodcastEpisodeContent
    {
        private string _title;
        private string _description;
        private Language _language;
        private PodcastEpisode _podcastEpisode;

        [Key]
        public int PodcastEpisodeContentId { get; set; }
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
        public int? PodcastEpisodeId { get; set; }
        [ForeignKey("PodcastEpisodeId")]
        public PodcastEpisode PodcastEpisode
        {
            get => _podcastEpisode;
            set => _podcastEpisode = value;
        }
    }
}
