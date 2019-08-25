namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init33 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicantRegistrations", "CrippledPercentage", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ApplicantRegistrations", "GardeningEcre", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ApplicantRegistrations", "CuminEcre", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ApplicantRegistrations", "LastYearTotalProductionInKG", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicantRegistrations", "LastYearTotalProductionInKG", c => c.Double());
            AlterColumn("dbo.ApplicantRegistrations", "CuminEcre", c => c.Double());
            AlterColumn("dbo.ApplicantRegistrations", "GardeningEcre", c => c.Double());
            AlterColumn("dbo.ApplicantRegistrations", "CrippledPercentage", c => c.Double());
        }
    }
}
