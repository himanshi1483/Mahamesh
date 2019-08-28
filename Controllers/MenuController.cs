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
            var ddlDist = db.Comp1Target.Select(x=>x.DistrictName).Distinct().ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select District--", Value = "0" });

            foreach (var m in ddlDist)
            {
                li.Add(new SelectListItem { Text = m, Value = m.ToString() });
                ViewBag.District = li;
            }
        }


        public JsonResult getTaluka(string dist)
        {
            var d = db.Comp1PhysicalTargetTaluka.ToList();
            var ddlTal = db.Comp1PhysicalTargetTaluka.Where(x => x.DistrictName == dist).Select(x=>x.TalukaName).ToList();
            List<SelectListItem> liTaluka = new List<SelectListItem>();

            liTaluka.Add(new SelectListItem { Text = "--Select Taluka--", Value = "0" });
            if (ddlTal != null)
            {
                foreach (var x in ddlTal)
                {
                    liTaluka.Add(new SelectListItem { Text = x, Value = x.ToString() });
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
            getDistrict();
            //var ddlDist = db.Comp1Target.Select(x =>  x.DistrictName).Distinct().ToList();
            //ViewBag.Dist = new SelectList(ddlDist, "Id", "DistrictName");
            var model = new PhysicalTargetViewModel();
            //var comp1 = db.Comp1Target.ToList();
            //var comp1Taluka = db.Comp1PhysicalTargetTaluka.ToList();
            //model.Comp1TargetList = comp1;
            //model.Comp1TalukaList = comp1Taluka;
            //var comp2 = db.Comp2PhysicalTarget.ToList();
            //var comp2Taluka = db.Comp2PhysicalTargetTaluka.ToList();
            //model.Comp2TargetList = comp2;
            //model.Comp2TalukaList = comp2Taluka;
            //var comp3 = db.Comp3PhysicalTarget.ToList();
            //var comp3Taluka = db.Comp3PhysicalTargetTaluka.ToList();
            //model.Comp3TargetList = comp3;
            //model.Comp3TalukaList = comp3Taluka;
            //var comp4 = db.Comp4PhysicalTarget.ToList();
            //var comp4Taluka = db.Comp4PhysicalTargetTaluka.ToList();
            //model.Comp4TargetList = comp4;
            //model.Comp4TalukaList = comp4Taluka;

            model.Comp1TargetList = new List<Comp1Target>();
            model.Comp2TargetList = new List<CompTarget2>();
            model.Comp3TargetList = new List<Comp3PhysicalTarget>();
            model.Comp4TargetList = new List<Comp4PhysicalTarget>();
            model.Comp1TalukaList = new List<Comp1TalukaTarget>();
            model.Comp2TalukaList = new List<Comp2TargetTaluka>();
            model.Comp3TalukaList = new List<Comp3TargetTaluka>();
            model.Comp4TalukaList = new List<Comp4TargetTaluka>();
            return View(model);
        }

        [HttpPost]
        public ActionResult MahameshYojanaTargets(PhysicalTargetViewModel model1)
        {
            getDistrict();
            //var ddlDist = db.Comp1Target.Select(x=>x.DistrictName).Distinct().ToList();
            //ViewBag.Dist = new SelectList(ddlDist, "DistrictName", "DistrictName");
            var model = new PhysicalTargetViewModel();
            var list = new List<PhysicalTargetViewModel>();
            model.Comp1TargetList = new List<Comp1Target>();
            model.Comp2TargetList = new List<CompTarget2>();
            model.Comp3TargetList = new List<Comp3PhysicalTarget>();
            model.Comp4TargetList = new List<Comp4PhysicalTarget>();
            model.Comp1TalukaList = new List<Comp1TalukaTarget>();
            model.Comp2TalukaList = new List<Comp2TargetTaluka>();
            model.Comp3TalukaList = new List<Comp3TargetTaluka>();
            model.Comp4TalukaList = new List<Comp4TargetTaluka>();
            if ((model1.Component == "1") && (model1.TalukaName == null) && (model1.DistrictName != null))
            {
                //var comp1 = db.Comp1Target.ToList();
                model.Comp1TargetList = db.Comp1Target.Where(x => x.DistrictName == model1.DistrictName.Trim()).ToList();
                //var data = db.Comp1Target.Where(x => x.DistrictName == district).ToList();
                
            }
            else if ((model1.Component == "2") && (model1.TalukaName == null) && (model1.DistrictName != null))
            {
                //var comp2 = db.Comp2PhysicalTarget.ToList();
                model.Comp2TargetList = db.Comp2PhysicalTarget.Where(x => x.DistrictName == model1.DistrictName.Trim()).ToList();
            }
            else if ((model1.Component == "3") && (model1.TalukaName == null) && (model1.DistrictName != null))
            {
                //var comp3 = db.Comp1Target.ToList();
                model.Comp3TargetList = db.Comp3PhysicalTarget.Where(x => x.DistrictName == model1.DistrictName.Trim()).ToList();
            }
            else if ((model1.Component == "4") && (model1.TalukaName == null) && (model1.DistrictName != null))
            {
                //var comp4 = db.Comp1Target.ToList();
                model.Comp4TargetList = db.Comp4PhysicalTarget.Where(x => x.DistrictName == model1.DistrictName.Trim()).ToList();
            }
            else if ((model1.Component == "1") && (model1.TalukaName != null) && (model1.DistrictName != null))
            {
                //var comp1 = db.Comp1Target.ToList();
                model.Comp1TalukaList = db.Comp1PhysicalTargetTaluka.Where(x => x.DistrictName == model1.DistrictName.Trim() && x.TalukaName == model1.TalukaName.Trim()).ToList();
                //var data = db.Comp1Target.Where(x => x.DistrictName == district).ToList();

            }
            else if ((model1.Component == "2") && (model1.TalukaName != null) && (model1.DistrictName != null))
            {
                //var comp2 = db.Comp2PhysicalTarget.ToList();
                model.Comp2TalukaList = db.Comp2PhysicalTargetTaluka.Where(x => x.DistrictName == model1.DistrictName.Trim() && x.TalukaName == model1.TalukaName.Trim()).ToList();
            }
            else if ((model1.Component == "3") && (model1.TalukaName != null) && (model1.DistrictName != null))
            {
                //var comp3 = db.Comp1Target.ToList();
                model.Comp3TalukaList = db.Comp3PhysicalTargetTaluka.Where(x => x.DistrictName == model1.DistrictName.Trim() && x.TalukaName == model1.TalukaName).ToList();
            }
            else if ((model1.Component == "4") && (model1.TalukaName != null) && (model1.DistrictName != null))
            {
                //var comp4 = db.Comp1Target.ToList();
                model.Comp4TalukaList = db.Comp4PhysicalTargetTaluka.Where(x => x.DistrictName == model1.DistrictName.Trim() && x.TalukaName == model1.TalukaName).ToList();
            }

            //model.Comp1TargetList = comp1;
            //model.Comp1TalukaList = comp1Taluka;
            //var comp2 = db.Comp2PhysicalTarget.ToList();
            //var comp2Taluka = db.Comp2PhysicalTargetTaluka.ToList();
            //model.Comp2TargetList = comp2;
            //model.Comp2TalukaList = comp2Taluka;
            //var comp3 = db.Comp3PhysicalTarget.ToList();
            //var comp3Taluka = db.Comp3PhysicalTargetTaluka.ToList();
            //model.Comp3TargetList = comp3;
            //model.Comp3TalukaList = comp3Taluka;
            //var comp4 = db.Comp4PhysicalTarget.ToList();
            //var comp4Taluka = db.Comp4PhysicalTargetTaluka.ToList();
            //model.Comp4TargetList = comp4;
            //model.Comp4TalukaList = comp4Taluka;
            return View(model);
            //return Json(model, JsonRequestBehavior.AllowGet);
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