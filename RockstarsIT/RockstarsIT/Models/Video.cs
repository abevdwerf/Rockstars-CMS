using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RockstarsIT.Controllers.VideoController;

namespace RockstarsIT.Models
{
    public class Video
    {
        private string _title;
        private string _description;
        private string _link;
        private string _linkType;
        private DateTime _dateCreated;
        private DateTime _dateModified;
        private DateTime _datePublished;
        private bool _publishedStatus;
        private int _viewCount;
        private Tribe _tribe;
        private Rockstar _rockstar;

        [Key]
        public int VideoId { get; set; }

        [Required]
        public string Title
        {
            get => _title; 
            set => _title = value;
        }

        public string Description
        {
            get => _description; 
            set => _description = value;
        }

        public string Link
        {
            get => _link; 
            set => _link = value;
        }
        
        public string LinkType
        {
            get => _linkType; 
            set => _linkType = value;
        }
        public DateTime DateCreated
        {
            get => _dateCreated;
            set => _dateCreated = value;
        }
        public DateTime DateModified
        {
            get => _dateModified;
            set => _dateModified = value;
        }
        public DateTime DatePublished
        {
            get => _datePublished;
            set => _datePublished = value;
        }
        public bool PublishedStatus
        {
            get => _publishedStatus;
            set => _publishedStatus = value;
        }
        public int ViewCount
        {
            get => _viewCount; 
            set => _viewCount = value;
        }
        public int? TribeId { get; set; }
        public Tribe Tribe
        {
            get => _tribe;
            set => _tribe = value;
        }
        public int RockstarId { get; set; }
        public Rockstar Rockstar
        {
            get => _rockstar;
            set => _rockstar = value;
        }
    }
}
