namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateValidationToUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Position", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Country", c => c.String());
            AlterColumn("dbo.AspNetUsers", "School", c => c.String());
            AlterColumn("dbo.AspNetUsers", "University", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Bio", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Bio", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "University", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "School", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Position", c => c.String(nullable: false));
        }
    }
}
