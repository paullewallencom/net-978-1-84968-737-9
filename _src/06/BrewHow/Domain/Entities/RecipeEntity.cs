using System;

namespace BrewHow.Domain.Entities
{
    public class RecipeEntity
    {
        public RecipeEntity()
        {
            this.Style = new StyleEntity();
        }

        public int RecipeId { get; set; }
        public string Name { get; set; }
        public StyleEntity Style { get; set; }
        public float OriginalGravity { get; set; }
        public float FinalGravity { get; set; }
        public string GrainBill { get; set; }
        public string Instructions { get; set; }

        public float PercentAlcoholByVolume
        {
            get 
            { 
                return (this.OriginalGravity - this.FinalGravity) * 131.25f; 
            }
        }
    }
}