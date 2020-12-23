namespace BrewHow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Slugs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "Slug", c => c.String());
            AddColumn("dbo.Styles", "Slug", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Styles", "Slug");
            DropColumn("dbo.Recipes", "Slug");
        }
    }
}
