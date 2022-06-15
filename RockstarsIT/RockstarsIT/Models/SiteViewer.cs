
namespace RockstarsIT.Models
{
    public class SiteViewer
    {
        private int _amount;
        
        public int SiteViewerId { get; set; }
        public int Amount { get => _amount; set => _amount = value; }
    }
}