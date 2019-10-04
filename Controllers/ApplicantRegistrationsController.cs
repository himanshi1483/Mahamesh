using Mahamesh.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static Mahamesh.Models.DatatableModel;

namespace Mahamesh.Controllers
{
    public class ApplicantRegistrationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public const int RecordsPerPage = 100;
   
        public ActionResult Index()
        {
            return View();
        }



        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue // Use this value to set your maximum size for all of your Requests
            };
        }
        public JsonResult GetApplicantList(string searchVil, string searchName)
        {
           
            int recordsTotal = 0;
            var applicantRegistrationList = new List<ApplicantRegistration>();
            var _dist = db.DistMaster.ToList();
            var _tal = db.TalMaster.ToList();
            var _vil = db.VillageMaster.ToList();
           
            if (searchVil == "Name")
            {
                applicantRegistrationList = db.ApplicantRegistrations.OrderByDescending(x => x.FormSubmitted).Where(x => x.ApName != null && x.ApName != string.Empty && x.ApName.ToLower().Contains(searchName)).ToList();
                foreach (var item in applicantRegistrationList)
                {
                    item.DistrictName = _dist.Where(x => x.Dist_Code == item.Dist).Select(x => x.DistName).FirstOrDefault();
                    item.TalukaName = _tal.Where(x => x.Tal_Code == item.Tahashil).Select(x => x.TalName).FirstOrDefault();
                    item.VilName = _vil.Where(x => x.Village_Code == item.VillageName).Select(x => x.VillageName).FirstOrDefault();
                }
            }
            else if (searchVil == "VillageName")
            {
                applicantRegistrationList = db.ApplicantRegistrations.OrderByDescending(x => x.FormSubmitted).Where(x => x.VillageName != null && x.VillageName.Value == Convert.ToInt64(searchName)).ToList();
                foreach (var item in applicantRegistrationList)
                {
                    item.DistrictName = _dist.Where(x => x.Dist_Code == item.Dist).Select(x => x.DistName).FirstOrDefault();
                    item.TalukaName = _tal.Where(x => x.Tal_Code == item.Tahashil).Select(x => x.TalName).FirstOrDefault();
                    item.VilName = _vil.Where(x => x.Village_Code == item.VillageName).Select(x => x.VillageName).FirstOrDefault();
                }
            }
            else if (searchVil == "VilName")
            {
                applicantRegistrationList = db.ApplicantRegistrations.OrderByDescending(x => x.FormSubmitted).Where(x => x.VilName != null && x.VilName.ToLower().Contains(searchName)).ToList();
                foreach (var item in applicantRegistrationList)
                {
                    item.DistrictName = _dist.Where(x => x.Dist_Code == item.Dist).Select(x => x.DistName).FirstOrDefault();
                    item.TalukaName = _tal.Where(x => x.Tal_Code == item.Tahashil).Select(x => x.TalName).FirstOrDefault();
                    item.VilName = _vil.Where(x => x.Village_Code == item.VillageName).Select(x => x.VillageName).FirstOrDefault();
                }
            }
            else if (searchVil == "DistrictName")
            {
                applicantRegistrationList = db.ApplicantRegistrations.OrderByDescending(x => x.FormSubmitted).Where(x => x.DistrictName != null && x.DistrictName != string.Empty && x.DistrictName.ToLower().Contains(searchName)).ToList();
                foreach (var item in applicantRegistrationList)
                {
                    item.DistrictName = _dist.Where(x => x.Dist_Code == item.Dist).Select(x => x.DistName).FirstOrDefault();
                    item.TalukaName = _tal.Where(x => x.Tal_Code == item.Tahashil).Select(x => x.TalName).FirstOrDefault();
                    item.VilName = _vil.Where(x => x.Village_Code == item.VillageName).Select(x => x.VillageName).FirstOrDefault();
                }
            }
            else if (searchVil == "TalukaName")
            {
                applicantRegistrationList = db.ApplicantRegistrations.OrderByDescending(x => x.FormSubmitted).Where(x => x.TalukaName != null && x.TalukaName != string.Empty && 
                x.TalukaName.ToLower().Contains(searchName)).ToList();
                foreach (var item in applicantRegistrationList)
                {
                    item.DistrictName = _dist.Where(x => x.Dist_Code == item.Dist).Select(x => x.DistName).FirstOrDefault();
                    item.TalukaName = _tal.Where(x => x.Tal_Code == item.Tahashil).Select(x => x.TalName).FirstOrDefault();
                    item.VilName = _vil.Where(x => x.Village_Code == item.VillageName).Select(x => x.VillageName).FirstOrDefault();
                }
            }
            else if (searchVil == "DistCode")
            {
                int distCode = Convert.ToInt32(searchName);
                applicantRegistrationList = db.ApplicantRegistrations.OrderByDescending(x => x.FormSubmitted).Where(x => x.Dist != null && x.Dist.Value == distCode).ToList();
                foreach (var item in applicantRegistrationList)
                {
                    item.DistrictName = _dist.Where(x => x.Dist_Code == item.Dist).Select(x => x.DistName).FirstOrDefault();
                    item.TalukaName = _tal.Where(x => x.Tal_Code == item.Tahashil).Select(x => x.TalName).FirstOrDefault();
                    item.VilName = _vil.Where(x => x.Village_Code == item.VillageName).Select(x => x.VillageName).FirstOrDefault();
                }
            }
            else if (searchVil == "Tahashil")
            {
                int talCOde = Convert.ToInt32(searchName);
                applicantRegistrationList = db.ApplicantRegistrations.OrderByDescending(x => x.FormSubmitted).Where(x => x.Tahashil != null && x.Tahashil.Value == talCOde).ToList();
                foreach (var item in applicantRegistrationList)
                {
                    item.DistrictName = _dist.Where(x => x.Dist_Code == item.Dist).Select(x => x.DistName).FirstOrDefault();
                    item.TalukaName = _tal.Where(x => x.Tal_Code == item.Tahashil).Select(x => x.TalName).FirstOrDefault();
                    item.VilName = _vil.Where(x => x.Village_Code == item.VillageName).Select(x => x.VillageName).FirstOrDefault();
                }
            }
            else if (searchVil == "Aadhar")
            {
                long aadhar = Convert.ToInt64(searchName);
                applicantRegistrationList = db.ApplicantRegistrations.OrderByDescending(x => x.FormSubmitted).Where(x => x.AdharCardNo != null && x.AdharCardNo == aadhar).ToList();
                foreach (var item in applicantRegistrationList)
                {
                    item.DistrictName = _dist.Where(x => x.Dist_Code == item.Dist).Select(x => x.DistName).FirstOrDefault();
                    item.TalukaName = _tal.Where(x => x.Tal_Code == item.Tahashil).Select(x => x.TalName).FirstOrDefault();
                    item.VilName = _vil.Where(x => x.Village_Code == item.VillageName).Select(x => x.VillageName).FirstOrDefault();
                }
            }
            else if (searchVil == "Component")
            {
               // var _compList = _applicant.CompNumber != null ? _applicant.CompNumber.Split(',').ToList() : null;
                var d = db.ApplicantRegistrations.OrderByDescending(x => x.FormSubmitted).Where(x => x.CompNumber.Contains((searchName).ToString())).ToList();
                foreach (var item in d)
                {
                    var _compList =  item.CompNumber.Trim().Split(',').ToList();
                    item.CompNumberList1 = _compList;
                    item.DistrictName = _dist.Where(x => x.Dist_Code == item.Dist).Select(x => x.DistName).FirstOrDefault();
                    item.TalukaName = _tal.Where(x => x.Tal_Code == item.Tahashil).Select(x => x.TalName).FirstOrDefault();
                    item.VilName = _vil.Where(x => x.Village_Code == item.VillageName).Select(x => x.VillageName).FirstOrDefault();
                }
                applicantRegistrationList = d.Where(x => x.CompNumberList1.Any(y => y == searchName)).ToList();
            }
            recordsTotal = applicantRegistrationList.Count();
            //Paging     
            var data = applicantRegistrationList.ToList();

            foreach (var item in data)
            {
               var _compList = item.CompNumber != null ? item.CompNumber.Split(',').ToList() : null;
                if (item.Age > 60 || item.Age < 18)
                {
                    item.IsAgeProper = false;
                }
                else
                {
                    item.IsAgeProper = true;
                }
                if (item.Child2006 > 2)
                {
                    item.IsChildCountProper = false;
                }
                else
                {
                    item.IsChildCountProper = true;
                }
                if (item.CompNumber != null)
                {
                    if ((_compList.Contains("1") || _compList.Contains("2")) && (_compList.Contains("4") || _compList.Contains("3") || _compList.Contains("5")
                        || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("8") || _compList.Contains("9") || _compList.Contains("10")
                         || _compList.Contains("11") || _compList.Contains("12") || _compList.Contains("13") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("3")) && (_compList.Contains("4") || _compList.Contains("5")
                        || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("4")) && (_compList.Contains("3") || _compList.Contains("5")
                     || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("5")) && (_compList.Contains("4") || _compList.Contains("3")
                     || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("6")) && (_compList.Contains("4") || _compList.Contains("5")
                     || _compList.Contains("3") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("7")) && (_compList.Contains("4") || _compList.Contains("5")
                     || _compList.Contains("6") || _compList.Contains("3") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("8")) && (_compList.Contains("9") || _compList.Contains("10")
                    || _compList.Contains("11") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("9")) && (_compList.Contains("8") || _compList.Contains("10")
                  || _compList.Contains("11") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("10")) && (_compList.Contains("9") || _compList.Contains("8")
                  || _compList.Contains("11") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    if ((_compList.Contains("11")) && (_compList.Contains("9") || _compList.Contains("10")
                  || _compList.Contains("8") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("12")) && (_compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2") || _compList.Contains("13")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("13")) && (_compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2") || _compList.Contains("12")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                }
              
                if (db.ApplicantRegistrations.Where(x=>x.AdharCardNo == item.AdharCardNo &&  x.FormSubmitted == true).ToList().Count > 1)
                {
                    item.IsAadharUnique = false;
                }
                else
                {
                    item.IsAadharUnique = true;
                }
                if (item.Photo == null)
                {
                    item.IsPhotoAvailable = false;
                }
                else
                {
                    item.IsPhotoAvailable = true;
                }
                if(db.BeneficiarySelectedList2018.Any(x=>x.Aadhar == item.AdharCardNo))
                {
                    item.IsPreviouslySelected = true;
                }
                else
                {
                    item.IsPreviouslySelected = false;
                }

            }
            //Returning Json Data    
            return Json( data, JsonRequestBehavior.AllowGet);

        }

        public List<ApplicantRegistration> GetApplications(string search,string searchType, string sortOrder, int start, int length, out int TotalCount)
        {
            var applicantRegistrationList = db.ApplicantRegistrations.Where(x => x.FormSubmitted == true).ToList();

            if (searchType == "distCode")
            {
                applicantRegistrationList = applicantRegistrationList.Where(x => x.Dist == Convert.ToInt64(search)).ToList();
            }
            var _dist = db.DistMaster.ToList();
            var _tal = db.TalMaster.ToList();
            var _vil = db.VillageMaster.ToList();
            foreach (var item in applicantRegistrationList)
            {
                item.DistrictName = _dist.Where(x => x.Dist_Code == item.Dist).Select(x => x.DistName).FirstOrDefault();
                item.TalukaName = _tal.Where(x => x.Tal_Code == item.Tahashil).Select(x => x.TalName).FirstOrDefault();
                item.VilName = _vil.Where(x => x.Village_Code == item.VillageName).Select(x => x.VillageName).FirstOrDefault();
            }

            TotalCount = applicantRegistrationList.Count;

            applicantRegistrationList = applicantRegistrationList.Skip(start).Take(length).ToList();
            return applicantRegistrationList.ToList();
        }

        public List<ApplicantRegistration> GetRecordsForPage(int pageNum)
        {
            var applicantRegistrationList = db.ApplicantRegistrations.Where(x => x.FormSubmitted == true).ToList();
            var _dist = db.DistMaster.ToList();
            var _tal = db.TalMaster.ToList();
            var _vil = db.VillageMaster.ToList();
            foreach (var item in applicantRegistrationList)
            {
                item.DistrictName = _dist.Where(x => x.Dist_Code == item.Dist).Select(x => x.DistName).FirstOrDefault();
                item.TalukaName = _tal.Where(x => x.Tal_Code == item.Tahashil).Select(x => x.TalName).FirstOrDefault();
                item.VilName = _vil.Where(x => x.Village_Code == item.VillageName).Select(x => x.VillageName).FirstOrDefault();
            }

            int from = (pageNum * RecordsPerPage);

            var tempList = (from rec in applicantRegistrationList
                            select rec).Skip(from).Take(20).ToList<ApplicantRegistration>();

            return tempList;
        }

        //  [Authorize]
        public ActionResult ApplicationIndexByDistrict()
        {
            var model = new ApplicationListsViewModel();

            model.ApplicantsListByDist = new List<ApplicationListsViewModel>();
            model.ApplicantsListByTal = new List<ApplicationListsViewModel>();
            model.ApplicantsListByComp = new List<ApplicationListsViewModel>();

            var list = db.ApplicantRegistrations.ToList();
            var districts = db.DistMaster.ToList();
            var talukaList = db.TalMaster.ToList();
            var districtTarget = db.DistrictTarget.ToList();
            var talukaTarget = db.TalukaTarget.ToList();
            var listModelDist = new List<ApplicationListsViewModel>();
            var listModelTal = new List<ApplicationListsViewModel>();
            var listModelComp = new List<ApplicationListsViewModel>();
            //District-wise
            foreach (var item in districts)
            {
                var _data1 = new ApplicationListsViewModel();
                var data = list.Where(x => x.Dist == item.Dist_Code).Count();
                var target = districtTarget.Where(x => x.Name_of_District.Trim() == item.District_Mr.Trim()).FirstOrDefault();
                _data1.CountByDistrict = data;
                _data1.DistrictCode = item.Dist_Code;
                _data1.DistrictName = item.District_Mr;
                _data1.CountByComponent1 = target.Component_No_1;
                _data1.CountByComponent2 = target.Component_No_2;
                _data1.CountByComponent3_7 = target.Component_No_3_7;
                _data1.CountByComponent8 = target.Component_No_8;
                _data1.CountByComponent9 = target.Component_No_9;
                _data1.CountByComponent10 = target.Component_No_10;
                _data1.CountByComponent11 = target.Component_No_11;
                _data1.CountByComponent12 = target.Component_No_12;
                _data1.CountByComponent13 = target.Component_No_13;
                listModelDist.Add(_data1);

                //Taluka-wise
                foreach (var taluka in talukaList.Where(x => x.Dist_Code == item.Dist_Code))
                {
                    var model2 = new ApplicationListsViewModel();

                    var target2 = talukaTarget.Where(x => x.Name_Of_Taluka.Trim() == taluka.Tal_Mr.Trim() && x.Name_of_District.Trim() == item.District_Mr.Trim()).FirstOrDefault();
                    model2.CountByTaluka = list.Where(x => x.Tahashil == taluka.Tal_Code && x.Dist == item.Dist_Code).Count();
                    model2.CountByDistrict = data;
                    model2.DistrictCode = item.Dist_Code;
                    model2.DistrictName = item.District_Mr;
                    model2.TalukaName = taluka.Tal_Mr;
                    model2.TalukaCode = taluka.Tal_Code;
                    if (target2 != null)
                    {
                        model2.CountByComponent1 = target2.Component_No_1;
                        model2.CountByComponent2 = target2.Component_No_2;
                        model2.CountByComponent3_7 = target2.Component_No_3_7;
                        model2.CountByComponent8 = target2.Component_No_8;
                        model2.CountByComponent9 = target2.Component_No_9;
                        model2.CountByComponent10 = target2.Component_No_10;
                        model2.CountByComponent11 = target2.Component_No_11;
                        model2.CountByComponent12 = target2.Component_No_12;
                        model2.CountByComponent13 = target2.Component_No_13;
                    }
                    listModelTal.Add(model2);

                    var model3 = new ApplicationListsViewModel();
                    model3.CountByTaluka = list.Where(x => x.Tahashil == taluka.Tal_Code && x.Dist == item.Dist_Code).Count();
                    model3.CountByDistrict = data;
                    model3.DistrictCode = item.Dist_Code;
                    model3.DistrictName = item.DistName;
                    model3.TalukaName = taluka.Tal_Mr;
                    model3.TalukaCode = taluka.Tal_Code;
                    //Component-wise
                    var _talukaList = list.Where(x => x.Tahashil == taluka.Tal_Code && x.Dist == item.Dist_Code).ToList();
                    int k = 0;
                    for (k = 0; k < 15; k++)
                    {
                        int _count = 0;
                        foreach (var _item in _talukaList.Where(x => x.CompNumber != null))
                        {

                            var d = _item.CompNumber;
                            var _comps = d.Trim().Split(',').ToList();
                            for (int j = 0; j < _comps.Count; j++)
                            {
                                _comps[j] = _comps[j].Trim();
                            }

                            if (_comps.Any(x => x == (k + 1).ToString()))
                            {
                                _count += 1;
                            }

                           
                        }
                        if (k == 0)
                        {
                            model3.CountByComponent1 = _count;
                        }
                        else if (k == 1)
                        {
                            model3.CountByComponent2 = _count;
                        }
                        else if (k == 2)
                        {
                            model3.CountByComponent3 = _count;
                        }
                        else if (k == 3)
                        {
                            model3.CountByComponent4 = _count;
                        }
                        else if (k == 4)
                        {
                            model3.CountByComponent5 = _count;
                        }
                        else if (k == 5)
                        {
                            model3.CountByComponent6 = _count;
                        }
                        else if (k == 6)
                        {
                            model3.CountByComponent7 = _count;
                        }
                        else if (k == 7)
                        {
                            model3.CountByComponent8 = _count;
                        }
                        else if (k == 8)
                        {
                            model3.CountByComponent9 = _count;
                        }
                        else if (k == 9)
                        {
                            model3.CountByComponent10 = _count;
                        }
                        else if (k == 10)
                        {
                            model3.CountByComponent11 = _count;
                        }
                        else if (k == 11)
                        {
                            model3.CountByComponent12 = _count;
                        }
                        else if (k == 12)
                        {
                            model3.CountByComponent13 = _count;
                        }
                        else if (k == 13)
                        {
                            model3.CountByComponent14 = _count;
                        }
                        else if (k == 14)
                        {
                            model3.CountByComponent15 = _count;
                        }

                    }
                    listModelComp.Add(model3);
                }
            }
            model.ApplicantsListByComp = listModelComp;
            model.ApplicantsListByDist = listModelDist.OrderBy(x => x.DistrictName).ToList();
            model.ApplicantsListByTal = listModelTal;

            return View(model);
        }

        public JsonResult ApplicationByDistrict()
        {
            var list = db.ApplicantRegistrations.ToList();
            var districts = db.DistMaster.ToList();
            var talukaList = db.TalMaster.ToList();
            var districtTarget = db.DistrictTarget.ToList();
            var model = new ApplicationListsViewModel();
            var listModel = new List<ApplicationListsViewModel>();
            foreach (var item in districts)
            {
                var d = new ApplicationListsViewModel();
                var target = districtTarget.Where(x => x.Name_of_District == item.DistName && x.Name_of_District != "Total").FirstOrDefault();
                var data = list.Where(x => x.Dist == item.Dist_Code).Count();
                d.CountByDistrict = data;
                d.DistrictCode = item.Dist_Code;
                d.DistrictName = item.DistName;
                d.CountByComponent1 = target.Component_No_1;
                d.CountByComponent2 = target.Component_No_2;
                d.CountByComponent3_7 = target.Component_No_3_7;
                d.CountByComponent8 = target.Component_No_8;
                d.CountByComponent9 = target.Component_No_9;
                d.CountByComponent10 = target.Component_No_10;
                d.CountByComponent11 = target.Component_No_11;
                d.CountByComponent12 = target.Component_No_12;
                d.CountByComponent13 = target.Component_No_13;
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
            var talukaTarget = db.TalukaTarget.ToList();
            var listModel = new List<ApplicationListsViewModel>();
            var model = new ApplicationListsViewModel();
            foreach (var item in districts)
            {
                foreach (var taluka in talukaList.Where(x => x.Dist_Code == item.Dist_Code))
                {
                    var target = talukaTarget.Where(x => x.Name_Of_Taluka == taluka.TalName && x.Name_of_District == item.DistName && x.Name_of_District != null && x.Name_Of_Taluka != null).FirstOrDefault();
                    var model2 = new ApplicationListsViewModel();
                    model2.CountByTaluka = list.Where(x => x.Tahashil == taluka.Tal_Code).Count();
                    model2.DistrictCode = taluka.Dist_Code;
                    model2.DistrictName = item.DistName;
                    model2.TalukaName = taluka.TalName;
                    model2.TalukaCode = taluka.Tal_Code;
                    model2.CountByComponent1 = target.Component_No_1;
                    model2.CountByComponent2 = target.Component_No_2;
                    model2.CountByComponent3_7 = target.Component_No_3_7;
                    model2.CountByComponent8 = target.Component_No_8;
                    model2.CountByComponent9 = target.Component_No_9;
                    model2.CountByComponent10 = target.Component_No_10;
                    model2.CountByComponent11 = target.Component_No_11;
                    model2.CountByComponent12 = target.Component_No_12;
                    model2.CountByComponent13 = target.Component_No_13;
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

                    foreach (var _item in list.Where(x => x.Tahashil == taluka.Tal_Code && x.CompNumber != null))
                    {
                        var d = _item.CompNumber;
                        var _comps = d.Split(',').ToList();
                        var _Components = new List<Tuple<int, string>>();

                        if (_comps.Any(x => x == "1"))
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
            model.appDuration = new ApplicationDuration();
            model.appDuration = db.ApplicationDuration.OrderByDescending(x => x.Id).FirstOrDefault();
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
                    _comp = applicant.CompNumber + ", " + _comp;
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

            return View(applicantRegistration);
        }

        [Authorize]
        public ActionResult GenerateRandomList()
        {
            var target = new TargetViewModel();
            var list = new List<TargetViewModel>();
            var distTarget = db.DistrictTarget.ToList();
            var talukaTarg = db.TalukaTarget.ToList();
            var distMaster = db.DistMaster.ToList();
            var talMaster = db.TalMaster.ToList();
            var applications = db.ApplicantRegistrations.Where(x => x.FormSubmitted == true).ToList();
            foreach (var district in distTarget.Where(x => x.Name_of_District != "Total"))
            {
                var model = new TargetViewModel();
                var talukaList = new List<TalukaViewModel>();
                var districtName = district.Name_of_District;
                model.Name_of_District = districtName;
                var distCode = distMaster.Where(x => x.District_Mr.Trim() == districtName.Trim()).Select(x => x.Dist_Code).FirstOrDefault();
                var application_dist = applications.Where(x => x.Dist == distCode).Count();
                model.ApplicationCount_dist = application_dist;
                //comp 1
                var comp1_target = district.Component_No_1;
                var handicap_comp1target = Math.Round(decimal.Multiply(3, comp1_target) / 100);
                var female_comp1target = Math.Round(decimal.Multiply(30, comp1_target) / 100);
                model.Component_No_1 = comp1_target;
                model.HandicapTarget_Component_No_1 = handicap_comp1target;
                model.FemaleTarget_Component_No_1 = female_comp1target;
                //comp 2
                var comp2_target = district.Component_No_2;
                var handicap_comp2target = Math.Round(decimal.Multiply(3, comp2_target) / 100);
                var female_comp2target = Math.Round(decimal.Multiply(30, comp2_target) / 100);
                model.Component_No_2 = comp2_target;
                model.HandicapTarget_Component_No_2 = handicap_comp2target;
                model.FemaleTarget_Component_No_2 = female_comp2target;
                //comp 3-7
                var comp3_7_target = district.Component_No_3_7;
                var handicap_comp3_7target = Math.Round(decimal.Multiply(3, comp3_7_target) / 100);
                var female_comp3_7target = Math.Round(decimal.Multiply(30, comp3_7_target) / 100);
                model.Component_No_3_7 = comp3_7_target;
                model.HandicapTarget_Component_No_3_7 = handicap_comp3_7target;
                model.FemaleTarget_Component_No_3_7 = female_comp3_7target;
                //comp 8
                var comp8_target = district.Component_No_8;
                var handicap_comp8target = Math.Round(decimal.Multiply(3, comp8_target) / 100);
                var female_comp8target = Math.Round(decimal.Multiply(30, comp8_target) / 100);
                model.Component_No_8 = comp8_target;
                model.HandicapTarget_Component_No_8 = handicap_comp8target;
                model.FemaleTarget_Component_No_8 = female_comp8target;
                //comp 9
                var comp9_target = district.Component_No_9;
                var handicap_comp9target = Math.Round(decimal.Multiply(3, comp9_target) / 100);
                var female_comp9target = Math.Round(decimal.Multiply(30, comp9_target) / 100);
                model.Component_No_9 = comp9_target;
                model.HandicapTarget_Component_No_9 = handicap_comp9target;
                model.FemaleTarget_Component_No_9 = female_comp9target;
                //comp 10
                var comp10_target = district.Component_No_10;
                var handicap_comp10target = Math.Round(decimal.Multiply(3, comp10_target) / 100);
                var female_comp10target = Math.Round(decimal.Multiply(30, comp10_target) / 100);
                model.Component_No_10 = comp10_target;
                model.HandicapTarget_Component_No_10 = handicap_comp10target;
                model.FemaleTarget_Component_No_10 = female_comp10target;
                //comp 11
                var comp11_target = district.Component_No_11;
                var handicap_comp11target = Math.Round(decimal.Multiply(3, comp11_target) / 100);
                var female_comp11target = Math.Round(decimal.Multiply(30, comp11_target) / 100);
                model.Component_No_11 = comp11_target;
                model.HandicapTarget_Component_No_11 = handicap_comp11target;
                model.FemaleTarget_Component_No_11 = female_comp11target;
                //comp 12
                var comp12_target = district.Component_No_12;
                var handicap_comp12target = Math.Round(decimal.Multiply(3, comp12_target) / 100);
                var female_comp12target = Math.Round(decimal.Multiply(30, comp12_target) / 100);
                model.Component_No_12 = comp12_target;
                model.HandicapTarget_Component_No_12 = handicap_comp12target;
                model.FemaleTarget_Component_No_12 = female_comp12target;
                //comp 13
                var comp13_target = district.Component_No_13;
                var handicap_comp13target = Math.Round(decimal.Multiply(3, comp13_target) / 100);
                var female_comp13target = Math.Round(decimal.Multiply(30, comp13_target) / 100);
                model.Component_No_13 = comp13_target;
                model.HandicapTarget_Component_No_13 = handicap_comp13target;
                model.FemaleTarget_Component_No_13 = female_comp13target;

                //taluka-wise
                foreach (var taluka in talukaTarg.Where(x => x.Name_of_District == district.Name_of_District && x.Name_of_District != "Total"))
                {
                    var talukaModel = new TalukaViewModel();
                    var talCode = talMaster.Where(x => x.Dist_Code == distCode && x.Tal_Mr.Trim() == taluka.Name_Of_Taluka.Trim()).Select(x => x.Tal_Code).FirstOrDefault();
                    if (talCode != 0)
                    {
                        var tal_applications = applications.Where(x => x.Tahashil == talCode && x.CompNumber != null).ToList();
                        Console.Write(talCode);
                        talukaModel.Application_Component_No_1 = tal_applications.Where(x => x.CompNumber.Contains("1")).Count();
                        talukaModel.Application_Component_No_2 = tal_applications.Where(x => x.Tahashil == talCode && x.CompNumber.Contains("2")).Count();
                        talukaModel.Application_Component_No_3_7 = tal_applications.Where(x => x.Tahashil == talCode && x.CompNumber.Contains("3")).Count() +
                            tal_applications.Where(x => x.Tahashil == talCode && x.CompNumber.Contains("4")).Count() +
                            tal_applications.Where(x => x.Tahashil == talCode && x.CompNumber.Contains("5")).Count() +
                            tal_applications.Where(x => x.Tahashil == talCode && x.CompNumber.Contains("6")).Count() +
                            tal_applications.Where(x => x.Tahashil == talCode && x.CompNumber.Contains("8")).Count();
                        talukaModel.Application_Component_No_8 = tal_applications.Where(x => x.Tahashil == talCode && x.CompNumber.Contains("8")).Count();
                        talukaModel.Application_Component_No_9 = tal_applications.Where(x => x.Tahashil == talCode && x.CompNumber.Contains("9")).Count();
                        talukaModel.Application_Component_No_10 = tal_applications.Where(x => x.Tahashil == talCode && x.CompNumber.Contains("10")).Count();
                        talukaModel.Application_Component_No_11 = tal_applications.Where(x => x.Tahashil == talCode && x.CompNumber.Contains("11")).Count();
                        talukaModel.Application_Component_No_12 = tal_applications.Where(x => x.Tahashil == talCode && x.CompNumber.Contains("12")).Count();
                        talukaModel.Application_Component_No_13 = tal_applications.Where(x => x.Tahashil == talCode && x.CompNumber.Contains("13")).Count();

                    }
                    talukaModel.Name_Of_Taluka = taluka.Name_Of_Taluka;
                    talukaModel.Component_No_1 = taluka.Component_No_1;
                    talukaModel.Component_No_2 = taluka.Component_No_2;
                    talukaModel.Component_No_3_7 = taluka.Component_No_3_7;
                    talukaModel.Component_No_8 = taluka.Component_No_8;
                    talukaModel.Component_No_9 = taluka.Component_No_9;
                    talukaModel.Component_No_10 = taluka.Component_No_10;
                    talukaModel.Component_No_11 = taluka.Component_No_11;
                    talukaModel.Component_No_12 = taluka.Component_No_12;
                    talukaModel.Component_No_13 = taluka.Component_No_13;
                    talukaModel.GeneralTarget_Component_No_1 = Math.Round(decimal.Divide(decimal.Multiply(67, talukaModel.Component_No_1), 100));
                    talukaModel.GeneralTarget_Component_No_2 = Math.Round(decimal.Multiply(67, talukaModel.Component_No_2) / 100);
                    talukaModel.GeneralTarget_Component_No_3_7 = Math.Round(decimal.Multiply(67, talukaModel.Component_No_3_7) / 100);
                    talukaModel.GeneralTarget_Component_No_8 = Math.Round(decimal.Multiply(67, talukaModel.Component_No_8) / 100);
                    talukaModel.GeneralTarget_Component_No_9 = Math.Round(decimal.Multiply(67, talukaModel.Component_No_9) / 100);
                    talukaModel.GeneralTarget_Component_No_10 = Math.Round(decimal.Multiply(67, talukaModel.Component_No_10) / 100);
                    talukaModel.GeneralTarget_Component_No_11 = Math.Round(decimal.Multiply(67, talukaModel.Component_No_11) / 100);
                    talukaModel.GeneralTarget_Component_No_12 = Math.Round(decimal.Multiply(67, talukaModel.Component_No_12) / 100);
                    talukaModel.GeneralTarget_Component_No_13 = Math.Round(decimal.Multiply(67, talukaModel.Component_No_13) / 100);

                    talukaList.Add(talukaModel);
                }
                model.TalukaTarget = talukaList;
                list.Add(model);

            }
            target.TargetList = list;
            return View(target);
        }

        public JsonResult SelectWaitingList(int distCode)
        {
            db.Database.ExecuteSqlCommand("delete from [SelectedGenerals] where Type = 'Waiting' and DistCode =" + distCode);
            var ofcrModel = new OfficerLogin();
            var model = new SelectedListViewModel();
            var distMaster = db.DistMaster.ToList();
            var distName_Mr = distMaster.Where(x => x.Dist_Code == distCode).Select(x => x.District_Mr).FirstOrDefault();
            var distTarget = db.DistrictTarget.Where(x => x.Name_of_District == distName_Mr).FirstOrDefault();
            var selected = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            var talukaTarg = db.TalukaTarget.Where(x => x.Name_of_District == distName_Mr).ToList();
            var list = db.ApplicantRegistrations.Where(x => x.FormSubmitted == true && x.Dist == distCode && (!selected.Contains(x.ApplicationNumber))).ToList();
            var taluka = db.TalMaster.ToList();
            //comp 1
            var comp1List = list.Where(x => x.CompNumber.Contains("1")).Select(x => x.Id).ToList();
            var comp1ListCount = distTarget.Component_No_1;
            //select 3% for handicapped
            var targetHandicapped = Math.Round(decimal.Multiply(3, comp1ListCount) / 100);
            //select 30% for females
            comp1ListCount = distTarget.Component_No_1;
            var targetFemale = Math.Round(decimal.Multiply(30, comp1ListCount) / 100);
            var totalTaluka1 = 0;
            foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
            {
                var d = talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_1).FirstOrDefault();
                var targ = Math.Round(decimal.Multiply(67, d) / 100);
                totalTaluka1 += Convert.ToInt32(targ);//talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_1).FirstOrDefault();
            }
           // foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
           // {
                comp1List = list.Where(x => x.CompNumber.Contains("1")).Select(x => x.Id).ToList();

                var targetTaluka1 = 5 * (targetFemale + targetHandicapped + totalTaluka1);
                IEnumerable<int> waitingRandomList = comp1List.Shuffle().Take((Int32)targetTaluka1).Distinct();
                foreach (var number in waitingRandomList.Distinct())
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;
                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Component = 1;
                    gen_sel.DistCode = distCode;
                    gen_sel.Type = "Waiting";
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil).FirstOrDefault().Tal_Mr;
                    gen_sel.Name = data.ApName;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    gen_sel.CreatedOn = DateTime.Now;
                    db.SelectedGeneral.Add(gen_sel);

                }
            //}
            db.SaveChanges();


            selected = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            var comp2ListCount = distTarget.Component_No_1;
            //select 3% for handicapped
            var targetHandicapped2 = Math.Round(decimal.Multiply(3, comp2ListCount) / 100);
            //select 30% for females
            comp2ListCount = distTarget.Component_No_2;
            var targetFemale2 = Math.Round(decimal.Multiply(30, comp2ListCount) / 100);
            var totalTaluka2 = 0;
            foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
            {
                var d = talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_2).FirstOrDefault();
                var targ = Math.Round(decimal.Multiply(67, d) / 100);
                totalTaluka2 += Convert.ToInt32(targ);
           
            }
            var comp2List = list.Where(x => x.CompNumber.Contains("2") && (!selected.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();

            var targetTaluka2 = 5 * (targetFemale2 + targetHandicapped2 + totalTaluka2);
            IEnumerable<int> waitingRandomList2 = comp2List.Shuffle().Take((Int32)targetTaluka2).Distinct();
            foreach (var number in waitingRandomList2.Distinct())
            {
                var gen_sel = new SelectedGeneral();
                gen_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                gen_sel.ApplicationNumber = data.ApplicationNumber;
                gen_sel.Component = 2;
                gen_sel.DistCode = distCode;
                gen_sel.Type = "Waiting";
                gen_sel.AadharNo = data.AdharCardNo;
                gen_sel.Gender = data.Gender;
                gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                gen_sel.PhNo = data.PhNo;
                gen_sel.District_Mr = distName_Mr;
                gen_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil).FirstOrDefault().Tal_Mr;
                gen_sel.Name = data.ApName;
                gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                gen_sel.TalukaCode = data.Tahashil.Value;
                gen_sel.CreatedOn = DateTime.Now;
                db.SelectedGeneral.Add(gen_sel);

            }
           // }
            db.SaveChanges();

            selected = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            var comp3List = list.Where(x => (x.CompNumber.Contains("3") || x.CompNumber.Contains("4") || x.CompNumber.Contains("5") || x.CompNumber.Contains("6")
            || x.CompNumber.Contains("7")) && (!selected.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            var comp3ListCount = distTarget.Component_No_3_7;
            //select 3% for handicapped
            var targetHandicapped3 = Math.Round(decimal.Multiply(3, comp3ListCount) / 100);
            //select 30% for females
            comp3ListCount = distTarget.Component_No_3_7;
            var targetFemale3 = Math.Round(decimal.Multiply(30, comp3ListCount) / 100);
            var totalTaluka3 = 0;
            foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
            {
                var d = talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_3_7).FirstOrDefault();
                var targ = Math.Round(decimal.Multiply(67, d) / 100);
                totalTaluka3 += Convert.ToInt32(targ);
                
            }
            var targetTaluka3 = 5 * (targetFemale3 + targetHandicapped3 + totalTaluka3);
            IEnumerable<int> waitingRandomList3 = comp3List.Shuffle().Take((Int32)targetTaluka3).Distinct();
            foreach (var number in waitingRandomList3.Distinct())
            {
                var gen_sel = new SelectedGeneral();
                gen_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                gen_sel.ApplicationNumber = data.ApplicationNumber;
                gen_sel.Component = 3;
                gen_sel.DistCode = distCode;
                gen_sel.Type = "Waiting";
                gen_sel.AadharNo = data.AdharCardNo;
                gen_sel.Gender = data.Gender;
                gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                gen_sel.PhNo = data.PhNo;
                gen_sel.District_Mr = distName_Mr;
                gen_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil).FirstOrDefault().Tal_Mr;
                gen_sel.Name = data.ApName;
                gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                gen_sel.TalukaCode = data.Tahashil.Value;
                gen_sel.CreatedOn = DateTime.Now;
                db.SelectedGeneral.Add(gen_sel);

            }
            db.SaveChanges();


          //  selected = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            var comp8List = list.Where(x => x.CompNumber.Contains("8") && (!selected.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            var comp8ListCount = distTarget.Component_No_8;
            //select 3% for handicapped
            var targetHandicapped8 = Math.Round(decimal.Multiply(3, comp8ListCount) / 100);
            //select 30% for females
            comp8ListCount = distTarget.Component_No_8;
            var targetFemale8 = Math.Round(decimal.Multiply(30, comp8ListCount) / 100);
            var totalTaluka8 = 0;
            foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
            {
                var d = talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_8).FirstOrDefault();
                var targ = Math.Round(decimal.Multiply(67, d) / 100);
                totalTaluka8 += Convert.ToInt32(targ);
            }
            var targetTaluka8 = 5 * (targetFemale8 + targetHandicapped8 + totalTaluka8);
            IEnumerable<int> waitingRandomList8 = comp8List.Shuffle().Take((Int32)targetTaluka8).Distinct();
            foreach (var number in waitingRandomList8.Distinct())
            {
                var gen_sel = new SelectedGeneral();
                gen_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                gen_sel.ApplicationNumber = data.ApplicationNumber;
                gen_sel.Component = 8;
                gen_sel.DistCode = distCode;
                gen_sel.Type = "Waiting";
                gen_sel.AadharNo = data.AdharCardNo;
                gen_sel.Gender = data.Gender;
                gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                gen_sel.PhNo = data.PhNo;
                gen_sel.District_Mr = distName_Mr;
                gen_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil).FirstOrDefault().Tal_Mr;
                gen_sel.Name = data.ApName;
                gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                gen_sel.TalukaCode = data.Tahashil.Value;
                gen_sel.CreatedOn = DateTime.Now;
                db.SelectedGeneral.Add(gen_sel);

            }
            db.SaveChanges();

            var comp9List = list.Where(x => x.CompNumber.Contains("9")).Select(x => x.Id).ToList();
            var comp9ListCount = distTarget.Component_No_9;
            //select 3% for handicapped
            var targetHandicapped9 = Math.Round(decimal.Multiply(3, comp9ListCount) / 100);
            //select 30% for females
            comp9ListCount = distTarget.Component_No_9;
            var targetFemale9 = Math.Round(decimal.Multiply(30, comp9ListCount) / 100);
            var totalTaluka9 = 0;
            foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
            {
                var d = talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_9).FirstOrDefault();
                var targ = Math.Round(decimal.Multiply(67, d) / 100);
                totalTaluka9 += Convert.ToInt32(targ);
               // totalTaluka9 += talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_9).FirstOrDefault();
            }
            var targetTaluka9 = 5 * (targetFemale9 + targetHandicapped9 + totalTaluka9);
            IEnumerable<int> waitingRandomList9 = comp9List.Shuffle().Take((Int32)targetTaluka9).Distinct();
            foreach (var number in waitingRandomList9.Distinct())
            {
                var gen_sel = new SelectedGeneral();
                gen_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                gen_sel.ApplicationNumber = data.ApplicationNumber;
                gen_sel.Component = 9;
                gen_sel.DistCode = distCode;
                gen_sel.Type = "Waiting";
                gen_sel.AadharNo = data.AdharCardNo;
                gen_sel.Gender = data.Gender;
                gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                gen_sel.PhNo = data.PhNo;
                gen_sel.District_Mr = distName_Mr;
                gen_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil).FirstOrDefault().Tal_Mr;
                gen_sel.Name = data.ApName;
                gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                gen_sel.TalukaCode = data.Tahashil.Value;
                gen_sel.CreatedOn = DateTime.Now;
                db.SelectedGeneral.Add(gen_sel);

            }
            db.SaveChanges();


            var comp10List = list.Where(x => x.CompNumber.Contains("10")).Select(x => x.Id).ToList();
            var comp10ListCount = distTarget.Component_No_10;
            //select 3% for handicapped
            var targetHandicapped10 = Math.Round(decimal.Multiply(3, comp10ListCount) / 100);
            //select 30% for females
            comp10ListCount = distTarget.Component_No_10;
            var targetFemale10 = Math.Round(decimal.Multiply(30, comp10ListCount) / 100);
            var totalTaluka10 = 0;
            foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
            {
                var d = talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_10).FirstOrDefault();
                var targ = Math.Round(decimal.Multiply(67, d) / 100);
                totalTaluka10 += Convert.ToInt32(targ);
            }
            var targetTaluka10 = 5 * (targetFemale10 + targetHandicapped10 + totalTaluka10);
            IEnumerable<int> waitingRandomList10 = comp10List.Shuffle().Take((Int32)targetTaluka10).Distinct();
            foreach (var number in waitingRandomList10.Distinct())
            {
                var gen_sel = new SelectedGeneral();
                gen_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                gen_sel.ApplicationNumber = data.ApplicationNumber;
                gen_sel.Component = 10;
                gen_sel.DistCode = distCode;
                gen_sel.Type = "Waiting";
                gen_sel.AadharNo = data.AdharCardNo;
                gen_sel.Gender = data.Gender;
                gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                gen_sel.PhNo = data.PhNo;
                gen_sel.District_Mr = distName_Mr;
                gen_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil).FirstOrDefault().Tal_Mr;
                gen_sel.Name = data.ApName;
                gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                gen_sel.TalukaCode = data.Tahashil.Value;
                gen_sel.CreatedOn = DateTime.Now;
                db.SelectedGeneral.Add(gen_sel);

            }
            db.SaveChanges();



            var comp11List = list.Where(x => x.CompNumber.Contains("11")).Select(x => x.Id).ToList();
            var comp11ListCount = distTarget.Component_No_11;
            //select 3% for handicapped
            var targetHandicapped11 = Math.Round(decimal.Multiply(3, comp11ListCount) / 100);
            //select 30% for females
            comp11ListCount = distTarget.Component_No_11;
            var targetFemale11 = Math.Round(decimal.Multiply(30, comp11ListCount) / 100);
            var totalTaluka11 = 0;
            foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
            {
                var d = talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_11).FirstOrDefault();
                var targ = Math.Round(decimal.Multiply(67, d) / 100);
                totalTaluka11 += Convert.ToInt32(targ);
            }
            var targetTaluka11 = 5 * (targetFemale11 + targetHandicapped11 + totalTaluka11);
            IEnumerable<int> waitingRandomList11 = comp11List.Shuffle().Take((Int32)targetTaluka11).Distinct();
            foreach (var number in waitingRandomList11.Distinct())
            {
                var gen_sel = new SelectedGeneral();
                gen_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                gen_sel.ApplicationNumber = data.ApplicationNumber;
                gen_sel.Component = 11;
                gen_sel.DistCode = distCode;
                gen_sel.Type = "Waiting";
                gen_sel.AadharNo = data.AdharCardNo;
                gen_sel.Gender = data.Gender;
                gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                gen_sel.PhNo = data.PhNo;
                gen_sel.District_Mr = distName_Mr;
                gen_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil).FirstOrDefault().Tal_Mr;
                gen_sel.Name = data.ApName;
                gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                gen_sel.TalukaCode = data.Tahashil.Value;
                gen_sel.CreatedOn = DateTime.Now;
                db.SelectedGeneral.Add(gen_sel);

            }
            db.SaveChanges();

            var comp12List = list.Where(x => x.CompNumber.Contains("12")).Select(x => x.Id).ToList();
            var comp12ListCount = distTarget.Component_No_12;
            //select 3% for handicapped
            var targetHandicapped12 = Math.Round(decimal.Multiply(3, comp12ListCount) / 100);
            //select 30% for females
            comp12ListCount = distTarget.Component_No_12;
            var targetFemale12 = Math.Round(decimal.Multiply(30, comp12ListCount) / 100);
            var totalTaluka12 = 0;
            foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
            {
                var d = talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_12).FirstOrDefault();
                var targ = Math.Round(decimal.Multiply(67, d) / 100);
                totalTaluka12 += Convert.ToInt32(targ);
            }
            var targetTaluka12 = 5 * (targetFemale12 + targetHandicapped12 + totalTaluka12);
            IEnumerable<int> waitingRandomList12 = comp12List.Shuffle().Take((Int32)targetTaluka12).Distinct();
            foreach (var number in waitingRandomList12.Distinct())
            {
                var gen_sel = new SelectedGeneral();
                gen_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                gen_sel.ApplicationNumber = data.ApplicationNumber;
                gen_sel.Component = 12;
                gen_sel.DistCode = distCode;
                gen_sel.Type = "Waiting";
                gen_sel.AadharNo = data.AdharCardNo;
                gen_sel.Gender = data.Gender;
                gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                gen_sel.PhNo = data.PhNo;
                gen_sel.District_Mr = distName_Mr;
                gen_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil).FirstOrDefault().Tal_Mr;
                gen_sel.Name = data.ApName;
                gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                gen_sel.TalukaCode = data.Tahashil.Value;
                gen_sel.CreatedOn = DateTime.Now;
                db.SelectedGeneral.Add(gen_sel);

            }
            db.SaveChanges();

            var comp13List = list.Where(x => x.CompNumber.Contains("13")).Select(x => x.Id).ToList();
            var comp13ListCount = distTarget.Component_No_13;
            //select 3% for handicapped
            var targetHandicapped13 = Math.Round(decimal.Multiply(3, comp13ListCount) / 100);
            //select 30% for females
            comp13ListCount = distTarget.Component_No_13;
            var targetFemale13 = Math.Round(decimal.Multiply(30, comp13ListCount) / 100);
            var totalTaluka13 = 0;
            foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
            {
                var d = talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_13).FirstOrDefault();
                var targ = Math.Round(decimal.Multiply(67, d) / 100);
                totalTaluka13 += Convert.ToInt32(targ);
               // totalTaluka13 += talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_13).FirstOrDefault();
            }
            var targetTaluka13 = 5 * (targetFemale13 + targetHandicapped13 + totalTaluka13);
            IEnumerable<int> waitingRandomList13 = comp13List.Shuffle().Take((Int32)targetTaluka13).Distinct();
            foreach (var number in waitingRandomList13.Distinct())
            {
                var gen_sel = new SelectedGeneral();
                gen_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                gen_sel.ApplicationNumber = data.ApplicationNumber;
                gen_sel.Component = 13;
                gen_sel.DistCode = distCode;
                gen_sel.Type = "Waiting";
                gen_sel.AadharNo = data.AdharCardNo;
                gen_sel.Gender = data.Gender;
                gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                gen_sel.PhNo = data.PhNo;
                gen_sel.District_Mr = distName_Mr;
                gen_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil).FirstOrDefault().Tal_Mr;
                gen_sel.Name = data.ApName;
                gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                gen_sel.TalukaCode = data.Tahashil.Value;
                gen_sel.CreatedOn = DateTime.Now;
                db.SelectedGeneral.Add(gen_sel);

            }
            db.SaveChanges();

            model.WaitingList = new List<SelectedGeneral>();
            model.WaitingList = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Waiting").ToList();
            ofcrModel.SelectedList = model;
            return Json(ofcrModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SelectList1(int distCode)
        {
            db.Database.ExecuteSqlCommand("delete from [SelectedFemales] where DistCode =" + distCode);
            db.Database.ExecuteSqlCommand("delete from [SelectedHandicappeds] where DistCode =" + distCode);
            db.Database.ExecuteSqlCommand("delete from [SelectedGenerals] where Type = 'Selected' and DistCode =" + distCode);
            var ofcrModel = new OfficerLogin();
            var model = new SelectedListViewModel();
            ofcrModel.SelectedList1 = new SelectedListViewModel();

            var distMaster = db.DistMaster.ToList();
            var selectedTaluka = db.SelectedGeneral.Where(x => x.DistCode == distCode).Select(x => x.TalukaCode).ToList();
            var taluka = db.TalMaster.Where(x => x.Dist_Code == distCode).ToList();
            var distName_Mr = distMaster.Where(x => x.Dist_Code == distCode).Select(x => x.District_Mr).FirstOrDefault();
            var distTarget = db.DistrictTarget.Where(x => x.Name_of_District.Trim() == distName_Mr.Trim()).FirstOrDefault();
            var talukaTarg = db.TalukaTarget.Where(x => x.Name_of_District.Trim() == distName_Mr.Trim()).ToList();
            ofcrModel.TCount = taluka.Count;
            var selected = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            var selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            var selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected.AddRange(selectedFemale);
            selected.AddRange(selectedHandicapped);
            var list = db.ApplicantRegistrations.Where(x => x.FormSubmitted == true && x.Dist == distCode && (!selected.Contains(x.ApplicationNumber))).ToList();


            var comp1List = list.Where(x => (x.CompNumber.Trim().Contains("1") && (!(x.CompNumber.Contains("13") ||
            x.CompNumber.Contains("11") || x.CompNumber.Contains("10") || x.CompNumber.Contains("12"))))
            && x.ApplicantCrippled == "होय").Select(x => x.Id).ToList();
            var comp1ListCount = distTarget.Component_No_1;
            //select 3% for handicapped
            var targetHandicapped = Math.Round(decimal.Multiply(3, comp1ListCount) / 100);
            IEnumerable<int> handicapRandomList = comp1List.Shuffle().Take((Int32)targetHandicapped).Distinct();
            // var villMaster = db.VillageMaster.ToList();
            foreach (var number in handicapRandomList)
            {
                var handicaps_sel = new SelectedHandicapped();
                handicaps_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                handicaps_sel.ApplicationNumber = data.ApplicationNumber;
                handicaps_sel.Component = 1;
                handicaps_sel.DistCode = distCode;
                handicaps_sel.TalukaCode = data.Tahashil.Value;
                handicaps_sel.AadharNo = data.AdharCardNo;
                handicaps_sel.Gender = data.Gender;
                handicaps_sel.ApplicantCrippled = data.ApplicantCrippled;
                handicaps_sel.PhNo = data.PhNo;
                handicaps_sel.District_Mr = distName_Mr;
                handicaps_sel.Name = data.ApName;
                handicaps_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil).FirstOrDefault().Tal_Mr;
                handicaps_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                handicaps_sel.Type = "Selected";
                handicaps_sel.CreatedOn = DateTime.Now;
                db.SelectedHandicapped.Add(handicaps_sel);

            }
            db.SaveChanges();
           // selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            //females
            var selected2 = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected2.AddRange(selectedFemale);
            //selected2.AddRange(selectedHandicapped);
            comp1List = list.Where(x => x.Dist == distCode && (x.CompNumber.Contains("1") && (!(x.CompNumber.Contains("13") ||
            x.CompNumber.Contains("11") || x.CompNumber.Contains("10") || x.CompNumber.Contains("12")))) && (x.Gender == "Female" || x.Gender == "स्त्री")
            && (!selected2.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            comp1ListCount = distTarget.Component_No_1;
            var targetFemale = Math.Round(decimal.Multiply(30, comp1ListCount) / 100);
            IEnumerable<int> femaleRandomList = comp1List.Shuffle().Take((Int32)targetFemale).Distinct();
            foreach (var number in femaleRandomList)
            {
                var female_sel = new SelectedFemale();
                female_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                female_sel.ApplicationNumber = data.ApplicationNumber;
                female_sel.Component = 1;
                female_sel.DistCode = distCode;
                female_sel.TalukaCode = data.Tahashil.Value;
                female_sel.AadharNo = data.AdharCardNo;
                female_sel.Gender = data.Gender;
                female_sel.ApplicantCrippled = data.ApplicantCrippled;
                female_sel.PhNo = data.PhNo;
                female_sel.Name = data.ApName;
                female_sel.District_Mr = distName_Mr;
                female_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil).FirstOrDefault().Tal_Mr;
                female_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                female_sel.Type = "Selected";
                female_sel.CreatedOn = DateTime.Now;
                db.SelectedFemale.Add(female_sel);

            }
            db.SaveChanges();

            selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
           // selected2 = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected2.AddRange(selectedFemale);
            selected2.AddRange(selectedHandicapped);
            var comp2List = list.Where(x => x.CompNumber.Trim().Contains("2") && x.ApplicantCrippled == "होय" && (!selected2.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            var comp2ListCount = distTarget.Component_No_2;
            //select 3% for handicapped
            var targetHandicapped2 = Math.Round(decimal.Multiply(3, comp2ListCount) / 100);
            IEnumerable<int> handicapRandomList2 = comp2List.Shuffle().Take((Int32)targetHandicapped2).Distinct();
            // var villMaster = db.VillageMaster.ToList();
            foreach (var number in handicapRandomList2)
            {
                var handicaps_sel = new SelectedHandicapped();
                handicaps_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                handicaps_sel.ApplicationNumber = data.ApplicationNumber;
                handicaps_sel.Component = 2;
                handicaps_sel.DistCode = distCode;
                handicaps_sel.TalukaCode = data.Tahashil.Value;
                handicaps_sel.AadharNo = data.AdharCardNo;
                handicaps_sel.Gender = data.Gender;
                handicaps_sel.ApplicantCrippled = data.ApplicantCrippled;
                handicaps_sel.PhNo = data.PhNo;
                handicaps_sel.District_Mr = distName_Mr;
                handicaps_sel.Name = data.ApName;
                handicaps_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil).FirstOrDefault().Tal_Mr;
                handicaps_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                handicaps_sel.Type = "Selected";
                handicaps_sel.CreatedOn = DateTime.Now;
                db.SelectedHandicapped.Add(handicaps_sel);

            }
            db.SaveChanges();

            //females
            selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            // selected2 = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected2.AddRange(selectedFemale);
            selected2.AddRange(selectedHandicapped); comp2List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("2") && (x.Gender == "Female" || x.Gender == "स्त्री") && (!selected2.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            comp2ListCount = distTarget.Component_No_2;
            var targetFemale2 = Math.Round(decimal.Multiply(30, comp2ListCount) / 100);
            IEnumerable<int> femaleRandomList2 = comp2List.Shuffle().Take((Int32)targetFemale2).Distinct();
            foreach (var number in femaleRandomList2)
            {
                var female_sel = new SelectedFemale();
                female_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                female_sel.ApplicationNumber = data.ApplicationNumber;
                female_sel.Component = 2;
                female_sel.DistCode = distCode;
                female_sel.TalukaCode = data.Tahashil.Value;
                female_sel.AadharNo = data.AdharCardNo;
                female_sel.Gender = data.Gender;
                female_sel.ApplicantCrippled = data.ApplicantCrippled;
                female_sel.PhNo = data.PhNo;
                female_sel.Name = data.ApName;
                female_sel.District_Mr = distName_Mr;
                female_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil).FirstOrDefault().Tal_Mr;
                female_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                female_sel.Type = "Selected";
                female_sel.CreatedOn = DateTime.Now;
                db.SelectedFemale.Add(female_sel);

            }
            db.SaveChanges();

            //comp 3 -7
            selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            // selected2 = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected2.AddRange(selectedFemale);
            selected2.AddRange(selectedHandicapped); var comp3List = list.Where(x => x.Dist == distCode && (x.CompNumber.Trim().Contains("3") || x.CompNumber.Trim().Contains("4") || x.CompNumber.Trim().Contains("5") || x.CompNumber.Trim().Contains("6") ||
            x.CompNumber.Trim().Contains("7")) && (!(x.CompNumber.Contains("13") || x.CompNumber.Contains("14") || x.CompNumber.Contains("15"))) && x.ApplicantCrippled == "होय" && (!selected2.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            var comp3ListCount = distTarget.Component_No_3_7;
            //select 3% for handicapped
            var targetHandicapped3 = Math.Round(decimal.Multiply(3, comp3ListCount) / 100);
            IEnumerable<int> handicapRandomList3 = comp3List.Shuffle().Take((Int32)targetHandicapped3).Distinct();
            foreach (var number in handicapRandomList3)
            {
                var handicaps_sel = new SelectedHandicapped();
                handicaps_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                handicaps_sel.ApplicationNumber = data.ApplicationNumber;
                handicaps_sel.Component = 3;
                handicaps_sel.DistCode = distCode;
                handicaps_sel.TalukaCode = data.Tahashil.Value;
                handicaps_sel.AadharNo = data.AdharCardNo;
                handicaps_sel.Gender = data.Gender;
                handicaps_sel.ApplicantCrippled = data.ApplicantCrippled;
                handicaps_sel.PhNo = data.PhNo;
                handicaps_sel.District_Mr = distName_Mr;
                handicaps_sel.Name = data.ApName;
                handicaps_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                handicaps_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                handicaps_sel.Type = "Selected";
                handicaps_sel.CreatedOn = DateTime.Now;
                db.SelectedHandicapped.Add(handicaps_sel);
            }
            db.SaveChanges();
            //select 30% for females
            selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            // selected2 = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected2.AddRange(selectedFemale);
            selected2.AddRange(selectedHandicapped); comp3List = list.Where(x => x.Dist == distCode && (x.CompNumber.Trim().Contains("3") || x.CompNumber.Trim().Contains("4") || x.CompNumber.Trim().Contains("5") || x.CompNumber.Trim().Contains("6") ||
            x.CompNumber.Trim().Contains("7")) && (!(x.CompNumber.Contains("13") || x.CompNumber.Contains("14") || x.CompNumber.Contains("15"))) && (x.Gender == "Female" || x.Gender == "स्त्री") && (!selected2.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            comp3ListCount = distTarget.Component_No_3_7;
            var targetFemale3 = Math.Round(decimal.Multiply(30, comp3ListCount) / 100);
            IEnumerable<int> femaleRandomList3 = comp3List.Shuffle().Take((Int32)targetFemale3).Distinct();
            foreach (var number in femaleRandomList3)
            {
                var female_sel = new SelectedFemale();
                female_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                female_sel.ApplicationNumber = data.ApplicationNumber;
                female_sel.Component = 3;
                female_sel.DistCode = distCode;
                female_sel.TalukaCode = data.Tahashil.Value;
                female_sel.AadharNo = data.AdharCardNo;
                female_sel.Gender = data.Gender;
                female_sel.ApplicantCrippled = data.ApplicantCrippled;
                female_sel.PhNo = data.PhNo;
                female_sel.Name = data.ApName;
                female_sel.District_Mr = distName_Mr;
                female_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                female_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                female_sel.Type = "Selected";
                female_sel.CreatedOn = DateTime.Now;
                db.SelectedFemale.Add(female_sel);
                // db.SaveChanges();
            }
            db.SaveChanges();

            selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            // selected2 = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected2.AddRange(selectedFemale);
            selected2.AddRange(selectedHandicapped); var comp8List = list.Where(x => x.Dist == distCode && x.CompNumber.Trim().Contains("8") && x.ApplicantCrippled == "होय" && (!selected2.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            var comp8ListCount = distTarget.Component_No_8;
            //select 3% for handicapped
            var targetHandicapped8 = Math.Round(decimal.Multiply(3, comp8ListCount) / 100);
            IEnumerable<int> handicapRandomList8 = comp8List.Shuffle().Take((Int32)targetHandicapped8).Distinct();
            foreach (var number in handicapRandomList8)
            {
                var handicaps_sel = new SelectedHandicapped();
                handicaps_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                handicaps_sel.ApplicationNumber = data.ApplicationNumber;
                handicaps_sel.Component = 8;
                handicaps_sel.DistCode = distCode;
                handicaps_sel.TalukaCode = data.Tahashil.Value;
                handicaps_sel.AadharNo = data.AdharCardNo;
                handicaps_sel.Gender = data.Gender;
                handicaps_sel.ApplicantCrippled = data.ApplicantCrippled;
                handicaps_sel.PhNo = data.PhNo;
                handicaps_sel.District_Mr = distName_Mr;
                handicaps_sel.Name = data.ApName;
                handicaps_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                handicaps_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                handicaps_sel.Type = "Selected";
                handicaps_sel.CreatedOn = DateTime.Now;
                db.SelectedHandicapped.Add(handicaps_sel);
            }
            db.SaveChanges();
            //select 30% for females
            selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            // selected2 = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected2.AddRange(selectedFemale);
            selected2.AddRange(selectedHandicapped); comp8List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("8") && (x.Gender == "Female" || x.Gender == "स्त्री") && (!selected2.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            comp8ListCount = distTarget.Component_No_8;
            var targetFemale8 = Math.Round(decimal.Multiply(30, comp8ListCount) / 100);
            IEnumerable<int> femaleRandomList8 = comp8List.Shuffle().Take((Int32)targetFemale8).Distinct();
            foreach (var number in femaleRandomList8)
            {
                var female_sel = new SelectedFemale();
                female_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                female_sel.ApplicationNumber = data.ApplicationNumber;
                female_sel.Component = 8;
                female_sel.DistCode = distCode;
                female_sel.DistCode = distCode;
                female_sel.TalukaCode = data.Tahashil.Value;
                female_sel.AadharNo = data.AdharCardNo;
                female_sel.Gender = data.Gender;
                female_sel.ApplicantCrippled = data.ApplicantCrippled;
                female_sel.PhNo = data.PhNo;
                female_sel.Name = data.ApName;
                female_sel.District_Mr = distName_Mr;
                female_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                female_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                female_sel.Type = "Selected";
                female_sel.CreatedOn = DateTime.Now;
                db.SelectedFemale.Add(female_sel);
                // db.SaveChanges();
            }
            db.SaveChanges();

            selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            // selected2 = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected2.AddRange(selectedFemale);
            selected2.AddRange(selectedHandicapped); var comp9List = list.Where(x => x.Dist == distCode && x.CompNumber.Trim().Contains("9") && x.ApplicantCrippled == "होय" && (!selected2.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            var comp9ListCount = distTarget.Component_No_9;
            //select 3% for handicapped
            var targetHandicapped9 = Math.Round(decimal.Multiply(3, comp9ListCount) / 100);
            IEnumerable<int> handicapRandomList9 = comp9List.Shuffle().Take((Int32)targetHandicapped9).Distinct();
            foreach (var number in handicapRandomList9)
            {
                var handicaps_sel = new SelectedHandicapped();
                handicaps_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                handicaps_sel.ApplicationNumber = data.ApplicationNumber;
                handicaps_sel.Component = 9;
                handicaps_sel.DistCode = distCode;
                handicaps_sel.TalukaCode = data.Tahashil.Value;
                handicaps_sel.AadharNo = data.AdharCardNo;
                handicaps_sel.Gender = data.Gender;
                handicaps_sel.ApplicantCrippled = data.ApplicantCrippled;
                handicaps_sel.PhNo = data.PhNo;
                handicaps_sel.District_Mr = distName_Mr;
                handicaps_sel.Name = data.ApName;
                handicaps_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                handicaps_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                handicaps_sel.Type = "Selected";
                handicaps_sel.CreatedOn = DateTime.Now;
                db.SelectedHandicapped.Add(handicaps_sel);
            }
            db.SaveChanges();
            //select 30% for females
            selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            // selected2 = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected2.AddRange(selectedFemale);
            selected2.AddRange(selectedHandicapped); comp9List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("9") && (x.Gender == "Female" || x.Gender == "स्त्री") && (!selected2.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            comp9ListCount = distTarget.Component_No_9;
            var targetFemale9 = Math.Round(decimal.Multiply(30, comp9ListCount) / 100);
            IEnumerable<int> femaleRandomList9 = comp9List.Shuffle().Take((Int32)targetFemale9).Distinct();
            foreach (var number in femaleRandomList9)
            {
                var female_sel = new SelectedFemale();
                female_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                female_sel.ApplicationNumber = data.ApplicationNumber;
                female_sel.Component = 9;
                female_sel.DistCode = distCode;
                female_sel.TalukaCode = data.Tahashil.Value;
                female_sel.AadharNo = data.AdharCardNo;
                female_sel.Gender = data.Gender;
                female_sel.ApplicantCrippled = data.ApplicantCrippled;
                female_sel.PhNo = data.PhNo;
                female_sel.Name = data.ApName;
                female_sel.District_Mr = distName_Mr;
                female_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                female_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                female_sel.Type = "Selected";
                female_sel.CreatedOn = DateTime.Now;
                db.SelectedFemale.Add(female_sel);
                // db.SaveChanges();
            }
            db.SaveChanges();


            selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            // selected2 = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected2.AddRange(selectedFemale);
            selected2.AddRange(selectedHandicapped);
            var comp10List = list.Where(x => x.Dist == distCode && x.CompNumber.Trim().Contains("10") && x.ApplicantCrippled == "होय" && (!selected2.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            var comp10ListCount = distTarget.Component_No_10;
            //select 3% for handicapped
            var targetHandicapped10 = Math.Round(decimal.Multiply(3, comp10ListCount) / 100);
            IEnumerable<int> handicapRandomList10 = comp10List.Shuffle().Take((Int32)targetHandicapped10).Distinct();
            foreach (var number in handicapRandomList10)
            {
                var handicaps_sel = new SelectedHandicapped();
                handicaps_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                handicaps_sel.ApplicationNumber = data.ApplicationNumber;
                handicaps_sel.Component = 10;
                handicaps_sel.DistCode = distCode;
                handicaps_sel.TalukaCode = data.Tahashil.Value;
                handicaps_sel.AadharNo = data.AdharCardNo;
                handicaps_sel.Gender = data.Gender;
                handicaps_sel.ApplicantCrippled = data.ApplicantCrippled;
                handicaps_sel.PhNo = data.PhNo;
                handicaps_sel.District_Mr = distName_Mr;
                handicaps_sel.Name = data.ApName;
                handicaps_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                handicaps_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                handicaps_sel.Type = "Selected";
                handicaps_sel.CreatedOn = DateTime.Now;
                db.SelectedHandicapped.Add(handicaps_sel);
            }
            db.SaveChanges();
            //select 30% for females
            selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            // selected2 = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected2.AddRange(selectedFemale);
            selected2.AddRange(selectedHandicapped);
            comp10List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("10") && (x.Gender == "Female" || x.Gender == "स्त्री") && (!selected2.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            comp10ListCount = distTarget.Component_No_10;
            var targetFemale10 = Math.Round(decimal.Multiply(30, comp10ListCount) / 100);
            IEnumerable<int> femaleRandomList10 = comp10List.Shuffle().Take((Int32)targetFemale10).Distinct();
            foreach (var number in femaleRandomList10)
            {
                var female_sel = new SelectedFemale();
                female_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                female_sel.ApplicationNumber = data.ApplicationNumber;
                female_sel.Component = 10;
                female_sel.DistCode = distCode;
                female_sel.TalukaCode = data.Tahashil.Value;
                female_sel.AadharNo = data.AdharCardNo;
                female_sel.Gender = data.Gender;
                female_sel.ApplicantCrippled = data.ApplicantCrippled;
                female_sel.PhNo = data.PhNo;
                female_sel.Name = data.ApName;
                female_sel.District_Mr = distName_Mr;
                female_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                female_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                female_sel.Type = "Selected";
                female_sel.CreatedOn = DateTime.Now;
                db.SelectedFemale.Add(female_sel);
                // db.SaveChanges();
            }
            db.SaveChanges();


            selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            // selected2 = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected2.AddRange(selectedFemale);
            selected2.AddRange(selectedHandicapped);
            var comp11List = list.Where(x => x.Dist == distCode && x.CompNumber.Trim().Contains("11") && x.ApplicantCrippled == "होय" && (!selected2.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            var comp11ListCount = distTarget.Component_No_11;
            //select 3% for handicapped
            var targetHandicapped11 = Math.Round(decimal.Multiply(3, comp11ListCount) / 100);
            IEnumerable<int> handicapRandomList11 = comp11List.Shuffle().Take((Int32)targetHandicapped11).Distinct();
            foreach (var number in handicapRandomList11)
            {
                var handicaps_sel = new SelectedHandicapped();
                handicaps_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                handicaps_sel.ApplicationNumber = data.ApplicationNumber;
                handicaps_sel.Component = 11;

                handicaps_sel.DistCode = distCode;
                handicaps_sel.TalukaCode = data.Tahashil.Value;
                handicaps_sel.AadharNo = data.AdharCardNo;
                handicaps_sel.Gender = data.Gender;
                handicaps_sel.ApplicantCrippled = data.ApplicantCrippled;
                handicaps_sel.PhNo = data.PhNo;
                handicaps_sel.District_Mr = distName_Mr;
                handicaps_sel.Name = data.ApName;
                handicaps_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                handicaps_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                handicaps_sel.Type = "Selected";
                handicaps_sel.CreatedOn = DateTime.Now;
                db.SelectedHandicapped.Add(handicaps_sel);
            }
            db.SaveChanges();
            //select 30% for females
            selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            // selected2 = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected2.AddRange(selectedFemale);
            selected2.AddRange(selectedHandicapped);
            comp11List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("11") && (x.Gender == "Female" || x.Gender == "स्त्री") && (!selected2.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            comp11ListCount = distTarget.Component_No_11;
            var targetFemale11 = Math.Round(decimal.Multiply(30, comp11ListCount) / 100);
            IEnumerable<int> femaleRandomList11 = comp11List.Shuffle().Take((Int32)targetFemale11).Distinct();
            foreach (var number in femaleRandomList11)
            {
                var female_sel = new SelectedFemale();
                female_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                female_sel.ApplicationNumber = data.ApplicationNumber;
                female_sel.Component = 11;
                female_sel.DistCode = distCode;
                female_sel.TalukaCode = data.Tahashil.Value;
                female_sel.AadharNo = data.AdharCardNo;
                female_sel.Gender = data.Gender;
                female_sel.ApplicantCrippled = data.ApplicantCrippled;
                female_sel.PhNo = data.PhNo;
                female_sel.Name = data.ApName;
                female_sel.District_Mr = distName_Mr;
                female_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                female_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                female_sel.Type = "Selected";
                female_sel.CreatedOn = DateTime.Now;
                db.SelectedFemale.Add(female_sel);
                // db.SaveChanges();
            }
            db.SaveChanges();

            selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            // selected2 = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected2.AddRange(selectedFemale);
            selected2.AddRange(selectedHandicapped);
            var comp12List = list.Where(x => x.Dist == distCode && x.CompNumber.Trim().Contains("12") && x.ApplicantCrippled == "होय" && (!selected2.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            var comp12ListCount = distTarget.Component_No_12;
            //select 3% for handicapped
            var targetHandicapped12 = Math.Round(decimal.Multiply(3, comp12ListCount) / 100);
            IEnumerable<int> handicapRandomList12 = comp12List.Shuffle().Take((Int32)targetHandicapped12).Distinct();
            foreach (var number in handicapRandomList12)
            {
                var handicaps_sel = new SelectedHandicapped();
                handicaps_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                handicaps_sel.ApplicationNumber = data.ApplicationNumber;
                handicaps_sel.Component = 12;
                handicaps_sel.DistCode = distCode;
                handicaps_sel.TalukaCode = data.Tahashil.Value;
                handicaps_sel.AadharNo = data.AdharCardNo;
                handicaps_sel.Gender = data.Gender;
                handicaps_sel.ApplicantCrippled = data.ApplicantCrippled;
                handicaps_sel.PhNo = data.PhNo;
                handicaps_sel.District_Mr = distName_Mr;
                handicaps_sel.Name = data.ApName;
                handicaps_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                handicaps_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                handicaps_sel.Type = "Selected";
                handicaps_sel.CreatedOn = DateTime.Now;
                db.SelectedHandicapped.Add(handicaps_sel);
            }
            db.SaveChanges();
            //select 30% for females
            selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            // selected2 = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected2.AddRange(selectedFemale);
            selected2.AddRange(selectedHandicapped);
            comp12List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("12") && (x.Gender == "Female" || x.Gender == "स्त्री") && (!selected2.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            comp12ListCount = distTarget.Component_No_12;
            var targetFemale12 = Math.Round(decimal.Multiply(30, comp12ListCount) / 100);
            IEnumerable<int> femaleRandomList12 = comp12List.Shuffle().Take((Int32)targetFemale12).Distinct();
            foreach (var number in femaleRandomList12)
            {
                var female_sel = new SelectedFemale();
                female_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                female_sel.ApplicationNumber = data.ApplicationNumber;
                female_sel.Component = 12;
                female_sel.DistCode = distCode;
                female_sel.TalukaCode = data.Tahashil.Value;
                female_sel.AadharNo = data.AdharCardNo;
                female_sel.Gender = data.Gender;
                female_sel.ApplicantCrippled = data.ApplicantCrippled;
                female_sel.PhNo = data.PhNo;
                female_sel.Name = data.ApName;
                female_sel.District_Mr = distName_Mr;
                female_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                female_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                female_sel.Type = "Selected";
                female_sel.CreatedOn = DateTime.Now;
                db.SelectedFemale.Add(female_sel);
                // db.SaveChanges();
            }
            db.SaveChanges();

            selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            // selected2 = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected2.AddRange(selectedFemale);
            selected2.AddRange(selectedHandicapped);

            var comp13List = list.Where(x => x.Dist == distCode && x.CompNumber.Trim().Contains("13") && x.ApplicantCrippled == "होय" && (!selected2.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            var comp13ListCount = distTarget.Component_No_13;
            //select 3% for handicapped
            var targetHandicapped13 = Math.Round(decimal.Multiply(3, comp13ListCount) / 100);
            IEnumerable<int> handicapRandomList13 = comp13List.Shuffle().Take((Int32)targetHandicapped13).Distinct();
            foreach (var number in handicapRandomList13)
            {
                var handicaps_sel = new SelectedHandicapped();
                handicaps_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                handicaps_sel.ApplicationNumber = data.ApplicationNumber;
                handicaps_sel.Component = 13;
                handicaps_sel.DistCode = distCode;
                handicaps_sel.TalukaCode = data.Tahashil.Value;
                handicaps_sel.AadharNo = data.AdharCardNo;
                handicaps_sel.Gender = data.Gender;
                handicaps_sel.ApplicantCrippled = data.ApplicantCrippled;
                handicaps_sel.PhNo = data.PhNo;
                handicaps_sel.District_Mr = distName_Mr;
                handicaps_sel.Name = data.ApName;
                handicaps_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                handicaps_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                handicaps_sel.Type = "Selected";
                handicaps_sel.CreatedOn = DateTime.Now;
                db.SelectedHandicapped.Add(handicaps_sel);
            }
            //select 30% for females

            selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            // selected2 = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected2.AddRange(selectedFemale);
            selected2.AddRange(selectedHandicapped);
            comp13List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("13") && (x.Gender == "Female" || x.Gender == "स्त्री") && (!selected2.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            comp13ListCount = distTarget.Component_No_13;
            var targetFemale13 = Math.Round(decimal.Multiply(30, comp13ListCount) / 100);
            IEnumerable<int> femaleRandomList13 = comp13List.Shuffle().Take((Int32)targetFemale13).Distinct();
            foreach (var number in femaleRandomList13.Distinct())
            {
                var female_sel = new SelectedFemale();
                female_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                female_sel.ApplicationNumber = data.ApplicationNumber;
                female_sel.Component = 13;
                female_sel.DistCode = distCode;
                female_sel.TalukaCode = data.Tahashil.Value;
                female_sel.AadharNo = data.AdharCardNo;
                female_sel.Gender = data.Gender;
                female_sel.ApplicantCrippled = data.ApplicantCrippled;
                female_sel.PhNo = data.PhNo;
                female_sel.Name = data.ApName;
                female_sel.District_Mr = distName_Mr;
                female_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                female_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                female_sel.Type = "Selected";
                female_sel.CreatedOn = DateTime.Now;
                db.SelectedFemale.Add(female_sel);
                // db.SaveChanges();
            }
            db.SaveChanges();


            //foreach (var item in taluka)
            //{
            //    //comp 1                           
            //    //general
            //    selected = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            //    comp1List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("1") && x.Tahashil == item.Tal_Code && (!selected.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            //    comp1ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item.Tal_Mr).Select(x => x.Component_No_1).FirstOrDefault();
            //    var targetTaluka = Math.Round(decimal.Multiply(67, comp1ListCount) / 100);
            //    IEnumerable<int> talukaRandomList = comp1List.Shuffle().Take((Int32)targetTaluka);
            //    foreach (var number in talukaRandomList)
            //    {
            //        var gen_sel = new SelectedGeneral();
            //        gen_sel.ApplicationId = number;
            //        var data = list.Where(x => x.Id == number).FirstOrDefault();
            //        gen_sel.ApplicationNumber = data.ApplicationNumber;
            //        gen_sel.Component = 1;
            //        gen_sel.DistCode = distCode;
            //        gen_sel.AadharNo = data.AdharCardNo;
            //        gen_sel.Gender = data.Gender;
            //        gen_sel.ApplicantCrippled = data.ApplicantCrippled;
            //        gen_sel.PhNo = data.PhNo;
            //        gen_sel.District_Mr = distName_Mr;
            //        gen_sel.Taluka_Mr = item.Tal_Mr;
            //        gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
            //        gen_sel.TalukaCode = data.Tahashil.Value;
            //        gen_sel.Name = data.ApName;
            //        gen_sel.Type = "Selected";
            //        gen_sel.CreatedOn = DateTime.Now;
            //        db.SelectedGeneral.Add(gen_sel);

            //    }
            //    db.SaveChanges();             

            //    selected = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            //    comp2List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("2") && x.Tahashil == item.Tal_Code && (!selected.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
            //    comp2ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item.Tal_Mr).Select(x => x.Component_No_1).FirstOrDefault();
            //    var targetTaluka2 = Math.Round(decimal.Multiply(67, comp2ListCount) / 100);
            //    IEnumerable<int> talukaRandomList2 = comp2List.Shuffle().Take((Int32)targetTaluka2);
            //    foreach (var number in talukaRandomList2)
            //    {
            //        var gen_sel = new SelectedGeneral();
            //        gen_sel.ApplicationId = number;
            //        var data = list.Where(x => x.Id == number).FirstOrDefault();
            //        gen_sel.ApplicationNumber = data.ApplicationNumber;
            //        gen_sel.Component = 2;
            //        gen_sel.DistCode = distCode;
            //        gen_sel.AadharNo = data.AdharCardNo;
            //        gen_sel.Gender = data.Gender;
            //        gen_sel.ApplicantCrippled = data.ApplicantCrippled;
            //        gen_sel.PhNo = data.PhNo;
            //        gen_sel.District_Mr = distName_Mr;
            //        gen_sel.Taluka_Mr = item.Tal_Mr;
            //        gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
            //        gen_sel.TalukaCode = data.Tahashil.Value;
            //        gen_sel.Name = data.ApName;
            //        gen_sel.Type = "Selected";
            //        gen_sel.CreatedOn = DateTime.Now;
            //        db.SelectedGeneral.Add(gen_sel);

            //    }

            //    db.SaveChanges();


            //    return Json(ofcrModel, JsonRequestBehavior.AllowGet);
            //}
            model.SelectedHandicappedList = new List<SelectedHandicapped>();
            model.SelectedHandicappedList = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").ToList();

            model.SelectedFemaleList = new List<SelectedFemale>();
            model.SelectedFemaleList = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").ToList();

            model.SelectedGeneralList = new List<SelectedGeneral>();
            model.SelectedGeneralList = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").ToList();
            ofcrModel.SelectedList1 = model;
            return Json(ofcrModel, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SelectList2(int distCode)
        {

            var ofcrModel = new OfficerLogin();
            var model = new SelectedListViewModel();
            ofcrModel.SelectedList1 = new SelectedListViewModel();

            var distMaster = db.DistMaster.ToList();
            var selectedTaluka = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.TalukaCode).ToList();
            var taluka = db.TalMaster.Where(x => x.Dist_Code == distCode).ToList();
            var taluka2 = taluka.Where(x => x.Dist_Code == distCode && (!selectedTaluka.Contains(x.Tal_Code))).ToList();
            var distName_Mr = distMaster.Where(x => x.Dist_Code == distCode).Select(x => x.District_Mr).FirstOrDefault();
            var distTarget = db.DistrictTarget.Where(x => x.Name_of_District.Trim() == distName_Mr.Trim()).FirstOrDefault();
            var talukaTarg = db.TalukaTarget.Where(x => x.Name_of_District.Trim() == distName_Mr.Trim()).ToList();
            var selected = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            var selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            var selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
            selected.AddRange(selectedFemale);
            selected.AddRange(selectedHandicapped);
            var list = db.ApplicantRegistrations.Where(x => x.FormSubmitted == true && x.Dist == distCode && (!selected.Contains(x.ApplicationNumber))).ToList();

            foreach (var item in taluka2)
            {

                //comp 1                           
                //general  
                selected = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
                selected.AddRange(selectedFemale);
                selected.AddRange(selectedHandicapped);

                var comp1List = list.Where(x => x.Dist == distCode && (x.CompNumber.Contains("1") && (!(x.CompNumber.Contains("14") || x.CompNumber.Contains("15")
                 || x.CompNumber.Contains("10") || x.CompNumber.Contains("11") || x.CompNumber.Contains("12") || x.CompNumber.Contains("13"))))
                && x.Tahashil == item.Tal_Code && (!selected.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
                var comp1ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item.Tal_Mr).Select(x => x.Component_No_1).FirstOrDefault();
                var targetTaluka = Math.Round(decimal.Multiply(67, comp1ListCount) / 100);
                IEnumerable<int> talukaRandomList = comp1List.Shuffle().Take(Convert.ToInt32(targetTaluka)).Distinct();
                foreach (var number in talukaRandomList)
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;
                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Component = 1;
                    gen_sel.DistCode = distCode;
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.Taluka_Mr = item.Tal_Mr;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    gen_sel.Name = data.ApName;
                    gen_sel.Type = "Selected";
                    gen_sel.CreatedOn = DateTime.Now;
                    db.SelectedGeneral.Add(gen_sel);

                }
                if (!(db.SelectedGeneral.Any(x => x.TalukaCode == item.Tal_Code && x.Type == "Selected" && x.Component == 1)))
                {
                    db.SaveChanges();
                }

                selected = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
                selected.AddRange(selectedFemale);
                selected.AddRange(selectedHandicapped);
                var comp2List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("2") && x.Tahashil == item.Tal_Code && (!selected.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
                var comp2ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item.Tal_Mr).Select(x => x.Component_No_2).FirstOrDefault();
                var targetTaluka2 = Math.Round(decimal.Multiply(67, comp2ListCount) / 100);
                IEnumerable<int> talukaRandomList2 = comp2List.Shuffle().Take((Int32)targetTaluka2).Distinct();
                foreach (var number in talukaRandomList2)
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;
                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Component = 2;
                    gen_sel.DistCode = distCode; gen_sel.Type = "Selected";
                    gen_sel.Name = data.ApName;
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.Taluka_Mr = item.Tal_Mr;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    gen_sel.CreatedOn = DateTime.Now;
                    db.SelectedGeneral.Add(gen_sel);

                }
                if (!(db.SelectedGeneral.Any(x => x.TalukaCode == item.Tal_Code && x.Type == "Selected" && x.Component == 2)))
                {
                    db.SaveChanges();
                }


                selected = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
                selected.AddRange(selectedFemale);
                selected.AddRange(selectedHandicapped);
                var comp3List = list.Where(x => x.Dist == distCode && ((x.CompNumber.Contains("3") || x.CompNumber.Contains("4") || x.CompNumber.Contains("5") || x.CompNumber.Contains("6") ||
           x.CompNumber.Contains("7")) && (!(x.CompNumber.Contains("13") || x.CompNumber.Contains("14") || x.CompNumber.Contains("15")))) && x.Tahashil == item.Tal_Code && (!selected.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
                var comp3ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item.Tal_Mr).Select(x => x.Component_No_3_7).FirstOrDefault();
                var targetTaluka3 = Math.Round(decimal.Multiply(67, comp3ListCount) / 100);
                IEnumerable<int> talukaRandomList3 = comp3List.Shuffle().Take((Int32)targetTaluka3).Distinct();
                foreach (var number in talukaRandomList3)
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;

                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Name = data.ApName;
                    gen_sel.Component = 3;
                    gen_sel.DistCode = distCode; gen_sel.Type = "Selected";
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.Taluka_Mr = item.Tal_Mr;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    gen_sel.CreatedOn = DateTime.Now;
                    db.SelectedGeneral.Add(gen_sel);

                }
                if (!(db.SelectedGeneral.Any(x => x.TalukaCode == item.Tal_Code && x.Type == "Selected" && x.Component == 3)))
                {
                    db.SaveChanges();
                }


                selected = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
                selected.AddRange(selectedFemale);
                selected.AddRange(selectedHandicapped);
                var comp8List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("8") && x.Tahashil == item.Tal_Code && (!selected.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
                var comp8ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item.Tal_Mr).Select(x => x.Component_No_8).FirstOrDefault();
                var targetTaluka8 = Math.Round(decimal.Multiply(67, comp8ListCount) / 100);
                IEnumerable<int> talukaRandomList8 = comp8List.Shuffle().Take((Int32)targetTaluka8).Distinct();
                foreach (var number in talukaRandomList8)
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;
                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Component = 8;
                    gen_sel.Name = data.ApName;
                    gen_sel.DistCode = distCode; gen_sel.Type = "Selected";
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.Taluka_Mr = item.Tal_Mr;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    gen_sel.CreatedOn = DateTime.Now;
                    db.SelectedGeneral.Add(gen_sel);

                }
                if (!(db.SelectedGeneral.Any(x => x.TalukaCode == item.Tal_Code && x.Type == "Selected" && x.Component == 8)))
                {
                    db.SaveChanges();
                }

                selected = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
                selected.AddRange(selectedFemale);
                selected.AddRange(selectedHandicapped);
                var comp9List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("9") && x.Tahashil == item.Tal_Code && (!selected.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
                var comp9ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item.Tal_Mr).Select(x => x.Component_No_9).FirstOrDefault();
                var targetTaluka9 = Math.Round(decimal.Multiply(67, comp9ListCount) / 100);
                IEnumerable<int> talukaRandomList9 = comp9List.Shuffle().Take((Int32)targetTaluka9).Distinct();
                foreach (var number in talukaRandomList9)
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;
                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Component = 9;
                    gen_sel.DistCode = distCode;
                    gen_sel.Type = "Selected";
                    gen_sel.Name = data.ApName;
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.Taluka_Mr = item.Tal_Mr;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    gen_sel.CreatedOn = DateTime.Now;
                    db.SelectedGeneral.Add(gen_sel);

                }
                if (!(db.SelectedGeneral.Any(x => x.TalukaCode == item.Tal_Code && x.Type == "Selected" && x.Component == 9)))
                {
                    db.SaveChanges();
                }


                selected = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
                selected.AddRange(selectedFemale);
                selected.AddRange(selectedHandicapped);
                var comp10List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("10") && x.Tahashil == item.Tal_Code && (!selected.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
                var comp10ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item.Tal_Mr).Select(x => x.Component_No_10).FirstOrDefault();
                var targetTaluka10 = Math.Round(decimal.Multiply(67, comp10ListCount) / 100);
                IEnumerable<int> talukaRandomList10 = comp10List.Shuffle().Take((Int32)targetTaluka10).Distinct();
                foreach (var number in talukaRandomList10)
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;
                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Component = 10;
                    gen_sel.Name = data.ApName;
                    gen_sel.DistCode = distCode; gen_sel.Type = "Selected";
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.CreatedOn = DateTime.Now;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.Taluka_Mr = item.Tal_Mr;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    db.SelectedGeneral.Add(gen_sel);

                }
                if (!(db.SelectedGeneral.Any(x => x.TalukaCode == item.Tal_Code && x.Type == "Selected" && x.Component == 10)))
                {
                    db.SaveChanges();
                }

                selected = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
                selected.AddRange(selectedFemale);
                selected.AddRange(selectedHandicapped);
                var comp11List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("11") && x.Tahashil == item.Tal_Code && (!selected.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
                var comp11ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item.Tal_Mr).Select(x => x.Component_No_11).FirstOrDefault();
                var targetTaluka11 = Math.Round(decimal.Multiply(67, comp11ListCount) / 100);
                IEnumerable<int> talukaRandomList11 = comp11List.Shuffle().Take((Int32)targetTaluka11).Distinct();
                foreach (var number in talukaRandomList11)
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;
                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Component = 11;
                    gen_sel.Name = data.ApName;
                    gen_sel.DistCode = distCode; gen_sel.Type = "Selected";
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.CreatedOn = DateTime.Now;
                    gen_sel.Taluka_Mr = item.Tal_Mr;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    db.SelectedGeneral.Add(gen_sel);

                }
                if (!(db.SelectedGeneral.Any(x => x.TalukaCode == item.Tal_Code && x.Type == "Selected" && x.Component == 11)))
                {
                    db.SaveChanges();
                }

                selected = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
                selected.AddRange(selectedFemale);
                selected.AddRange(selectedHandicapped);
                var comp12List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("12") && x.Tahashil == item.Tal_Code && (!selected.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
                var comp12ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item.Tal_Mr).Select(x => x.Component_No_12).FirstOrDefault();
                var targetTaluka12 = Math.Round(decimal.Multiply(67, comp12ListCount) / 100);
                IEnumerable<int> talukaRandomList12 = comp12List.Shuffle().Take((Int32)targetTaluka12).Distinct();
                foreach (var number in talukaRandomList12)
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;
                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Component = 12;
                    gen_sel.DistCode = distCode; gen_sel.Type = "Selected";
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.Name = data.ApName;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.CreatedOn = DateTime.Now;
                    gen_sel.Taluka_Mr = item.Tal_Mr;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    db.SelectedGeneral.Add(gen_sel);

                }
                if (!(db.SelectedGeneral.Any(x => x.TalukaCode == item.Tal_Code && x.Type == "Selected" && x.Component == 12)))
                {
                    db.SaveChanges();
                }

                selected = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Select(x => x.ApplicationNumber).ToList();
                selected.AddRange(selectedFemale);
                selected.AddRange(selectedHandicapped);
                var comp13List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("13") && x.Tahashil == item.Tal_Code && (!selected.Contains(x.ApplicationNumber))).Select(x => x.Id).ToList();
                var comp13ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item.Tal_Mr).Select(x => x.Component_No_13).FirstOrDefault();
                var targetTaluka13 = Math.Round(decimal.Multiply(67, comp13ListCount) / 100);
                IEnumerable<int> talukaRandomList13 = comp13List.Shuffle().Take((Int32)targetTaluka13).Distinct();
                foreach (var number in talukaRandomList13)
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;
                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Component = 13;
                    gen_sel.DistCode = distCode; gen_sel.Type = "Selected";
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.Name = data.ApName;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.Taluka_Mr = item.Tal_Mr;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    gen_sel.CreatedOn = DateTime.Now;
                    db.SelectedGeneral.Add(gen_sel);

                }


                if (!(db.SelectedGeneral.Any(x => x.TalukaCode == item.Tal_Code && x.Type == "Selected" && x.Component == 13)))
                {
                    db.SaveChanges();
                }
                //model.SelectedHandicappedList = new List<SelectedHandicapped>();
                //model.SelectedHandicappedList = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Distinct().ToList();
                //ofcrModel.SelectedList1.SelectedHandicappedList = new List<SelectedHandicapped>();
                //model.SelectedFemaleList = new List<SelectedFemale>();
                //model.SelectedFemaleList = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Distinct().ToList();
                //ofcrModel.SelectedList1.SelectedFemaleList = new List<SelectedFemale>();
                //model.SelectedGeneralList = new List<SelectedGeneral>();
                //ofcrModel.SelectedList1.SelectedGeneralList = new List<SelectedGeneral>();
                //model.SelectedGeneralList = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Distinct().ToList();
                //ofcrModel.SelectedList1.SelectedGeneralList = model.SelectedGeneralList;
                //ofcrModel.SelectedList1.SelectedFemaleList = model.SelectedFemaleList;
                //ofcrModel.SelectedList1.SelectedHandicappedList = model.SelectedHandicappedList;
                ////ofcrModel.TCount = taluka2.Count;
                //return Json(ofcrModel, JsonRequestBehavior.AllowGet);
            }
            model.SelectedHandicappedList = new List<SelectedHandicapped>();
            model.SelectedHandicappedList = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").Distinct().ToList();
            ofcrModel.SelectedList1.SelectedHandicappedList = new List<SelectedHandicapped>();
            model.SelectedFemaleList = new List<SelectedFemale>();
            model.SelectedFemaleList = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").Distinct().ToList();
            ofcrModel.SelectedList1.SelectedFemaleList = new List<SelectedFemale>();
            model.SelectedGeneralList = new List<SelectedGeneral>();
            ofcrModel.SelectedList1.SelectedGeneralList = new List<SelectedGeneral>();
            model.SelectedGeneralList = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Distinct().ToList();
            ofcrModel.SelectedList1.SelectedGeneralList = model.SelectedGeneralList;
            ofcrModel.SelectedList1.SelectedFemaleList = model.SelectedFemaleList;
            ofcrModel.SelectedList1.SelectedHandicappedList = model.SelectedHandicappedList;
            //ofcrModel.TCount = taluka2.Count;
            return Json(ofcrModel, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SelectRandomList(int distCode)
        {
            db.Database.ExecuteSqlCommand("delete from [SelectedFemales] where DistCode =" + distCode);
            db.Database.ExecuteSqlCommand("delete from [SelectedHandicappeds] where DistCode =" + distCode);
            db.Database.ExecuteSqlCommand("delete from [SelectedGenerals] where Type = 'Selected' and DistCode =" + distCode);
            var ofcrModel = new OfficerLogin();
            var model = new SelectedListViewModel();
            var distMaster = db.DistMaster.ToList();
            var distName_Mr = distMaster.Where(x => x.Dist_Code == distCode).Select(x => x.District_Mr).FirstOrDefault();
            var distTarget = db.DistrictTarget.Where(x => x.Name_of_District.Trim() == distName_Mr.Trim()).FirstOrDefault();
            var talukaTarg = db.TalukaTarget.Where(x => x.Name_of_District.Trim() == distName_Mr.Trim()).ToList();
            var list = db.ApplicantRegistrations.Where(x => x.FormSubmitted == true && x.Dist == distCode).ToList();
            var taluka = db.TalMaster.ToList();

            //comp 1
            var comp1List = list.Where(x => x.CompNumber.Trim().Contains("1") && x.ApplicantCrippled == "होय").Select(x => x.Id).ToList();
            var comp1ListCount = distTarget.Component_No_1;
            //select 3% for handicapped
            var targetHandicapped = Math.Round(decimal.Multiply(3, comp1ListCount) / 100);
            IEnumerable<int> handicapRandomList = comp1List.Shuffle().Take((Int32)targetHandicapped);
            // var villMaster = db.VillageMaster.ToList();
            foreach (var number in handicapRandomList)
            {
                var handicaps_sel = new SelectedHandicapped();
                handicaps_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                handicaps_sel.ApplicationNumber = data.ApplicationNumber;
                handicaps_sel.Component = 1;
                handicaps_sel.DistCode = distCode;
                handicaps_sel.TalukaCode = data.Tahashil.Value;
                handicaps_sel.AadharNo = data.AdharCardNo;
                handicaps_sel.Gender = data.Gender;
                handicaps_sel.ApplicantCrippled = data.ApplicantCrippled;
                handicaps_sel.PhNo = data.PhNo;
                handicaps_sel.District_Mr = distName_Mr;
                handicaps_sel.Name = data.ApName;
                handicaps_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                handicaps_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                handicaps_sel.Type = "Selected";
                handicaps_sel.CreatedOn = DateTime.Now;
                db.SelectedHandicapped.Add(handicaps_sel);

            }
            db.SaveChanges();
            //select 30% for females
            comp1List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("1") && (x.Gender == "Female" || x.Gender == "स्त्री")).Select(x => x.Id).ToList();
            comp1ListCount = distTarget.Component_No_1;
            var targetFemale = Math.Round(decimal.Multiply(30, comp1ListCount) / 100);
            IEnumerable<int> femaleRandomList = comp1List.Shuffle().Take((Int32)targetFemale);
            foreach (var number in femaleRandomList)
            {
                var female_sel = new SelectedFemale();
                female_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                female_sel.ApplicationNumber = data.ApplicationNumber;
                female_sel.Component = 1;
                female_sel.DistCode = distCode;
                female_sel.TalukaCode = data.Tahashil.Value;
                female_sel.AadharNo = data.AdharCardNo;
                female_sel.Gender = data.Gender;
                female_sel.ApplicantCrippled = data.ApplicantCrippled;
                female_sel.PhNo = data.PhNo;
                female_sel.Name = data.ApName;
                female_sel.District_Mr = distName_Mr;
                female_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                female_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                female_sel.Type = "Selected";
                female_sel.CreatedOn = DateTime.Now;
                db.SelectedFemale.Add(female_sel);
                // db.SaveChanges();
            }
            db.SaveChanges();
            //taluka wise 67% 
            foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
            {
                comp1List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("1") && x.Tahashil == item2.Tal_Code).Select(x => x.Id).ToList();
                comp1ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_1).FirstOrDefault();
                var targetTaluka = Math.Round(decimal.Multiply(67, comp1ListCount) / 100);
                IEnumerable<int> talukaRandomList = comp1List.Shuffle().Take((Int32)targetTaluka);
                foreach (var number in talukaRandomList)
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;
                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Component = 1;
                    gen_sel.DistCode = distCode;
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.Taluka_Mr = item2.Tal_Mr;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    gen_sel.Name = data.ApName;
                    gen_sel.Type = "Selected";
                    gen_sel.CreatedOn = DateTime.Now;
                    db.SelectedGeneral.Add(gen_sel);

                }
                db.SaveChanges();
                ////Waiting List 1
                //comp1List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("1")).Select(x => x.Id).ToList();
                //comp1ListCount = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("1")).Count();
                //var waitingTarget = 5 * (targetFemale + targetHandicapped + targetTaluka);
                //IEnumerable<int> waitingRandomList = comp1List.Shuffle().Take((Int32)comp1ListCount);
                //foreach (var number in waitingRandomList)
                //{
                //    var gen_sel = new SelectedGeneral();
                //    gen_sel.ApplicationId = number;
                //    var data = list.Where(x => x.Id == number).FirstOrDefault();
                //    gen_sel.ApplicationNumber = data.ApplicationNumber;
                //    gen_sel.Component = 1;
                //    gen_sel.DistCode = distCode;
                //    gen_sel.Type = "Waiting";
                //    gen_sel.AadharNo = data.AdharCardNo;
                //    gen_sel.Gender = data.Gender;
                //    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                //    gen_sel.PhNo = data.PhNo;
                //    gen_sel.District_Mr = distName_Mr;
                //    gen_sel.Taluka_Mr = item2.Tal_Mr;
                //    gen_sel.Name = data.ApName;
                //    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                //    gen_sel.TalukaCode = data.Tahashil.Value;
                //    gen_sel.CreatedOn = DateTime.Now;
                //    db.SelectedGeneral.Add(gen_sel);

                //}
            }


            //comp2
            var comp2List = list.Where(x => x.Dist == distCode && x.CompNumber.Trim().Contains("2") && x.ApplicantCrippled == "होय").Select(x => x.Id).ToList();
            var comp2ListCount = distTarget.Component_No_2;
            //select 3% for handicapped
            var targetHandicapped2 = Math.Round(decimal.Multiply(3, comp2ListCount) / 100);
            IEnumerable<int> handicapRandomList2 = comp2List.Shuffle().Take((Int32)targetHandicapped2);
            foreach (var number in handicapRandomList2)
            {
                var handicaps_sel = new SelectedHandicapped();
                handicaps_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                handicaps_sel.ApplicationNumber = data.ApplicationNumber;
                handicaps_sel.Component = 2;
                handicaps_sel.DistCode = distCode;
                handicaps_sel.TalukaCode = data.Tahashil.Value;
                handicaps_sel.AadharNo = data.AdharCardNo;
                handicaps_sel.Gender = data.Gender;
                handicaps_sel.ApplicantCrippled = data.ApplicantCrippled;
                handicaps_sel.PhNo = data.PhNo;
                handicaps_sel.District_Mr = distName_Mr;
                handicaps_sel.Name = data.ApName;
                handicaps_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                handicaps_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                handicaps_sel.Type = "Selected";
                handicaps_sel.CreatedOn = DateTime.Now;
                db.SelectedHandicapped.Add(handicaps_sel);
                // db.SaveChanges();
            }
            db.SaveChanges();
            //select 30% for females
            comp2List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("2") && (x.Gender == "Female" || x.Gender == "स्त्री")).Select(x => x.Id).ToList();
            comp2ListCount = distTarget.Component_No_2;
            var targetFemale2 = Math.Round(decimal.Multiply(30, comp2ListCount) / 100);
            IEnumerable<int> femaleRandomList2 = comp2List.Shuffle().Take((Int32)targetFemale2);
            foreach (var number in femaleRandomList)
            {
                var female_sel = new SelectedFemale();
                female_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                female_sel.ApplicationNumber = data.ApplicationNumber;
                female_sel.Component = 2;
                female_sel.DistCode = distCode;
                female_sel.TalukaCode = data.Tahashil.Value;
                female_sel.AadharNo = data.AdharCardNo;
                female_sel.Gender = data.Gender;
                female_sel.ApplicantCrippled = data.ApplicantCrippled;
                female_sel.PhNo = data.PhNo;
                female_sel.Name = data.ApName;
                female_sel.District_Mr = distName_Mr;
                female_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                female_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                female_sel.Type = "Selected";
                female_sel.CreatedOn = DateTime.Now;
                db.SelectedFemale.Add(female_sel);
                // db.SaveChanges();
            }
            db.SaveChanges();
            //taluka wise 67% 
            foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
            {
                comp2List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("2") && x.Tahashil == item2.Tal_Code).Select(x => x.Id).ToList();
                comp2ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_2).FirstOrDefault();
                var targetTaluka2 = Math.Round(decimal.Multiply(67, comp2ListCount) / 100);
                IEnumerable<int> talukaRandomList2 = comp2List.Shuffle().Take((Int32)targetTaluka2);
                foreach (var number in talukaRandomList2)
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;
                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Component = 2;
                    gen_sel.DistCode = distCode; gen_sel.Type = "Selected";
                    gen_sel.Name = data.ApName;
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.Taluka_Mr = item2.Tal_Mr;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    gen_sel.CreatedOn = DateTime.Now;
                    db.SelectedGeneral.Add(gen_sel);

                }
                db.SaveChanges();
                ////Waiting List 2
                //comp2List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("2")).Select(x => x.Id).ToList();
                //comp2ListCount = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("2")).Count();
                //var waitingTarget = 5 * (targetFemale2 + targetHandicapped2 + targetTaluka2);
                //IEnumerable<int> waitingRandomList = comp2List.Shuffle().Take((Int32)comp2ListCount);
                //foreach (var number in waitingRandomList)
                //{
                //    var gen_sel = new SelectedGeneral();
                //    gen_sel.ApplicationId = number;
                //    var data = list.Where(x => x.Id == number).FirstOrDefault();
                //    gen_sel.ApplicationNumber = data.ApplicationNumber;
                //    gen_sel.Component = 2;
                //    gen_sel.DistCode = distCode; gen_sel.Type = "Waiting";
                //    gen_sel.AadharNo = data.AdharCardNo;
                //    gen_sel.Gender = data.Gender;
                //    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                //    gen_sel.PhNo = data.PhNo;
                //    gen_sel.Name = data.ApName;
                //    gen_sel.District_Mr = distName_Mr;
                //    gen_sel.Taluka_Mr = item2.Tal_Mr;
                //    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                //    gen_sel.TalukaCode = data.Tahashil.Value;
                //    gen_sel.CreatedOn = DateTime.Now;
                //    db.SelectedGeneral.Add(gen_sel);

                //}
            }

            //comp 3 -7
            var comp3List = list.Where(x => x.Dist == distCode && (x.CompNumber.Trim().Contains("3") || x.CompNumber.Trim().Contains("4") || x.CompNumber.Trim().Contains("5") || x.CompNumber.Trim().Contains("6") ||
            x.CompNumber.Trim().Contains("7")) && x.ApplicantCrippled == "होय").Select(x => x.Id).ToList();
            var comp3ListCount = distTarget.Component_No_3_7;
            //select 3% for handicapped
            var targetHandicapped3 = Math.Round(decimal.Multiply(3, comp3ListCount) / 100);
            IEnumerable<int> handicapRandomList3 = comp3List.Shuffle().Take((Int32)targetHandicapped3);
            foreach (var number in handicapRandomList3)
            {
                var handicaps_sel = new SelectedHandicapped();
                handicaps_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                handicaps_sel.ApplicationNumber = data.ApplicationNumber;
                handicaps_sel.Component = 3;
                handicaps_sel.DistCode = distCode;
                handicaps_sel.TalukaCode = data.Tahashil.Value;
                handicaps_sel.AadharNo = data.AdharCardNo;
                handicaps_sel.Gender = data.Gender;
                handicaps_sel.ApplicantCrippled = data.ApplicantCrippled;
                handicaps_sel.PhNo = data.PhNo;
                handicaps_sel.District_Mr = distName_Mr;
                handicaps_sel.Name = data.ApName;
                handicaps_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                handicaps_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                handicaps_sel.Type = "Selected";
                handicaps_sel.CreatedOn = DateTime.Now;
                db.SelectedHandicapped.Add(handicaps_sel);
            }
            db.SaveChanges();
            //select 30% for females
            comp3List = list.Where(x => x.Dist == distCode && (x.CompNumber.Trim().Contains("3") || x.CompNumber.Trim().Contains("4") || x.CompNumber.Trim().Contains("5") || x.CompNumber.Trim().Contains("6") ||
            x.CompNumber.Trim().Contains("7")) && (x.Gender == "Female" || x.Gender == "स्त्री")).Select(x => x.Id).ToList();
            comp3ListCount = distTarget.Component_No_3_7;
            var targetFemale3 = Math.Round(decimal.Multiply(30, comp3ListCount) / 100);
            IEnumerable<int> femaleRandomList3 = comp3List.Shuffle().Take((Int32)targetFemale3);
            foreach (var number in femaleRandomList3)
            {
                var female_sel = new SelectedFemale();
                female_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                female_sel.ApplicationNumber = data.ApplicationNumber;
                female_sel.Component = 3;
                female_sel.DistCode = distCode;
                female_sel.TalukaCode = data.Tahashil.Value;
                female_sel.AadharNo = data.AdharCardNo;
                female_sel.Gender = data.Gender;
                female_sel.ApplicantCrippled = data.ApplicantCrippled;
                female_sel.PhNo = data.PhNo;
                female_sel.Name = data.ApName;
                female_sel.District_Mr = distName_Mr;
                female_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                female_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                female_sel.Type = "Selected";
                female_sel.CreatedOn = DateTime.Now;
                db.SelectedFemale.Add(female_sel);
                // db.SaveChanges();
            }
            db.SaveChanges();
            //taluka wise 67% 
            foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
            {
                comp3List = list.Where(x => x.Dist == distCode && (x.CompNumber.Trim().Contains("3") || x.CompNumber.Trim().Contains("4") || x.CompNumber.Trim().Contains("5") || x.CompNumber.Trim().Contains("6") ||
            x.CompNumber.Trim().Contains("7")) && x.Tahashil == item2.Tal_Code).Select(x => x.Id).ToList();
                comp3ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_3_7).FirstOrDefault();
                var targetTaluka3 = Math.Round(decimal.Multiply(67, comp3ListCount) / 100);
                IEnumerable<int> talukaRandomList3 = comp3List.Shuffle().Take((Int32)targetTaluka3);
                foreach (var number in talukaRandomList3)
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;

                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Name = data.ApName;
                    gen_sel.Component = 3;
                    gen_sel.DistCode = distCode; gen_sel.Type = "Selected";
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.Taluka_Mr = item2.Tal_Mr;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    gen_sel.CreatedOn = DateTime.Now;
                    db.SelectedGeneral.Add(gen_sel);

                }
                db.SaveChanges();
                //    //Waiting List 3
                //    comp3List = list.Where(x => x.Dist == distCode && (x.CompNumber.Trim().Contains("3") || x.CompNumber.Trim().Contains("4") || x.CompNumber.Trim().Contains("5") || x.CompNumber.Trim().Contains("6") ||
                //x.CompNumber.Trim().Contains("7"))).Select(x => x.Id).ToList();
                //    comp3ListCount = list.Where(x => x.Dist == distCode && (x.CompNumber.Trim().Contains("3") || x.CompNumber.Trim().Contains("4") || x.CompNumber.Trim().Contains("5") || x.CompNumber.Trim().Contains("6") ||
                //x.CompNumber.Trim().Contains("7"))).Count();
                //    var waitingTarget = 5 * (targetFemale3 + targetHandicapped3 + targetTaluka3);
                //    IEnumerable<int> waitingRandomList = comp3List.Shuffle().Take((Int32)comp3ListCount);
                //    foreach (var number in waitingRandomList)
                //    {
                //        var gen_sel = new SelectedGeneral();
                //        gen_sel.ApplicationId = number;
                //        var data = list.Where(x => x.Id == number).FirstOrDefault();
                //        gen_sel.ApplicationNumber = data.ApplicationNumber;
                //        gen_sel.Component = 3;
                //        gen_sel.DistCode = distCode;
                //        gen_sel.Type = "Waiting";
                //        gen_sel.Name = data.ApName;
                //        gen_sel.AadharNo = data.AdharCardNo;
                //        gen_sel.Gender = data.Gender;
                //        gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                //        gen_sel.PhNo = data.PhNo;
                //        gen_sel.District_Mr = distName_Mr;
                //        gen_sel.Taluka_Mr = item2.Tal_Mr;
                //        gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                //        gen_sel.TalukaCode = data.Tahashil.Value;
                //        gen_sel.CreatedOn = DateTime.Now;
                //        db.SelectedGeneral.Add(gen_sel);

                //    }
            }


            //comp8

            var comp8List = list.Where(x => x.Dist == distCode && x.CompNumber.Trim().Contains("8") && x.ApplicantCrippled == "होय").Select(x => x.Id).ToList();
            var comp8ListCount = distTarget.Component_No_8;
            //select 3% for handicapped
            var targetHandicapped8 = Math.Round(decimal.Multiply(3, comp8ListCount) / 100);
            IEnumerable<int> handicapRandomList8 = comp8List.Shuffle().Take((Int32)targetHandicapped8);
            foreach (var number in handicapRandomList8)
            {
                var handicaps_sel = new SelectedHandicapped();
                handicaps_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                handicaps_sel.ApplicationNumber = data.ApplicationNumber;
                handicaps_sel.Component = 8;
                handicaps_sel.DistCode = distCode;
                handicaps_sel.TalukaCode = data.Tahashil.Value;
                handicaps_sel.AadharNo = data.AdharCardNo;
                handicaps_sel.Gender = data.Gender;
                handicaps_sel.ApplicantCrippled = data.ApplicantCrippled;
                handicaps_sel.PhNo = data.PhNo;
                handicaps_sel.District_Mr = distName_Mr;
                handicaps_sel.Name = data.ApName;
                handicaps_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                handicaps_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                handicaps_sel.Type = "Selected";
                handicaps_sel.CreatedOn = DateTime.Now;
                db.SelectedHandicapped.Add(handicaps_sel);
            }
            db.SaveChanges();
            //select 30% for females
            comp8List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("8") && (x.Gender == "Female" || x.Gender == "स्त्री")).Select(x => x.Id).ToList();
            comp8ListCount = distTarget.Component_No_8;
            var targetFemale8 = Math.Round(decimal.Multiply(30, comp8ListCount) / 100);
            IEnumerable<int> femaleRandomList8 = comp8List.Shuffle().Take((Int32)targetFemale2);
            foreach (var number in femaleRandomList8)
            {
                var female_sel = new SelectedFemale();
                female_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                female_sel.ApplicationNumber = data.ApplicationNumber;
                female_sel.Component = 8;
                female_sel.DistCode = distCode;
                female_sel.DistCode = distCode;
                female_sel.TalukaCode = data.Tahashil.Value;
                female_sel.AadharNo = data.AdharCardNo;
                female_sel.Gender = data.Gender;
                female_sel.ApplicantCrippled = data.ApplicantCrippled;
                female_sel.PhNo = data.PhNo;
                female_sel.Name = data.ApName;
                female_sel.District_Mr = distName_Mr;
                female_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                female_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                female_sel.Type = "Selected";
                female_sel.CreatedOn = DateTime.Now;
                db.SelectedFemale.Add(female_sel);
                // db.SaveChanges();
            }
            db.SaveChanges();
            //taluka wise 67% 
            foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
            {
                comp8List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("8") && x.Tahashil == item2.Tal_Code).Select(x => x.Id).ToList();
                comp8ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_8).FirstOrDefault();
                var targetTaluka8 = Math.Round(decimal.Multiply(67, comp8ListCount) / 100);
                IEnumerable<int> talukaRandomList8 = comp8List.Shuffle().Take((Int32)targetTaluka8);
                foreach (var number in talukaRandomList8)
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;
                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Component = 8;
                    gen_sel.Name = data.ApName;
                    gen_sel.DistCode = distCode; gen_sel.Type = "Selected";
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.Taluka_Mr = item2.Tal_Mr;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    gen_sel.CreatedOn = DateTime.Now;
                    db.SelectedGeneral.Add(gen_sel);

                }
                db.SaveChanges();
                ////Waiting List 8
                //comp8List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("8")).Select(x => x.Id).ToList();
                //comp8ListCount = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("8")).Count();
                //var waitingTarget = 5 * (targetFemale8 + targetHandicapped8 + targetTaluka8);
                //IEnumerable<int> waitingRandomList = comp8List.Shuffle().Take((Int32)comp8ListCount);
                //foreach (var number in waitingRandomList)
                //{
                //    var gen_sel = new SelectedGeneral();
                //    gen_sel.ApplicationId = number;
                //    var data = list.Where(x => x.Id == number).FirstOrDefault();
                //    gen_sel.ApplicationNumber = data.ApplicationNumber;
                //    gen_sel.Component = 8;
                //    gen_sel.DistCode = distCode;
                //    gen_sel.Type = "Waiting";
                //    gen_sel.AadharNo = data.AdharCardNo;
                //    gen_sel.Gender = data.Gender;
                //    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                //    gen_sel.PhNo = data.PhNo;
                //    gen_sel.Name = data.ApName;
                //    gen_sel.District_Mr = distName_Mr;
                //    gen_sel.Taluka_Mr = item2.Tal_Mr;
                //    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                //    gen_sel.TalukaCode = data.Tahashil.Value;
                //    gen_sel.CreatedOn = DateTime.Now;
                //    db.SelectedGeneral.Add(gen_sel);

                //}
            }


            //comp9
            var comp9List = list.Where(x => x.Dist == distCode && x.CompNumber.Trim().Contains("9") && x.ApplicantCrippled == "होय").Select(x => x.Id).ToList();
            var comp9ListCount = distTarget.Component_No_9;
            //select 3% for handicapped
            var targetHandicapped9 = Math.Round(decimal.Multiply(3, comp9ListCount) / 100);
            IEnumerable<int> handicapRandomList9 = comp9List.Shuffle().Take((Int32)targetHandicapped9);
            foreach (var number in handicapRandomList9)
            {
                var handicaps_sel = new SelectedHandicapped();
                handicaps_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                handicaps_sel.ApplicationNumber = data.ApplicationNumber;
                handicaps_sel.Component = 9;
                handicaps_sel.DistCode = distCode;
                handicaps_sel.TalukaCode = data.Tahashil.Value;
                handicaps_sel.AadharNo = data.AdharCardNo;
                handicaps_sel.Gender = data.Gender;
                handicaps_sel.ApplicantCrippled = data.ApplicantCrippled;
                handicaps_sel.PhNo = data.PhNo;
                handicaps_sel.District_Mr = distName_Mr;
                handicaps_sel.Name = data.ApName;
                handicaps_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                handicaps_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                handicaps_sel.Type = "Selected";
                handicaps_sel.CreatedOn = DateTime.Now;
                db.SelectedHandicapped.Add(handicaps_sel);
            }
            db.SaveChanges();
            //select 30% for females
            comp9List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("9") && (x.Gender == "Female" || x.Gender == "स्त्री")).Select(x => x.Id).ToList();
            comp9ListCount = distTarget.Component_No_9;
            var targetFemale9 = Math.Round(decimal.Multiply(30, comp9ListCount) / 100);
            IEnumerable<int> femaleRandomList9 = comp9List.Shuffle().Take((Int32)targetFemale9);
            foreach (var number in femaleRandomList9)
            {
                var female_sel = new SelectedFemale();
                female_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                female_sel.ApplicationNumber = data.ApplicationNumber;
                female_sel.Component = 9;
                female_sel.DistCode = distCode;
                female_sel.TalukaCode = data.Tahashil.Value;
                female_sel.AadharNo = data.AdharCardNo;
                female_sel.Gender = data.Gender;
                female_sel.ApplicantCrippled = data.ApplicantCrippled;
                female_sel.PhNo = data.PhNo;
                female_sel.Name = data.ApName;
                female_sel.District_Mr = distName_Mr;
                female_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                female_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                female_sel.Type = "Selected";
                female_sel.CreatedOn = DateTime.Now;
                db.SelectedFemale.Add(female_sel);
                // db.SaveChanges();
            }
            db.SaveChanges();
            //taluka wise 67% 
            foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
            {
                comp9List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("9") && x.Tahashil == item2.Tal_Code).Select(x => x.Id).ToList();
                comp9ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_9).FirstOrDefault();
                var targetTaluka9 = Math.Round(decimal.Multiply(67, comp9ListCount) / 100);
                IEnumerable<int> talukaRandomList9 = comp9List.Shuffle().Take((Int32)targetTaluka9);
                foreach (var number in talukaRandomList9)
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;
                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Component = 9;
                    gen_sel.DistCode = distCode;
                    gen_sel.Type = "Selected";
                    gen_sel.Name = data.ApName;
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.Taluka_Mr = item2.Tal_Mr;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    gen_sel.CreatedOn = DateTime.Now;
                    db.SelectedGeneral.Add(gen_sel);

                }
                db.SaveChanges();

                ////Waiting List 9
                //comp9List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("9")).Select(x => x.Id).ToList();
                //comp9ListCount = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("9")).Count();
                //var waitingTarget = 5 * (targetFemale9 + targetHandicapped9 + targetTaluka9);
                //IEnumerable<int> waitingRandomList = comp9List.Shuffle().Take((Int32)comp9ListCount);
                //foreach (var number in waitingRandomList)
                //{
                //    var gen_sel = new SelectedGeneral();
                //    gen_sel.ApplicationId = number;
                //    var data = list.Where(x => x.Id == number).FirstOrDefault();
                //    gen_sel.ApplicationNumber = data.ApplicationNumber;
                //    gen_sel.Component = 9;
                //    gen_sel.DistCode = distCode;
                //    gen_sel.Type = "Waiting";
                //    gen_sel.AadharNo = data.AdharCardNo;
                //    gen_sel.Gender = data.Gender;
                //    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                //    gen_sel.PhNo = data.PhNo;
                //    gen_sel.Name = data.ApName;
                //    gen_sel.District_Mr = distName_Mr;
                //    gen_sel.Taluka_Mr = item2.Tal_Mr;
                //    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                //    gen_sel.TalukaCode = data.Tahashil.Value;
                //    gen_sel.CreatedOn = DateTime.Now;
                //    db.SelectedGeneral.Add(gen_sel);

                //}
            }

            //comp 10
            var comp10List = list.Where(x => x.Dist == distCode && x.CompNumber.Trim().Contains("10") && x.ApplicantCrippled == "होय").Select(x => x.Id).ToList();
            var comp10ListCount = distTarget.Component_No_10;
            //select 3% for handicapped
            var targetHandicapped10 = Math.Round(decimal.Multiply(3, comp10ListCount) / 100);
            IEnumerable<int> handicapRandomList10 = comp10List.Shuffle().Take((Int32)targetHandicapped10);
            foreach (var number in handicapRandomList10)
            {
                var handicaps_sel = new SelectedHandicapped();
                handicaps_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                handicaps_sel.ApplicationNumber = data.ApplicationNumber;
                handicaps_sel.Component = 10;
                handicaps_sel.DistCode = distCode;
                handicaps_sel.TalukaCode = data.Tahashil.Value;
                handicaps_sel.AadharNo = data.AdharCardNo;
                handicaps_sel.Gender = data.Gender;
                handicaps_sel.ApplicantCrippled = data.ApplicantCrippled;
                handicaps_sel.PhNo = data.PhNo;
                handicaps_sel.District_Mr = distName_Mr;
                handicaps_sel.Name = data.ApName;
                handicaps_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                handicaps_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                handicaps_sel.Type = "Selected";
                handicaps_sel.CreatedOn = DateTime.Now;
                db.SelectedHandicapped.Add(handicaps_sel);
            }
            db.SaveChanges();
            //select 30% for females
            comp10List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("10") && (x.Gender == "Female" || x.Gender == "स्त्री")).Select(x => x.Id).ToList();
            comp10ListCount = distTarget.Component_No_10;
            var targetFemale10 = Math.Round(decimal.Multiply(30, comp10ListCount) / 100);
            IEnumerable<int> femaleRandomList10 = comp10List.Shuffle().Take((Int32)targetFemale10);
            foreach (var number in femaleRandomList10)
            {
                var female_sel = new SelectedFemale();
                female_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                female_sel.ApplicationNumber = data.ApplicationNumber;
                female_sel.Component = 10;
                female_sel.DistCode = distCode;
                female_sel.TalukaCode = data.Tahashil.Value;
                female_sel.AadharNo = data.AdharCardNo;
                female_sel.Gender = data.Gender;
                female_sel.ApplicantCrippled = data.ApplicantCrippled;
                female_sel.PhNo = data.PhNo;
                female_sel.Name = data.ApName;
                female_sel.District_Mr = distName_Mr;
                female_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                female_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                female_sel.Type = "Selected";
                female_sel.CreatedOn = DateTime.Now;
                db.SelectedFemale.Add(female_sel);
                // db.SaveChanges();
            }
            db.SaveChanges();
            //taluka wise 67% 
            foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
            {
                comp10List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("10") && x.Tahashil == item2.Tal_Code).Select(x => x.Id).ToList();
                comp10ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_10).FirstOrDefault();
                var targetTaluka10 = Math.Round(decimal.Multiply(67, comp10ListCount) / 100);
                IEnumerable<int> talukaRandomList10 = comp10List.Shuffle().Take((Int32)targetTaluka10);
                foreach (var number in talukaRandomList10)
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;
                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Component = 10;
                    gen_sel.Name = data.ApName;
                    gen_sel.DistCode = distCode; gen_sel.Type = "Selected";
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.CreatedOn = DateTime.Now;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.Taluka_Mr = item2.Tal_Mr;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    db.SelectedGeneral.Add(gen_sel);

                }
                db.SaveChanges();

                ////Waiting List 10
                //comp10List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("10")).Select(x => x.Id).ToList();
                //comp10ListCount = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("10")).Count();
                //var waitingTarget = 5 * (targetFemale10 + targetHandicapped10 + targetTaluka10);
                //IEnumerable<int> waitingRandomList = comp10List.Shuffle().Take((Int32)comp10ListCount);
                //foreach (var number in waitingRandomList)
                //{
                //    var gen_sel = new SelectedGeneral();
                //    gen_sel.ApplicationId = number;
                //    var data = list.Where(x => x.Id == number).FirstOrDefault();
                //    gen_sel.ApplicationNumber = data.ApplicationNumber;
                //    gen_sel.Component = 10;
                //    gen_sel.DistCode = distCode;
                //    gen_sel.Type = "Waiting";
                //    gen_sel.Name = data.ApName;
                //    gen_sel.AadharNo = data.AdharCardNo;
                //    gen_sel.Gender = data.Gender;
                //    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                //    gen_sel.PhNo = data.PhNo;
                //    gen_sel.District_Mr = distName_Mr;
                //    gen_sel.Taluka_Mr = item2.Tal_Mr;
                //    gen_sel.CreatedOn = DateTime.Now;
                //    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                //    gen_sel.TalukaCode = data.Tahashil.Value;
                //    db.SelectedGeneral.Add(gen_sel);

                //}
            }

            //comp11
            var comp11List = list.Where(x => x.Dist == distCode && x.CompNumber.Trim().Contains("11") && x.ApplicantCrippled == "होय").Select(x => x.Id).ToList();
            var comp11ListCount = distTarget.Component_No_11;
            //select 3% for handicapped
            var targetHandicapped11 = Math.Round(decimal.Multiply(3, comp11ListCount) / 100);
            IEnumerable<int> handicapRandomList11 = comp11List.Shuffle().Take((Int32)targetHandicapped11);
            foreach (var number in handicapRandomList11)
            {
                var handicaps_sel = new SelectedHandicapped();
                handicaps_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                handicaps_sel.ApplicationNumber = data.ApplicationNumber;
                handicaps_sel.Component = 11;

                handicaps_sel.DistCode = distCode;
                handicaps_sel.TalukaCode = data.Tahashil.Value;
                handicaps_sel.AadharNo = data.AdharCardNo;
                handicaps_sel.Gender = data.Gender;
                handicaps_sel.ApplicantCrippled = data.ApplicantCrippled;
                handicaps_sel.PhNo = data.PhNo;
                handicaps_sel.District_Mr = distName_Mr;
                handicaps_sel.Name = data.ApName;
                handicaps_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                handicaps_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                handicaps_sel.Type = "Selected";
                handicaps_sel.CreatedOn = DateTime.Now;
                db.SelectedHandicapped.Add(handicaps_sel);
            }
            db.SaveChanges();
            //select 30% for females
            comp11List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("11") && (x.Gender == "Female" || x.Gender == "स्त्री")).Select(x => x.Id).ToList();
            comp11ListCount = distTarget.Component_No_11;
            var targetFemale11 = Math.Round(decimal.Multiply(30, comp11ListCount) / 100);
            IEnumerable<int> femaleRandomList11 = comp11List.Shuffle().Take((Int32)targetFemale11);
            foreach (var number in femaleRandomList11)
            {
                var female_sel = new SelectedFemale();
                female_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                female_sel.ApplicationNumber = data.ApplicationNumber;
                female_sel.Component = 11;
                female_sel.DistCode = distCode;
                female_sel.TalukaCode = data.Tahashil.Value;
                female_sel.AadharNo = data.AdharCardNo;
                female_sel.Gender = data.Gender;
                female_sel.ApplicantCrippled = data.ApplicantCrippled;
                female_sel.PhNo = data.PhNo;
                female_sel.Name = data.ApName;
                female_sel.District_Mr = distName_Mr;
                female_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                female_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                female_sel.Type = "Selected";
                female_sel.CreatedOn = DateTime.Now;
                db.SelectedFemale.Add(female_sel);
                // db.SaveChanges();
            }
            db.SaveChanges();
            //taluka wise 67% 
            foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
            {
                comp11List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("11") && x.Tahashil == item2.Tal_Code).Select(x => x.Id).ToList();
                comp11ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_11).FirstOrDefault();
                var targetTaluka11 = Math.Round(decimal.Multiply(67, comp11ListCount) / 100);
                IEnumerable<int> talukaRandomList11 = comp11List.Shuffle().Take((Int32)targetTaluka11);
                foreach (var number in talukaRandomList11)
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;
                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Component = 11;
                    gen_sel.Name = data.ApName;
                    gen_sel.DistCode = distCode; gen_sel.Type = "Selected";
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.CreatedOn = DateTime.Now;
                    gen_sel.Taluka_Mr = item2.Tal_Mr;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    db.SelectedGeneral.Add(gen_sel);

                }
                db.SaveChanges();
                ////Waiting List 11
                //comp11List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("11")).Select(x => x.Id).ToList();
                //comp11ListCount = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("11")).Count();
                //var waitingTarget = 5 * (targetFemale11 + targetHandicapped11 + targetTaluka11);
                //IEnumerable<int> waitingRandomList = comp11List.Shuffle().Take((Int32)comp11ListCount);
                //foreach (var number in waitingRandomList)
                //{
                //    var gen_sel = new SelectedGeneral();
                //    gen_sel.ApplicationId = number;
                //    var data = list.Where(x => x.Id == number).FirstOrDefault();
                //    gen_sel.ApplicationNumber = data.ApplicationNumber;
                //    gen_sel.Component = 11;
                //    gen_sel.DistCode = distCode;
                //    gen_sel.Type = "Waiting";
                //    gen_sel.AadharNo = data.AdharCardNo;
                //    gen_sel.Gender = data.Gender;
                //    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                //    gen_sel.PhNo = data.PhNo;
                //    gen_sel.Name = data.ApName;
                //    gen_sel.District_Mr = distName_Mr;
                //    gen_sel.Taluka_Mr = item2.Tal_Mr;
                //    gen_sel.CreatedOn = DateTime.Now;
                //    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                //    gen_sel.TalukaCode = data.Tahashil.Value;
                //    db.SelectedGeneral.Add(gen_sel);

                //}
            }


            //comp12
            var comp12List = list.Where(x => x.Dist == distCode && x.CompNumber.Trim().Contains("12") && x.ApplicantCrippled == "होय").Select(x => x.Id).ToList();
            var comp12ListCount = distTarget.Component_No_12;
            //select 3% for handicapped
            var targetHandicapped12 = Math.Round(decimal.Multiply(3, comp12ListCount) / 100);
            IEnumerable<int> handicapRandomList12 = comp12List.Shuffle().Take((Int32)targetHandicapped12);
            foreach (var number in handicapRandomList12)
            {
                var handicaps_sel = new SelectedHandicapped();
                handicaps_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                handicaps_sel.ApplicationNumber = data.ApplicationNumber;
                handicaps_sel.Component = 12;
                handicaps_sel.DistCode = distCode;
                handicaps_sel.TalukaCode = data.Tahashil.Value;
                handicaps_sel.AadharNo = data.AdharCardNo;
                handicaps_sel.Gender = data.Gender;
                handicaps_sel.ApplicantCrippled = data.ApplicantCrippled;
                handicaps_sel.PhNo = data.PhNo;
                handicaps_sel.District_Mr = distName_Mr;
                handicaps_sel.Name = data.ApName;
                handicaps_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                handicaps_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                handicaps_sel.Type = "Selected";
                handicaps_sel.CreatedOn = DateTime.Now;
                db.SelectedHandicapped.Add(handicaps_sel);
            }
            db.SaveChanges();
            //select 30% for females
            comp12List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("12") && (x.Gender == "Female" || x.Gender == "स्त्री")).Select(x => x.Id).ToList();
            comp12ListCount = distTarget.Component_No_12;
            var targetFemale12 = Math.Round(decimal.Multiply(30, comp12ListCount) / 100);
            IEnumerable<int> femaleRandomList12 = comp12List.Shuffle().Take((Int32)targetFemale12);
            foreach (var number in femaleRandomList12)
            {
                var female_sel = new SelectedFemale();
                female_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                female_sel.ApplicationNumber = data.ApplicationNumber;
                female_sel.Component = 12;
                female_sel.DistCode = distCode;
                female_sel.TalukaCode = data.Tahashil.Value;
                female_sel.AadharNo = data.AdharCardNo;
                female_sel.Gender = data.Gender;
                female_sel.ApplicantCrippled = data.ApplicantCrippled;
                female_sel.PhNo = data.PhNo;
                female_sel.Name = data.ApName;
                female_sel.District_Mr = distName_Mr;
                female_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                female_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                female_sel.Type = "Selected";
                female_sel.CreatedOn = DateTime.Now;
                db.SelectedFemale.Add(female_sel);
                // db.SaveChanges();
            }
            db.SaveChanges();
            //taluka wise 67% 
            foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
            {
                comp12List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("12") && x.Tahashil == item2.Tal_Code).Select(x => x.Id).ToList();
                comp12ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_12).FirstOrDefault();
                var targetTaluka12 = Math.Round(decimal.Multiply(67, comp12ListCount) / 100);
                IEnumerable<int> talukaRandomList12 = comp12List.Shuffle().Take((Int32)targetTaluka12);
                foreach (var number in talukaRandomList12)
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;
                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Component = 12;
                    gen_sel.DistCode = distCode; gen_sel.Type = "Selected";
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.Name = data.ApName;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.CreatedOn = DateTime.Now;
                    gen_sel.Taluka_Mr = item2.Tal_Mr;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    db.SelectedGeneral.Add(gen_sel);

                }
                db.SaveChanges();
                ////Waiting List 12
                //comp12List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("12")).Select(x => x.Id).ToList();
                //comp12ListCount = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("12")).Count();
                //var waitingTarget = 5 * (targetFemale12 + targetHandicapped12 + targetTaluka12);
                //IEnumerable<int> waitingRandomList = comp12List.Shuffle().Take((Int32)comp12ListCount);
                //foreach (var number in waitingRandomList)
                //{
                //    var gen_sel = new SelectedGeneral();
                //    gen_sel.ApplicationId = number;
                //    var data = list.Where(x => x.Id == number).FirstOrDefault();
                //    gen_sel.ApplicationNumber = data.ApplicationNumber;
                //    gen_sel.Component = 12;
                //    gen_sel.DistCode = distCode;
                //    gen_sel.Type = "Waiting";
                //    gen_sel.AadharNo = data.AdharCardNo;
                //    gen_sel.Gender = data.Gender;
                //    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                //    gen_sel.PhNo = data.PhNo;
                //    gen_sel.District_Mr = distName_Mr;
                //    gen_sel.Taluka_Mr = item2.Tal_Mr;
                //    gen_sel.Name = data.ApName;
                //    gen_sel.CreatedOn = DateTime.Now;
                //    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                //    gen_sel.TalukaCode = data.Tahashil.Value;
                //    db.SelectedGeneral.Add(gen_sel);

                //}
            }
            db.SaveChanges();

            //comp 13
            var comp13List = list.Where(x => x.Dist == distCode && x.CompNumber.Trim().Contains("13") && x.ApplicantCrippled == "होय").Select(x => x.Id).ToList();
            var comp13ListCount = distTarget.Component_No_13;
            //select 3% for handicapped
            var targetHandicapped13 = Math.Round(decimal.Multiply(3, comp13ListCount) / 100);
            IEnumerable<int> handicapRandomList13 = comp13List.Shuffle().Take((Int32)targetHandicapped13);
            foreach (var number in handicapRandomList13)
            {
                var handicaps_sel = new SelectedHandicapped();
                handicaps_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                handicaps_sel.ApplicationNumber = data.ApplicationNumber;
                handicaps_sel.Component = 13;
                handicaps_sel.DistCode = distCode;
                handicaps_sel.TalukaCode = data.Tahashil.Value;
                handicaps_sel.AadharNo = data.AdharCardNo;
                handicaps_sel.Gender = data.Gender;
                handicaps_sel.ApplicantCrippled = data.ApplicantCrippled;
                handicaps_sel.PhNo = data.PhNo;
                handicaps_sel.District_Mr = distName_Mr;
                handicaps_sel.Name = data.ApName;
                handicaps_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                handicaps_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                handicaps_sel.Type = "Selected";
                handicaps_sel.CreatedOn = DateTime.Now;
                db.SelectedHandicapped.Add(handicaps_sel);
            }
            //select 30% for females
            comp13List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("13") && (x.Gender == "Female" || x.Gender == "स्त्री")).Select(x => x.Id).ToList();
            comp13ListCount = distTarget.Component_No_13;
            var targetFemale13 = Math.Round(decimal.Multiply(30, comp13ListCount) / 100);
            IEnumerable<int> femaleRandomList13 = comp13List.Shuffle().Take((Int32)targetFemale13);
            foreach (var number in femaleRandomList13)
            {
                var female_sel = new SelectedFemale();
                female_sel.ApplicationId = number;
                var data = list.Where(x => x.Id == number).FirstOrDefault();
                female_sel.ApplicationNumber = data.ApplicationNumber;
                female_sel.Component = 13;
                female_sel.DistCode = distCode;
                female_sel.TalukaCode = data.Tahashil.Value;
                female_sel.AadharNo = data.AdharCardNo;
                female_sel.Gender = data.Gender;
                female_sel.ApplicantCrippled = data.ApplicantCrippled;
                female_sel.PhNo = data.PhNo;
                female_sel.Name = data.ApName;
                female_sel.District_Mr = distName_Mr;
                female_sel.Taluka_Mr = taluka.Where(x => x.Tal_Code == data.Tahashil.Value).Select(x => x.Tal_Mr).FirstOrDefault();
                female_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                female_sel.Type = "Selected";
                female_sel.CreatedOn = DateTime.Now;
                db.SelectedFemale.Add(female_sel);
                // db.SaveChanges();
            }
            db.SaveChanges();
            //taluka wise 67% 
            foreach (var item2 in taluka.Where(x => x.Dist_Code == distCode))
            {
                comp13List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("13") && x.Tahashil == item2.Tal_Code).Select(x => x.Id).ToList();
                comp13ListCount = talukaTarg.Where(x => x.Name_Of_Taluka == item2.Tal_Mr).Select(x => x.Component_No_13).FirstOrDefault();
                var targetTaluka13 = Math.Round(decimal.Multiply(67, comp13ListCount) / 100);
                IEnumerable<int> talukaRandomList13 = comp13List.Shuffle().Take((Int32)targetTaluka13);
                foreach (var number in talukaRandomList13)
                {
                    var gen_sel = new SelectedGeneral();
                    gen_sel.ApplicationId = number;
                    var data = list.Where(x => x.Id == number).FirstOrDefault();
                    gen_sel.ApplicationNumber = data.ApplicationNumber;
                    gen_sel.Component = 13;
                    gen_sel.DistCode = distCode; gen_sel.Type = "Selected";
                    gen_sel.AadharNo = data.AdharCardNo;
                    gen_sel.Gender = data.Gender;
                    gen_sel.Name = data.ApName;
                    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                    gen_sel.PhNo = data.PhNo;
                    gen_sel.District_Mr = distName_Mr;
                    gen_sel.Taluka_Mr = item2.Tal_Mr;
                    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                    gen_sel.TalukaCode = data.Tahashil.Value;
                    gen_sel.CreatedOn = DateTime.Now;
                    db.SelectedGeneral.Add(gen_sel);

                }
                db.SaveChanges();
                ////Waiting List 13
                //comp13List = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("13")).Select(x => x.Id).ToList();
                //comp12ListCount = list.Where(x => x.Dist == distCode && x.CompNumber.Contains("13")).Count();
                //var waitingTarget = 5 * (targetFemale13 + targetHandicapped13 + targetTaluka13);
                //IEnumerable<int> waitingRandomList = comp13List.Shuffle().Take((Int32)comp13ListCount);
                //foreach (var number in waitingRandomList)
                //{
                //    var gen_sel = new SelectedGeneral();
                //    gen_sel.ApplicationId = number;
                //    var data = list.Where(x => x.Id == number).FirstOrDefault();
                //    gen_sel.ApplicationNumber = data.ApplicationNumber;
                //    gen_sel.Component = 13;
                //    gen_sel.DistCode = distCode;
                //    gen_sel.Type = "Waiting";
                //    gen_sel.AadharNo = data.AdharCardNo;
                //    gen_sel.Gender = data.Gender;
                //    gen_sel.ApplicantCrippled = data.ApplicantCrippled;
                //    gen_sel.PhNo = data.PhNo;
                //    gen_sel.Name = data.ApName;
                //    gen_sel.District_Mr = distName_Mr;
                //    gen_sel.Taluka_Mr = item2.Tal_Mr;
                //    gen_sel.VillageName = db.VillageMaster.Where(x => x.Village_Code == data.VillageName).Select(x => x.VillageName).FirstOrDefault();
                //    gen_sel.TalukaCode = data.Tahashil.Value;
                //    gen_sel.CreatedOn = DateTime.Now;
                //    db.SelectedGeneral.Add(gen_sel);

                //}
            }

            model.SelectedFemaleList = new List<SelectedFemale>();
            model.SelectedFemaleList = db.SelectedFemale.Where(x => x.DistCode == distCode && x.Type == "Selected").ToList();

            model.SelectedHandicappedList = new List<SelectedHandicapped>();
            model.SelectedHandicappedList = db.SelectedHandicapped.Where(x => x.DistCode == distCode && x.Type == "Selected").ToList();

            model.SelectedGeneralList = new List<SelectedGeneral>();
            model.SelectedGeneralList = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").ToList();
            //model.WaitingList = new List<SelectedGeneral>();
            //model.WaitingList = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Waiting").ToList();
            ofcrModel.SelectedList = model;
            return Json(ofcrModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PreliminaryList(int distCode)
        {
            var model = new OfficerLogin();
            var selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode).OrderBy(x => x.Component).ToList();
            var selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode).OrderBy(x => x.Component).ToList();
            var selectedGeneral = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Distinct().OrderBy(x => x.Component).ToList();
            var selectedWaiting = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Waiting").Distinct().OrderBy(x => x.Component).ToList();
            var talukaList = db.SelectedGeneral.Select(x => x.TalukaCode).Distinct();
            model.SelectedList = new SelectedListViewModel();
            model.SelectedList.SelectedFemaleList = new List<SelectedFemale>();
            model.SelectedList.SelectedHandicappedList = new List<SelectedHandicapped>();
            model.SelectedList.SelectedGeneralList = new List<SelectedGeneral>();
            model.SelectedList.WaitingList = new List<SelectedGeneral>();
            model.SelectedList.SelectedFemaleList = selectedFemale;
            model.SelectedList.SelectedHandicappedList = selectedHandicapped;
            model.district = distCode;
            model.SelectedList.SelectedGeneralList = selectedGeneral;
            model.SelectedList.WaitingList = selectedWaiting;

            return View(model);
        }

        public ActionResult PrelimList_Dist(FormCollection form, string dist)
        {
            var distCode = Convert.ToInt32(form["DistrictVal"]);
            if (dist != "" && dist != null)
                distCode = Convert.ToInt32(dist);

            var model = new OfficerLogin();
            model.SelectedList = new SelectedListViewModel();
            model.SelectedList.SelectedFemaleList = new List<SelectedFemale>();
            model.SelectedList.SelectedHandicappedList = new List<SelectedHandicapped>();
            model.SelectedList.SelectedGeneralList = new List<SelectedGeneral>();
            model.SelectedList.WaitingList = new List<SelectedGeneral>();

            var selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode).OrderBy(x => x.Component).ToList();
            var selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode).OrderBy(x => x.Component).ToList();
            var selectedGeneral = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Distinct().OrderBy(x => x.Component).ToList();
            var selectedWaiting = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Waiting").Distinct().OrderBy(x => x.Component).ToList();
            var talukaList = db.SelectedGeneral.Select(x => x.TalukaCode).Distinct();
            var applications = db.ApplicantRegistrations.Where(x => x.FormSubmitted == true && x.Dist == distCode).ToList();

            foreach (var item in selectedWaiting)
            {
                var _applicant = applications.Where(x => x.Id == item.ApplicationId).FirstOrDefault();
                var _compList = _applicant.CompNumber != null ? _applicant.CompNumber.Split(',').ToList() : null;
                if (_applicant.Age> 60 || _applicant.Age < 18)
                {
                    item.IsAgeProper = false;
                }
                else
                {
                    item.IsAgeProper = true;
                }

                if (_applicant.Photo == null)
                {
                    item.IsPhotoAvailable = false;
                }
                else
                {
                    item.IsPhotoAvailable = true;
                }

                if (_applicant.Child2006 > 2)
                {
                    item.IsChildCountProper = false;
                }
                else
                {
                    item.IsChildCountProper = true;
                }

                if (_compList != null)
                {
                    if ((_compList.Contains("1") || _compList.Contains("2")) && (_compList.Contains("4") || _compList.Contains("3") || _compList.Contains("5")
                        || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("8") || _compList.Contains("9") || _compList.Contains("10")
                         || _compList.Contains("11") || _compList.Contains("12") || _compList.Contains("13") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("3")) && (_compList.Contains("4") || _compList.Contains("5")
                        || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("4")) && (_compList.Contains("3") || _compList.Contains("5")
                     || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("5")) && (_compList.Contains("4") || _compList.Contains("3")
                     || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("6")) && (_compList.Contains("4") || _compList.Contains("5")
                     || _compList.Contains("3") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("7")) && (_compList.Contains("4") || _compList.Contains("5")
                     || _compList.Contains("6") || _compList.Contains("3") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("8")) && (_compList.Contains("9") || _compList.Contains("10")
                    || _compList.Contains("11") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("9")) && (_compList.Contains("8") || _compList.Contains("10")
                  || _compList.Contains("11") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("10")) && (_compList.Contains("9") || _compList.Contains("8")
                  || _compList.Contains("11") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("11")) && (_compList.Contains("9") || _compList.Contains("10")
                  || _compList.Contains("8") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("12")) && (_compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2") || _compList.Contains("13")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("13")) && (_compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2") || _compList.Contains("12")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                }

                if (db.ApplicantRegistrations.Where(x => x.AdharCardNo == item.AadharNo && x.FormSubmitted == true).ToList().Count > 1)
                {
                    item.IsAadharUnique = false;
                }
                else
                {
                    item.IsAadharUnique = true;
                }

                if(_applicant.CompNumber.Contains(item.Component.ToString()))
                {
                    item.IsWrongEntry = false;
                }
                else
                {
                    item.IsWrongEntry = true;
                }

                if(selectedGeneral.Where(x=>x.Type == "Selected").Select(x=>x.ApplicationId).ToList().Contains(item.ApplicationId)
                    || selectedFemale.Select(x => x.ApplicationId).ToList().Contains(item.ApplicationId)
                    || selectedHandicapped.Select(x => x.ApplicationId).ToList().Contains(item.ApplicationId))
                {
                    item.IsSelectedTwice = true;
                }
                else
                {
                    item.IsSelectedTwice = false;
                }
                if (db.BeneficiarySelectedList2018.Any(x=>x.Aadhar == item.AadharNo))
                {
                    item.IsPreviouslySelected = true;
                }
                else
                {
                    item.IsPreviouslySelected = false;
                }
            }

            foreach (var item in selectedGeneral)
            {
                var _applicant = applications.Where(x => x.Id == item.ApplicationId).FirstOrDefault();
                var _compList = _applicant.CompNumber != null ? _applicant.CompNumber.Split(',').ToList() : null;
                if (_applicant.Age > 60 || _applicant.Age < 18)
                {
                    item.IsAgeProper = false;
                }
                else
                {
                    item.IsAgeProper = true;
                }

                if (_applicant.Photo == null)
                {
                    item.IsPhotoAvailable = false;
                }
                else
                {
                    item.IsPhotoAvailable = true;
                }

                if (_applicant.Child2006 > 2)
                {
                    item.IsChildCountProper = false;
                }
                else
                {
                    item.IsChildCountProper = true;
                }

                if (_compList != null)
                {
                    if ((_compList.Contains("1") || _compList.Contains("2")) && (_compList.Contains("4") || _compList.Contains("3") || _compList.Contains("5")
                        || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("8") || _compList.Contains("9") || _compList.Contains("10")
                         || _compList.Contains("11") || _compList.Contains("12") || _compList.Contains("13") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("3")) && (_compList.Contains("4") || _compList.Contains("5")
                        || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("4")) && (_compList.Contains("3") || _compList.Contains("5")
                     || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("5")) && (_compList.Contains("4") || _compList.Contains("3")
                     || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("6")) && (_compList.Contains("4") || _compList.Contains("5")
                     || _compList.Contains("3") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("7")) && (_compList.Contains("4") || _compList.Contains("5")
                     || _compList.Contains("6") || _compList.Contains("3") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("8")) && (_compList.Contains("9") || _compList.Contains("10")
                    || _compList.Contains("11") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("9")) && (_compList.Contains("8") || _compList.Contains("10")
                  || _compList.Contains("11") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("10")) && (_compList.Contains("9") || _compList.Contains("8")
                  || _compList.Contains("11") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("11")) && (_compList.Contains("9") || _compList.Contains("10")
                  || _compList.Contains("8") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("12")) && (_compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2") || _compList.Contains("13")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("13")) && (_compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2") || _compList.Contains("12")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                }
                if (_applicant.CompNumber.Contains(item.Component.ToString()))
                {
                    item.IsWrongEntry = false;
                }
                else
                {
                    item.IsWrongEntry = true;
                }

                if (db.ApplicantRegistrations.Where(x => x.AdharCardNo == item.AadharNo && x.FormSubmitted == true).ToList().Count > 1)
                {
                    item.IsAadharUnique = false;
                }
                else
                {
                    item.IsAadharUnique = true;
                }
                if ( selectedFemale.Select(x => x.ApplicationId).ToList().Contains(item.ApplicationId)
                   || selectedHandicapped.Select(x => x.ApplicationId).ToList().Contains(item.ApplicationId))
                {
                    item.IsSelectedTwice = true;
                }
                else
                {
                    item.IsSelectedTwice = false;
                }
                if (db.BeneficiarySelectedList2018.Any(x => x.Aadhar == item.AadharNo))
                {
                    item.IsPreviouslySelected = true;
                }
                else
                {
                    item.IsPreviouslySelected = false;
                }
            }

            foreach (var item in selectedFemale)
            {
                var _applicant = applications.Where(x => x.Id == item.ApplicationId).FirstOrDefault();
                var _compList = _applicant.CompNumber != null ? _applicant.CompNumber.Split(',').ToList() : null;
                if (_applicant.Age > 60 || _applicant.Age < 18)
                {
                    item.IsAgeProper = false;
                }
                else
                {
                    item.IsAgeProper = true;
                }

                if (_applicant.Photo == null)
                {
                    item.IsPhotoAvailable = false;
                }
                else
                {
                    item.IsPhotoAvailable = true;
                }

                if (_applicant.Child2006 > 2)
                {
                    item.IsChildCountProper = false;
                }
                else
                {
                    item.IsChildCountProper = true;
                }

                if (_compList != null)
                {
                    if ((_compList.Contains("1") || _compList.Contains("2")) && (_compList.Contains("4") || _compList.Contains("3") || _compList.Contains("5")
                        || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("8") || _compList.Contains("9") || _compList.Contains("10")
                         || _compList.Contains("11") || _compList.Contains("12") || _compList.Contains("13") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("3")) && (_compList.Contains("4") || _compList.Contains("5")
                        || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("4")) && (_compList.Contains("3") || _compList.Contains("5")
                     || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("5")) && (_compList.Contains("4") || _compList.Contains("3")
                     || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("6")) && (_compList.Contains("4") || _compList.Contains("5")
                     || _compList.Contains("3") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("7")) && (_compList.Contains("4") || _compList.Contains("5")
                     || _compList.Contains("6") || _compList.Contains("3") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("8")) && (_compList.Contains("9") || _compList.Contains("10")
                    || _compList.Contains("11") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("9")) && (_compList.Contains("8") || _compList.Contains("10")
                  || _compList.Contains("11") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("10")) && (_compList.Contains("9") || _compList.Contains("8")
                  || _compList.Contains("11") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("11")) && (_compList.Contains("9") || _compList.Contains("10")
                  || _compList.Contains("8") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("12")) && (_compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2") || _compList.Contains("13")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("13")) && (_compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2") || _compList.Contains("12")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                }
                if (_applicant.CompNumber.Contains(item.Component.ToString()))
                {
                    item.IsWrongEntry = false;
                }
                else
                {
                    item.IsWrongEntry = true;
                }
                if (db.ApplicantRegistrations.Where(x => x.AdharCardNo == item.AadharNo && x.FormSubmitted == true).ToList().Count > 1)
                {
                    item.IsAadharUnique = false;
                }
                else
                {
                    item.IsAadharUnique = true;
                }
                if (selectedGeneral.Select(x => x.ApplicationId).ToList().Contains(item.ApplicationId)               
                 || selectedHandicapped.Select(x => x.ApplicationId).ToList().Contains(item.ApplicationId))
                {
                    item.IsSelectedTwice = true;
                }
                else
                {
                    item.IsSelectedTwice = false;
                }
                if (db.BeneficiarySelectedList2018.Any(x => x.Aadhar == item.AadharNo))
                {
                    item.IsPreviouslySelected = true;
                }
                else
                {
                    item.IsPreviouslySelected = false;
                }
            }

            foreach (var item in selectedHandicapped)
            {
                var _applicant = applications.Where(x => x.Id == item.ApplicationId).FirstOrDefault();
                var _compList = _applicant.CompNumber != null ? _applicant.CompNumber.Split(',').ToList() : null;
                if (_applicant.Age > 60 || _applicant.Age < 18)
                {
                    item.IsAgeProper = false;
                }
                else
                {
                    item.IsAgeProper = true;
                }

                if (_applicant.Photo == null)
                {
                    item.IsPhotoAvailable = false;
                }
                else
                {
                    item.IsPhotoAvailable = true;
                }

                if (_applicant.Child2006 > 2)
                {
                    item.IsChildCountProper = false;
                }
                else
                {
                    item.IsChildCountProper = true;
                }

                if (_compList != null)
                {
                    if ((_compList.Contains("1") || _compList.Contains("2")) && (_compList.Contains("4") || _compList.Contains("3") || _compList.Contains("5")
                        || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("8") || _compList.Contains("9") || _compList.Contains("10")
                         || _compList.Contains("11") || _compList.Contains("12") || _compList.Contains("13") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("3")) && (_compList.Contains("4") || _compList.Contains("5")
                        || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("4")) && (_compList.Contains("3") || _compList.Contains("5")
                     || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("5")) && (_compList.Contains("4") || _compList.Contains("3")
                     || _compList.Contains("6") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("6")) && (_compList.Contains("4") || _compList.Contains("5")
                     || _compList.Contains("3") || _compList.Contains("7") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("7")) && (_compList.Contains("4") || _compList.Contains("5")
                     || _compList.Contains("6") || _compList.Contains("3") || _compList.Contains("14") || _compList.Contains("15")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("8")) && (_compList.Contains("9") || _compList.Contains("10")
                    || _compList.Contains("11") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("9")) && (_compList.Contains("8") || _compList.Contains("10")
                  || _compList.Contains("11") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("10")) && (_compList.Contains("9") || _compList.Contains("8")
                  || _compList.Contains("11") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("11")) && (_compList.Contains("9") || _compList.Contains("10")
                  || _compList.Contains("8") || _compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("12")) && (_compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2") || _compList.Contains("13")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                    if ((_compList.Contains("13")) && (_compList.Contains("14") || _compList.Contains("15") || _compList.Contains("1") || _compList.Contains("2") || _compList.Contains("12")))
                    {
                        item.IsComponentProper = false;
                    }
                    else
                    {
                        item.IsComponentProper = true;
                    }
                }
                if (_applicant.CompNumber.Contains(item.Component.ToString()))
                {
                    item.IsWrongEntry = false;
                }
                else
                {
                    item.IsWrongEntry = true;
                }
                if (db.ApplicantRegistrations.Where(x => x.AdharCardNo == item.AadharNo && x.FormSubmitted == true).ToList().Count > 1)
                {
                    item.IsAadharUnique = false;
                }
                else
                {
                    item.IsAadharUnique = true;
                }
                if (selectedGeneral.Select(x => x.ApplicationId).ToList().Contains(item.ApplicationId)
                 || selectedFemale.Select(x => x.ApplicationId).ToList().Contains(item.ApplicationId))
                {
                    item.IsSelectedTwice = true;
                }
                else
                {
                    item.IsSelectedTwice = false;
                }
                if (db.BeneficiarySelectedList2018.Any(x => x.Aadhar == item.AadharNo))
                {
                    item.IsPreviouslySelected = true;
                }
                else
                {
                    item.IsPreviouslySelected = false;
                }
            }

            model.district = distCode;
            model.SelectedList.SelectedFemaleList = selectedFemale;
            model.SelectedList.SelectedHandicappedList = selectedHandicapped;
            model.SelectedList.SelectedGeneralList = selectedGeneral;
            model.SelectedList.WaitingList = selectedWaiting;


            return View(model);
        }

        public static void SaveHttpResponseAsFile(string RequestUrl, string FilePath)
        {
            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(RequestUrl);
                httpRequest.UserAgent = "Mozilla / 5.0(compatible; MSIE 9.0; Windows NT 6.1; Trident / 5.0)";
                httpRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)httpRequest.GetResponse();
                }
                catch (System.Net.WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                        response = (HttpWebResponse)ex.Response;
                }

                using (Stream responseStream = response.GetResponseStream())
                {
                    Stream FinalStream = responseStream;
                    if (response.ContentEncoding.ToLower().Contains("gzip"))
                        FinalStream = new GZipStream(FinalStream, CompressionMode.Decompress);
                    else if (response.ContentEncoding.ToLower().Contains("deflate"))
                        FinalStream = new DeflateStream(FinalStream, CompressionMode.Decompress);

                    using (var fileStream = System.IO.File.Create(FilePath))
                    {
                        FinalStream.CopyTo(fileStream);
                    }

                    response.Close();
                    FinalStream.Close();
                }
            }
            catch
            { }
        }

        public ActionResult Publish(int distCode)
        {

            var root = Server.MapPath("~/Documents/MahameshYojana/");
            var pdfname = String.Format("PreliminaryList_" + distCode + ".pdf");
            var path = Path.Combine(root, pdfname);
            path = Path.GetFullPath(path);
            var model = new OfficerLogin();
            var selectedHandicapped = db.SelectedHandicapped.Where(x => x.DistCode == distCode).OrderBy(x => x.TalukaCode).ToList();
            var selectedFemale = db.SelectedFemale.Where(x => x.DistCode == distCode).OrderBy(x => x.TalukaCode).ToList();
            var selectedGeneral = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Selected").Distinct().OrderBy(x => x.TalukaCode).ToList();
            var selectedWaiting = db.SelectedGeneral.Where(x => x.DistCode == distCode && x.Type == "Waiting").Distinct().OrderBy(x => x.TalukaCode).ToList();
            model.SelectedList = new SelectedListViewModel();
            model.SelectedList.SelectedFemaleList = new List<SelectedFemale>();
            model.SelectedList.SelectedHandicappedList = new List<SelectedHandicapped>();
            model.SelectedList.SelectedGeneralList = new List<SelectedGeneral>();
            model.SelectedList.WaitingList = new List<SelectedGeneral>();
            model.SelectedList.SelectedFemaleList = selectedFemale;
            model.SelectedList.SelectedHandicappedList = selectedHandicapped;
            model.SelectedList.SelectedGeneralList = selectedGeneral;
            model.SelectedList.WaitingList = selectedWaiting;
            var something = new Rotativa.ViewAsPdf("PreliminaryList", model) { FileName = "PreliminaryList_" + distCode + ".pdf", PageMargins = { Left = 10, Bottom = 10, Right = 10, Top = 10 }, PageSize = Rotativa.Options.Size.A4 };
            SaveHttpResponseAsFile(Url.Action("PreliminaryList", new { distCode = distCode }), path);
            return something;
            //return new ActionAsPdf("PreliminaryList",new { distCode = distCode })
            //{
            //   // SaveOnServerPath = "",
            //    FileName = "PreliminaryList _"+distCode+".pdf",
            //    PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 },
            //    PageSize = Rotativa.Options.Size.A4
            //};
            /*Creating iTextSharp’s Document & Writer*/

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
