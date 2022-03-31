﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class Article
    {
        private Rockstar _rockstar;
        private string title;
        private string description;
        private string author;
        private string text;
        [Display(Name = "Selecteer een of meerdere afbeldingen")]
        [NotMapped]
        public IFormFileCollection Images { get; set; }

        public List<ArticleImages> articleImages { get; set; }

        public int ArticleId { get; set; }
        public int? RockstarId { get; set; }
        [ForeignKey("RockstarId")]
        public virtual Rockstar Rockstar
        {
            get => _rockstar;
            set => _rockstar = value;
        }
        public string Title { get { return title; } set { title = value; } }
        public string Description { get { return description; } set { description = value; } }
        public string Author { get { return author; } set { author = value; } }
        public string Text { get { return text; } set { text = value; } }
    }
}