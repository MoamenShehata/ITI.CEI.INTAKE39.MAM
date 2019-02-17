namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAllEntitiesToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        FK_LinkedInUserId = c.String(maxLength: 128),
                        FK_PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FK_LinkedInUserId)
                .ForeignKey("dbo.Post", t => t.FK_PostId, cascadeDelete: true)
                .Index(t => t.FK_LinkedInUserId)
                .Index(t => t.FK_PostId);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        MediaURL = c.String(),
                        FK_LinkedInUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FK_LinkedInUserId)
                .Index(t => t.FK_LinkedInUserId);
            
            CreateTable(
                "dbo.Like",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FK_LinkedInUserId = c.String(maxLength: 128),
                        FK_PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FK_LinkedInUserId)
                .ForeignKey("dbo.Post", t => t.FK_PostId, cascadeDelete: true)
                .Index(t => t.FK_LinkedInUserId)
                .Index(t => t.FK_PostId);
            
            CreateTable(
                "dbo.Experience",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Place = c.String(),
                        Position = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        FK_LinkedInUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FK_LinkedInUserId)
                .Index(t => t.FK_LinkedInUserId);
            
            CreateTable(
                "dbo.Skill",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FK_LinkedInUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FK_LinkedInUserId)
                .Index(t => t.FK_LinkedInUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skill", "FK_LinkedInUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Experience", "FK_LinkedInUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comment", "FK_PostId", "dbo.Post");
            DropForeignKey("dbo.Post", "FK_LinkedInUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Like", "FK_PostId", "dbo.Post");
            DropForeignKey("dbo.Like", "FK_LinkedInUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comment", "FK_LinkedInUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Skill", new[] { "FK_LinkedInUserId" });
            DropIndex("dbo.Experience", new[] { "FK_LinkedInUserId" });
            DropIndex("dbo.Like", new[] { "FK_PostId" });
            DropIndex("dbo.Like", new[] { "FK_LinkedInUserId" });
            DropIndex("dbo.Post", new[] { "FK_LinkedInUserId" });
            DropIndex("dbo.Comment", new[] { "FK_PostId" });
            DropIndex("dbo.Comment", new[] { "FK_LinkedInUserId" });
            DropTable("dbo.Skill");
            DropTable("dbo.Experience");
            DropTable("dbo.Like");
            DropTable("dbo.Post");
            DropTable("dbo.Comment");
        }
    }
}
