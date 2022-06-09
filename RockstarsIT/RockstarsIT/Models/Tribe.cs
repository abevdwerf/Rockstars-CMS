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
        private Tag _tag;
        public List<TribeImages> TribeImages { get; set; }
        public List<TribeTextBlock> TribeTextBlocks { get; set; }


        public int TribeId { get; set; }

        private int? TagId { get; set; }
        [ForeignKey(name: "TagId")]
        public  virtual Tag tag
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
    }
}