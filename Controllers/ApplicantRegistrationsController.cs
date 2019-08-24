using Mahamesh.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mahamesh.Controllers
{
    public class ApplicantRegistrationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicantRegistrations
        public ActionResult Index()
        {
            return View(db.ApplicantRegistrations.ToList());
        }

        // GET: ApplicantRegistrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantRegistration applicantRegistration = db.ApplicantRegistrations.Find(id);
            //   var _caste = db.CasteUnderNTC.Where(x=>x.Caste = applicantRegistration.SubCatse);
            var _dist = db.DistMaster.Where(x => x.Dist_Code == applicantRegistration.Dist).Select(x => x.DistName).FirstOrDefault();
            var _tal = db.TalMaster.Where(x => x.Dist_Code == applicantRegistration.Tahashil).Select(x=>x.TalName).FirstOrDefault();
            var _vil = db.VillageMaster.Where(x => x.Village_Code == applicantRegistration.VillageName).Select(x => x.VillageName).FirstOrDefault();
            var _hvil = db.VillageMaster.Where(x => x.Village_Code == applicantRegistration.HVillage).Select(x => x.VillageName).FirstOrDefault();

            // applicantRegistration.SubCasteName = _caste;
            applicantRegistration.DistrictName = _dist;
            applicantRegistration.TalukaName = _tal;
            applicantRegistration.VilName = _vil;
            applicantRegistration.HvilName = _hvil;
            if (applicantRegistration == null)
            {
                return HttpNotFound();
            }
            return View(applicantRegistration);
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


        public void getWaterSource()
        {
            var ddlDist = db.WaterSource.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select Source--", Value = "0" });

            foreach (var m in ddlDist)
            {
                li.Add(new SelectListItem { Text = m.SourceName, Value = m.ID.ToString() });
                ViewBag.WaterSource = li;
            }
        }

        public void getDuration()
        {
            var ddlDist = db.DurationWaterAvailableForIrrigation.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select Duration--", Value = "0" });

            foreach (var m in ddlDist)
            {
                li.Add(new SelectListItem { Text = m.DurationName, Value = m.ID.ToString() });
                ViewBag.Duration = li;
            }
        }

        public void getAcre()
        {
            var ddlDist = db.AcreMaster.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select--", Value = "0" });

            foreach (var m in ddlDist)
            {
                li.Add(new SelectListItem { Text = m.Acre.ToString(), Value = m.id.ToString() });
                ViewBag.Acre = li;
            }
        }

        public void getTypeCastle()
        {
            var ddlDist = db.TypeExistingCastle.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select--", Value = "0" });

            foreach (var m in ddlDist)
            {
                li.Add(new SelectListItem { Text = m.TypeName.ToString(), Value = m.ID.ToString() });
                ViewBag.TypeCastle = li;
            }
        }

        public void getCaste()
        {
            var ddlCaste = db.CasteUnderNTC.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select Caste--", Value = "0" });

            foreach (var m in ddlCaste)
            {
                li.Add(new SelectListItem { Text = m.Caste, Value = m.Caste.ToString() });
                ViewBag.Caste = li;
            }
        }

        public void getCripplePercent()
        {
            var ddlCripple = db.CrippledMaster.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select --", Value = "0" });

            foreach (var m in ddlCripple)
            {
                li.Add(new SelectListItem { Text = m.Percentage.ToString(), Value = m.ID.ToString() });
                ViewBag.Percentage = li;
            }
        }

        public void getNoOfSheep()
        {
            var ddlSheep = db.NoOfSheepMaster.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select --", Value = "0" });

            foreach (var m in ddlSheep)
            {
                li.Add(new SelectListItem { Text = m.NoOfSheep, Value = m.id.ToString() });
                ViewBag.Sheep = li;
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


        // GET: ApplicantRegistrations/Create
        public ActionResult Create(long aadhar)
        {
            getDistrict();
            getCaste();
            getCripplePercent();
            getNoOfSheep();
            getAcre();
            getWaterSource();
            getTypeCastle();
            getDuration();
            var model = new ApplicantRegistration();
            model.AdharCardNo = aadhar;
            model.Caste = "भटक्या जमातीचा - क";
            return View(model);
        }

        // POST: ApplicantRegistrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicantRegistration applicantRegistration, HttpPostedFileBase file)
        {
            var userList = db.ApplicantRegistrations.Select(x => x.AdharCardNo).ToList();
            if(userList.Any(x=>x == applicantRegistration.AdharCardNo))
            {
                ViewBag.Error = "User with this Aadhar Number already exist.";
                getDistrict();
                getCaste();
                getCripplePercent();
                getNoOfSheep();
                getAcre();
                getWaterSource();
                getTypeCastle();
                getDuration();
                return View(applicantRegistration);
            }
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/ApplicantPhoto"), fileName);
                    file.SaveAs(path);
                    var relativePath = "/Images/ApplicantPhoto/" + fileName;
                    applicantRegistration.Photo = relativePath;
                }

                applicantRegistration.SubmitDatetime = DateTime.Now;
                var id = db.ApplicantRegistrations.OrderByDescending(x => x.Id).Select(x=>x.Id).FirstOrDefault();
                applicantRegistration.ApplicationNumber = "530 / 4286 / 567358 /" + (id + 1);
                db.ApplicantRegistrations.Add(applicantRegistration);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = applicantRegistration.Id });
            }


            getDistrict();
            getCaste();
            getCripplePercent();
            getNoOfSheep();
            getAcre();
            getWaterSource();
            getTypeCastle();
            getDuration();
            return View(applicantRegistration);
        }

        // GET: ApplicantRegistrations/Edit/5
        public ActionResult Edit(int? id)
        {
           
            var ddlDist = db.DistMaster.ToList();
            var ddlTal = db.TalMaster.ToList();
            var ddlCaste = db.CasteUnderNTC.ToList();
            var ddlSheep = db.NoOfSheepMaster.ToList();
            var ddAcre = db.AcreMaster.ToList();
            var ddWater = db.WaterSource.ToList();
            var typecastle = db.TypeExistingCastle.ToList();
            var duration = db.DurationWaterAvailableForIrrigation.ToList();
            var cripple = db.CrippledMaster.ToList();
            var ddlVil = db.VillageMaster.ToList();
          

            ApplicantRegistration applicantRegistration = db.ApplicantRegistrations.Find(id);
            ViewBag.Dist = new SelectList(ddlDist,  "Dist_Code", "DistName", applicantRegistration.Dist);
            ViewBag.Caste = new SelectList(ddlCaste, "ID", "Caste", applicantRegistration.SubCatse);
            ViewBag.Taluka = new SelectList(ddlTal,  "Tal_Code", "Talname", applicantRegistration.Tahashil);
            ViewBag.Village = new SelectList(ddlVil, "Village_Code", "VillageName", applicantRegistration.VillageName);
            ViewBag.HVilage = new SelectList(ddlVil, "Village_Code", "VillageName", applicantRegistration.HVillage);
            ViewBag.WaterSource = new SelectList(ddWater, "ID", "SourceName", applicantRegistration.WaterSource);
            ViewBag.Sheep = new SelectList(ddlSheep, "id", "NoOfSheep", applicantRegistration.NumberOfSheepIs);
            ViewBag.TypeCastle = new SelectList(typecastle, "ID", "TypeName",  applicantRegistration.TypeExistingCastle);
            ViewBag.Duration = new SelectList(duration, "ID", "DurationName", applicantRegistration.DurationOfWater);
            ViewBag.Percentage = new SelectList(cripple, "ID", "Percentage", applicantRegistration.CrippledPercentage);
            ViewBag.AcreOwned = new SelectList(ddAcre, "id", "Acre", applicantRegistration.YesApplicantOwnedLandEcre);
            ViewBag.AcreLeased = new SelectList(ddAcre, "id", "Acre",  applicantRegistration.YesAvailableOnLeaseEcre);
            ViewBag.GardenAcre = new SelectList(ddAcre, "id", "Acre",  applicantRegistration.GardeningEcre);
            ViewBag.CuminAcre = new SelectList(ddAcre, "id", "Acre",  applicantRegistration.CuminEcre);

            if (applicantRegistration == null)
            {
                return HttpNotFound();
            }
            return View(applicantRegistration);
        }

        // POST: ApplicantRegistrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicantRegistration applicantRegistration, HttpPostedFileBase file)
        {
           
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/ApplicantPhoto"), fileName);
                    file.SaveAs(path);
                    var relativePath = "/Images/ApplicantPhoto/" + fileName;
                    applicantRegistration.Photo = relativePath;
                }
                applicantRegistration.SubmitDatetime = DateTime.Now;
                db.Entry(applicantRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = applicantRegistration.Id });
            }
            getDistrict();
            getCaste();
            getCripplePercent();
            getNoOfSheep();
            getAcre();
            getWaterSource();
            getTypeCastle();
            getDuration();
            return View(applicantRegistration);
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

        public ActionResult GenerateReceipt(int id)
        {
            var application = db.ApplicantRegistrations.Find(id);
            application.FormSubmitted = true;
            application.SubmitDatetime = DateTime.Now;
            application.UserIP = GetIPAddress();
            db.Entry(application).State = EntityState.Modified;
            db.SaveChanges();

            return View(application);
        }

        // GET: ApplicantRegistrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantRegistration applicantRegistration = db.ApplicantRegistrations.Find(id);
            if (applicantRegistration == null)
            {
                return HttpNotFound();
            }
            return View(applicantRegistration);
        }

        // POST: ApplicantRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApplicantRegistration applicantRegistration = db.ApplicantRegistrations.Find(id);
            db.ApplicantRegistrations.Remove(applicantRegistration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
