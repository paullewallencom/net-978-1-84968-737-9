using System;
using System.ComponentModel.DataAnnotations;

namespace BrewHow.Validations
{
    public class RecipeValidationMetadata
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [Range(1.0f, 2.0f)]
        public float OriginalGravity { get; set; }

        [Required]
        [Range(1.0f, 2.0f)]
        public float FinalGravity { get; set; }

        [Required]
        public string GrainBill { get; set; }

        [Required]
        public string Instructions { get; set; }
    }
}