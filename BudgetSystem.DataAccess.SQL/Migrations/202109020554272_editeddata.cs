namespace BudgetSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editeddata : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ORSMainInformations", "Payee", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ORSMainInformations", "Payee", c => c.String(nullable: false));
        }
    }
}
