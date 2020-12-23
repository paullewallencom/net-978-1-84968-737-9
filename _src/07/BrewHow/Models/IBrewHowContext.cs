using System;
using System.Data.Entity;

namespace BrewHow.Models
{
    public interface IBrewHowContext
    {
        IDbSet<Recipe> Recipes { get; set; }
        IDbSet<Review> Reviews { get; set; }
        IDbSet<Style> Styles { get; set; }

        int SaveChanges();
    }
}
