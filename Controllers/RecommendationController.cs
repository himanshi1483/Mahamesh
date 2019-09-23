using Mahamesh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                             PhNo = post.PhNo,
                             LDORecommended = meta.LDORecommended,
                             Gender = post.Gender,
                             DAHORecommended = false,
                             DDCRecommended = false,
                             Component = post.Component,
                             Id = post.Id,
                             CreatedOn = post.CreatedOn,
                             Type = post.Type,
                             DOB = applicant.DOB,
                             SubCaste = applicant.SubCasteName,
                             ChildCount = applicant.ChildCount.Value,
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
                             PhNo = post.PhNo,
                             LDORecommended = meta.LDORecommended,
                             Gender = post.Gender,
                             DAHORecommended = false,
                             DDCRecommended = false,
                             Component = post.Component,
                             Id = post.Id,
                             CreatedOn = post.CreatedOn,
                             Type = post.Type,
                             DOB = applicant.DOB,
                             SubCaste = applicant.SubCasteName,
                             ChildCount = applicant.ChildCount.Value,
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
                         PhNo = post.PhNo,
                         LDORecommended = meta.LDORecommended,
                         Gender = post.Gender,
                         DAHORecommended = false,
                         DDCRecommended = false,
                         Component = post.Component,
                         Id = post.Id,
                         CreatedOn = post.CreatedOn,
                         Type = post.Type,
                         DOB = applicant.DOB,
                         SubCaste = applicant.SubCasteName,
                         ChildCount = applicant.ChildCount.Value,
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
                       PhNo = post.PhNo,
                       LDORecommended = meta.LDORecommended,
                       Gender = post.Gender,
                       DAHORecommended = false,
                       DDCRecommended = false,
                       Component = post.Component,
                       Id = post.Id,
                       CreatedOn = post.CreatedOn,
                       Type = post.Type,
                       DOB = applicant.DOB,
                       SubCaste = applicant.SubCasteName,
                       ChildCount = applicant.ChildCount.Value,
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
            var applicantDoc = db.ApplicantDocument.Where(x => x.ApplicantID == id).ToList();
            var model = new RecommendationViewModel();
            model.ApplicantDocuments = applicantDoc;
            model.AdharCardFU = applicantDoc.Where(x => x.DocNumber == 1).Select(x => x.GoogleDocID).FirstOrDefault();
            model.CasteCertificate = applicantDoc.Where(x => x.DocNumber == 3).Select(x => x.GoogleDocID).FirstOrDefault();
            model.LivestockDevOffCertificate = applicantDoc.Where(x => x.DocNumber == 4).Select(x => x.GoogleDocID).FirstOrDefault();
            model.FU712orIncomeCertificate = applicantDoc.Where(x => x.DocNumber == 7).Select(x => x.GoogleDocID).FirstOrDefault();
            model.TenancyAgreement = applicantDoc.Where(x => x.DocNumber == 8).Select(x => x.GoogleDocID).FirstOrDefault();
            model.ShedCertificate = applicantDoc.Where(x => x.DocNumber == 9).Select(x => x.GoogleDocID).FirstOrDefault();
            model.BachatMemberCertificate = applicantDoc.Where(x => x.DocNumber == 10).Select(x => x.GoogleDocID).FirstOrDefault();
            model.Childcertificate = applicantDoc.Where(x => x.DocNumber == 6).Select(x => x.GoogleDocID).FirstOrDefault();
            model.DisabilityCertificate = applicantDoc.Where(x => x.DocNumber == 12).Select(x => x.GoogleDocID).FirstOrDefault();
            model.CompanyMemberCertificate = applicantDoc.Where(x => x.DocNumber == 11).Select(x => x.GoogleDocID).FirstOrDefault();
            var applicant = db.ApplicantRegistrations.Where(x => x.Id == id).FirstOrDefault();

            model._applicant = applicant;
            return View(model);
        }
    }
}