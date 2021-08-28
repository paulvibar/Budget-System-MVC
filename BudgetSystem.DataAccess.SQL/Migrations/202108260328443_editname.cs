namespace BudgetSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editname : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ORSMainInformations", "Payee", c => c.String(nullable: false));
            AlterColumn("dbo.ORSMainInformations", "Particulars", c => c.String(nullable: false));
            AlterColumn("dbo.ORSMainInformations", "BoxASignatory", c => c.String(nullable: false));
            AlterColumn("dbo.ORSMainInformations", "BoxAPosition", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ORSMainInformations", "BoxAPosition", c => c.String());
            AlterColumn("dbo.ORSMainInformations", "BoxASignatory", c => c.String());
            AlterColumn("dbo.ORSMainInformations", "Particulars", c => c.String());
            AlterColumn("dbo.ORSMainInformations", "Payee", c => c.String());
        }
    }
}
