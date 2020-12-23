using System;
using System.Data.Entity;

namespace BrewHow.Models
{
    public class BrewHowContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Style> Styles { get; set; }

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