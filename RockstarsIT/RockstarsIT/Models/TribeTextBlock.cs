using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class TribeTextBlock
    {
        private Tribe _tribe;
        private string _text;

        [Key]
        public int TribeTextBlockId { get; set; }

        [ForeignKey(name: "TribeId")]
        public int? TribeId { get; set; }

        public virtual Tribe tribe
        {
            get => _tribe;
            set => _tribe = value;
        }

        public string Text
        {
            get => _text;
            set => _text = value;
        }
    }
}
