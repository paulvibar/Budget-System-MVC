namespace BudgetSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyingRC : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ResponsibilityCenters", "PAP", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ResponsibilityCenters", "PAP", c => c.String());
        }
    }
}
