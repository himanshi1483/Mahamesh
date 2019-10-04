using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class RecommendationViewModel
    {
        public List<ApplicantRegistration> Applicants { get; set; }
        public List<SelectedGeneral> SelectedGenerals { get; set; }
        public List<SelectedFemale> SelectedFemales { get; set; }
        public List<SelectedHandicapped> SelectedHandicaps { get; set; }
        public List<SelectedGeneral> WaitingList { get; set; }
        public List<PreliminaryList> Preliminaries { get; set; }
        public List<ApplicantDocuments> ApplicantDocuments { get; set; }
        public List<LDOConditions> LDOConditions { get; set; }
        public  ApplicantRegistration _applicant { get; set; }
        public List<SelectedGeneral> NonRecommendedGenerals { get; set; }

        public int TotalApplications { get; set; }
        public int TotalRecommended { get; set; }
        public int TotalNonRecommended { get; set; }
        public int TotalPending { get; set; }
        public int TotalApplications_Taluka { get; set; }

        public int TotalRecommended_Taluka { get; set; }
        public int TotalNonRecommended_Taluka { get; set; }
        public int TotalPending_Taluka { get; set; }

        public int TotalApplications_Dist { get; set; }

        public int TotalRecommended_Dist { get; set; }
        public int TotalNonRecommended_Dist { get; set; }
        public int TotalPending_Dist { get; set; }
        public int TotalSaved_LDO { get; set; }
        public int TotalSaved_LDO_Taluka { get; set; }
        public int TotalLDORecommended_Tal { get; set; }
        public int TotalLDONonRecommended_Tal { get; set; }
        public int TotalPendingLDO_Tal { get; set; }
        public int TotalSaved_DAHO { get; set; }
        public int TotalSaved_DDC { get; set; }
        public int TotalLDORecommended { get; set; }
        public int TotalLDONonRecommended { get; set; }
        public int TotalDAHORecommended { get; set; }
        public int TotalDAHONonRecommended { get; set; }
        public int TotalDDCRecommended { get; set; }
        public int TotalDDCNonRecommended { get; set; }

        public int TotalLDORecommended_Dist { get; set; }
        public int TotalLDONonRecommended_Dist { get; set; }
        public int TotalDAHORecommended_Dist { get; set; }
        public int TotalDAHONonRecommended_Dist { get; set; }
        public int TotalDDCRecommended_Dist { get; set; }
        public int TotalDDCNonRecommended_Dist { get; set; }

        public int ApplicantId { get; set; }
        public int DistCode { get; set; }
        public string DistName { get; set; }
        public int TalukaCode { get; set; }
        public string TalukaName { get; set; }

        [Display(Name = "आधार कार्ड")]
        public string AdharCardFU { get; set; }
        [Display(Name = "संबंधीत पशुवैद्यकीय दवाखान्याचे पशुधन विकास अधिकारी यांचे प्रमाणपत्र")]
        public string LivestockDevOffCertificate { get; set; }
        [Display(Name = "जातीचा दाखला")]
        public string CasteCertificate { get; set; }
        [Display(Name = "ResidentCertificate ")]
        public string ResidentCertificate { get; set; }
        [Display(Name = "अपत्य दाखला")]
        public string Childcertificate { get; set; }
        [Display(Name = "भाडेतत्वावर शेतजमीन घेतली असल्यास शेतजमीन मालकासमवेत केलेल्या भाडेकराराची सत्यप्रत")]
        public string TenancyAgreement { get; set; }
        [Display(Name = "शेतजमिनीचा ७/१२ उतारा किंवा कुटुंबियांपैकी व्यक्तीच्या नावाच्या ७/१२ उतारा व संमतीपत्र.")]
        public string FU712orIncomeCertificate { get; set; }
        [Display(Name = "BankPassBook ")]
        public string BankPassBook { get; set; }
        [Display(Name = "बचतगटाचे सदस्य असल्याचे प्रमाणपत्र")]
        public string BachatMemberCertificate { get; set; }
        [Display(Name = "पशुपालक उत्पादक कंपनीचे सदस्य असल्याचे प्रमाणपत्र")]
        public string CompanyMemberCertificate { get; set; }
        [Display(Name = "अपंगत्व प्रमाणपत्र")]
        public string DisabilityCertificate { get; set; }
        [Display(Name = "ReshanCard ")]
        public string ReshanCard { get; set; }
        [Display(Name = "HamiPtra ")]
        public string HamiPtra { get; set; }
        [Display(Name = "TrainingCertificate ")]
        public string TrainingCertificate { get; set; }
        [Display(Name = "१ गुंठा जागा उपलब्ध असल्याबाबत ७/१२ उतारा / मिळकत दाखला.")]
        public string ShedCertificate { get; set; }

    }
}