namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init16 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicantRegistrations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Photo = c.String(),
                        ApplicationID = c.Int(nullable: false),
                        ApName = c.String(),
                        VillageName = c.Int(nullable: false),
                        Tahashil = c.Int(nullable: false),
                        Dist = c.Int(nullable: false),
                        PinCode = c.Int(nullable: false),
                        HVillage = c.Int(nullable: false),
                        PhNo = c.Long(nullable: false),
                        DOB = c.String(),
                        Age = c.Int(nullable: false),
                        Gender = c.String(),
                        ChildCount = c.Int(nullable: false),
                        Child2006 = c.Int(nullable: false),
                        Caste = c.String(),
                        SubCatse = c.String(),
                        AdharCardNo = c.Long(nullable: false),
                        ApplicantCrippled = c.String(),
                        CrippledPercentage = c.Double(nullable: false),
                        PresentDaySheep = c.String(),
                        NumberOfSheepIs = c.Int(nullable: false),
                        ApplicantsPermanentInOnePlace = c.String(),
                        ApplicantsMigratedByWayOfTransit = c.String(),
                        IsApplicantOwnedLand = c.String(),
                        YesApplicantOwnedLandEcre = c.Int(nullable: false),
                        YesApplicantOwnedLandGuntha = c.Int(nullable: false),
                        IsNotIsAtLeastOnePinpointSpace = c.String(),
                        IsNotIsAvailableOnLease = c.String(),
                        YesAvailableOnLeaseEcre = c.Int(nullable: false),
                        YesAvailableOnLeaseGuntha = c.Int(nullable: false),
                        GardeningEcre = c.Double(nullable: false),
                        CuminEcre = c.Double(nullable: false),
                        WaterSource = c.String(),
                        DurationOfWater = c.String(),
                        LastYearFooder = c.String(),
                        LastYearTotalProductionInKG = c.Double(nullable: false),
                        IsWarehouseForSheep = c.String(),
                        YesIntekOfSheepInWarehouse = c.Int(nullable: false),
                        TypeExistingCastle = c.String(),
                        IsSavingsGroupMember = c.String(),
                        SavingGroupName = c.String(),
                        SavingGroupRegNumber = c.String(),
                        IsanimalHusbandryManufacturingCompanyMember = c.String(),
                        IsanimalHusbandryManufacturingCompanyName = c.String(),
                        IsanimalHusbandryManufacturingCompanyRegNumber = c.String(),
                        RationCardNumber = c.String(),
                        IsTrained = c.String(),
                        CompNumber = c.String(),
                        AdharCardFU = c.String(),
                        LivestockDevOffCertificate = c.String(),
                        CasteCertificate = c.String(),
                        ResidentCertificate = c.String(),
                        Childcertificate = c.String(),
                        FU712Certificate = c.String(),
                        TenancyAgreement = c.String(),
                        FU712orIncomeCertificate = c.String(),
                        BankPassBook = c.String(),
                        BachatMemberCertificate = c.String(),
                        CompanyMemberCertificate = c.String(),
                        DisabilityCertificate = c.String(),
                        ReshanCard = c.String(),
                        HamiPtra = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApplicantRegistrations");
        }
    }
}
