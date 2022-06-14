using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public enum LinkType
    {
        Youtube, Vimeo
    }
    public class Video
    {
        private string _link;
        private LinkType _linkType;
        private DateTime _dateCreated;
        private DateTime _dateModified;
        private DateTime _datePublished;
        private bool _publishedStatus;
        private int _viewCount;
        private Tribe _tribe;
        private Rockstar _rockstar;

        [Key]
        public int VideoId { get; set; }

        public string Link
        {
            get => _link; 
            set => _link = value;
        }
        public LinkType LinkType
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
        [ForeignKey("TribeId")]
        public virtual Tribe Tribe
        {
            get => _tribe;
            set => _tribe = value;
        }
        public int? RockstarId { get; set; }
        [ForeignKey("RockstarId")]
        public virtual Rockstar Rockstar
        {
            get => _rockstar;
            set => _rockstar = value;
        }

        public List<VideoContent> VideoContents { get; set; }

        [NotMapped]
        public int VideoContentId { get; set; }
        [NotMapped]
        public string Title { get; set; }
        [NotMapped]
        public string Description { get; set; }
    }
}
