namespace BrewHow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using BrewHow.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<BrewHow.Models.BrewHowContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BrewHow.Models.BrewHowContext context)
        {
            var brownAle = new Style { Name = "Brown Ale", Category = Models.Category.Ale };
            var milkStout = new Style { Name = "Sweet/Milk Stout", Category = Models.Category.Ale };
            var heffeweisen = new Style { Name = "Heffeweisen", Category = Models.Category.Ale };

            context.Styles.AddOrUpdate(
                style => style.Name,
                brownAle,
                milkStout,
                heffeweisen
            );

            context.Recipes.AddOrUpdate(
                recipe => recipe.Name,
                new Recipe { Name = "Sweaty Brown Ale", Style = brownAle, OriginalGravity = 1.05f, FinalGravity = 1.01f },
                new Recipe { Name = "Festive Milk Stout", Style = milkStout, OriginalGravity = 1.058f, FinalGravity = 1.015f },
                new Recipe { Name = "Andy's Heffy", Style = heffeweisen, OriginalGravity = 1.045f, FinalGravity = 1.012f }
            );
        }
    }
}
