using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class TribeImages
    {
        private Tribe _tribe;
        private string _url;

        [Key]
        public int TribeImageId { get; set; }

        [ForeignKey(name: "TribeId")]
        public int? TribeId { get; set; }
        public virtual Tribe Tribe
        {
            get => _tribe;
            set => _tribe = value;
        }

        public string URL { get { return _url; } set { _url = value; } }
    }
}
