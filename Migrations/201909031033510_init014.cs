namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init014 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OfficerLogins", "Reset_Pwd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OfficerLogins", "Reset_Pwd", c => c.String());
        }
    }
}
