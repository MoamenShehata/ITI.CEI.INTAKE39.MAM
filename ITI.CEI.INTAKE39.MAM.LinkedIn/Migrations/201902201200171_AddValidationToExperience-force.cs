namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidationToExperienceforce : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Experience", "Company", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Experience", "Location", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Experience", "Location", c => c.String());
            AlterColumn("dbo.Experience", "Company", c => c.String());
        }
    }
}
