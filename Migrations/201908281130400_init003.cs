namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init003 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comp1Target", "TempGrant", c => c.Double(nullable: false));
            AddColumn("dbo.Comp1Target", "TempOrigin", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comp1Target", "TempOrigin");
            DropColumn("dbo.Comp1Target", "TempGrant");

        }
    }
}
