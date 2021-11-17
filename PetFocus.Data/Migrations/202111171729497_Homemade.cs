namespace PetFocus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Homemade : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HomemadeList",
                c => new
                    {
                        HomemadeListId = c.Int(nullable: false, identity: true),
                        Ingredient = c.String(nullable: false),
                        Quantity = c.String(nullable: false),
                        HomemadeFood_HomemadeFoodId = c.Int(),
                    })
                .PrimaryKey(t => t.HomemadeListId)
                .ForeignKey("dbo.HomemadeFood", t => t.HomemadeFood_HomemadeFoodId)
                .Index(t => t.HomemadeFood_HomemadeFoodId);
            
            DropColumn("dbo.HomemadeFood", "Ingredient");
            DropColumn("dbo.HomemadeFood", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HomemadeFood", "Quantity", c => c.String(nullable: false));
            AddColumn("dbo.HomemadeFood", "Ingredient", c => c.String(nullable: false));
            DropForeignKey("dbo.HomemadeList", "HomemadeFood_HomemadeFoodId", "dbo.HomemadeFood");
            DropIndex("dbo.HomemadeList", new[] { "HomemadeFood_HomemadeFoodId" });
            DropTable("dbo.HomemadeList");
        }
    }
}
