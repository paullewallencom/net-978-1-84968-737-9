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
        public IDbSet<UserProfile> UserProfiles { get; set; }

        public BrewHowContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Add a foreign key to Recipe from Review 
            // to account for the new relationship.
            modelBuilder.Entity<Recipe>().HasMany(r => r.Reviews).WithRequired().Map(m => m.MapKey("RecipeId"));

            // Adjust the relationship between Style 
            // and Recipe to fix the key name.
            modelBuilder.Entity<Recipe>()
                .HasRequired(s => s.Style)
                .WithMany(s => s.Recipes)
                .Map(m => m.MapKey("StyleId"));

            modelBuilder.Entity<Recipe>()
                .HasRequired(s => s.Contributor)
                .WithMany()
                .Map(m => m.MapKey("ContributorUserId"));

            modelBuilder.Entity<Review>()
                .HasRequired(r => r.Reviewer)
                .WithMany()
                .Map(m => m.MapKey("ReviewerUserId"));

            modelBuilder.Entity<UserProfile>()
                .HasMany(r => r.Library)
                .WithMany()
                .Map(
                m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("RecipeId");
                    m.ToTable("UserRecipeLibrary");
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}