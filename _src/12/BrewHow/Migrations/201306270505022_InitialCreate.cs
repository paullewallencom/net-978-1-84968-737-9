namespace BrewHow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        RecipeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        OriginalGravity = c.Single(nullable: false),
                        FinalGravity = c.Single(nullable: false),
                        GrainBill = c.String(nullable: false),
                        Instructions = c.String(nullable: false),
                        Slug = c.String(),
                        StyleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeId)
                .ForeignKey("dbo.Styles", t => t.StyleId, cascadeDelete: true)
                .Index(t => t.StyleId);
            
            CreateTable(
                "dbo.Styles",
                c => new
                    {
                        StyleId = c.Int(nullable: false, identity: true),
                        Category = c.Int(nullable: false),
                        Name = c.String(),
                        Slug = c.String(),
                    })
                .PrimaryKey(t => t.StyleId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        Comment = c.String(),
                        RecipeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reviews", new[] { "RecipeId" });
            DropIndex("dbo.Recipes", new[] { "StyleId" });
            DropForeignKey("dbo.Reviews", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Recipes", "StyleId", "dbo.Styles");
            DropTable("dbo.UserProfile");
            DropTable("dbo.Reviews");
            DropTable("dbo.Styles");
            DropTable("dbo.Recipes");
        }
    }
}
