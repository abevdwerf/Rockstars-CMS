namespace RockstarsIT.Models
{
    public class Tribe
    {
        private string _name;
        private string _description;

        public int TribeId { get; set; }
        
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public string Description
        {
            get => _description;
            set => _description = value;
        }
    }
}