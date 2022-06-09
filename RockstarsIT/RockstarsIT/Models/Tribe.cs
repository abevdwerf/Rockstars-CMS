using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockstarsIT.Models
{
    public class Tribe
    {
        private string _name;
        private string _description;
        private bool _publishedStatus;
        private string _spotify;
        private string _leadaddress;
        private int _blocknumber;
        private int _imagenumber;
        private DateTime _datePublished;

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
        public bool PublishedStatus
        {
            get => _publishedStatus;
            set => _publishedStatus = value;
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

        public int ImageNumber
        {
            get => _imagenumber;
            set => _imagenumber = value;

        }

        public string LeadAddress
        {
            get => _leadaddress;
            set => _leadaddress = value;
        }

        public int BlockNumber
        {
            get => _blocknumber;
            set => _blocknumber = value;
        }

        public int DatePublished
        {
            get => _datePublished;
            set => _datePublished = value;

        }
    }
}