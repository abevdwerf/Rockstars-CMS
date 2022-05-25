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

        public int TribeId { get; set; }

        public string Text
        {
            get => _text;
            set => _text = value;
        }
    }
}
