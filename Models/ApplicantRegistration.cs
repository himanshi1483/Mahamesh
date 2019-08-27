using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mahamesh.Models
{
    public class ApplicantRegistration
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "अर्जदाराचा फोटो")]
        public string Photo { get; set; }

        [Display(Name = "अर्ज क्रमांक")]
        public string ApplicationNumber { get; set; }

        public bool FormSubmitted { get; set; }

        [Display(Name = "Submitted Machine IP Address")]
        public string UserIP { get; set; }

        public DateTime? SubmitDatetime { get; set; }

        [Display(Name = "अर्जदाराचे नाव")]
        public string ApName { get; set; }

        [Display(Name = "गावाचे नाव (मु. पो.) ")]
        public int? VillageName { get; set; }

        //    [NotMapped]
        public int tabId { get; set; }

        [NotMapped]
        public string DistrictName { get; set; }

        [NotMapped]
        public string TalukaName { get; set; }

        [NotMapped]
        public string VilName { get; set; }

        [NotMapped]
        public string HvilName { get; set; }

        [NotMapped]
        public string SubCasteName { get; set; }

        [NotMapped]
        public ApplicationDuration appDuration { get; set; }

        [Display(Name = "तालुका")]
        public int? Tahashil { get; set; }

        [Display(Name = "जिल्हा")]
        public int? Dist { get; set; }

        [Display(Name = "पिन कोड")]
        public int? PinCode { get; set; }

        [Display(Name = "आपल्या गावाचा समावेश ज्या पशुवैद्यकीय दवाखान्याच्या कार्यक्षेत्रामध्ये आहे त्या गावाचे नाव")]
        public int? HVillage { get; set; }

        [Display(Name = "भ्रमणध्वनी क्रमांक")]
        public long? PhNo { get; set; }

        [Display(Name = "अर्जदाराची जन्म तारीख (DD/MM/YYYY)")]
        public string DOB { get; set; }

        [Display(Name = "वय")]
        public int? Age { get; set; }

        [Display(Name = "लिंग ")]
        public string Gender { get; set; }

        [Display(Name = "एकूण अपत्यांची संख्या ")]
        public int? ChildCount { get; set; }

        [Display(Name = "दिनांक १ मे २००१ व त्यानंतर माझ्या एकूण जन्माला आलेल्या मुलांची संख्या ")]
        public int? Child2006 { get; set; }

        [Display(Name = "अर्जदाराच्या जातीचा प्रवर्ग")]
        public string Caste { get; set; }

        [Display(Name = "अर्जदाराची जात")]
        public string SubCatse { get; set; }

        [Required]
        [Display(Name = "आधार कार्ड क्रमांक")]
        public long AdharCardNo { get; set; }

        [Display(Name = "अर्जदार अपंग आहे काय ?")]
        public string ApplicantCrippled { get; set; }

        [Display(Name = "असल्यास %")]
        public decimal? CrippledPercentage { get; set; }

        [Display(Name = "अर्जदाराकडे सद्यस्थितीत मेंढया आहेत काय ?")]
        public string PresentDaySheep { get; set; }

        [Display(Name = "असल्यास मेंढ्यांची संख्या")]
        public int? NumberOfSheepIs { get; set; }

        [Display(Name = "अर्जदार कायमस्वरूपी एका ठिकाणी राहून मेंढी पालन करणारे (स्थायी) आहेत काय? ")]
        public string ApplicantsPermanentInOnePlace { get; set; }

        [Display(Name = "अर्जदार स्थलांतर पद्धतीने मेंढीपालन करणारे (स्थलांतरीत) आहेत काय ? ")]
        public string ApplicantsMigratedByWayOfTransit { get; set; }

        [Display(Name = "अर्जदाराच्या मालकीची शेत जमीन आहे काय?")]
        public string IsApplicantOwnedLand { get; set; }

        [Display(Name = "हेक्टर")]
        public decimal? YesApplicantOwnedLandEcre { get; set; }

        [Display(Name = "आर")]
        public decimal? YesApplicantOwnedLandGuntha { get; set; }


        [Display(Name = "नसल्यास,भाडेकरारावर शेत जमीन उपलब्ध केली आहे काय ? ")]
        public string IsNotIsAvailableOnLease { get; set; }

        [Display(Name = "हेक्टर ")]
        public decimal? YesAvailableOnLeaseEcre { get; set; }

        [Display(Name = "आर ")]
        public decimal? YesAvailableOnLeaseGuntha { get; set; }

        [Display(Name = "बागायत हेक्टर")]
        public decimal? GardeningEcre { get; set; }

        [Display(Name = "जिरायत हेक्टर ")]
        public decimal? CuminEcre { get; set; }

        [Display(Name = "बारमाही/हंगामी सिंचनाकरिता पाण्याचा स्रोत")]
        public string WaterSource { get; set; }

        [Display(Name = "सिंचनाकरिता पाणी उपलब्ध असल्याचा कालावधी")]
        public string DurationOfWater { get; set; }

        [Display(Name = "मागील वर्षी पिकविण्यात आलेले चारापिके उदा. मका , ज्वारी, नेपियर बहुवार्षिक पिके व इतर ")]
        public string LastYearFooder { get; set; }

        [Display(Name = "मागील वर्षी झालेल्या एकूण चार्‍याचे उत्पादन (किलो) :(नंबर इंग्लिश मध्ये असणे आवश्यक आहे) ")]
        public decimal? LastYearTotalProductionInKG { get; set; }

        [Display(Name = "अर्जदाराकडे मेंढ्यांसाठी वाडा उपलब्ध आहे काय ?")]
        public string IsWarehouseForSheep { get; set; }

        [Display(Name = "असल्यास,किती मेंढ्यांसाठी")]
        public int? YesIntekOfSheepInWarehouse { get; set; }

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
        [Display(Name = "Component ")]
        public string CompNumber { get; set; }

        [NotMapped]
        public List<string> CompNumberList1 { get; set; }
        [NotMapped]
        public List<string> CompNumberList2 { get; set; }
        [NotMapped]
        public List<string> CompNumberList3 { get; set; }
        [NotMapped]
        public List<string> CompNumberList4 { get; set; }
        [NotMapped]
        public List<string> CompNumberList5 { get; set; }

        [NotMapped]
        public List<Tuple<int,string>> CompList { get; set; }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //For Documenent////
        [Display(Name = "AdharCardFU ")]
        public string AdharCardFU { get; set; }
        [Display(Name = "LivestockDevOffCertificate ")]
        public string LivestockDevOffCertificate { get; set; }
        [Display(Name = "CasteCertificate ")]
        public string CasteCertificate { get; set; }
        [Display(Name = "ResidentCertificate ")]
        public string ResidentCertificate { get; set; }
        [Display(Name = "Childcertificate ")]
        public string Childcertificate { get; set; }
        [Display(Name = "FU712Certificate ")]
        public string FU712Certificate { get; set; }
        [Display(Name = "TenancyAgreement ")]
        public string TenancyAgreement { get; set; }
        [Display(Name = "FU712orIncomeCertificate ")]
        public string FU712orIncomeCertificate { get; set; }
        [Display(Name = "BankPassBook ")]
        public string BankPassBook { get; set; }
        [Display(Name = "BachatMemberCertificate ")]
        public string BachatMemberCertificate { get; set; }
        [Display(Name = "CompanyMemberCertificate ")]
        public string CompanyMemberCertificate { get; set; }
        [Display(Name = "DisabilityCertificate ")]
        public string DisabilityCertificate { get; set; }
        [Display(Name = "ReshanCard ")]
        public string ReshanCard { get; set; }
        [Display(Name = "HamiPtra ")]
        public string HamiPtra { get; set; }
        /////////////////////////////////////////////////////////////////////
    }

    public enum Components
    {

        [Description("कायमस्वरूपी एका ठिकाणी राहून मेंढीपालन करण्याकरिता पायाभूत सोई – सुविधेसह २० मेंढया + १ मेंढानर असा मेंढीगट ७५% अनुदानावर वाटप करणे (स्थायी)")]
        Comp1 = 1,

        [Description("स्थलांतर पद्धतीने मेंढीपालन करण्याकरिता पायाभूत सोई – सुविधेसह २० मेंढया + १ मेंढानर असा मेंढीगट ७५% अनुदानावर वाटप करणे (स्थलांतरीत)")]
        Comp2 = 2,

        [Description(" ज्यांच्याकडे स्वतचे २० किंवा त्यापेक्षा अधिक परंतु ४० पेक्षा कमी मेंढया आहेत अशा लाभार्थ्यांना सुधारित प्रजातीचा १ नरमेंढा ७५% अनुदानावर वाटप करणे.")]
        Comp3 = 3,

        [Description("ज्यांच्याकडे स्वतचे ४० किंवा त्यापेक्षा अधिक परंतु ६० पेक्षा कमी मेंढया आहेत अशा लाभार्थ्यांना सुधारित प्रजातीचा २ नरमेंढे ७५% अनुदानावर वाटप करणे.")]
        Comp4 = 4,

        [Description("ज्यांच्याकडे स्वतचे ६० किंवा त्यापेक्षा अधिक परंतु ८० पेक्षा कमी मेंढया आहेत अशा लाभार्थ्यांना सुधारित प्रजातीचा ३ नरमेंढे ७५% अनुदानावर वाटप करणे.")]
        Comp5 = 5,

        [Description("ज्यांच्याकडे स्वतचे ८० किंवा त्यापेक्षा अधिक परंतु १०० पेक्षा कमी मेंढया आहेत अशा लाभार्थ्यांना सुधारित प्रजातीचा ४ नरमेंढे ७५% अनुदानावर वाटप करणे.")]
        Comp6 = 6,

        [Description("ज्यांच्याकडे स्वतचे १०० किंवा त्यापेक्षा अधिक मेंढया आहेत अशा लाभार्थ्यांना सुधारित प्रजातीचा ५ नरमेंढे ७५% अनुदानावर वाटप करणे.")]
        Comp7 = 7,

        [Description("8 ज्यांच्याकडे स्वत: च्या २० मेंढया व १ मेंढानर अशा एकूण २१ मेंढया किंवा त्यापेक्षा अधिक परंतु ४० मेंढ्यापेक्षा कमी अशा मेंढ्यांच्या एका ठिकाणी राहून स्थायी स्वरूपाचे मेंढी पालनासाठी पायाभूत सोई-सुविधा उपलब्ध करून देण्यासाठी ७५% अनुदान वाटप (स्थायी)")]
        Comp8 = 8,

        [Description("ज्यांच्याकडे स्वत: च्या २० मेंढया व १ मेंढानर अशा एकूण २१ मेंढया किंवा त्यापेक्षा अधिक परंतु ४० मेंढ्यापेक्षा कमी अशा मेंढ्यांच्या स्थलांतरीत स्वरूपाचे मेंढी पालनासाठी पायाभूत सोई-सुविधा उपलब्ध करून देण्यासाठी ७५% अनुदान वाटप (स्थलांतरीत)")]
        Comp9 = 9,

        [Description("ज्यांच्याकडे स्वत: च्या ४० मेंढया व २ मेंढानर अशा एकूण ४२ मेंढया किंवा त्यापेक्षा अधिक अशा मेंढ्यांच्या एका ठिकाणी राहून स्थायी स्वरूपाचे मेंढी पालनासाठी पायाभूत सोई-सुविधा उपलब्ध करून देण्यासाठी ७५% अनुदान वाटप (स्थायी)")]
        Comp10 = 10,

        [Description("ज्यांच्याकडे स्वत: च्या ४० मेंढया व २ मेंढानर अशा एकूण ४२ मेंढया किंवा त्यापेक्षा अधिक अशा मेंढ्यांच्या स्थलांतरीत स्वरूपाचे मेंढी पालनासाठी पायाभूत सोई-सुविधा उपलब्ध करून देण्यासाठी ७५% अनुदान वाटप (स्थलांतरीत))")]
        Comp11 = 11,

        [Description("एका ठिकाणी राहून स्थायी स्वरूपाचे मेंढी पालनासाठी संतुलित खाद्य उपलब्ध करून देण्यासाठी ७५% अनुदान वाटप (स्थायी) (१०० ग्रॅम प्रती दिन प्रती मेंढी याप्रमाणे माहे एप्रिल ते जुलै या ४ महिन्याच्या कालावधी करिता )")]
        Comp12 = 12,

        [Description("भटकंती करणारे स्थलांतरीत स्वरूपाच्या मेंढी पालनासाठी संतुलित खाद्य उपलब्ध करून देण्यासाठी ७५% अनुदान वाटप (स्थलांतरीत)(१०० ग्रॅम प्रती दिन प्रती मेंढी याप्रमाणे जून ते जुलै या २ महिन्याच्या कालावधी करिता )")]
        Comp13 = 13,

        [Description("कुट्टी केलेल्या हिरव्या चार्‍याचा मुरघस करण्याकरिता घासड्या बांधण्याचे यंत्र (Mini Sailage Baler cum Wrapper) खरेदी करण्यासाठी ५०% अनुदान वाटप")]
        Comp14 = 14,

        [Description("पशुखाद्या कारखाने उभारणीसाठी ५०% अनुदान वाटप")]
        Comp15 = 15
    }



}