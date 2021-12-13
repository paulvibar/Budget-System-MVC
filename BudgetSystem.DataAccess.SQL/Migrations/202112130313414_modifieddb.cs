namespace BudgetSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifieddb : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ResponsibilityCenters", "PAP");
            AddForeignKey("dbo.ResponsibilityCenters", "PAP", "dbo.MFOPAPs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResponsibilityCenters", "PAP", "dbo.MFOPAPs");
            DropIndex("dbo.ResponsibilityCenters", new[] { "PAP" });
        }
    }
}
