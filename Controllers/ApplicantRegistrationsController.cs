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
                li.Add(new SelectListItem { Text = m.Caste, Value = m.ID.ToString() });
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
        public ActionResult Create()
        {
            getDistrict();
            getCaste();
            getCripplePercent();
            getNoOfSheep();
            getAcre();
            getWaterSource();
            getTypeCastle();
            getDuration();
            return View();
        }

        // POST: ApplicantRegistrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicantRegistration applicantRegistration, HttpPostedFileBase files)
        {
            var userList = db.ApplicantRegistrations.Select(x => x.AdharCardNo).ToList();
            if(userList.Any(x=>x == applicantRegistration.AdharCardNo))
            {
                ViewBag.Error = "User with this Aadhar Number already exist.";
                return View();
            }
            if (ModelState.IsValid)
            {
                if (files != null && files.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(files.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/ApplicantPhoto"), fileName);
                    files.SaveAs(path);
                    var relativePath = "/Images/ApplicantPhoto/" + fileName;
                    applicantRegistration.Photo = relativePath;
                }


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
            getDistrict();
            getCaste();
            getCripplePercent();
            getNoOfSheep();
            getAcre();
            getWaterSource();
            getTypeCastle();
            getDuration();
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

        // POST: ApplicantRegistrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicantRegistration applicantRegistration)
        {
           
            if (ModelState.IsValid)
            {
                db.Entry(applicantRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
