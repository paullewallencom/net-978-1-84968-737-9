using System;
using System.Text.RegularExpressions;

namespace BrewHow.Domain.Entities
{
    public class RecipeEntity
    {
        private string _slug;

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

        /// <summary>
        /// Gets or Sets the slug for the recipe. A slug 
        /// can be set once and only once.  If it is not
        /// set implicitly, it is set upon first retrieval
        /// from the recipe’s name.
        /// </summary>
        public string Slug
        {
            get
            {
                if (string.IsNullOrEmpty(this._slug))
                {
                    if (string.IsNullOrEmpty(this.Name))
                    {
                        return string.Empty;
                    }

                    this._slug = Regex.Replace(this.Name.ToLower().Trim(), "[^a-z0-9-]", "-");
                }

                return this._slug;
            }

            set
            {
                if (!string.IsNullOrEmpty(this._slug))
                {
                    throw new InvalidOperationException(
                        "The slug for the recipe has already been set.");
                }

                this._slug = value;
            }
        }
    }
}