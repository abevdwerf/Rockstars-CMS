namespace RockstarsIT.Models
{
    public class Role
    {
        private string _name;

        public int RoleId { get; set; }
        public string Name 
        {
            get => _name;
            set => _name = value;
        }
    }
}
