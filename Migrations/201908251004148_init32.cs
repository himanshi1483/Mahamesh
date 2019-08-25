namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init32 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicantRegistrations", "YesApplicantOwnedLandEcre", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ApplicantRegistrations", "YesApplicantOwnedLandGuntha", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ApplicantRegistrations", "YesAvailableOnLeaseEcre", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ApplicantRegistrations", "YesAvailableOnLeaseGuntha", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicantRegistrations", "YesAvailableOnLeaseGuntha", c => c.Int());
            AlterColumn("dbo.ApplicantRegistrations", "YesAvailableOnLeaseEcre", c => c.Int());
            AlterColumn("dbo.ApplicantRegistrations", "YesApplicantOwnedLandGuntha", c => c.Int());
            AlterColumn("dbo.ApplicantRegistrations", "YesApplicantOwnedLandEcre", c => c.Int());
        }
    }
}
