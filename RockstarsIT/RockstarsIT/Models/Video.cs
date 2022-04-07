using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class Video
    {
        private int _id;
        private string _title;
        private string _description;
        private string _link;
        private Tribe _tribe;
        private Rockstar _rockstar;

        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int? TribeId { get; set; }
        [ForeignKey("TribeId")]
        public virtual Tribe Tribe
        {
            get => _tribe;
            set => _tribe = value;
        }
        public int? RockstarId { get; set; }
        [ForeignKey("RockstarId")]
        public virtual Rockstar Rockstar
        {
            get => _rockstar;
            set => _rockstar = value;
        }
    }
}
