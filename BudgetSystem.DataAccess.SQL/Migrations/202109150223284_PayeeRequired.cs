namespace BudgetSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PayeeRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ORSMainInformations", "Payee", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ORSMainInformations", "Payee", c => c.String());
        }
    }
}
