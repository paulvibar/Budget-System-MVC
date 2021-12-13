namespace BudgetSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyorsdetails : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ORSDetailsInformations", "PAPId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ORSDetailsInformations", "PAPId", c => c.Int(nullable: false));
        }
    }
}
