namespace BudgetSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editeddate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ORSMainInformations", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ORSMainInformations", "Date", c => c.DateTime());
        }
    }
}
