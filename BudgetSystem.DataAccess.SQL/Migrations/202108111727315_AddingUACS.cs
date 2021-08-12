namespace BudgetSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUACS : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Identifiers", "CreatedAt");
            DropColumn("dbo.MFOPAPs", "CreatedAt");
            DropColumn("dbo.ResponsibilityCenters", "CreatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ResponsibilityCenters", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.MFOPAPs", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Identifiers", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
