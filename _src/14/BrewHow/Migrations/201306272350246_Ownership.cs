namespace BrewHow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ownership : DbMigration
    {
        public override void Up()
        {

            AddColumn("dbo.Recipes", "ContributorUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Reviews", "ReviewerUserId", c => c.Int(nullable: false));

            Sql("Update dbo.Recipes Set ContributorUserId = (select UserId from dbo.UserProfile where username = 'brewmaster')");
            Sql("Update dbo.Reviews Set ReviewerUserId = (select UserId from dbo.UserProfile where username = 'brewmaster')");

            AddForeignKey("dbo.Recipes", "ContributorUserId", "dbo.UserProfile", "UserId", cascadeDelete: false); // was true.  Both of them.
            AddForeignKey("dbo.Reviews", "ReviewerUserId", "dbo.UserProfile", "UserId", cascadeDelete: false);
            CreateIndex("dbo.Recipes", "ContributorUserId");
            CreateIndex("dbo.Reviews", "ReviewerUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reviews", new[] { "ReviewerUserId" });
            DropIndex("dbo.Recipes", new[] { "ContributorUserId" });
            DropForeignKey("dbo.Reviews", "ReviewerUserId", "dbo.UserProfile");
            DropForeignKey("dbo.Recipes", "ContributorUserId", "dbo.UserProfile");
            DropColumn("dbo.Reviews", "ReviewerUserId");
            DropColumn("dbo.Recipes", "ContributorUserId");
        }
    }
}
