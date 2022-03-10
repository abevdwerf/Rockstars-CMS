namespace RockstarsIT.Models
{
    public class Tribe
    {
        private string _name;
        private string _desctription;

        public int TribeId { get; set; }
        
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public string Desctription
        {
            get => _desctription;
            set => _desctription = value;
        }
    }
}