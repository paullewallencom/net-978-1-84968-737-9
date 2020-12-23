namespace BrewHow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reviews : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Recipes", "Style_StyleId", "dbo.Styles");
            DropIndex("dbo.Recipes", new[] { "Style_StyleId" });
            RenameColumn(table: "dbo.Recipes", name: "Style_StyleId", newName: "StyleId");
            AddColumn("dbo.Reviews", "RecipeId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Recipes", "StyleId", "dbo.Styles", "StyleId", cascadeDelete: false);
            AddForeignKey("dbo.Reviews", "RecipeId", "dbo.Recipes", "RecipeId", cascadeDelete: false);
            CreateIndex("dbo.Recipes", "StyleId");
            CreateIndex("dbo.Reviews", "RecipeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reviews", new[] { "RecipeId" });
            DropIndex("dbo.Recipes", new[] { "StyleId" });
            DropForeignKey("dbo.Reviews", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Recipes", "StyleId", "dbo.Styles");
            DropColumn("dbo.Reviews", "RecipeId");
            RenameColumn(table: "dbo.Recipes", name: "StyleId", newName: "Style_StyleId");
            CreateIndex("dbo.Recipes", "Style_StyleId");
            AddForeignKey("dbo.Recipes", "Style_StyleId", "dbo.Styles", "StyleId");
        }
    }
}
