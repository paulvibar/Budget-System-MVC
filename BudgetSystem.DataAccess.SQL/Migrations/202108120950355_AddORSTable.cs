namespace BudgetSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddORSTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ORSDetailsInformations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ORSId = c.Int(nullable: false),
                        RCId = c.Int(nullable: false),
                        PAPId = c.Int(nullable: false),
                        UACSId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ORSMainInformations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        AllotmentCode = c.String(),
                        FundSource = c.String(),
                        FundCluster = c.String(),
                        Payee = c.String(),
                        Office = c.String(),
                        Address = c.String(),
                        Particulars = c.String(),
                        BoxASignatory = c.String(),
                        BoxAPosition = c.String(),
                        BoxBSignatory = c.String(),
                        BoxBPosition = c.String(),
                        Processor = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ORSMainInformations");
            DropTable("dbo.ORSDetailsInformations");
        }
    }
}
