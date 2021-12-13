namespace BudgetSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rcedit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ResponsibilityCenters", "MFOPAP_Id", "dbo.MFOPAPs");
            DropIndex("dbo.ResponsibilityCenters", new[] { "MFOPAP_Id" });
            DropColumn("dbo.ResponsibilityCenters", "MFOPAP_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ResponsibilityCenters", "MFOPAP_Id", c => c.Int());
            CreateIndex("dbo.ResponsibilityCenters", "MFOPAP_Id");
            AddForeignKey("dbo.ResponsibilityCenters", "MFOPAP_Id", "dbo.MFOPAPs", "Id");
        }
    }
}
