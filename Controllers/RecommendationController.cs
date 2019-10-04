using Mahamesh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mahamesh.Controllers
{
    public class RecommendationController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Recommendation
        public ActionResult LDODashboard(int distCode, int talCode)
        {
            RecommendationViewModel model = new RecommendationViewModel();
            model.DistCode = distCode;
            model.TalukaCode = talCode;

            var selectedFemales = db.SelectedFemale.Where(x => x.DistCode == distCode && x.TalukaCode == talCode).ToList();
            var selectedHandicaps = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.TalukaCode == talCode).ToList();
            var selectedGenerals = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.TalukaCode == talCode && x.Type == "Selected").ToList();
            var applicants = db.ApplicantRegistrations.Where(x => x.FormSubmitted == true && x.Dist == distCode && x.Tahashil == talCode).ToList();

            var preliminary = db.PreliminaryList.Where(x => x.DistCode == distCode && x.TalukaCode == talCode).ToList();

            var queryFemale =
                         from post in selectedFemales
                         join meta in preliminary on post.ApplicationId equals meta.ApplicantID
                         join applicant in applicants on post.ApplicationId equals applicant.Id
                         //   where meta.LDORecommended == true
                         select new SelectedFemale
                         {
                             ApplicationId = post.ApplicationId,
                             ApplicationNumber = post.ApplicationNumber,
                             AadharNo = post.AadharNo,
                             ApplicantCrippled = post.ApplicantCrippled,
                             DistCode = post.DistCode,
                             District_Mr = post.District_Mr,
                             TalukaCode = post.TalukaCode,
                             Taluka_Mr = post.Taluka_Mr,
                             VillageName = post.VillageName,
                             Name = post.Name,
                             PhNo = post.PhNo.Value,
                             LDORecommended = meta.LDORecommended,
                             Gender = post.Gender,
                             DAHORecommended = meta.DAHORecommended,
                             DDCRecommended = meta.DDCRecommended,
                             Component = post.Component,
                             Id = post.Id,
                             CreatedOn = post.CreatedOn,
                             Type = post.Type,
                             DOB = applicant.DOB,
                             SubCaste = applicant.SubCasteName,
                             ChildCount = applicant.ChildCount == null ? 0 : applicant.ChildCount.Value,
                             CripplePercent = applicant.CrippledPercentage != null ? applicant.CrippledPercentage.Value : 0
                         };

            var queryHandiCaps =
                         from post in selectedHandicaps
                         join meta in preliminary on post.ApplicationId equals meta.ApplicantID
                         join applicant in applicants on post.ApplicationId equals applicant.Id
                         //   where meta.LDORecommended == true
                         select new SelectedHandicapped
                         {
                             ApplicationId = post.ApplicationId,
                             ApplicationNumber = post.ApplicationNumber,
                             AadharNo = post.AadharNo,
                             ApplicantCrippled = post.ApplicantCrippled,
                             DistCode = post.DistCode,
                             District_Mr = post.District_Mr,
                             TalukaCode = post.TalukaCode,
                             Taluka_Mr = post.Taluka_Mr,
                             VillageName = post.VillageName,
                             Name = post.Name,
                             PhNo = post.PhNo.Value,
                             LDORecommended = meta.LDORecommended,
                             Gender = post.Gender,
                             DAHORecommended = meta.DAHORecommended,
                             DDCRecommended = meta.DDCRecommended,
                             Component = post.Component,
                             Id = post.Id,
                             CreatedOn = post.CreatedOn,
                             Type = post.Type,
                             DOB = applicant.DOB,
                             SubCaste = applicant.SubCasteName,
                             ChildCount = applicant.ChildCount == null ? 0 : applicant.ChildCount.Value,
                             CripplePercent = applicant.CrippledPercentage != null ? applicant.CrippledPercentage.Value : 0
                         };

            var queryGenerals =
                     from post in selectedGenerals
                     join meta in preliminary on post.ApplicationId equals meta.ApplicantID
                     join applicant in applicants on post.ApplicationId equals applicant.Id
                     //   where meta.LDORecommended == true
                     select new SelectedGeneral
                     {
                         ApplicationId = post.ApplicationId,
                         ApplicationNumber = post.ApplicationNumber,
                         AadharNo = post.AadharNo,
                         ApplicantCrippled = post.ApplicantCrippled,
                         DistCode = post.DistCode,
                         District_Mr = post.District_Mr,
                         TalukaCode = post.TalukaCode,
                         Taluka_Mr = post.Taluka_Mr,
                         VillageName = post.VillageName,
                         Name = post.Name,
                         PhNo = post.PhNo.Value,
                         LDORecommended = meta.LDORecommended,
                         Gender = post.Gender,
                         DAHORecommended = meta.DAHORecommended,
                         DDCRecommended = meta.DDCRecommended,
                         Component = post.Component,
                         Id = post.Id,
                         CreatedOn = post.CreatedOn,
                         Type = post.Type,
                         DOB = applicant.DOB,
                         SubCaste = applicant.SubCasteName,
                         ChildCount = applicant.ChildCount == null ? 0 : applicant.ChildCount.Value,
                         CripplePercent = applicant.CrippledPercentage != null ? applicant.CrippledPercentage.Value : 0
                     };


            var queryNonRecom =
                   from post in selectedGenerals
                   join meta in preliminary on post.ApplicationId equals meta.ApplicantID
                   join applicant in applicants on post.ApplicationId equals applicant.Id
                   //  where meta.LDORecommended == false
                   select new SelectedGeneral
                   {
                       ApplicationId = post.ApplicationId,
                       ApplicationNumber = post.ApplicationNumber,
                       AadharNo = post.AadharNo,
                       ApplicantCrippled = post.ApplicantCrippled,
                       DistCode = post.DistCode,
                       District_Mr = post.District_Mr,
                       TalukaCode = post.TalukaCode,
                       Taluka_Mr = post.Taluka_Mr,
                       VillageName = post.VillageName,
                       Name = post.Name,
                       PhNo = post.PhNo.Value,
                       LDORecommended = meta.LDORecommended,
                       Gender = post.Gender,
                       DAHORecommended = meta.DAHORecommended,
                       DDCRecommended = meta.DDCRecommended,
                       Component = post.Component,
                       Id = post.Id,
                       CreatedOn = post.CreatedOn,
                       Type = post.Type,
                       DOB = applicant.DOB,
                       SubCaste = applicant.SubCasteName,
                       ChildCount = applicant.ChildCount == null ? 0 : applicant.ChildCount.Value,
                       CripplePercent = applicant.CrippledPercentage != null ? applicant.CrippledPercentage.Value : 0
                   };


            model.SelectedFemales = queryFemale.ToList();
            model.SelectedGenerals = queryGenerals.ToList();
            model.SelectedHandicaps = queryHandiCaps.ToList();
            model.NonRecommendedGenerals = queryNonRecom.ToList();
            return View(model);
        }

        public ActionResult RecommenendApplicant(int id)
        {

            var model = new RecommendationViewModel();
            var applicant = db.ApplicantRegistrations.Where(x => x.Id == id).FirstOrDefault();
            int subCasteId = Convert.ToInt32(applicant.SubCatse);
            applicant.DistrictName = db.DistMaster.Where(x => x.Dist_Code == applicant.Dist).Select(x => x.DistName).FirstOrDefault();
            applicant.TalukaName = db.TalMaster.Where(x => x.Dist_Code == applicant.Dist && x.Tal_Code == applicant.Tahashil.Value).Select(x => x.TalName).FirstOrDefault();
            applicant.VilName = db.VillageMaster.Where(x => x.Tal_Code == applicant.Tahashil.Value && x.Village_Code == applicant.VillageName).Select(x => x.VillageName).FirstOrDefault();
            model.AdharCardFU = applicant.AdharCardFU;
            applicant.SubCasteName = db.CasteUnderNTC.Where(x => x.ID == subCasteId).Select(x => x.Caste).FirstOrDefault();
            model.CasteCertificate = applicant.CasteCertificate;
            model.LivestockDevOffCertificate = applicant.LivestockDevOffCertificate;
            model.FU712orIncomeCertificate = applicant.FU712orIncomeCertificate;
            model.TenancyAgreement = applicant.TenancyAgreement;
            model.ShedCertificate = applicant.ShedCertificate;
            model.BachatMemberCertificate = applicant.BachatMemberCertificate;
            model.Childcertificate = applicant.Childcertificate;
            model.DisabilityCertificate = applicant.DisabilityCertificate;
            model.CompanyMemberCertificate = applicant.CompanyMemberCertificate;
            model.ApplicantId = applicant.Id;
            var checkData = db.LDOCondition.Any(x => x.ApplicantID == applicant.Id);
            if (!checkData)
                {
                //Create LDO Conditions
                //1
                var ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 1;
                ldoCondition.DocNumber = 1;
                ldoCondition.DocName = "आधार कार्ड";
                ldoCondition.GoogleDocID = applicant.AdharCardFU;
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "अर्जदाराचे नाव";
                ldoCondition.ApprovalValue = applicant.ApName;
                ldoCondition.LDORemarks = string.Empty;
                db.LDOCondition.Add(ldoCondition);
                //2
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 2;
                ldoCondition.DocNumber = 1;
                ldoCondition.DocName = "आधार कार्ड";
                ldoCondition.GoogleDocID = applicant.AdharCardFU;
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "पत्रव्यवहाराचा पत्ता";
                ldoCondition.ApprovalValue = applicant.VilName + "-" + applicant.TalukaName + "-" + applicant.DistrictName;
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);
                //3
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 3;
                ldoCondition.DocNumber = 0;
                ldoCondition.DocName = " ";
                ldoCondition.GoogleDocID = string.Empty;
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "भ्रमणध्वनी क्रमांक";
                ldoCondition.LDORemarks = string.Empty;

                ldoCondition.ApprovalValue = applicant.PhNo.Value.ToString();
                db.LDOCondition.Add(ldoCondition);

                //4
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 4;
                ldoCondition.DocNumber = 1;
                ldoCondition.DocName = "आधार कार्ड";
                ldoCondition.GoogleDocID = applicant.AdharCardFU;
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "अर्जदाराची जन्म तारीख";
                ldoCondition.ApprovalValue = applicant.DOB;
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);



                //5
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 5;
                ldoCondition.DocNumber = 1;
                ldoCondition.DocName = "आधार कार्ड";
                ldoCondition.GoogleDocID = applicant.AdharCardFU;
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "लिंग";
                ldoCondition.ApprovalValue = applicant.Gender;
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);

                //6
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 6;
                ldoCondition.DocNumber = 6;
                ldoCondition.DocName = "अपत्य दाखला";
                ldoCondition.GoogleDocID = applicant.Childcertificate;
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "एकूण अपत्यांची संख्या";
                ldoCondition.ApprovalValue = applicant.ChildCount + "- Child after 2001-" + applicant.Child2006;
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);

                //7
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 7;
                ldoCondition.DocNumber = 3;
                ldoCondition.DocName = "जातीचा दाखला";
                ldoCondition.GoogleDocID = applicant.CasteCertificate;
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "अर्जदाराची जात";
                ldoCondition.ApprovalValue = applicant.Caste + "-" + applicant.SubCasteName;
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);
                //8
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 8;
                ldoCondition.DocNumber = 1;
                ldoCondition.DocName = "आधार कार्ड";
                ldoCondition.GoogleDocID = applicant.AdharCardFU;
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "आधार कार्ड क्रमांक";
                ldoCondition.ApprovalValue = applicant.AdharCardNo.ToString();
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);
                //9
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 9;
                ldoCondition.DocNumber = 12;
                ldoCondition.DocName = "अपंगत्व प्रमाणपत्र";
                ldoCondition.GoogleDocID = applicant.DisabilityCertificate;
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "अर्जदार अपंग आहे काय?";
                ldoCondition.ApprovalValue = applicant.ApplicantCrippled + "-" + applicant.CrippledPercentage;
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);
                //10
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 10;
                ldoCondition.DocNumber = 12;
                ldoCondition.DocName = "संबंधीत पशुवैद्यकीय दवाखान्याचे पशुधन विकास अधिकारी यांचे प्रमाणपत्र";
                ldoCondition.GoogleDocID = applicant.LivestockDevOffCertificate;
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "अर्जदाराकडे सद्यस्थितीत मेंढया आहेत काय?";
                ldoCondition.ApprovalValue = applicant.PresentDaySheep;
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);
                //11
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 11;
                ldoCondition.DocNumber = 12;
                ldoCondition.DocName = "संबंधीत पशुवैद्यकीय दवाखान्याचे पशुधन विकास अधिकारी यांचे प्रमाणपत्र";
                ldoCondition.GoogleDocID = applicant.LivestockDevOffCertificate;
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "अर्जदार कायमस्वरूपी एका ठिकाणी राहून मेंढी पालन करणारे (स्थायी) आहेत काय?";
                ldoCondition.ApprovalValue = applicant.ApplicantsPermanentInOnePlace;
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);
                //12
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 12;
                ldoCondition.DocNumber = 12;
                ldoCondition.DocName = "संबंधीत पशुवैद्यकीय दवाखान्याचे पशुधन विकास अधिकारी यांचे प्रमाणपत्र";
                ldoCondition.GoogleDocID = applicant.LivestockDevOffCertificate;
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "अर्जदार स्थलांतर पद्धतीने मेंढीपालन करणारे (स्थलांतरीत) आहेत काय?";
                ldoCondition.ApprovalValue = applicant.ApplicantsMigratedByWayOfTransit;
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);
                //13
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 13;
                ldoCondition.DocNumber = 7;
                ldoCondition.DocName = "शेतजमिनीचा ७/१२ उतारा किंवा कुटुंबियांपैकी व्यक्तीच्या नावाच्या ७/१२ उतारा व संमतीपत्र.";
                ldoCondition.GoogleDocID = applicant.FU712orIncomeCertificate;
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "अर्जदाराच्या मालकीची शेत जमीन आहे काय?";
                ldoCondition.ApprovalValue = applicant.IsApplicantOwnedLand;
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);
                //14
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 14;
                ldoCondition.DocNumber = 8;
                ldoCondition.DocName = "भाडेतत्वावर शेतजमीन घेतली असल्यास शेतजमीन मालकासमवेत केलेल्या भाडेकराराची सत्यप्रत";
                ldoCondition.GoogleDocID = applicant.TenancyAgreement;
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "नसल्यास,भाडेकरारावर शेत जमीन उपलब्ध केली आहे काय?";
                ldoCondition.ApprovalValue = applicant.IsNotIsAvailableOnLease;
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);
                //15
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 15;
                ldoCondition.DocNumber = 0;
                ldoCondition.DocName = "अर्जदाराने दिलेल्या माहितीनुसार";
                ldoCondition.GoogleDocID = "";
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "जमिनीचा तपशील";
                ldoCondition.ApprovalValue = "बागायत:" + applicant.GardeningEcre + " हेक्टर- जिरायत:" + applicant.CuminEcre + "हेक्टर";
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);
                //16
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 16;
                ldoCondition.DocNumber = 0;
                ldoCondition.DocName = "अर्जदाराने दिलेल्या माहितीनुसार";
                ldoCondition.GoogleDocID = "";
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "बारमाही/हंगामी सिंचनाकरिता पाण्याचा स्रोत";
                ldoCondition.ApprovalValue = applicant.WaterSource;
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);
                //17
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 17;
                ldoCondition.DocNumber = 0;
                ldoCondition.DocName = "अर्जदाराने दिलेल्या माहितीनुसार";
                ldoCondition.GoogleDocID = "";
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "सिंचनाकरिता पाणी उपलब्ध असल्याचा कालावधी";
                ldoCondition.ApprovalValue = applicant.DurationOfWater;
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);
                //18
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 18;
                ldoCondition.DocNumber = 0;
                ldoCondition.DocName = "अर्जदाराने दिलेल्या माहितीनुसार";
                ldoCondition.GoogleDocID = "";
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "मागील वर्षी पिकविण्यात आलेले चारापिके व चाऱ्याचे एकूण उत्पादन(किलो)";
                ldoCondition.ApprovalValue = applicant.LastYearTotalProductionInKG != null ? applicant.LastYearTotalProductionInKG.Value.ToString() : "";
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);
                //19
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 19;
                ldoCondition.DocNumber = 0;
                ldoCondition.DocName = "अर्जदाराने दिलेल्या माहितीनुसार";
                ldoCondition.GoogleDocID = "";
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "अर्जदाराकडे मेंढ्यांसाठी वाडा उपलब्ध आहे काय?";
                ldoCondition.ApprovalValue = applicant.IsWarehouseForSheep;
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);
                //20
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 20;
                ldoCondition.DocNumber = 9;
                ldoCondition.DocName = "१ गुंठा जागा उपलब्ध असल्याबाबत ७/१२ उतारा / मिळकत दाखला.";
                ldoCondition.GoogleDocID = applicant.ShedCertificate;
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "नसल्यास शेड बांधकामासाठी स्वत: ची किमान १ गुंठा जागा उपलब्ध आहे काय?";
                ldoCondition.ApprovalValue = applicant.YesIntekOfSheepInWarehouse != null ? applicant.YesIntekOfSheepInWarehouse.Value.ToString() : "";
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);
                //21
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 21;
                ldoCondition.DocNumber = 10;
                ldoCondition.DocName = "बचतगटाचे सदस्य असल्याचे प्रमाणपत्र";
                ldoCondition.GoogleDocID = applicant.BachatMemberCertificate;
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "अर्जदार बचत गटाचा सदस्य आहे काय?";
                ldoCondition.ApprovalValue = applicant.IsSavingsGroupMember + "- नाव:" + applicant.SavingGroupName +
                     "- क्रमांक:" + applicant.SavingGroupRegNumber;
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);
                //22
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 22;
                ldoCondition.DocNumber = 11;
                ldoCondition.DocName = "पशुपालक उत्पादक कंपनीचे सदस्य असल्याचे प्रमाणपत्र";
                ldoCondition.GoogleDocID = applicant.CompanyMemberCertificate;
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "अर्जदार पशुपालक उत्पादक कंपनीचा सदस्य आहे काय?";
                ldoCondition.ApprovalValue = applicant.IsanimalHusbandryManufacturingCompanyMember + "- नाव:" + applicant.IsanimalHusbandryManufacturingCompanyName +
                     "- क्रमांक:" + applicant.IsanimalHusbandryManufacturingCompanyRegNumber;
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);
                //24 - Age Condition
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 23;
                ldoCondition.DocNumber = 1;
                ldoCondition.DocName = "आधार कार्ड";
                ldoCondition.GoogleDocID = applicant.AdharCardFU;
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "Is Age Valid? (Below 60 years and above 18 years)";
                ldoCondition.ApprovalValue = applicant.Age != null ? applicant.Age.ToString() : "0";
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);

                //25 - Age Condition
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 24;
                ldoCondition.DocNumber = 0;
                ldoCondition.DocName = "अर्जदाराने दिलेल्या माहितीनुसार";
                ldoCondition.GoogleDocID = "";
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "Child Count after 2001";
                ldoCondition.ApprovalValue = applicant.Child2006 != null ? applicant.Child2006.ToString() : "0";
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);

                //25 - Previously Selected Condition
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 25;
                ldoCondition.DocNumber = 0;
                ldoCondition.DocName = "अर्जदाराने दिलेल्या माहितीनुसार";
                ldoCondition.GoogleDocID = "";
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "Benefits Availed Previously?";
                ldoCondition.ApprovalValue = db.BeneficiarySelectedList2018.Any(x => x.Aadhar == applicant.AdharCardNo).ToString();
                ldoCondition.LDORemarks = string.Empty;

                db.LDOCondition.Add(ldoCondition);

                //23
                ldoCondition = new LDOConditions();
                ldoCondition.ApplicantID = applicant.Id;
                ldoCondition.AadharNumber = applicant.AdharCardNo;
                ldoCondition.DistCode = applicant.Dist.Value;
                ldoCondition.TalukaCode = applicant.Tahashil.Value;
                ldoCondition.ConditionNo = 26;
                ldoCondition.DocNumber = 0;
                ldoCondition.DocName = "";
                ldoCondition.GoogleDocID = "";
                ldoCondition.LDOApproved = false;
                ldoCondition.ApprovalCondition = "योजनेच्या वरील घटकाचा लाभ घेण्यासाठी अर्जदार पात्र आहे का";
                ldoCondition.LDORemarks = string.Empty;
                ldoCondition.ApprovalValue = applicant.CompNumber;
              
                db.LDOCondition.Add(ldoCondition);

                db.SaveChanges();
            }
            var ldoConditions = db.LDOCondition.Where(x => x.ApplicantID == id).ToList();
            var prelimList = db.PreliminaryList.Where(x => x.ApplicantID == id).FirstOrDefault();
            model.LDOConditions = ldoConditions;
            var compData = "";
            Components d = (Components)Enum.Parse(typeof(Components), applicant.CompNumber);
            compData = d.GetDescription();
            model.LDOConditions[24].ComponentDesc = compData;
            model._applicant = applicant;
            model.DistCode = applicant.Dist.Value;
            model.DistName = db.DistMaster.Where(x => x.Dist_Code == model.DistCode).Select(x => x.DistName).FirstOrDefault();
            model.TalukaCode = applicant.Tahashil.Value;
            model.TalukaName = db.TalMaster.Where(x => x.Dist_Code == model.DistCode && x.Tal_Code == model.TalukaCode).Select(x => x.TalName).FirstOrDefault();

            model.Preliminaries = new List<PreliminaryList>();
            model.Preliminaries.Add(prelimList);
            return View(model);
        }

        public string GetIPAddress()
        {
            string IPAddress = "";
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }

        [HttpPost]
        public ActionResult RecommenendApplicant(RecommendationViewModel model, string submit)
        {
            foreach (var item in model.LDOConditions)
            {
                var ldoCond = db.LDOCondition.Where(x => x.Id == item.Id).FirstOrDefault();
                ldoCond.LDOApproved = item.LDOApproved;
                ldoCond.LDORemarks = item.LDORemarks;
                db.Entry(ldoCond).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            var prelim = db.PreliminaryList.Where(x => x.ApplicantID == model.ApplicantId).FirstOrDefault();
            prelim.LDOName = model.Preliminaries[0].LDOName;
            prelim.LDORecommended = model.Preliminaries[0].LDORecommended;
            prelim.LDORemarks = model.Preliminaries[0].LDORemarks;
            prelim.LDOSubmitDate = DateTime.Now;
            prelim.LDO_ComponentApproved = model.Preliminaries[0].LDO_ComponentApproved;
            prelim.LDO_ComponentRemarks = model.Preliminaries[0].LDO_ComponentRemarks;
            prelim.LDO_Ip = GetIPAddress();
            switch (submit)
            {
                case "save":
                    prelim.SavedByLDO = true;
                    break;
                case "submit":
                    prelim.SavedByLDO = false;
                    break;
            }
            db.Entry(prelim).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("OfficerDashboard", "Menu", new { username = User.Identity.Name});
        }

        public ActionResult LDORecommendation(int id)
        {
            var ldoCondition = db.LDOCondition.Where(x => x.ApplicantID == id).ToList();
            var prelim = db.PreliminaryList.Where(x => x.ApplicantID == id).FirstOrDefault();
            var model = new RecommendationViewModel();
            model.Preliminaries = new List<PreliminaryList>();
            model.Preliminaries.Add(prelim);
            model._applicant = new ApplicantRegistration();
            model._applicant = db.ApplicantRegistrations.Where(x => x.Id == id).FirstOrDefault();
            model.ApplicantId = model._applicant.Id;
            model.LDOConditions = ldoCondition;
            var compData = "";
            Components d = (Components)Enum.Parse(typeof(Components), prelim.Component.ToString());
            compData = d.GetDescription();
            model.LDOConditions[24].ComponentDesc = compData;
            model.DistCode = model._applicant.Dist.Value;
            model.DistName = db.DistMaster.Where(x => x.Dist_Code == model.DistCode).Select(x => x.DistName).FirstOrDefault();
            model.TalukaCode = model._applicant.Tahashil.Value;
            model.TalukaName = db.TalMaster.Where(x => x.Dist_Code == model.DistCode && x.Tal_Code == model.TalukaCode).Select(x => x.TalName).FirstOrDefault();

            return View(model);
        }

        public ActionResult DAHORecommendation(int id)
        {
            var prelim = db.PreliminaryList.Where(x => x.ApplicantID == id).FirstOrDefault();
            var model = new RecommendationViewModel();
            model.Preliminaries = new List<PreliminaryList>();
            model.Preliminaries.Add(prelim);
            model._applicant = new ApplicantRegistration();
            model._applicant = db.ApplicantRegistrations.Where(x => x.Id == id).FirstOrDefault();
            model.ApplicantId = model._applicant.Id;
            model.DistCode = model._applicant.Dist.Value;
            model.DistName = db.DistMaster.Where(x => x.Dist_Code == model.DistCode).Select(x => x.DistName).FirstOrDefault();
            model.TalukaCode = model._applicant.Tahashil.Value;
            model.TalukaName = db.TalMaster.Where(x => x.Dist_Code == model.DistCode && x.Tal_Code == model.TalukaCode).Select(x => x.TalName).FirstOrDefault();

            return View(model);
        }



        public ActionResult DAHORecommend(int id)
        {
            var prelim = db.PreliminaryList.Where(x => x.ApplicantID == id).FirstOrDefault();
            var model = new RecommendationViewModel();
            model.Preliminaries = new List<PreliminaryList>();
            model.Preliminaries.Add(prelim);
            model._applicant = db.ApplicantRegistrations.Where(x => x.Id == id).FirstOrDefault();
            model.ApplicantId = model._applicant.Id;
            model.DistCode = model._applicant.Dist.Value;
            return View(model);
        }

        [HttpPost]
        public ActionResult DAHORecommend(RecommendationViewModel model, string submit)
        {
            var prelim = db.PreliminaryList.Where(x => x.ApplicantID == model.ApplicantId).FirstOrDefault();
            prelim.DAHOName = model.Preliminaries[0].DAHOName;
            prelim.DAHORecommended = model.Preliminaries[0].DAHORecommended;
            prelim.DAHORemarks = model.Preliminaries[0].DAHORemarks;
            prelim.DAHOSubmitDate = DateTime.Now;
            prelim.DAHO_Ip = GetIPAddress();
            switch (submit)
            {
                case "save":
                    prelim.SavedByDAHO = true;
                    break;
                case "submit":
                    prelim.SavedByDAHO = false;
                    break;
            }
            db.Entry(prelim).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("OfficerDashboard", "Menu", new { username = User.Identity.Name });
        }

        public ActionResult DDCRecommend(int id)
        {
            var prelim = db.PreliminaryList.Where(x => x.ApplicantID == id).FirstOrDefault();
            var model = new RecommendationViewModel();
            model.Preliminaries = new List<PreliminaryList>();
            model.Preliminaries.Add(prelim);
            model._applicant = db.ApplicantRegistrations.Where(x => x.Id == id).FirstOrDefault();
            model.ApplicantId = model._applicant.Id;
            model.DistCode = model._applicant.Dist.Value;
            return View(model);
        }

        [HttpPost]
        public ActionResult DDCRecommend(RecommendationViewModel model, string submit)
        {
            var prelim = db.PreliminaryList.Where(x => x.ApplicantID == model.ApplicantId).FirstOrDefault();
            prelim.DDCName = model.Preliminaries[0].DDCName;
            prelim.DDCRecommended = model.Preliminaries[0].DDCRecommended;
            prelim.DDCRemarks = model.Preliminaries[0].DDCRemarks;
            prelim.DDCSubmitDate = DateTime.Now;
            prelim.DDC_Ip = GetIPAddress();
            switch (submit)
            {
                case "save":
                    prelim.SavedByDDC = true;
                    break;
                case "submit":
                    prelim.SavedByDDC = false;
                    break;
            }
            db.Entry(prelim).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("OfficerDashboard", "Menu", new { username = User.Identity.Name });
        }
    }
}