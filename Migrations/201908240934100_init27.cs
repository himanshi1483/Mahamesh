namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init27 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicantRegistrations", "VillageName", c => c.Int());
            AlterColumn("dbo.ApplicantRegistrations", "Tahashil", c => c.Int());
            AlterColumn("dbo.ApplicantRegistrations", "Dist", c => c.Int());
            AlterColumn("dbo.ApplicantRegistrations", "PinCode", c => c.Int());
            AlterColumn("dbo.ApplicantRegistrations", "HVillage", c => c.Int());
            AlterColumn("dbo.ApplicantRegistrations", "PhNo", c => c.Long());
            AlterColumn("dbo.ApplicantRegistrations", "Age", c => c.Int());
            AlterColumn("dbo.ApplicantRegistrations", "ChildCount", c => c.Int());
            AlterColumn("dbo.ApplicantRegistrations", "Child2006", c => c.Int());
            AlterColumn("dbo.ApplicantRegistrations", "CrippledPercentage", c => c.Double());
            AlterColumn("dbo.ApplicantRegistrations", "NumberOfSheepIs", c => c.Int());
            AlterColumn("dbo.ApplicantRegistrations", "YesApplicantOwnedLandEcre", c => c.Int());
            AlterColumn("dbo.ApplicantRegistrations", "YesApplicantOwnedLandGuntha", c => c.Int());
            AlterColumn("dbo.ApplicantRegistrations", "YesAvailableOnLeaseEcre", c => c.Int());
            AlterColumn("dbo.ApplicantRegistrations", "YesAvailableOnLeaseGuntha", c => c.Int());
            AlterColumn("dbo.ApplicantRegistrations", "GardeningEcre", c => c.Double());
            AlterColumn("dbo.ApplicantRegistrations", "CuminEcre", c => c.Double());
            AlterColumn("dbo.ApplicantRegistrations", "LastYearTotalProductionInKG", c => c.Double());
            AlterColumn("dbo.ApplicantRegistrations", "YesIntekOfSheepInWarehouse", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicantRegistrations", "YesIntekOfSheepInWarehouse", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicantRegistrations", "LastYearTotalProductionInKG", c => c.Double(nullable: false));
            AlterColumn("dbo.ApplicantRegistrations", "CuminEcre", c => c.Double(nullable: false));
            AlterColumn("dbo.ApplicantRegistrations", "GardeningEcre", c => c.Double(nullable: false));
            AlterColumn("dbo.ApplicantRegistrations", "YesAvailableOnLeaseGuntha", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicantRegistrations", "YesAvailableOnLeaseEcre", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicantRegistrations", "YesApplicantOwnedLandGuntha", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicantRegistrations", "YesApplicantOwnedLandEcre", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicantRegistrations", "NumberOfSheepIs", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicantRegistrations", "CrippledPercentage", c => c.Double(nullable: false));
            AlterColumn("dbo.ApplicantRegistrations", "Child2006", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicantRegistrations", "ChildCount", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicantRegistrations", "Age", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicantRegistrations", "PhNo", c => c.Long(nullable: false));
            AlterColumn("dbo.ApplicantRegistrations", "HVillage", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicantRegistrations", "PinCode", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicantRegistrations", "Dist", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicantRegistrations", "Tahashil", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicantRegistrations", "VillageName", c => c.Int(nullable: false));
        }
    }
}
