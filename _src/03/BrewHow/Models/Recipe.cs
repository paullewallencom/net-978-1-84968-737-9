using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewHow.Models
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Style { get; set; }
        public float OriginalGravity { get; set; }
        public float FinalGravity { get; set; }
    }
}