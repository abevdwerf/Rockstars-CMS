using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class VideoContent
    {
        private string _title;
        private string _description;
        private Language _language;
        private Video _video;

        [Key]
        public int VideoContentId { get; set; }
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
        public int? VideoId { get; set; }
        [ForeignKey("VideoId")]
        public Video Video
        {
            get => _video;
            set => _video = value;
        }
    }
}
