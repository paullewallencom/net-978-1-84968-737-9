namespace BrewHow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecipeValidation : DbMigration
    {
        public override void Up()
        {
            Sql("update dbo.Recipes set GrainBill = 'No grain bill.' where GrainBill is null");
            Sql("update dbo.Recipes set Instructions = 'No instructions.' where Instructions is null");

            AlterColumn("dbo.Recipes", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Recipes", "GrainBill", c => c.String(nullable: false));
            AlterColumn("dbo.Recipes", "Instructions", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipes", "Instructions", c => c.String());
            AlterColumn("dbo.Recipes", "GrainBill", c => c.String());
            AlterColumn("dbo.Recipes", "Name", c => c.String());
        }
    }
}
