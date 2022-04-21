using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class Article
    {
        private Rockstar _rockstar;
        private string _title;
        private string _description;
        private string _text;
        private Tribe _tribe;
        private DateTime _dateCreated;
        private DateTime _dateModified;
        private DateTime _datePublished;
        private bool _publishedStatus;
        private int _viewCount;

        public int ArticleId { get; set; }
        public List<ArticleImages> ArticleImages { get; set; }
        public int? RockstarId { get; set; }
        [ForeignKey("RockstarId")]
        public virtual Rockstar Rockstar
        {
            get => _rockstar;
            set => _rockstar = value;
        }
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
        public string Text
        {
            get => _text;
            set => _text = value;
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

        //[Required]
        [Display(Name = "Selecteer een of meerdere afbeeldingen")]
        [NotMapped]
        public IFormFileCollection Images { get; set; }
    }
}