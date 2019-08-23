using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class ApplicantRegistration
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "अर्जदाराचा फोटो")]
        public string Photo { get; set; }

        public int ApplicationID { get; set; }

        [Display(Name = "अर्जदाराचे नाव")]
        public string ApName { get; set; }

        [Display(Name = "गावाचे नाव (मु. पो.) ")]
        public int VillageName { get; set; }

        [Display(Name = "तालुका")]
        public int Tahashil { get; set; }

        [Display(Name = "जिल्हा")]
        public int Dist { get; set; }

        [Display(Name = "पिन कोड")]
        public int PinCode { get; set; }

        [Display(Name = "आपल्या गावाचा समावेश ज्या पशुवैद्यकीय दवाखान्याच्या कार्यक्षेत्रामध्ये आहे त्या गावाचे नाव")]
        public int HVillage { get; set; }

        [Display(Name = "भ्रमणध्वनी क्रमांक")]
        public long PhNo { get; set; }

        [Display(Name = "अर्जदाराची जन्म तारीख (DD/MM/YYYY)")]
        public string DOB { get; set; }

        [Display(Name = "वय")]
        public int Age { get; set; }

        [Display(Name = "लिंग ")]
        public string Gender { get; set; }

        [Display(Name = "एकूण अपत्यांची संख्या ")]
        public int ChildCount { get; set; }

        [Display(Name = "दिनांक १ मे २००१ व त्यानंतर माझ्या एकूण जन्माला आलेल्या मुलांची संख्या ")]
        public int Child2006 { get; set; }

        [Display(Name = "अर्जदाराच्या जातीचा प्रवर्ग")]
        public string Caste { get; set; }

        [Display(Name = "अर्जदाराची जात")]
        public string SubCatse { get; set; }

        [Display(Name = "आधार कार्ड क्रमांक")]
        public long AdharCardNo { get; set; }

        [Display(Name = "अर्जदार अपंग आहे काय ?")]
        public string ApplicantCrippled { get; set; }

        [Display(Name = "असल्यास %")]
        public double CrippledPercentage { get; set; }

        [Display(Name = "अर्जदाराकडे सद्यस्थितीत मेंढया आहेत काय ?")]
        public string PresentDaySheep { get; set; }

        [Display(Name = "असल्यास मेंढ्यांची संख्या")]
        public int NumberOfSheepIs { get; set; }

        [Display(Name = "अर्जदार कायमस्वरूपी एका ठिकाणी राहून मेंढी पालन करणारे (स्थायी) आहेत काय? ")]
        public string ApplicantsPermanentInOnePlace { get; set; }

        [Display(Name = "अर्जदार स्थलांतर पद्धतीने मेंढीपालन करणारे (स्थलांतरीत) आहेत काय ? ")]
        public string ApplicantsMigratedByWayOfTransit { get; set; }

        [Display(Name = "अर्जदाराच्या मालकीची शेत जमीन आहे काय?")]
        public string IsApplicantOwnedLand { get; set; }

        [Display(Name = "हेक्टर")]
        public int YesApplicantOwnedLandEcre { get; set; }

        [Display(Name = "आर")]
        public int YesApplicantOwnedLandGuntha { get; set; }

      
        [Display(Name = "नसल्यास,भाडेकरारावर शेत जमीन उपलब्ध केली आहे काय ? ")]
        public string IsNotIsAvailableOnLease { get; set; }

        [Display(Name = "हेक्टर ")]
        public int YesAvailableOnLeaseEcre { get; set; }

        [Display(Name = "आर ")]
        public int YesAvailableOnLeaseGuntha { get; set; }

        [Display(Name = "बागायत हेक्टर")]
        public double GardeningEcre { get; set; }

        [Display(Name = "जिरायत हेक्टर ")]
        public double CuminEcre { get; set; }

        [Display(Name = "बारमाही/हंगामी सिंचनाकरिता पाण्याचा स्रोत")]
        public string WaterSource { get; set; }

        [Display(Name = "सिंचनाकरिता पाणी उपलब्ध असल्याचा कालावधी")]
        public string DurationOfWater { get; set; }

        [Display(Name = "मागील वर्षी पिकविण्यात आलेले चारापिके उदा. मका , ज्वारी, नेपियर बहुवार्षिक पिके व इतर ")]
        public string LastYearFooder { get; set; }

        [Display(Name = "मागील वर्षी झालेल्या एकूण चार्‍याचे उत्पादन (किलो) :(नंबर इंग्लिश मध्ये असणे आवश्यक आहे) ")]
        public double LastYearTotalProductionInKG { get; set; }

        [Display(Name = "अर्जदाराकडे मेंढ्यांसाठी वाडा उपलब्ध आहे काय ?")]
        public string IsWarehouseForSheep { get; set; }

        [Display(Name = "असल्यास,किती मेंढ्यांसाठी")]
        public int YesIntekOfSheepInWarehouse { get; set; }

        [Display(Name = "अस्तित्वातील वाड्याचा प्रकार")]
        public string TypeExistingCastle { get; set; }

        [Display(Name = "नसल्यास शेड बांधकामासाठी स्वत: ची किमान १ गुंठा जागा उपलब्ध आहे काय?")]
        public string IsNotIsAtLeastOnePinpointSpace { get; set; }


        [Display(Name = "अर्जदार बचत गटाचा सदस्य आहे काय ? ")]
        public string IsSavingsGroupMember { get; set; }

        [Display(Name = "बचत गट नाव ")]
        public string SavingGroupName { get; set; }

        [Display(Name = "नोंदणी क्रमांक")]
        public string SavingGroupRegNumber { get; set; }

        [Display(Name = "अर्जदार पशुपालक उत्पादक कंपनीचा सदस्य आहे काय ? ")]
        public string IsanimalHusbandryManufacturingCompanyMember { get; set; }
        [Display(Name = "पशुपालक उत्पादक कंपनी ")]
        public string IsanimalHusbandryManufacturingCompanyName { get; set; }
        [Display(Name = "नोंदणी क्रमांक ")]
        public string IsanimalHusbandryManufacturingCompanyRegNumber { get; set; }

        [Display(Name = "रेशन कार्ड क्रमांक ")]
        public string RationCardNumber { get; set; }

        [Display(Name = "पुण्यश्लोक अहिल्यादेवी महाराष्ट्र मेंढी व शेळी विकास महामंडळ येथून प्रशिक्षण घेतले आहे का ? ")]
        public string IsTrained { get; set; }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //For Components///
        [Display(Name = "लिंग ")]
        public string CompNumber { get; set; }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //For Documenent////
        [Display(Name = "लिंग ")]
        public string AdharCardFU { get; set; }
        [Display(Name = "लिंग ")]
        public string LivestockDevOffCertificate { get; set; }
        [Display(Name = "लिंग ")]
        public string CasteCertificate { get; set; }
        [Display(Name = "लिंग ")]
        public string ResidentCertificate { get; set; }
        [Display(Name = "लिंग ")]
        public string Childcertificate { get; set; }
        [Display(Name = "लिंग ")]
        public string FU712Certificate { get; set; }
        [Display(Name = "लिंग ")]
        public string TenancyAgreement { get; set; }
        [Display(Name = "लिंग ")]
        public string FU712orIncomeCertificate { get; set; }
        [Display(Name = "लिंग ")]
        public string BankPassBook { get; set; }
        [Display(Name = "लिंग ")]
        public string BachatMemberCertificate { get; set; }
        [Display(Name = "लिंग ")]
        public string CompanyMemberCertificate { get; set; }
        [Display(Name = "लिंग ")]
        public string DisabilityCertificate { get; set; }
        [Display(Name = "लिंग ")]
        public string ReshanCard { get; set; }
        [Display(Name = "लिंग ")]
        public string HamiPtra { get; set; }
        /////////////////////////////////////////////////////////////////////
    }
}