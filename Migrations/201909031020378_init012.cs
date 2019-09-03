namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init012 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.OfficerLogins", "desgination", c => c.String());
            //AddColumn("dbo.OfficerLogins", "Reset_Pwd", c => c.String());
            //DropColumn("dbo.OfficerLogins", "designation");
            //DropColumn("dbo.OfficerLogins", "ResetPwd");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.OfficerLogins", "ResetPwd", c => c.String());
            //AddColumn("dbo.OfficerLogins", "designation", c => c.String());
            //DropColumn("dbo.OfficerLogins", "Reset_Pwd");
            //DropColumn("dbo.OfficerLogins", "desgination");
        }
    }
}
