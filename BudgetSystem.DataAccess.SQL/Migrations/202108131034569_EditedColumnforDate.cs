namespace BudgetSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedColumnforDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ORSMainInformations", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ORSMainInformations", "Date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
