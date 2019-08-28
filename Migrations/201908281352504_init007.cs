namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init007 : DbMigration
    {
        public override void Up()
        {
           // AddColumn("dbo.Comp1Target", "TempOrigin", c => c.Int(nullable: false));
           // AddColumn("dbo.Comp1Target", "TempGrant", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            //DropColumn("dbo.Comp1Target", "TempGrant");
           // DropColumn("dbo.Comp1Target", "TempOrigin");
        }
    }
}
