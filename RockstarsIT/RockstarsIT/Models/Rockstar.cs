using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace RockstarsIT.Models
{
    public class Rockstar
    {
        private Tribe _tribe;
        private string _name;
        private string _linkedIn;
        private string _description;
        private string _img;

        public int RockstarId { get; set; }
        
        public int? TribeId { get; set; }
        [ForeignKey("TribeId")]
        public virtual Tribe Tribe {
            get => _tribe;
            set => _tribe = value;
        }
        
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public string LinkedIn
        {
            get => _linkedIn;
            set => _linkedIn = value;
        }
        
        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public string IMG
        {
            get => _img; 
            set => _img = value;
        }
    }
}