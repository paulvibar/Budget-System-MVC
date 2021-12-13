namespace BudgetSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyingors : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ORSDetailsInformations", "ORSId");
            AddForeignKey("dbo.ORSDetailsInformations", "ORSId", "dbo.ORSMainInformations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ORSDetailsInformations", "ORSId", "dbo.ORSMainInformations");
            DropIndex("dbo.ORSDetailsInformations", new[] { "ORSId" });
        }
    }
}
