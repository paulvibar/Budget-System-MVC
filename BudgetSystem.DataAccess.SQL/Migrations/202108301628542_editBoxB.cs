namespace BudgetSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editBoxB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ORSMainInformations", "BoxBID", c => c.Int(nullable: false));
            DropColumn("dbo.ORSMainInformations", "BoxBSignatory");
            DropColumn("dbo.ORSMainInformations", "BoxBPosition");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ORSMainInformations", "BoxBPosition", c => c.String());
            AddColumn("dbo.ORSMainInformations", "BoxBSignatory", c => c.String());
            DropColumn("dbo.ORSMainInformations", "BoxBID");
        }
    }
}
