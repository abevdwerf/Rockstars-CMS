using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockstarsIT.Models
{
    public class Tag
    {
        private string _name;
        private int TagId { get; set; }

        public string Name
        {
            get => _name;
            set => _name = value;
        }
    }
}
