namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init013 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OfficerLogins", "ResetPwd", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OfficerLogins", "ResetPwd");
        }
    }
}
