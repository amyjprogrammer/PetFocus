namespace PetFocus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HomemadeFood",
                c => new
                    {
                        HomemadeFoodId = c.Int(nullable: false, identity: true),
                        Ingredient = c.String(nullable: false),
                        Quantity = c.String(nullable: false),
                        Notes = c.String(),
                        IsStillUsed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.HomemadeFoodId);
            
            CreateTable(
                "dbo.Pet",
                c => new
                    {
                        PetId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        PetName = c.String(nullable: false, maxLength: 100),
                        Species = c.Int(nullable: false),
                        PetSex = c.Int(nullable: false),
                        IsSpayedNeutered = c.Boolean(nullable: false),
                        Breed = c.String(nullable: false),
                        Birthdate = c.DateTimeOffset(nullable: false, precision: 7),
                        MicrochipNum = c.String(),
                        VetName = c.String(),
                    })
                .PrimaryKey(t => t.PetId);
            
            CreateTable(
                "dbo.Reminder",
                c => new
                    {
                        PetId = c.Int(nullable: false),
                        HeartwormMed = c.DateTime(nullable: false),
                        RabiesVac = c.DateTime(nullable: false),
                        IsThreeYearRabiesVac = c.Boolean(nullable: false),
                        FleaTreatment = c.DateTime(nullable: false),
                        NailTrim = c.DateTime(nullable: false),
                        TrimSchedule = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PetId)
                .ForeignKey("dbo.Pet", t => t.PetId)
                .Index(t => t.PetId);
            
            CreateTable(
                "dbo.Weight",
                c => new
                    {
                        WeightId = c.Int(nullable: false, identity: true),
                        PetId = c.Int(nullable: false),
                        PetWeight = c.Double(nullable: false),
                        WeightDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.WeightId)
                .ForeignKey("dbo.Pet", t => t.PetId, cascadeDelete: true)
                .Index(t => t.PetId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.PetHomemade",
                c => new
                    {
                        PetId = c.Int(nullable: false),
                        HomemadeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PetId, t.HomemadeId })
                .ForeignKey("dbo.Pet", t => t.PetId, cascadeDelete: true)
                .ForeignKey("dbo.HomemadeFood", t => t.HomemadeId, cascadeDelete: true)
                .Index(t => t.PetId)
                .Index(t => t.HomemadeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Weight", "PetId", "dbo.Pet");
            DropForeignKey("dbo.Reminder", "PetId", "dbo.Pet");
            DropForeignKey("dbo.PetHomemade", "HomemadeId", "dbo.HomemadeFood");
            DropForeignKey("dbo.PetHomemade", "PetId", "dbo.Pet");
            DropIndex("dbo.PetHomemade", new[] { "HomemadeId" });
            DropIndex("dbo.PetHomemade", new[] { "PetId" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Weight", new[] { "PetId" });
            DropIndex("dbo.Reminder", new[] { "PetId" });
            DropTable("dbo.PetHomemade");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Weight");
            DropTable("dbo.Reminder");
            DropTable("dbo.Pet");
            DropTable("dbo.HomemadeFood");
        }
    }
}
