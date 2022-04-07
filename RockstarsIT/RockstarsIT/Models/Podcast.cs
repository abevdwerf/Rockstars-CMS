using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class Podcast
    {
        // Fields
        private string _titel;
        private string _omschrijving;
        private string _url;
        private Rockstar _auteur;
        private Tribe _tribe;
        // Properties
        public int PodcastId { get; set; }
        public string Titel
        {
            get => _titel; 
            set => _titel = value;
        }
        public string Omschrijving
        {
            get => _omschrijving; 
            set => _omschrijving = value;
        }
        public string URL
        {
            get => _url; 
            set => _url = value;
        }
        public int? RockstarId { get; set; }
        [ForeignKey("RockstarId")]
        public Rockstar Auteur
        {
            get => _auteur; 
            set => _auteur = value;
        }
        public int? TribeId { get; set; }
        [ForeignKey("TribeId")]
        public Tribe Tribe
        {
            get => _tribe; 
            set => _tribe = value;
        }
        // Methods
        
    }
}