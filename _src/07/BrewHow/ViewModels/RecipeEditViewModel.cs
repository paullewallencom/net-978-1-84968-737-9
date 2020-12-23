using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BrewHow.ViewModels
{
    /// <summary>
    /// This ViewModel is used by views wishing
    /// to perform edits on a recipe entity.
    /// </summary>
    public class RecipeEditViewModel
    {
        [Key]
        public int RecipeId { get; set; }

        [Display(Name="Style")]
        public int StyleId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Original Gravity")]
        [DisplayFormat(DataFormatString = "{0:0.00##}")]
        public float OriginalGravity { get; set; }

        [Display(Name = "Final Gravity")]
        [DisplayFormat(DataFormatString = "{0:0.00##}")]
        public float FinalGravity { get; set; }

        [Display(Name = "Grain Bill")]
        [DataType(DataType.MultilineText)]
        public string GrainBill { get; set; }

        [Display(Name = "Instructions")]
        [DataType(DataType.MultilineText)]
        public string Instructions { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Slug { get; set; }

        public SelectList StyleList { get; set; }
    }
}