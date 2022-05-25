using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using static RockstarsIT.Controllers.RockstarsController;

namespace RockstarsIT.Models
{
    public enum Chapter
    {
        North, Central, West, East, UpperSouth, LowerSouth
    }

    public class Rockstar
    {
        private Tribe _tribe;
        private Role _role;
        private Chapter _chapter;
        private string _name;
        private string _linkedIn;
        private string _description;
        private string _quote;
        private string _img;

        public int RockstarId { get; set; }
        
        // [ForeignKey("TribeId")]
        public int TribeId { get; set; }
        public Tribe Tribe {
            get => _tribe;
            set => _tribe = value;
        }
        [ForeignKey("RoleId")]
        public int? RoleId { get; set; }
        public Role Role
        {
            get => _role;
            set => _role = value;
        }
        public Chapter Chapter
        {
            get => _chapter;
            set => _chapter = value;
        }
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string LinkedIn
        {
            get => _linkedIn;
            set => _linkedIn = value;
        }
        public string Description
        {
            get => _description;
            set => _description = value;
        }
        public string Quote
        {
            get => _quote;
            set => _quote = value;
        }
        public string IMG
        {
            get => _img; 
            set => _img = value;
        }
        [NotMapped]
        [Display(Name = "upload foto")]
        public IFormFile ImageFile { get; set; }
        
        public List<Article> Articles { get; set; }
        public List<PodcastEpisode> PodcastEpisodes { get; set; }
        public List<Video> Videos { get; set; }
    }
}