namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyExperienceEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Experience", "Title", c => c.String());
            AddColumn("dbo.Experience", "Company", c => c.String());
            AddColumn("dbo.Experience", "Location", c => c.String());
            DropColumn("dbo.Experience", "Place");
            DropColumn("dbo.Experience", "Position");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Experience", "Position", c => c.String());
            AddColumn("dbo.Experience", "Place", c => c.String());
            DropColumn("dbo.Experience", "Location");
            DropColumn("dbo.Experience", "Company");
            DropColumn("dbo.Experience", "Title");
        }
    }
}
