namespace PetFocus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Diabetes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diabetes",
                c => new
                    {
                        DiabetesId = c.Int(nullable: false, identity: true),
                        PetId = c.Int(nullable: false),
                        Glucose = c.Double(nullable: false),
                        DiabetesDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DiabetesId)
                .ForeignKey("dbo.Pet", t => t.PetId, cascadeDelete: true)
                .Index(t => t.PetId);
            
            AddColumn("dbo.Pet", "HasDiabetes", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Diabetes", "PetId", "dbo.Pet");
            DropIndex("dbo.Diabetes", new[] { "PetId" });
            DropColumn("dbo.Pet", "HasDiabetes");
            DropTable("dbo.Diabetes");
        }
    }
}
