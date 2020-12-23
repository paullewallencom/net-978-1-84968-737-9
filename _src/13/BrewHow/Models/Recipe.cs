using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BrewHow.Validations;

namespace BrewHow.Models
{
    [MetadataType(typeof(RecipeValidationMetadata))]
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

        public UserProfile Contributor { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}