﻿using System;
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
        public  ApplicantRegistration _applicant { get; set; }
        public List<SelectedGeneral> NonRecommendedGenerals { get; set; }
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