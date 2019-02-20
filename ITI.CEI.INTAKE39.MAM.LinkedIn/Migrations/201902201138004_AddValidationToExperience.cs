namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidationToExperience : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Experience", "Title", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Experience", "Title", c => c.String());
        }
    }
}
