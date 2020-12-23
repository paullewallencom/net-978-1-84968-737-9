using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewHow.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public Style Style { get; set; }
        public float OriginalGravity { get; set; }
        public float FinalGravity { get; set; }
        public string GrainBill { get; set; }
        public string Instructions { get; set; }
        public string Slug { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}