namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyEducationEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Education", "School", c => c.String());
            AlterColumn("dbo.Education", "Degree", c => c.String());
            DropColumn("dbo.Education", "Place");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Education", "Place", c => c.String());
            AlterColumn("dbo.Education", "Degree", c => c.Int(nullable: false));
            DropColumn("dbo.Education", "School");
        }
    }
}
