namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidationToSkillAndEducation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Education", "School", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Education", "Degree", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Skill", "Name", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Skill", "Name", c => c.String());
            AlterColumn("dbo.Education", "Degree", c => c.String());
            AlterColumn("dbo.Education", "School", c => c.String());
        }
    }
}
