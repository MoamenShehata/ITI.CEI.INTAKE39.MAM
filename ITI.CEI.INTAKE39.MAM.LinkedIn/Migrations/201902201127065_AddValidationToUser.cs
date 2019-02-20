namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidationToUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "FName", c => c.String(nullable: true, maxLength: 30));
            AlterColumn("dbo.AspNetUsers", "LName", c => c.String(nullable: true, maxLength: 30));
            AlterColumn("dbo.AspNetUsers", "Position", c => c.String(nullable: true));
            AlterColumn("dbo.AspNetUsers", "Country", c => c.String(nullable: true));
            AlterColumn("dbo.AspNetUsers", "School", c => c.String(nullable: true));
            AlterColumn("dbo.AspNetUsers", "University", c => c.String(nullable: true));
            AlterColumn("dbo.AspNetUsers", "Bio", c => c.String(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Bio", c => c.String());
            AlterColumn("dbo.AspNetUsers", "University", c => c.String());
            AlterColumn("dbo.AspNetUsers", "School", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Country", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Position", c => c.String());
            AlterColumn("dbo.AspNetUsers", "LName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "FName", c => c.String());
        }
    }
}
