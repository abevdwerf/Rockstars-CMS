using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class Tribe
    {
        private string _name;
        private string _description;
        private string _spotify;
        private string _leadaddress;
        private int _blocknumber;
        private int _imagenumber;
        private Tag _tag;
        private Podcast _podcast;
        public int TribeId { get; set; }

        [ForeignKey(name: "TagId")]
        private int? TagId { get; set; }
        public Tag tag
        {
            get => _tag;
            set => _tag = value;
        }

        [Required(ErrorMessage = "Naam is verplicht")]
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
        public string Spotify
        {
            get => _spotify;
            set => _spotify = value;
        }

        public string LeadAddress
        {
            get => _leadaddress;
            set => _leadaddress = value;
        }

        [Required(ErrorMessage = "Geen afbeelding geselecteerd")]
        [Display(Name = "Selecteer een of meerdere afbeeldingen")]
        [NotMapped]
        public IFormFileCollection Images { get; set; }
        public List<TribeImage> TribeImages { get; set; }
        public List<TribeTextBlock> TribeTextBlocks { get; set; }
        
        public List<Article> Articles { get; set; }
        
        public List<Podcast> Podcasts { get; set; }
        
        public List<Video> Videos { get; set; }
        
        public List<Rockstar> Rockstars { get; set; }
    }
}