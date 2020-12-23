using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewHow.Models
{
    public class Style
    {
        public int StyleId { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}