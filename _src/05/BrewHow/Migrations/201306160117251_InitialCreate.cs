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
                        Name = c.String(),
                        OriginalGravity = c.Single(nullable: false),
                        FinalGravity = c.Single(nullable: false),
                        GrainBill = c.String(),
                        Instructions = c.String(),
                        Style_StyleId = c.Int(),
                    })
                .PrimaryKey(t => t.RecipeId)
                .ForeignKey("dbo.Styles", t => t.Style_StyleId)
                .Index(t => t.Style_StyleId);
            
            CreateTable(
                "dbo.Styles",
                c => new
                    {
                        StyleId = c.Int(nullable: false, identity: true),
                        Category = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StyleId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.ReviewId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Recipes", new[] { "Style_StyleId" });
            DropForeignKey("dbo.Recipes", "Style_StyleId", "dbo.Styles");
            DropTable("dbo.Reviews");
            DropTable("dbo.Styles");
            DropTable("dbo.Recipes");
        }
    }
}
