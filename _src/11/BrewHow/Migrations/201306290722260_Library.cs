namespace BrewHow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Library : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRecipeLibrary",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RecipeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RecipeId })
                .ForeignKey("dbo.UserProfile", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RecipeId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserRecipeLibrary", new[] { "RecipeId" });
            DropIndex("dbo.UserRecipeLibrary", new[] { "UserId" });
            DropForeignKey("dbo.UserRecipeLibrary", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.UserRecipeLibrary", "UserId", "dbo.UserProfile");
            DropTable("dbo.UserRecipeLibrary");
        }
    }
}
