namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEducationToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Education",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Place = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Degree = c.Int(nullable: false),
                        FK_LinkedInUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FK_LinkedInUserId)
                .Index(t => t.FK_LinkedInUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Education", "FK_LinkedInUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Education", new[] { "FK_LinkedInUserId" });
            DropTable("dbo.Education");
        }
    }
}
