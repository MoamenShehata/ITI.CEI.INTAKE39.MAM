namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FriendRequestbool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User_Friends", "Request", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User_Friends", "Request");
        }
    }
}
