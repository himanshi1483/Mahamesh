using Mahamesh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mahamesh.Controllers
{
    public class MenuController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
          
            return View();
        }

        public void getDistrict()
        {
            var ddlDist = db.DistMaster.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select District--", Value = "0" });

            foreach (var m in ddlDist)
            {
                li.Add(new SelectListItem { Text = m.DistName, Value = m.Dist_Code.ToString() });
                ViewBag.District = li;
            }
        }


        public JsonResult getTaluka(int id)
        {
            var ddlTal = db.TalMaster.Where(x => x.Dist_Code == id).ToList();
            List<SelectListItem> liTaluka = new List<SelectListItem>();

            liTaluka.Add(new SelectListItem { Text = "--Select Taluka--", Value = "0" });
            if (ddlTal != null)
            {
                foreach (var x in ddlTal)
                {
                    liTaluka.Add(new SelectListItem { Text = x.TalName, Value = x.Tal_Code.ToString() });
                }
            }
            return Json(new SelectList(liTaluka, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        public JsonResult getVillage(int id)
        {
            var ddlVil = db.VillageMaster.Where(x => x.Tal_Code == id).ToList();
            List<SelectListItem> liVil = new List<SelectListItem>();

            liVil.Add(new SelectListItem { Text = "--Select Village--", Value = "0" });
            if (ddlVil != null)
            {
                foreach (var x in ddlVil)
                {
                    liVil.Add(new SelectListItem { Text = x.VillageName, Value = x.Village_Code.ToString() });
                }
            }
            return Json(new SelectList(liVil, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        // GET: Menu
        public ActionResult AdminPanel()
        {
            var ImgFolders = db.MediaFolders.Where(x=>x.MediaType == "Pictures").ToList();
            ViewBag.ImgFolders = new SelectList(ImgFolders, "FolderName", "FolderName");
            var VidFolders = db.MediaFolders.Where(x => x.MediaType == "Videos").ToList();
            ViewBag.VidFolders = new SelectList(VidFolders, "FolderName", "FolderName");
            var model = new AdminPanelViewModel();
            model.TenderList = db.TenderModels.ToList();
            model.NewsList = db.NewsModels.ToList();
            model.PressList = db.PressInformationModels.ToList();
            model.FolderList = db.MediaFolders.ToList();
            model.FeedbackList = db.FeedbackModels.ToList();
            return View(model);
        }

        // GET: Menu
        public ActionResult MahameshYojana()
        {
            return View();
        }

        public ActionResult MahameshYojanaHighlights()
        {
            return View();
        }

        public ActionResult MahameshYojanaTimeTable()
        {
            return View();
        }

        public ActionResult MahameshYojanaGuidelines()
        {
            return View();
        }
        public ActionResult MahameshYojanaTargets()
        {
            var ddlDist = db.DistMaster.ToList();
            ViewBag.Dist = new SelectList(ddlDist, "Dist_Code", "DistName");
            return View();
        }

        public ActionResult MahameshYojanaBeneficiary()
        {
            return View();
        }
        public ActionResult MahameshYojanaUserLogin(string msg)
        {
            if(msg != null)
                ViewBag.Msg = "Your appliation has been submitted successfully. To view the appliation, please login again.";

            var applicationTime = new ApplicantRegistration();
            applicationTime.appDuration = db.ApplicationDuration.FirstOrDefault();

            return View(applicationTime);
        }
        [HttpPost]
        public ActionResult MahameshYojanaUserLogin(long AdharCardNo)
        {
            var applicantExist = db.ApplicantRegistrations.Any(x => x.AdharCardNo == AdharCardNo);
            if(applicantExist == true)
            {
                var applicantData = db.ApplicantRegistrations.Where(x => x.AdharCardNo == AdharCardNo).FirstOrDefault();
                if(applicantData.FormSubmitted == true)
                {
                    return RedirectToAction("UserIndex", "ApplicantRegistrations", new { id = applicantData.Id });
                }
                else
                {
                    return RedirectToAction("Edit", "ApplicantRegistrations", new { id = applicantData.Id });
                }
            }
            else
            {
                return RedirectToAction("Create", "ApplicantRegistrations", new { @aadhar = AdharCardNo });
            }
            return View();
        }
        public ActionResult MahameshYojanaOfficerLogin()
        {
            return View();
        }
        public ActionResult MahameshYojanaContact()
        {
            return View();
        }
        public ActionResult MahameshYojanaDownloads()
        {
            return View();
        }
        public ActionResult MahameshYojanaUserManual()
        {
            return View();
        }
        public ActionResult MahameshYojanaVideo()
        {
            return View();
        }

        public ActionResult MahameshYojanaApp()
        {
            return View();
        }

        // GET: Menu
        public ActionResult ProductsAtFarm()
        {
            return View();
        }

        // GET: Menu
        public ActionResult ProductsWool()
        {
            return View();
        }

        // GET: Menu
        public ActionResult StockAvailability()
        {
            return View();
        }

        // GET: Menu
        public ActionResult DeptGuidelines()
        {
            return View();
        }

        // GET: Menu
        public ActionResult DeptGuidelinesGR()
        {
            return View();
        }
        // GET: Menu
        public ActionResult BeneficiaryList()
        {
            return View();
        }

        // GET: Menu
        public ActionResult ProjectStatus()
        {
            return View();
        }

        // GET: Menu
        public ActionResult OfficeDocuments()
        {
            return View();
        }

        // GET: Menu
        public ActionResult AuditedReports()
        {
            return View();
        }

        // GET: Menu
        public ActionResult Information()
        {
            return View();
        }

        // GET: Menu
        public ActionResult OfficersList()
        {
            return View();
        }

        // GET: Menu
        public ActionResult InseminationInformation()
        {
            return View();
        }

        // GET: Menu
        public ActionResult HospitalsInformation()
        {
            return View();
        }

        // GET: Menu
        public ActionResult BreedsAvailable()
        {
            return View();
        }

        // GET: Menu
        public ActionResult VaccinationList()
        {
            return View();
        }

        // GET: Menu
        public ActionResult FeedingInformation()
        {
            return View();
        }

        // GET: Menu
        public ActionResult BreedingInformation()
        {
            return View();
        }

        // GET: Menu
        public ActionResult OngoingSchemes()
        {
            return View();
        }

        // GET: Menu
        public ActionResult AnimalTradingPolicy()
        {
            return View();
        }

        // GET: Menu
        public ActionResult ModelScheme()
        {
            return View();
        }

        // GET: Menu
        public ActionResult FeedandFodder()
        {
            return View();
        }

        // GET: Menu
        public ActionResult ProductAvailibility()
        {
            return View();
        }

        // GET: Menu
        public ActionResult SuccessStories()
        {
            return View();
        }

        // GET: Menu
        public ActionResult FarmersList()
        {
            return View();
        }


        // GET: Menu
        public ActionResult LivestockInsurance()
        {
            return View();
        }


        // GET: Menu
        public ActionResult Precautions()
        {
            return View();
        }


        // GET: Menu
        public ActionResult WebsitePolicy()
        {
            return View();
        }
    }
}