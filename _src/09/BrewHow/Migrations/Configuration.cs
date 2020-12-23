using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Security;
using BrewHow.Infrastructure.Repositories;
using BrewHow.Models;
using WebMatrix.WebData;

namespace BrewHow.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BrewHowContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BrewHowContext context)
        {
            SeedMembership(context);
            SeedContent(context);
        }

        private void SeedContent(BrewHowContext context)
        {
            var brewMaster = context.UserProfiles.First(u => u.UserName == "brewmaster");

            var brownAle = new Style { Name = "Brown Ale", Category = Category.Ale, Slug = "brown-ale" };
            var milkStout = new Style { Name = "Sweet/Milk Stout", Category = Category.Ale, Slug = "sweet-milk-stout" };
            var heffeweisen = new Style { Name = "Heffeweisen", Category = Category.Ale, Slug = "heffeweisen" };

            context.Styles.AddOrUpdate(
                style => style.Name,
                brownAle,
                milkStout,
                heffeweisen
            );

            var sweatyBrownAle = new Recipe { Name = "Sweaty Brown Ale", Style = brownAle, OriginalGravity = 1.05f, FinalGravity = 1.01f, Slug = "sweaty-brown-ale", Instructions = "None", GrainBill = "None", Contributor = brewMaster };
            var festiveMilkStout = new Recipe { Name = "Festive Milk Stout", Style = milkStout, OriginalGravity = 1.058f, FinalGravity = 1.015f, Slug = "festive-milk-stout", Instructions = "None", GrainBill = "None", Contributor = brewMaster };
            var andysHeffy = new Recipe { Name = "Andy's Heffy", Style = heffeweisen, OriginalGravity = 1.045f, FinalGravity = 1.012f, Slug = "andys-heffy", Instructions = "None", GrainBill = "None", Contributor = brewMaster };

            context.Recipes.AddOrUpdate(
                recipe => recipe.Name,
                sweatyBrownAle,
                festiveMilkStout,
                andysHeffy
            );

            brewMaster.Library.Add(sweatyBrownAle);
            brewMaster.Library.Add(festiveMilkStout);
        }

        private void SeedMembership(BrewHowContext context)
        {
            WebSecurity.InitializeDatabaseConnection(
                "DefaultConnection",
                "UserProfile",
                "UserId",
                "UserName",
                autoCreateTables: true);

            var membership = (SimpleMembershipProvider)
                Membership.Provider;

            if (membership.GetUser("brewmaster", false) == null)
            {
                membership
                    .CreateUserAndAccount(
                        "brewmaster",
                        "supersecret"
                    );
            }
        }
    }
}
