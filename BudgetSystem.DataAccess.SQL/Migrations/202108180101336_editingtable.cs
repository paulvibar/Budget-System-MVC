namespace BudgetSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editingtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ORSDetailsInformations", "ORSMainInformation_Id", c => c.Int());
            CreateIndex("dbo.ORSDetailsInformations", "ORSMainInformation_Id");
            AddForeignKey("dbo.ORSDetailsInformations", "ORSMainInformation_Id", "dbo.ORSMainInformations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ORSDetailsInformations", "ORSMainInformation_Id", "dbo.ORSMainInformations");
            DropIndex("dbo.ORSDetailsInformations", new[] { "ORSMainInformation_Id" });
            DropColumn("dbo.ORSDetailsInformations", "ORSMainInformation_Id");
        }
    }
}
