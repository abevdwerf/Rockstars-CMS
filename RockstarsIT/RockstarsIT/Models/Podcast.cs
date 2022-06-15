using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class Podcast
    {
        // Fields
        private string _title;
        private string _description;
        private string _url;
        private Tribe _tribe;
        private DateTime _dateCreated;
        private DateTime _dateModified;
        private DateTime _datePublished;
        private bool _publishedStatus;
        private int _viewCount;
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
        public int? TribeId { get; set; }
        [ForeignKey("TribeId")]
        public Tribe Tribe
        {
            get => _tribe; 
            set => _tribe = value;
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

        public List<PodcastEpisode> PodcastEpisodes;

        // Methods
    }
}