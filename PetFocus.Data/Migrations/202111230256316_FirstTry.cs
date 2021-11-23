namespace PetFocus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstTry : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Reminder", name: "PetId", newName: "ReminderId");
            RenameIndex(table: "dbo.Reminder", name: "IX_PetId", newName: "IX_ReminderId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Reminder", name: "IX_ReminderId", newName: "IX_PetId");
            RenameColumn(table: "dbo.Reminder", name: "ReminderId", newName: "PetId");
        }
    }
}
