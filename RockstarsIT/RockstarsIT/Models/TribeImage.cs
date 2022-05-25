using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class TribeImage
    {
        private Tribe _tribe;
        private string _url;

        [Key]
        public int TribeImageId { get; set; }
        
        public int TribeId { get; set; }

        public string URL { get { return _url; } set { _url = value; } }
    }
}
