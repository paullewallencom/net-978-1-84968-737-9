namespace BrewHow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using BrewHow.Infrastructure.Repositories;
    using BrewHow.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<BrewHowContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BrewHowContext context)
        {
            var brownAle = new Style { Name = "Brown Ale", Category = Category.Ale, Slug = "brown-ale" };
            var milkStout = new Style { Name = "Sweet/Milk Stout", Category = Category.Ale, Slug = "sweet-milk-stout" };
            var heffeweisen = new Style { Name = "Heffeweisen", Category = Category.Ale, Slug = "heffeweisen" };

            context.Styles.AddOrUpdate(
                style => style.Name,
                brownAle,
                milkStout,
                heffeweisen
            );

            context.Recipes.AddOrUpdate(
                recipe => recipe.Name,
                new Recipe { Name = "Sweaty Brown Ale", Style = brownAle, OriginalGravity = 1.05f, FinalGravity = 1.01f, Slug = "sweaty-brown-ale" },
                new Recipe { Name = "Festive Milk Stout", Style = milkStout, OriginalGravity = 1.058f, FinalGravity = 1.015f, Slug = "festive-milk-stout" },
                new Recipe { Name = "Andy's Heffy", Style = heffeweisen, OriginalGravity = 1.045f, FinalGravity = 1.012f, Slug = "andys-heffy" }
            );
        }
    }
}
