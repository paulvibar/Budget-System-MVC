namespace BudgetSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUACS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UACSObjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        ClassificationId = c.Int(nullable: false),
                        ClassId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UACS", "ObjectId", c => c.Int(nullable: false));
            AddColumn("dbo.UACSGroups", "ClassificationId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UACSGroups", "ClassificationId");
            DropColumn("dbo.UACS", "ObjectId");
            DropTable("dbo.UACSObjects");
        }
    }
}
