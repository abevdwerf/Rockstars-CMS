using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class Podcast
    {
        // Fields
        private string _title;
        private string _description;
        private string _url;
        private DateTime _dateCreated;
        private DateTime _dateModified;
        private DateTime _datePublished;
        private bool _publishedStatus;
        private int _viewCount;
        private Rockstar _rockstar;
        private Tribe _tribe;
        // Properties
        public int PodcastId { get; set; }
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
        public string URL
        {
            get => _url; 
            set => _url = value;
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
        public int? RockstarId { get; set; }
        [ForeignKey("RockstarId")]
        public Rockstar Rockstar
        {
            get => _rockstar; 
            set => _rockstar = value;
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