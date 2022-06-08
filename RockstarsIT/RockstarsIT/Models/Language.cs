namespace RockstarsIT.Models
{
    public class Language
    {
        private string _name;

        public int LanguageId { get; set; }
        public string Name
        {
            get => _name;
            set => _name = value;
        }
    }
}
