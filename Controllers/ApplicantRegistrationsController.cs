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

       [Authorize]
        public ActionResult Index()
        {
            return View(db.ApplicantRegistrations.ToList());
        }

        [Authorize]
        public ActionResult ApplicationIndexByDistrict()
        {            
            var model = new ApplicationListsViewModel();
            
            model.ApplicantsListByDist = new List<ApplicationListsViewModel>();
            model.ApplicantsListByTal = new List<ApplicationListsViewModel>();
            model.ApplicantsListByComp = new List<ApplicationListsViewModel>();

            var list = db.ApplicantRegistrations.ToList();
            var districts = db.DistMaster.ToList();
            var talukaList = db.TalMaster.ToList();
            var listModelDist = new List<ApplicationListsViewModel>();
            var listModelTal = new List<ApplicationListsViewModel>();
            var listModelComp = new List<ApplicationListsViewModel>();
            //District-wise
            foreach (var item in districts)
            {
                var _data1 = new ApplicationListsViewModel();
                var data = list.Where(x => x.Dist == item.Dist_Code).Count();
                _data1.CountByDistrict = data;
                _data1.DistrictCode = item.Dist_Code;
                _data1.DistrictName = item.DistName;
                listModelDist.Add(_data1);

                //Taluka-wise
                foreach (var taluka in talukaList.Where(x => x.Dist_Code == item.Dist_Code))
                {
                    var model2 = new ApplicationListsViewModel();
                    model2.CountByTaluka = list.Where(x => x.Tahashil == taluka.Tal_Code).Count();
                    model2.CountByDistrict = data;
                    model2.DistrictCode = taluka.Dist_Code;
                    model2.DistrictName = item.DistName;
                    model2.TalukaName = taluka.TalName;
                    model2.TalukaCode = taluka.Tal_Code;
                    listModelTal.Add(model2);

                    //Component-wise
                    foreach (var _item in list.Where(x => x.Tahashil == taluka.Tal_Code && x.CompNumber != null))
                    {
                        var model3 = new ApplicationListsViewModel();
                        model3.CountByTaluka = list.Where(x => x.Tahashil == taluka.Tal_Code).Count();
                        model3.CountByDistrict = data;
                        model3.DistrictCode = taluka.Dist_Code;
                        model3.DistrictName = item.DistName;
                        model3.TalukaName = taluka.TalName;
                        model3.TalukaCode = taluka.Tal_Code;
                        var d = _item.CompNumber;
                        var _comps = d.Split(',').ToList();
                        var _Components = new List<Tuple<int, string>>();

                        if (_comps.Any(x => x == "1"))
                        {
                            model3.CountByComponent1 += 1;
                        }
                        else if (_comps.Any(x => x == "2"))
                        {
                            model3.CountByComponent2 += 1;
                        }
                        else if (_comps.Any(x => x == "3"))
                        {
                            model3.CountByComponent3 += 1;
                        }
                        else if (_comps.Any(x => x == "4"))
                        {
                            model3.CountByComponent4 += 1;
                        }
                        else if (_comps.Any(x => x == "5"))
                        {
                            model3.CountByComponent5 += 1;
                        }
                        else if (_comps.Any(x => x == "6"))
                        {
                            model3.CountByComponent6 += 1;
                        }
                        else if (_comps.Any(x => x == "7"))
                        {
                            model3.CountByComponent7 += 1;
                        }
                        else if (_comps.Any(x => x == "8"))
                        {
                            model3.CountByComponent8 += 1;
                        }
                        else if (_comps.Any(x => x == "9"))
                        {
                            model3.CountByComponent9 += 1;
                        }
                        else if (_comps.Any(x => x == "10"))
                        {
                            model3.CountByComponent10 += 1;
                        }
                        else if (_comps.Any(x => x == "11"))
                        {
                            model3.CountByComponent11 += 1;
                        }
                        else if (_comps.Any(x => x == "12"))
                        {
                            model3.CountByComponent12 += 1;
                        }
                        else if (_comps.Any(x => x == "13"))
                        {
                            model3.CountByComponent13 += 1;
                        }
                        else if (_comps.Any(x => x == "14"))
                        {
                            model3.CountByComponent14 += 1;
                        }
                        else if (_comps.Any(x => x == "15"))
                        {
                            model3.CountByComponent15 += 1;
                        }
                        listModelComp.Add(model3);
                    }

                    
                }
            }
            model.ApplicantsListByComp = listModelComp;
            model.ApplicantsListByDist = listModelDist;
            model.ApplicantsListByTal = listModelTal;

            return View(model);
        }

        public JsonResult ApplicationByDistrict()
        {
            var list = db.ApplicantRegistrations.ToList();
            var districts = db.DistMaster.ToList();
            var talukaList = db.TalMaster.ToList();
            var model = new ApplicationListsViewModel();
            var listModel = new List<ApplicationListsViewModel>();
            foreach (var item in districts)
            {
                var d = new ApplicationListsViewModel();
                var data = list.Where(x => x.Dist == item.Dist_Code).Count();
                d.CountByDistrict = data;
                d.DistrictCode = item.Dist_Code;
                d.DistrictName = item.DistName;
                listModel.Add(d);

            }
            model.ApplicantsListByDist = listModel;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ApplicationIndexByTaluka()
        {
            var list = db.ApplicantRegistrations.ToList();
            var districts = db.DistMaster.ToList();
            var talukaList = db.TalMaster.ToList();
            var listModel = new List<ApplicationListsViewModel>();
            var model = new ApplicationListsViewModel();
            foreach (var item in districts)
            {
                foreach (var taluka in talukaList.Where(x => x.Dist_Code == item.Dist_Code))
                {
                    var model2 = new ApplicationListsViewModel();
                    model2.CountByTaluka = list.Where(x => x.Tahashil == taluka.Tal_Code).Count();
                    model2.DistrictCode = taluka.Dist_Code;
                    model2.DistrictName = item.DistName;
                    model2.TalukaName = taluka.TalName;
                    model2.TalukaCode = taluka.Tal_Code;
                    listModel.Add(model2);
                }
            }
            model.ApplicantsListByTal = listModel;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ApplicationIndexByComponents()
        {
            var list = db.ApplicantRegistrations.ToList();
            var districts = db.DistMaster.ToList();
            var talukaList = db.TalMaster.ToList();
           
            var model = new ApplicationListsViewModel();
            var listModel = new List<ApplicationListsViewModel>();
            foreach (var item in districts)
            {
                var data = list.Where(x => x.Dist == item.Dist_Code).Count();
              
                foreach (var taluka in talukaList.Where(x => x.Dist_Code == item.Dist_Code))
                {
                    var model2 = new ApplicationListsViewModel();
                    model2.CountByTaluka = list.Where(x => x.Tahashil == taluka.Tal_Code).Count();
                    model2.CountByDistrict = data;
                    model2.DistrictCode = taluka.Dist_Code;
                    model2.DistrictName = item.DistName;
                    model2.TalukaName = taluka.TalName;
                    model2.TalukaCode = taluka.Tal_Code;

                    foreach (var _item in list.Where(x=>x.Tahashil == taluka.Tal_Code && x.CompNumber != null))
                    {
                        var d = _item.CompNumber;
                        var _comps = d.Split(',').ToList();
                        var _Components = new List<Tuple<int, string>>();

                        if(_comps.Any(x=>x == "1"))
                        {
                            model2.CountByComponent1 += 1;
                        }
                        else if (_comps.Any(x => x == "2"))
                        {
                            model2.CountByComponent2 += 1;
                        }
                        else if (_comps.Any(x => x == "3"))
                        {
                            model2.CountByComponent3 += 1;
                        }
                        else if (_comps.Any(x => x == "4"))
                        {
                            model2.CountByComponent4 += 1;
                        }
                        else if (_comps.Any(x => x == "5"))
                        {
                            model2.CountByComponent5 += 1;
                        }
                        else if (_comps.Any(x => x == "6"))
                        {
                            model2.CountByComponent6 += 1;
                        }
                        else if (_comps.Any(x => x == "7"))
                        {
                            model2.CountByComponent7 += 1;
                        }
                        else if (_comps.Any(x => x == "8"))
                        {
                            model2.CountByComponent8 += 1;
                        }
                        else if (_comps.Any(x => x == "9"))
                        {
                            model2.CountByComponent9 += 1;
                        }
                        else if (_comps.Any(x => x == "10"))
                        {
                            model2.CountByComponent10 += 1;
                        }
                        else if (_comps.Any(x => x == "11"))
                        {
                            model2.CountByComponent11 += 1;
                        }
                        else if (_comps.Any(x => x == "12"))
                        {
                            model2.CountByComponent12 += 1;
                        }
                        else if (_comps.Any(x => x == "13"))
                        {
                            model2.CountByComponent13 += 1;
                        }
                        else if (_comps.Any(x => x == "14"))
                        {
                            model2.CountByComponent14 += 1;
                        }
                        else if (_comps.Any(x => x == "15"))
                        {
                            model2.CountByComponent15 += 1;
                        }
                    }

                    listModel.Add(model2);
                }
            }
            model.ApplicantsListByComp = listModel;
            return Json(model, JsonRequestBehavior.AllowGet);
        }



        public ActionResult UserIndex(int id)
        {
            var model = db.ApplicantRegistrations.Where(x => x.Id == id).FirstOrDefault();
            return View(model);
        }

        // GET: ApplicantRegistrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantRegistration applicantRegistration = db.ApplicantRegistrations.Find(id);
            var subCaste = Convert.ToInt32(applicantRegistration.SubCatse);
            var _caste = db.CasteUnderNTC.Where(x => x.ID == subCaste).Select(x => x.Caste).FirstOrDefault();
            var _dist = db.DistMaster.Where(x => x.Dist_Code == applicantRegistration.Dist).Select(x => x.DistName).FirstOrDefault();
            var _tal = db.TalMaster.Where(x => x.Tal_Code == applicantRegistration.Tahashil).Select(x => x.TalName).FirstOrDefault();
            var _vil = db.VillageMaster.Where(x => x.Village_Code == applicantRegistration.VillageName).Select(x => x.VillageName).FirstOrDefault();
            var _hvil = db.VillageMaster.Where(x => x.Village_Code == applicantRegistration.HVillage).Select(x => x.VillageName).FirstOrDefault();

            applicantRegistration.SubCasteName = _caste;
            applicantRegistration.DistrictName = _dist;
            applicantRegistration.TalukaName = _tal;
            applicantRegistration.VilName = _vil;
            applicantRegistration.HvilName = _hvil;
            applicantRegistration.YesAvailableOnLeaseEcre = Math.Round(Convert.ToDecimal(applicantRegistration.YesAvailableOnLeaseEcre), 2);
            applicantRegistration.YesApplicantOwnedLandEcre = Math.Round(Convert.ToDecimal(applicantRegistration.YesApplicantOwnedLandEcre), 2);
            applicantRegistration.GardeningEcre = Math.Round(Convert.ToDecimal(applicantRegistration.GardeningEcre), 2);
            applicantRegistration.CuminEcre = Math.Round(Convert.ToDecimal(applicantRegistration.CuminEcre), 2);
            applicantRegistration.CompNumberList1 = applicantRegistration.CompNumber.Split(',').ToList();
            applicantRegistration.CompList = new List<Tuple<int, string>>();
            
            foreach (var item in applicantRegistration.CompNumberList1)
            {
                Components d = (Components)Enum.Parse(typeof(Components), item);
                applicantRegistration.CompList.Add(new Tuple<int, string>((Convert.ToInt32(item)), d.GetDescription()));
            }
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
        public ActionResult Create(long aadhar, string formStatus)
        {

            //getDistrict();
            //getCaste();
            //getCripplePercent();
            //getNoOfSheep();
            //getAcre();
            //getWaterSource();
            //getTypeCastle();
            //getDuration();
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

            var model = new ApplicantRegistration();
            ViewBag.Dist = new SelectList(ddlDist, "Dist_Code", "DistName", model.Dist);
            ViewBag.Caste = new SelectList(ddlCaste, "ID", "Caste", model.SubCatse);
            ViewBag.Taluka = new SelectList(ddlTal, "Tal_Code", "Talname", model.Tahashil);
            ViewBag.Village = new SelectList(ddlVil, "Village_Code", "VillageName", model.VillageName);
            ViewBag.HVilage = new SelectList(ddlVil, "Village_Code", "VillageName", model.HVillage);
            ViewBag.WaterSource = new SelectList(ddWater, "ID", "SourceName", model.WaterSource);
            ViewBag.Sheep = new SelectList(ddlSheep, "id", "NoOfSheep", model.NumberOfSheepIs);
            ViewBag.TypeCastle = new SelectList(typecastle, "ID", "TypeName", model.TypeExistingCastle);
            ViewBag.Duration = new SelectList(duration, "ID", "DurationName", model.DurationOfWater);
            ViewBag.Percentage = new SelectList(cripple, "ID", "Percentage", model.CrippledPercentage);
            ViewBag.AcreOwned = new SelectList(ddAcre, "id", "Acre", model.YesApplicantOwnedLandEcre);
            ViewBag.AcreLeased = new SelectList(ddAcre, "id", "Acre", model.YesAvailableOnLeaseEcre);
            ViewBag.GardenAcre = new SelectList(ddAcre, "id", "Acre", model.GardeningEcre);
            ViewBag.CuminAcre = new SelectList(ddAcre, "id", "Acre", model.CuminEcre);
            ViewBag.GunthaOwned = new SelectList(ddAcre, "id", "Acre", model.YesApplicantOwnedLandGuntha);
            ViewBag.GunthaLeased = new SelectList(ddAcre, "id", "Acre", model.YesAvailableOnLeaseGuntha);
            if (formStatus == "Draft")
            {
                model = db.ApplicantRegistrations.Where(x => x.AdharCardNo == aadhar).FirstOrDefault();
 
                return View(model);
            }
            else
            {
                model.AdharCardNo = aadhar;
                model.Caste = "भटक्या जमातीचा - क";
                model.tabId = 0;
                return View(model);

            }
        }

        // POST: ApplicantRegistrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicantRegistration applicantRegistration, HttpPostedFileBase file)
        {
            var applicant = db.ApplicantRegistrations.AsNoTracking().Where(x => x.AdharCardNo == applicantRegistration.AdharCardNo).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(file.FileName);
                    var exten = Path.GetExtension(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/ApplicantPhoto"), applicant.AdharCardNo + "_" + applicantRegistration.ApName + "." + exten);
                    file.SaveAs(path);
                    var relativePath = "/Images/ApplicantPhoto/" + applicant.AdharCardNo + "_" + applicant.ApName + "." + exten;
                    applicant.Photo = relativePath;
                }
                string _comp = "";
                if (applicantRegistration.CompNumberList1 != null && applicantRegistration.CompNumberList1.ToString() != ", ")
                {
                    applicant.CompNumber = String.Join(", ", applicantRegistration.CompNumberList1.ToArray());
                    _comp = applicant.CompNumber;
                }
                if (applicantRegistration.CompNumberList2 != null && applicantRegistration.CompNumberList2.ToString() != ", ")
                {
                    applicant.CompNumber = String.Join(", ", applicantRegistration.CompNumberList2.ToArray());
                    _comp =  applicant.CompNumber + ", " + _comp ;
                }
                if (applicantRegistration.CompNumberList3 != null && applicantRegistration.CompNumberList3.ToString() != ", ")
                {
                    applicant.CompNumber = String.Join(", ", applicantRegistration.CompNumberList3.ToArray());
                    _comp = applicant.CompNumber + ", " + _comp;
                }
                if (applicantRegistration.CompNumberList4 != null && applicantRegistration.CompNumberList4.ToString() != ", ")
                {
                    applicant.CompNumber = String.Join(", ", applicantRegistration.CompNumberList4.ToArray());
                    _comp = applicant.CompNumber + ", " + _comp;
                }
                if (applicantRegistration.CompNumberList5 != null && applicantRegistration.CompNumberList5.ToString() != ", ")
                {
                    applicant.CompNumber = String.Join(", ", applicantRegistration.CompNumberList5.ToArray());
                    _comp = applicant.CompNumber + ", " + _comp;
                }

                _comp = _comp.TrimEnd();
                applicant.CompNumber = _comp.TrimEnd(',');
                applicant.SubmitDatetime = DateTime.UtcNow;
              //  var id = db.ApplicantRegistrations.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                applicant.ApplicationNumber = applicant.Dist + "/" + applicant.Tahashil + "/" + applicant.VillageName + "/" + applicant.Id;
                applicant.FormSubmitted = true;
                applicant.UserIP = GetIPAddress();
                applicantRegistration.Id = applicant.Id;

                db.Entry(applicant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MahameshYojanaUserLogin", "Menu", new { msg = "formSubmit" });
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePartial(ApplicantRegistration applicantRegistration, HttpPostedFileBase file)
        {
            var applicant = db.ApplicantRegistrations.AsNoTracking().Any(x => x.AdharCardNo == applicantRegistration.AdharCardNo);
            if (applicant == false)
            {
                if (file != null && file.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(file.FileName);
                    var exten = Path.GetExtension(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/ApplicantPhoto"), applicantRegistration.AdharCardNo + "_" + applicantRegistration.ApName + "." + exten);
                    file.SaveAs(path);
                    var relativePath = "/Images/ApplicantPhoto/" + applicantRegistration.AdharCardNo + "_" + applicantRegistration.ApName + "." + exten;
                    applicantRegistration.Photo = relativePath;
                }

                //string _comp = "";
                //if (applicantRegistration.CompNumberList1 != null)
                //{
                //    applicantRegistration.CompNumber = String.Join(", ", applicantRegistration.CompNumberList1.ToArray());
                //    _comp = applicantRegistration.CompNumber;
                //}
                //if (applicantRegistration.CompNumberList2 != null)
                //{
                //    applicantRegistration.CompNumber = String.Join(", ", applicantRegistration.CompNumberList2.ToArray());
                //    _comp = _comp + ", " + applicantRegistration.CompNumber;
                //}
                //if (applicantRegistration.CompNumberList3 != null)
                //{
                //    applicantRegistration.CompNumber = String.Join(", ", applicantRegistration.CompNumberList3.ToArray());
                //    _comp = _comp + ", " + applicantRegistration.CompNumber;
                //}
                //if (applicantRegistration.CompNumberList4 != null)
                //{
                //    applicantRegistration.CompNumber = String.Join(", ", applicantRegistration.CompNumberList4.ToArray());
                //    _comp = _comp + ", " + applicantRegistration.CompNumber;
                //}
                //if (applicantRegistration.CompNumberList5 != null)
                //{
                //    applicantRegistration.CompNumber = String.Join(", ", applicantRegistration.CompNumberList5.ToArray());
                //    _comp = _comp + ", " + applicantRegistration.CompNumber;
                //}
                //applicantRegistration.CompNumber = _comp;

                applicantRegistration.SubmitDatetime = DateTime.UtcNow;
                //var id = db.ApplicantRegistrations.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                //applicantRegistration.ApplicationNumber = applicantRegistration.Dist + "/" + applicantRegistration.Tahashil + "/" + applicantRegistration.VillageName + "/" + (id + 1);
                applicantRegistration.FormSubmitted = false;
                applicantRegistration.UserIP = GetIPAddress();
                applicantRegistration.tabId = 1;
                db.ApplicantRegistrations.Add(applicantRegistration);
                db.SaveChanges();
            }
            else
            {
                var applicants = db.ApplicantRegistrations.AsNoTracking().Where(x => x.AdharCardNo == applicantRegistration.AdharCardNo).FirstOrDefault();
                applicantRegistration.Id = applicants.Id;
                if (file != null && file.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(file.FileName);
                    var exten = Path.GetExtension(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/ApplicantPhoto"), applicantRegistration.AdharCardNo + "_" + applicantRegistration.ApName + "." + exten);
                    file.SaveAs(path);
                    var relativePath = "/Images/ApplicantPhoto/" + applicantRegistration.AdharCardNo + "_" + applicantRegistration.ApName + "." + exten;
                    applicantRegistration.Photo = relativePath;
                }

                //string _comp = "";
                //if (applicantRegistration.CompNumberList1 != null)
                //{
                //    applicants.CompNumber = String.Join(", ", applicantRegistration.CompNumberList1.ToArray());
                //    _comp = applicants.CompNumber;
                //}
                //if (applicantRegistration.CompNumberList2 != null)
                //{
                //    applicants.CompNumber = String.Join(", ", applicantRegistration.CompNumberList2.ToArray());
                //    _comp = _comp + ", " + applicants.CompNumber;
                //}
                //if (applicantRegistration.CompNumberList3 != null)
                //{
                //    applicants.CompNumber = String.Join(", ", applicantRegistration.CompNumberList3.ToArray());
                //    _comp = _comp + ", " + applicants.CompNumber;
                //}
                //if (applicantRegistration.CompNumberList4 != null)
                //{
                //    applicants.CompNumber = String.Join(", ", applicantRegistration.CompNumberList4.ToArray());
                //    _comp = _comp + ", " + applicants.CompNumber;
                //}
                //if (applicantRegistration.CompNumberList5 != null)
                //{
                //    applicants.CompNumber = String.Join(", ", applicantRegistration.CompNumberList5.ToArray());
                //    _comp = _comp + ", " + applicants.CompNumber;
                //}
                //applicants.CompNumber = _comp;

                applicantRegistration.SubmitDatetime = DateTime.UtcNow;
                //var id = db.ApplicantRegistrations.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                //applicantRegistration.ApplicationNumber = applicantRegistration.Dist + "/" + applicantRegistration.Tahashil + "/" + applicantRegistration.VillageName + "/" + (id + 1);
                applicantRegistration.FormSubmitted = false;
                applicantRegistration.UserIP = GetIPAddress();
                applicantRegistration.tabId = 1;
                db.Entry(applicantRegistration).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Create", new { aadhar = applicantRegistration.AdharCardNo, formStatus = "Draft" });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePartial2(ApplicantRegistration applicantRegistration, HttpPostedFileBase file)
        {
            var applicant = db.ApplicantRegistrations.AsNoTracking().Where(x => x.AdharCardNo == applicantRegistration.AdharCardNo).FirstOrDefault();
            applicantRegistration.Id = applicant.Id;
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                var exten = Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/ApplicantPhoto"), applicant.AdharCardNo + "_" + applicant.ApName + "." + exten);
                file.SaveAs(path);
                var relativePath = "/Images/ApplicantPhoto/" + applicant.AdharCardNo + "_" + applicant.ApName + "." + exten;
                applicant.Photo = relativePath;
            }

            //string _comp = "";
            //if (applicantRegistration.CompNumberList1 != null)
            //{
            //    applicant.CompNumber = String.Join(", ", applicantRegistration.CompNumberList1.ToArray());
            //    _comp = applicant.CompNumber;
            //}
            //if (applicantRegistration.CompNumberList2 != null)
            //{
            //    applicant.CompNumber = String.Join(", ", applicantRegistration.CompNumberList2.ToArray());
            //    _comp = _comp + ", " + applicant.CompNumber;
            //}
            //if (applicantRegistration.CompNumberList3 != null)
            //{
            //    applicant.CompNumber = String.Join(", ", applicantRegistration.CompNumberList3.ToArray());
            //    _comp = _comp + ", " + applicant.CompNumber;
            //}
            //if (applicantRegistration.CompNumberList4 != null)
            //{
            //    applicant.CompNumber = String.Join(", ", applicantRegistration.CompNumberList4.ToArray());
            //    _comp = _comp + ", " + applicant.CompNumber;
            //}
            //if (applicantRegistration.CompNumberList5 != null)
            //{
            //    applicant.CompNumber = String.Join(", ", applicantRegistration.CompNumberList5.ToArray());
            //    _comp = _comp + ", " + applicant.CompNumber;
            //}
            //applicant.CompNumber = _comp;
            applicant.GardeningEcre = applicantRegistration.GardeningEcre;
            applicant.CuminEcre = applicantRegistration.CuminEcre;
            applicant.WaterSource = applicantRegistration.WaterSource;
            applicant.TypeExistingCastle = applicantRegistration.TypeExistingCastle;
            applicant.DurationOfWater = applicantRegistration.DurationOfWater;
            applicant.LastYearFooder = applicantRegistration.LastYearFooder;
            applicant.LastYearTotalProductionInKG = applicantRegistration.LastYearTotalProductionInKG;
            applicant.IsWarehouseForSheep = applicantRegistration.IsWarehouseForSheep;
            applicant.IsNotIsAtLeastOnePinpointSpace = applicantRegistration.IsNotIsAtLeastOnePinpointSpace;
            applicant.YesIntekOfSheepInWarehouse = applicantRegistration.YesIntekOfSheepInWarehouse;
            applicant.IsSavingsGroupMember = applicantRegistration.IsSavingsGroupMember;
            applicant.SavingGroupName = applicantRegistration.SavingGroupName;
            applicant.SavingGroupRegNumber = applicantRegistration.SavingGroupRegNumber;
            applicant.IsanimalHusbandryManufacturingCompanyMember = applicantRegistration.IsanimalHusbandryManufacturingCompanyMember;
            applicant.IsanimalHusbandryManufacturingCompanyName = applicantRegistration.IsanimalHusbandryManufacturingCompanyName;
            applicant.IsanimalHusbandryManufacturingCompanyRegNumber = applicantRegistration.IsanimalHusbandryManufacturingCompanyRegNumber;
            applicant.SubmitDatetime = DateTime.UtcNow;
            applicant.IsTrained = applicantRegistration.IsTrained;
            //var id = db.ApplicantRegistrations.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
            //applicantRegistration.ApplicationNumber = applicantRegistration.Dist + "/" + applicantRegistration.Tahashil + "/" + applicantRegistration.VillageName + "/" + (id + 1);
            applicant.FormSubmitted = false;
            applicant.UserIP = GetIPAddress();
            applicant.tabId = 2;
            db.Entry(applicant).State = EntityState.Modified;
            db.SaveChanges();

           
            return RedirectToAction("Create", new { aadhar = applicantRegistration.AdharCardNo, formStatus = "Draft" });

        }



        // GET: ApplicantRegistrations/Edit/5
        public ActionResult Edit(int? id, string exit)
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
            ViewBag.Dist = new SelectList(ddlDist, "Dist_Code", "DistName", applicantRegistration.Dist);
            ViewBag.Caste = new SelectList(ddlCaste, "ID", "Caste", applicantRegistration.SubCatse);
            ViewBag.Taluka = new SelectList(ddlTal, "Tal_Code", "Talname", applicantRegistration.Tahashil);
            ViewBag.Village = new SelectList(ddlVil, "Village_Code", "VillageName", applicantRegistration.VillageName);
            ViewBag.HVilage = new SelectList(ddlVil, "Village_Code", "VillageName", applicantRegistration.HVillage);
            ViewBag.WaterSource = new SelectList(ddWater, "ID", "SourceName", applicantRegistration.WaterSource);
            ViewBag.Sheep = new SelectList(ddlSheep, "id", "NoOfSheep", applicantRegistration.NumberOfSheepIs);
            ViewBag.TypeCastle = new SelectList(typecastle, "ID", "TypeName", applicantRegistration.TypeExistingCastle);
            ViewBag.Duration = new SelectList(duration, "ID", "DurationName", applicantRegistration.DurationOfWater);
            ViewBag.Percentage = new SelectList(cripple, "ID", "Percentage", applicantRegistration.CrippledPercentage);
            ViewBag.AcreOwned = new SelectList(ddAcre, "id", "Acre", applicantRegistration.YesApplicantOwnedLandEcre);
            ViewBag.AcreLeased = new SelectList(ddAcre, "id", "Acre", applicantRegistration.YesAvailableOnLeaseEcre);
            ViewBag.GardenAcre = new SelectList(ddAcre, "id", "Acre", applicantRegistration.GardeningEcre);
            ViewBag.CuminAcre = new SelectList(ddAcre, "id", "Acre", applicantRegistration.CuminEcre);
            ViewBag.GunthaOwned = new SelectList(ddAcre, "id", "Acre", applicantRegistration.YesApplicantOwnedLandGuntha);
            ViewBag.GunthaLeased = new SelectList(ddAcre, "id", "Acre", applicantRegistration.YesAvailableOnLeaseGuntha);
            if (applicantRegistration == null)
            {
                return HttpNotFound();
            }
            if (exit == "home")
            {
                return RedirectToAction("MahameshYojana", "Menu");
            }
            else
            {
                return View(applicantRegistration); ;
            }
            //return View(applicantRegistration);
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
                    var path = Path.Combine(Server.MapPath("~/Images/ApplicantPhoto"), applicantRegistration.AdharCardNo + "_" + applicantRegistration.ApName);
                    file.SaveAs(path);
                    var relativePath = "/Images/ApplicantPhoto/" + applicantRegistration.AdharCardNo + "_" + applicantRegistration.ApName;
                    applicantRegistration.Photo = relativePath;
                }
                string _comp = "";
                if (applicantRegistration.CompNumberList1 != null && applicantRegistration.CompNumberList1.ToString() != ", ")
                {
                    applicantRegistration.CompNumber = String.Join(", ", applicantRegistration.CompNumberList1.ToArray());
                    _comp = applicantRegistration.CompNumber;
                }
                if (applicantRegistration.CompNumberList2 != null && applicantRegistration.CompNumberList2.ToString() != ", ")
                {
                    applicantRegistration.CompNumber = String.Join(", ", applicantRegistration.CompNumberList2.ToArray());
                    _comp = applicantRegistration.CompNumber + ", " + _comp;
                }
                if (applicantRegistration.CompNumberList3 != null && applicantRegistration.CompNumberList3.ToString() != ", ")
                {
                    applicantRegistration.CompNumber = String.Join(", ", applicantRegistration.CompNumberList3.ToArray());
                    _comp = applicantRegistration.CompNumber + ", " + _comp;
                }
                if (applicantRegistration.CompNumberList4 != null && applicantRegistration.CompNumberList4.ToString() != ", ")
                {
                    applicantRegistration.CompNumber = String.Join(", ", applicantRegistration.CompNumberList4.ToArray());
                    _comp = applicantRegistration.CompNumber + ", " + _comp;
                }
                if (applicantRegistration.CompNumberList5 != null && applicantRegistration.CompNumberList5.ToString() != ", ")
                {
                    applicantRegistration.CompNumber = String.Join(", ", applicantRegistration.CompNumberList5.ToArray());
                    _comp = applicantRegistration.CompNumber + ", " + _comp;
                }
                applicantRegistration.CompNumber = _comp;

                applicantRegistration.SubmitDatetime = DateTime.UtcNow;
                applicantRegistration.FormSubmitted = true;
                applicantRegistration.UserIP = GetIPAddress();
                db.Entry(applicantRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MahameshYojanaUserLogin", "Menu", new { msg = "formSubmit" });
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
            ApplicantRegistration applicantRegistration = db.ApplicantRegistrations.Find(id);
            var subCaste = Convert.ToInt32(applicantRegistration.SubCatse);
            var _caste = db.CasteUnderNTC.Where(x=>x.ID == subCaste).Select(x=>x.Caste).FirstOrDefault();
            var _dist = db.DistMaster.Where(x => x.Dist_Code == applicantRegistration.Dist).Select(x => x.DistName).FirstOrDefault();
            var _tal = db.TalMaster.Where(x => x.Tal_Code == applicantRegistration.Tahashil).Select(x => x.TalName).FirstOrDefault();
            var _vil = db.VillageMaster.Where(x => x.Village_Code == applicantRegistration.VillageName).Select(x => x.VillageName).FirstOrDefault();
            var _hvil = db.VillageMaster.Where(x => x.Village_Code == applicantRegistration.HVillage).Select(x => x.VillageName).FirstOrDefault();

            applicantRegistration.SubCasteName = _caste;
            applicantRegistration.DistrictName = _dist;
            applicantRegistration.TalukaName = _tal;
            applicantRegistration.VilName = _vil;
            applicantRegistration.HvilName = _hvil;
            applicantRegistration.YesAvailableOnLeaseEcre = Math.Round(Convert.ToDecimal(applicantRegistration.YesAvailableOnLeaseEcre), 2);
            applicantRegistration.YesApplicantOwnedLandEcre = Math.Round(Convert.ToDecimal(applicantRegistration.YesApplicantOwnedLandEcre), 2);
            applicantRegistration.GardeningEcre = Math.Round(Convert.ToDecimal(applicantRegistration.GardeningEcre), 2);
            applicantRegistration.CuminEcre = Math.Round(Convert.ToDecimal(applicantRegistration.CuminEcre), 2);
            applicantRegistration.CompNumberList1 = applicantRegistration.CompNumber.Split(',').ToList();

            return View(applicantRegistration);
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
