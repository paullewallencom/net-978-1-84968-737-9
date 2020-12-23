using System;
using System.Data.Entity;

using BrewHow.Models;

namespace BrewHow.Infrastructure.Repositories
{
    public class BrewHowContext : DbContext, IBrewHowContext
    {
        public IDbSet<Recipe> Recipes { get; set; }
        public IDbSet<Review> Reviews { get; set; }
        public IDbSet<Style> Styles { get; set; }

        public BrewHowContext()
            : base("BrewHow.Models.BrewHowContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Add a foreign key to Recipe from Review 
            // to account for the new relationship.
            modelBuilder.Entity<Recipe>().HasMany(r => r.Reviews).WithRequired().Map(m => m.MapKey("RecipeId"));

            // Adjust the relationship between Style 
            // and Recipe to fix the key name.
            modelBuilder.Entity<Recipe>().HasRequired(s => s.Style).WithMany(s => s.Recipes).Map(m => m.MapKey("StyleId"));

            base.OnModelCreating(modelBuilder);
        }
    }
}