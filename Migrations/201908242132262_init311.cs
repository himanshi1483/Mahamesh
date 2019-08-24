namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init311 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AcreMasters", "Acre", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AcreMasters", "Acre", c => c.Int());
        }
    }
}
