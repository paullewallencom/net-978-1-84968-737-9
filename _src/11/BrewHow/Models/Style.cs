using System;
using System.Collections.Generic;

namespace BrewHow.Models
{
    public class Style
    {
        public int StyleId { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}