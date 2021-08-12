namespace BudgetSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Identifiers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: true, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MFOPAPs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Type = c.String(),
                        Status = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: true, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ResponsibilityCenters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Acronym = c.String(),
                        PAP = c.String(),
                        Status = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: true, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ResponsibilityCenters");
            DropTable("dbo.MFOPAPs");
            DropTable("dbo.Identifiers");
        }
    }
}
