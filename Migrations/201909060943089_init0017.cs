namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init0017 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TalMasters", "Tal_Mr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TalMasters", "Tal_Mr");
        }
    }
}
