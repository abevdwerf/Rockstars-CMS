using System.ComponentModel.DataAnnotations;

namespace RockstarsIT.Models
{
    public class Language
    {
        private string _name;

        [Key]
        public int LanguageId { get; set; }
        public string Name
        {
            get => _name;
            set => _name = value;
        }
    }
}
