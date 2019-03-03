namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Friendrequestsforce : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User_Friends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.String(),
                        Friend_Id = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User_Friends");
        }
    }
}
