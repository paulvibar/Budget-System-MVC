namespace BudgetSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ORSMainInformations", "Date", c => c.DateTime(nullable: true, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ORSMainInformations", "Date", c => c.DateTime(nullable: false));
        }
    }
}
