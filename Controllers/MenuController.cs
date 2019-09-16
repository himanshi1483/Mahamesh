using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Mahamesh.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using static Mahamesh.FilterConfig;
using DriveService = Google.Apis.Drive.v3.DriveService;

namespace Mahamesh.Controllers
{
    public class MenuController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly DriveService _service;
        private static string _clientID = "386766609952-jg6mglmbo59qr0jam0bsvg4235lknm7q.apps.googleusercontent.com";
        private static string _clientSecret = "g9b-RPF7j32WnVGal3x79pRn";
        static string[] Scopes = { DriveService.Scope.Drive, DriveService.Scope.DriveFile, DriveService.Scope.DriveMetadata, DriveService.Scope.DriveAppdata };
        static string ApplicationName = "Mahamesh";
        public MenuController()
        {
        }
        public MenuController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Menu
        public ActionResult Index()
        {

            return View();
        }

        public void getDistrict()
        {
            var ddlDist = db.Comp1Target.Select(x => x.DistrictName).Distinct().ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select District--", Value = "0" });

            foreach (var m in ddlDist)
            {
                li.Add(new SelectListItem { Text = m, Value = m.ToString() });
                ViewBag.District = li;
            }
        }


        public JsonResult getDist(string dist)
        {
            var ddlDist = db.Comp1Target.Select(x => x.DistrictName).Distinct().ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select District--", Value = "0" });

            foreach (var m in ddlDist)
            {
                li.Add(new SelectListItem { Text = m, Value = m.ToString() });
                ViewBag.District = li;
            }
            return Json(new SelectList(li, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        public JsonResult getTaluka(string dist)
        {
            var d = db.DistMaster.Where(x => x.DistName.Trim() == dist.Trim()).FirstOrDefault();
            var ddlTal = db.TalMaster.Where(x => x.Dist_Code == d.Dist_Code).Select(x => x.Tal_Mr).ToList();
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
        [Authorize]
        public ActionResult AdminPanel()
        {
            var ImgFolders = db.MediaFolders.Where(x => x.MediaType == "Pictures").ToList();
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

            var model = new PhysicalTargetViewModel();

            var ddlTal = db.Comp1PhysicalTargetTaluka.Select(x => new { x.DistrictName, x.TalukaName }).Distinct().ToList();
            ViewBag.Taluka = new SelectList(ddlTal, "DistrictName", "TalukaName");

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
            var model = new PhysicalTargetViewModel();
            model.Component = model1.Component;
            model.DistrictName = model1.DistrictName;

            model.TalukaName = model1.TalukaName;
            var ddlTal = db.Comp1PhysicalTargetTaluka.Where(x => x.DistrictName == model.DistrictName).Select(x => new { x.DistrictName, x.TalukaName }).Distinct().ToList();
            ViewBag.Taluka = new SelectList(ddlTal, "TalukaName", "TalukaName", model.TalukaName);

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


            return View(model);
            //return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult MahameshYojanaTargetsByDist(string comp, string dist, string tal)
        {
            // getDistrict();
            var model = new PhysicalTargetViewModel();
            var d = db.DistMaster.Where(x => x.DistName == dist).FirstOrDefault();

            model.Component = comp;
            model.DistrictName = d.District_Mr;
            //model1.TargetModel.DistrictName = d.District_Mr;
            model.TalukaName = tal;
            var ddlTal = db.Comp1PhysicalTargetTaluka.Where(x => x.DistrictName == model.DistrictName).Select(x => new { x.DistrictName, x.TalukaName }).Distinct().ToList();
            ViewBag.Taluka = new SelectList(ddlTal, "TalukaName", "TalukaName", model.TalukaName);

            var list = new List<PhysicalTargetViewModel>();
            model.Comp1TargetList = new List<Comp1Target>();
            model.Comp2TargetList = new List<CompTarget2>();
            model.Comp3TargetList = new List<Comp3PhysicalTarget>();
            model.Comp4TargetList = new List<Comp4PhysicalTarget>();
            model.Comp1TalukaList = new List<Comp1TalukaTarget>();
            model.Comp2TalukaList = new List<Comp2TargetTaluka>();
            model.Comp3TalukaList = new List<Comp3TargetTaluka>();
            model.Comp4TalukaList = new List<Comp4TargetTaluka>();
            if ((model.Component == "1") && (model.TalukaName == "0") && (model.DistrictName != null))
            {
                //var comp1 = db.Comp1Target.ToList();
                model.Comp1TargetList = db.Comp1Target.Where(x => x.DistrictName == model.DistrictName.Trim()).ToList();
                //var data = db.Comp1Target.Where(x => x.DistrictName == district).ToList();

            }
            else if ((model.Component == "2") && (model.TalukaName == "0") && (model.DistrictName != null))
            {
                //var comp2 = db.Comp2PhysicalTarget.ToList();
                model.Comp2TargetList = db.Comp2PhysicalTarget.Where(x => x.DistrictName == model.DistrictName.Trim()).ToList();
            }
            else if ((model.Component == "3") && (model.TalukaName == "0") && (model.DistrictName != null))
            {
                //var comp3 = db.Comp1Target.ToList();
                model.Comp3TargetList = db.Comp3PhysicalTarget.Where(x => x.DistrictName == model.DistrictName.Trim()).ToList();
            }
            else if ((model.Component == "4") && (model.TalukaName == "0") && (model.DistrictName != null))
            {
                //var comp4 = db.Comp1Target.ToList();
                model.Comp4TargetList = db.Comp4PhysicalTarget.Where(x => x.DistrictName == model.DistrictName.Trim()).ToList();
            }
            else if ((model.Component == "1") && (model.TalukaName != "0") && (model.DistrictName != null))
            {
                //var comp1 = db.Comp1Target.ToList();
                model.Comp1TalukaList = db.Comp1PhysicalTargetTaluka.Where(x => x.DistrictName == model.DistrictName.Trim() && x.TalukaName == model.TalukaName.Trim()).ToList();
                //var data = db.Comp1Target.Where(x => x.DistrictName == district).ToList();

            }
            else if ((model.Component == "2") && (model.TalukaName != "0") && (model.DistrictName != null))
            {
                //var comp2 = db.Comp2PhysicalTarget.ToList();
                model.Comp2TalukaList = db.Comp2PhysicalTargetTaluka.Where(x => x.DistrictName == model.DistrictName.Trim() && x.TalukaName == model.TalukaName.Trim()).ToList();
            }
            else if ((model.Component == "3") && (model.TalukaName != "0") && (model.DistrictName != null))
            {
                //var comp3 = db.Comp1Target.ToList();
                model.Comp3TalukaList = db.Comp3PhysicalTargetTaluka.Where(x => x.DistrictName == model.DistrictName.Trim() && x.TalukaName == model.TalukaName).ToList();
            }
            else if ((model.Component == "4") && (model.TalukaName != "0") && (model.DistrictName != null))
            {
                //var comp4 = db.Comp1Target.ToList();
                model.Comp4TalukaList = db.Comp4PhysicalTargetTaluka.Where(x => x.DistrictName == model.DistrictName.Trim() && x.TalukaName == model.TalukaName).ToList();
            }
            var model1 = new OfficerLogin();
            model1.TargetModel = model;
            //return Redirect(Request.UrlReferrer.PathAndQuery);
            return Json(model1, JsonRequestBehavior.AllowGet);
        }


        public ActionResult MahameshYojanaBeneficiary()
        {
            //var model = new OfficerLogin();
            //var timeTable = db.DistrictCountdown.ToList();
            //model.TimerList = new List<DistrictCountdown>();
            //var distMaster = db.DistMaster.ToList();
            //var isGenerated = db.SelectedFemale.ToList();
            //foreach (var item in timeTable)
            //{

            //    if(isGenerated.Any(x=>x.DistCode == item.DistCode))
            //    {
            //        item.DistrictName = isGenerated.Where(x => x.DistCode == item.DistCode).Select(x => x.District_Mr).FirstOrDefault();
            //        item.CreatedDate = isGenerated.Where(x => x.DistCode == item.DistCode).Select(x => x.CreatedOn).FirstOrDefault();
            //        item.IsEnabled = true;
            //    }
            //    else
            //    {
            //        item.DistrictName = distMaster.Where(x => x.Dist_Code == item.DistCode).Select(x => x.District_Mr).FirstOrDefault();
            //        item.CreatedDate = null;
            //        item.IsEnabled = false;
            //    }
            //    model.TimerList.Add(item);
            //}
            return View();

        }

        public ActionResult PrelimBeneficiary()
        {
            var model = new OfficerLogin();
            var timeTable = db.DistrictCountdown.ToList();
            model.TimerList = new List<DistrictCountdown>();
            var distMaster = db.DistMaster.ToList();
            var isGenerated = db.SelectedFemale.ToList();
            foreach (var item in timeTable)
            {

                if (isGenerated.Any(x => x.DistCode == item.DistCode))
                {
                    item.DistrictName = isGenerated.Where(x => x.DistCode == item.DistCode).Select(x => x.District_Mr).FirstOrDefault();
                    item.CreatedDate = isGenerated.Where(x => x.DistCode == item.DistCode).Select(x => x.CreatedOn).FirstOrDefault();
                    item.IsEnabled = true;
                }
                else
                {
                    item.DistrictName = distMaster.Where(x => x.Dist_Code == item.DistCode).Select(x => x.District_Mr).FirstOrDefault();
                    item.CreatedDate = null;
                    item.IsEnabled = false;
                }
                model.TimerList.Add(item);
            }
            return View(model);

        }
        public ActionResult MahameshYojanaUserLogin(string msg)
        {
            if (msg != null)
                ViewBag.Msg = "Your appliation has been submitted successfully. To view the appliation, please login again.";

            var applicationTime = new ApplicantRegistration();
            applicationTime.appDuration = db.ApplicationDuration.FirstOrDefault();

            return View(applicationTime);
        }
        [HttpPost]
        public ActionResult MahameshYojanaUserLogin(long AdharCardNo)
        {
            var applicantExist = db.ApplicantRegistrations.Any(x => x.AdharCardNo == AdharCardNo);
            if (applicantExist == true)
            {
                var applicantData = db.ApplicantRegistrations.Where(x => x.AdharCardNo == AdharCardNo).FirstOrDefault();
                if (applicantData.FormSubmitted == true)
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
            // return View();
        }

        public ActionResult MahameshYojanaOfficerLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MahameshYojanaOfficerLogin(OfficerLogin model)
        {
            var officers = db.OfficerLogins.Any(x => x.Username == model.Username);
            if (officers)
            {
                var ofcrDetail = new OfficerLogin();
                ofcrDetail = db.OfficerLogins.Where(x => x.Username == model.Username).FirstOrDefault();
                if (ofcrDetail.ResetPwd == null && ofcrDetail.pwd == model.pwd)
                {
                    var user = new ApplicationUser();
                    //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                    UserManager.PasswordValidator = new PasswordValidator
                    {
                        RequireDigit = false,
                        RequiredLength = 3,
                        RequireLowercase = false,
                        RequireNonLetterOrDigit = false,
                        RequireUppercase = false

                    };
                    UserManager.UserValidator = new UserValidator<ApplicationUser>(UserManager)
                    {
                        AllowOnlyAlphanumericUserNames = false,
                        RequireUniqueEmail = false
                    };

                    user.UserName = model.Username.ToString();
                    user.Email = "admin@mahamesh.co.in";
                    var chkUser = UserManager.Create(user, model.pwd);

                    //Add default User to Role Admin    
                    if (chkUser.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(user.Id, ofcrDetail.desgination);

                    }

                    String hashedNewPassword = UserManager.PasswordHasher.HashPassword(model.pwd);
                    UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(db);
                    store.SetPasswordHashAsync(user, hashedNewPassword);
                    store.UpdateAsync(user);

                    return RedirectToAction("ResetPwdNew", "Menu", new { username = model.Username });

                }
                else if (ofcrDetail.ResetPwd == model.pwd)
                {
                    model.ChangedBy = model.Username;
                    var result = SignInManager.PasswordSignIn(model.Username, model.pwd, false, false);

                    return RedirectToAction("OfficerDashboard", "Menu", new { username = model.Username });
                }
            }

            return View();
        }

        [NoDirectAccess]
        public ActionResult ResetPwd(string username, string changedBy)
        {
            var model = new OfficerLogin();
            model.Username = username;
            model.ChangedBy = changedBy;
            return View(model);
        }

        [NoDirectAccess]
        [HttpPost]
        public ActionResult ResetPwd(OfficerLogin model)
        {
            UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(db);
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var loginModel = new OfficerLogin();
            loginModel = db.OfficerLogins.Where(x => x.Username == model.Username).FirstOrDefault();
            loginModel.ResetPwd = model.ResetPwd;
            db.Entry(loginModel).State = EntityState.Modified;
            db.SaveChanges();
            var user = UserManager.FindByName(model.Username);
            if (user == null)
            {
                UserManager.PasswordValidator = new PasswordValidator
                {
                    RequireDigit = false,
                    RequiredLength = 3,
                    RequireLowercase = false,
                    RequireNonLetterOrDigit = false,
                    RequireUppercase = false

                };
                UserManager.UserValidator = new UserValidator<ApplicationUser>(UserManager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = false
                };
                var user1 = new ApplicationUser();

                user1.UserName = loginModel.Username.ToString();
                user1.Email = "admin@mahamesh.co.in";
                //string pwd = "Admin123@";
                var chkUser = UserManager.Create(user1, loginModel.pwd);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user1.Id, loginModel.desgination);

                }
                user = user1;
            }

            String hashedNewPassword = UserManager.PasswordHasher.HashPassword(model.ResetPwd);

            store.SetPasswordHashAsync(user, hashedNewPassword);
            store.UpdateAsync(user);
            return RedirectToAction("OfficerDashboard", new { username = model.ChangedBy });
        }

        [NoDirectAccess]
        public ActionResult ResetPwdNew(string username, string changedBy)
        {
            var model = new OfficerLogin();
            model.Username = username;
            model.ChangedBy = changedBy;
            return View(model);
        }

        [NoDirectAccess]
        [HttpPost]
        public ActionResult ResetPwdNew(OfficerLogin model)
        {
            UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(db);
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var loginModel = new OfficerLogin();
            loginModel = db.OfficerLogins.Where(x => x.Username == model.Username).FirstOrDefault();
            loginModel.ResetPwd = model.ResetPwd;
            db.Entry(loginModel).State = EntityState.Modified;
            db.SaveChanges();
            var user = UserManager.FindByName(model.Username);
            if (user == null)
            {
                UserManager.PasswordValidator = new PasswordValidator
                {
                    RequireDigit = false,
                    RequiredLength = 3,
                    RequireLowercase = false,
                    RequireNonLetterOrDigit = false,
                    RequireUppercase = false

                };
                UserManager.UserValidator = new UserValidator<ApplicationUser>(UserManager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = false
                };
                var user1 = new ApplicationUser();

                user1.UserName = loginModel.Username.ToString();
                user1.Email = "admin@mahamesh.co.in";
                //string pwd = "Admin123@";
                var chkUser = UserManager.Create(user1, loginModel.pwd);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user1.Id, loginModel.desgination);

                }
                user = user1;
            }

            String hashedNewPassword = UserManager.PasswordHasher.HashPassword(model.ResetPwd);

            store.SetPasswordHashAsync(user, hashedNewPassword);
            store.UpdateAsync(user);
            return RedirectToAction("MahameshYojanaOfficerLogin");
        }

        [NoDirectAccess]
        public ActionResult ChangePwd(string username)
        {
            var model = new OfficerLogin();
            model.Username = username;
            return View(model);
        }

        [NoDirectAccess]
        [HttpPost]
        public ActionResult ChangePwd(OfficerLogin model)
        {
            UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(db);
            var loginModel = new OfficerLogin();
            loginModel = db.OfficerLogins.Where(x => x.Username == model.Username).FirstOrDefault();
            loginModel.ResetPwd = model.ResetPwd;
            db.Entry(loginModel).State = EntityState.Modified;
            db.SaveChanges();

            var user = UserManager.FindByName(loginModel.Username);
            if (user == null)
            {
                UserManager.PasswordValidator = new PasswordValidator
                {
                    RequireDigit = false,
                    RequiredLength = 3,
                    RequireLowercase = false,
                    RequireNonLetterOrDigit = false,
                    RequireUppercase = false

                };
                UserManager.UserValidator = new UserValidator<ApplicationUser>(UserManager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = false
                };
                var user1 = new ApplicationUser();

                user1.UserName = loginModel.Username.ToString();
                user1.Email = "admin@mahamesh.co.in";
                //string pwd = "Admin123@";
                var chkUser = UserManager.Create(user1, loginModel.pwd);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user1.Id, loginModel.desgination);

                }
                user = user1;
            }

            String hashedNewPassword = UserManager.PasswordHasher.HashPassword(loginModel.ResetPwd);

            store.SetPasswordHashAsync(user, hashedNewPassword);
            store.UpdateAsync(user);

            return RedirectToAction("MahameshYojanaOfficerLogin");
        }
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        //  [NoDirectAccess]
        public ActionResult OfficerDashboard(string username)
        {
            var list = db.OfficerLogins.ToList();
            var officer = list.Where(x => x.Username == username).FirstOrDefault();
            var distTarget = db.DistrictTarget.ToList();
            var talukaTarg = db.TalukaTarget.ToList();
            var distMaster = db.DistMaster.ToList();
            var model = new OfficerLogin();
            model = officer;
            var timer = db.DistrictCountdown.Where(x => x.DistCode == model.district).FirstOrDefault();


            var talMaster = db.TalMaster.Where(x => x.Dist_Code == model.district).ToList();
            model.TalukaList = talMaster;
            model.TCount = talMaster.Count;
            model.DistName = distMaster.Where(x => x.Dist_Code == model.district).Select(x => x.DistName).FirstOrDefault();
            model.isGenerated = db.SelectedGeneral.Any(x => x.DistCode == model.district);
            model.OfficerList = list.Where(x => x.district == model.district && (x.desgination == "LDO" || x.desgination == "DAHO")).ToList();
            foreach (var item in model.OfficerList)
            {
                item.TalukaName = talMaster.Where(x => x.Dist_Code == item.district && x.Tal_Code == item.taluka).Select(x => x.Tal_Mr).FirstOrDefault();
            }
            var dist_mr = distMaster.Where(x => x.Dist_Code == model.district).Select(x => x.District_Mr).FirstOrDefault();
            getDistrict();
            model.Timer = timer;
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            model.CurrentTime = indianTime;
            var model1 = new PhysicalTargetViewModel();
            model.Component = model1.Component;
            var list1 = new List<PhysicalTargetViewModel>();
            model1.Comp1TargetList = new List<Comp1Target>();
            model1.Comp2TargetList = new List<CompTarget2>();
            model1.Comp3TargetList = new List<Comp3PhysicalTarget>();
            model1.Comp4TargetList = new List<Comp4PhysicalTarget>();
            model1.Comp1TalukaList = new List<Comp1TalukaTarget>();
            model1.Comp2TalukaList = new List<Comp2TargetTaluka>();
            model1.Comp3TalukaList = new List<Comp3TargetTaluka>();
            model1.Comp4TalukaList = new List<Comp4TargetTaluka>();
            var ddlTal = db.Comp1PhysicalTargetTaluka.Where(x => x.DistrictName == model1.DistrictName).Select(x => new { x.DistrictName, x.TalukaName }).Distinct().ToList();
            ViewBag.Taluka = new SelectList(ddlTal, "TalukaName", "TalukaName", model1.TalukaName);
            model.TargetModel = model1;

            //stats
            var target = new TargetViewModel();
            var _list = new List<TargetViewModel>();

            var applications = db.ApplicantRegistrations.Where(x => x.FormSubmitted == true && x.Dist == model.district).ToList();
            foreach (var district in distTarget.Where(x => x.Name_of_District.Trim() == dist_mr))
            {
                var model4 = new TargetViewModel();
                var talukaList = new List<TalukaViewModel>();
                var districtName = district.Name_of_District;
                var distCode = distMaster.Where(x => x.District_Mr.Trim() == districtName.Trim()).Select(x => x.Dist_Code).FirstOrDefault();

                model4.Name_of_District = districtName;
                //comp 1
                var comp1_target = district.Component_No_1;
                var handicap_comp1target = Math.Round(decimal.Multiply(3, comp1_target) / 100);
                var female_comp1target = Math.Round(decimal.Multiply(30, comp1_target) / 100);
                model4.Component_No_1 = comp1_target;
                model4.HandicapTarget_Component_No_1 = handicap_comp1target;
                model4.FemaleTarget_Component_No_1 = female_comp1target;
                model4.ApFemaleTarget_Component_No_1 = applications.Where(x => (x.Gender == "Female" || x.Gender == "स्त्री") && (x.CompNumber.Contains("1") &&
                ((!(x.CompNumber.Contains("13") || x.CompNumber.Contains("14") || x.CompNumber.Contains("15") || x.CompNumber.Contains("10") ||
                        x.CompNumber.Contains("11") || x.CompNumber.Contains("12")))))).Count();

                model4.ApFemaleTarget_Component_No_2 = applications.Where(x => (x.Gender == "Female" || x.Gender == "स्त्री") && (x.CompNumber.Contains("2") &&
                ((!(x.CompNumber.Contains("12")))))).Count();

                model4.ApFemaleTarget_Component_No_3_7 = applications.Where(x => (x.Gender == "Female" || x.Gender == "स्त्री") && ((x.CompNumber.Contains("3") || x.CompNumber.Contains("4")
                || x.CompNumber.Contains("5") || x.CompNumber.Contains("6") || x.CompNumber.Contains("7")) && ((!(x.CompNumber.Contains("13") || x.CompNumber.Contains("14") || x.CompNumber.Contains("15")))))).Count();
                model4.ApFemaleTarget_Component_No_1 = applications.Where(x => x.Gender == "Female" || x.Gender == "स्त्री" && (x.CompNumber.Contains("1") && ((!(x.CompNumber.Contains("13") || x.CompNumber.Contains("14") || x.CompNumber.Contains("15") || x.CompNumber.Contains("10") ||
                        x.CompNumber.Contains("11") || x.CompNumber.Contains("12")))))).Count();

                model4.ApFemaleTarget_Component_No_8 = applications.Where(x => (x.Gender == "Female" || x.Gender == "स्त्री") && x.CompNumber.Contains("8")).Count();
                model4.ApFemaleTarget_Component_No_9 = applications.Where(x => (x.Gender == "Female" || x.Gender == "स्त्री") && x.CompNumber.Contains("9")).Count();
                model4.ApFemaleTarget_Component_No_10 = applications.Where(x => (x.Gender == "Female" || x.Gender == "स्त्री") && x.CompNumber.Contains("10")).Count();
                model4.ApFemaleTarget_Component_No_11 = applications.Where(x => (x.Gender == "Female" || x.Gender == "स्त्री") && x.CompNumber.Contains("11")).Count();
                model4.ApFemaleTarget_Component_No_12 = applications.Where(x => (x.Gender == "Female" || x.Gender == "स्त्री") && x.CompNumber.Contains("12")).Count();
                model4.ApFemaleTarget_Component_No_13 = applications.Where(x => (x.Gender == "Female" || x.Gender == "स्त्री") && x.CompNumber.Contains("13")).Count();

                //
                model4.ApHandicapTarget_Component_No_1 = applications.Where(x => x.ApplicantCrippled == "होय" && (x.CompNumber.Contains("1") &&
                ((!(x.CompNumber.Contains("13") || x.CompNumber.Contains("14") || x.CompNumber.Contains("15") || x.CompNumber.Contains("10") ||
                        x.CompNumber.Contains("11") || x.CompNumber.Contains("12")))))).Count();

                model4.ApHandicapTarget_Component_No_2 = applications.Where(x => x.ApplicantCrippled == "होय" && (x.CompNumber.Contains("2") &&
                ((!(x.CompNumber.Contains("12")))))).Count();

                model4.ApHandicapTarget_Component_No_3_7 = applications.Where(x => x.ApplicantCrippled == "होय" && ((x.CompNumber.Contains("3") || x.CompNumber.Contains("4")
                || x.CompNumber.Contains("5") || x.CompNumber.Contains("6") || x.CompNumber.Contains("7")) && ((!(x.CompNumber.Contains("13") || x.CompNumber.Contains("14") || x.CompNumber.Contains("15")))))).Count();

                model4.ApHandicapTarget_Component_No_8 = applications.Where(x => x.ApplicantCrippled == "होय" && x.CompNumber.Contains("8")).Count();

                model4.ApHandicapTarget_Component_No_9 = applications.Where(x => x.ApplicantCrippled == "होय" && x.CompNumber.Contains("9")).Count();
                model4.ApHandicapTarget_Component_No_10 = applications.Where(x => x.ApplicantCrippled == "होय" && x.CompNumber.Contains("10")).Count();
                model4.ApHandicapTarget_Component_No_11 = applications.Where(x => x.ApplicantCrippled == "होय" && x.CompNumber.Contains("11")).Count();
                model4.ApHandicapTarget_Component_No_12 = applications.Where(x => x.ApplicantCrippled == "होय" && x.CompNumber.Contains("12")).Count();
                model4.ApHandicapTarget_Component_No_13 = applications.Where(x => x.ApplicantCrippled == "होय" && x.CompNumber.Contains("13")).Count();
                //comp 2
                var comp2_target = district.Component_No_2;
                var handicap_comp2target = Math.Round(decimal.Multiply(3, comp2_target) / 100);
                var female_comp2target = Math.Round(decimal.Multiply(30, comp2_target) / 100);
                model4.Component_No_2 = comp2_target;
                model4.HandicapTarget_Component_No_2 = handicap_comp2target;
                model4.FemaleTarget_Component_No_2 = female_comp2target;
                //comp 3-7
                var comp3_7_target = district.Component_No_3_7;
                var handicap_comp3_7target = Math.Round(decimal.Multiply(3, comp3_7_target) / 100);
                var female_comp3_7target = Math.Round(decimal.Multiply(30, comp3_7_target) / 100);
                model4.Component_No_3_7 = comp3_7_target;
                model4.HandicapTarget_Component_No_3_7 = handicap_comp3_7target;
                model4.FemaleTarget_Component_No_3_7 = female_comp3_7target;
                //comp 8
                var comp8_target = district.Component_No_8;
                var handicap_comp8target = Math.Round(decimal.Multiply(3, comp8_target) / 100);
                var female_comp8target = Math.Round(decimal.Multiply(30, comp8_target) / 100);
                model4.Component_No_8 = comp8_target;
                model4.HandicapTarget_Component_No_8 = handicap_comp8target;
                model4.FemaleTarget_Component_No_8 = female_comp8target;
                //comp 9
                var comp9_target = district.Component_No_9;
                var handicap_comp9target = Math.Round(decimal.Multiply(3, comp9_target) / 100);
                var female_comp9target = Math.Round(decimal.Multiply(30, comp9_target) / 100);
                model4.Component_No_9 = comp9_target;
                model4.HandicapTarget_Component_No_9 = handicap_comp9target;
                model4.FemaleTarget_Component_No_9 = female_comp9target;
                //comp 10
                var comp10_target = district.Component_No_10;
                var handicap_comp10target = Math.Round(decimal.Multiply(3, comp10_target) / 100);
                var female_comp10target = Math.Round(decimal.Multiply(30, comp10_target) / 100);
                model4.Component_No_10 = comp10_target;
                model4.HandicapTarget_Component_No_10 = handicap_comp10target;
                model4.FemaleTarget_Component_No_10 = female_comp10target;
                //comp 11
                var comp11_target = district.Component_No_11;
                var handicap_comp11target = Math.Round(decimal.Multiply(3, comp11_target) / 100);
                var female_comp11target = Math.Round(decimal.Multiply(30, comp11_target) / 100);
                model4.Component_No_11 = comp11_target;
                model4.HandicapTarget_Component_No_11 = handicap_comp11target;
                model4.FemaleTarget_Component_No_11 = female_comp11target;
                //comp 12
                var comp12_target = district.Component_No_12;
                var handicap_comp12target = Math.Round(decimal.Multiply(3, comp12_target) / 100);
                var female_comp12target = Math.Round(decimal.Multiply(30, comp12_target) / 100);
                model4.Component_No_12 = comp12_target;
                model4.HandicapTarget_Component_No_12 = handicap_comp12target;
                model4.FemaleTarget_Component_No_12 = female_comp12target;
                //comp 13
                var comp13_target = district.Component_No_13;
                var handicap_comp13target = Math.Round(decimal.Multiply(3, comp13_target) / 100);
                var female_comp13target = Math.Round(decimal.Multiply(30, comp13_target) / 100);
                model4.Component_No_13 = comp13_target;
                model4.HandicapTarget_Component_No_13 = handicap_comp13target;
                model4.FemaleTarget_Component_No_13 = female_comp13target;

                //taluka-wise
                foreach (var taluka in talukaTarg.Where(x => x.Name_of_District.Trim() == district.Name_of_District.Trim() && x.Name_of_District != "Total"))
                {
                    var talukaModel = new TalukaViewModel();
                    talukaModel.Name_Of_Taluka = taluka.Name_Of_Taluka;
                    var talCode = talMaster.Where(x => x.Dist_Code == distCode && x.Tal_Mr.Trim() == taluka.Name_Of_Taluka.Trim()).Select(x => x.Tal_Code).FirstOrDefault();
                    if (talCode != 0)
                    {
                        var tal_applications = applications.Where(x => x.Tahashil == talCode && x.CompNumber != null).ToList();
                        Console.Write(talCode);
                        talukaModel.Application_Component_No_1 = tal_applications.Where(x => x.CompNumber.Contains("1") &&
                        (!(x.CompNumber.Contains("13") || x.CompNumber.Contains("14") || x.CompNumber.Contains("15") || x.CompNumber.Contains("10") ||
                        x.CompNumber.Contains("11") || x.CompNumber.Contains("12")))).Count();
                        talukaModel.Application_Component_No_2 = tal_applications.Where(x => x.Tahashil == talCode && (x.CompNumber.Contains("2") &&
                        !(x.CompNumber.Contains("12")))).Count();

                        talukaModel.Application_Component_No_3_7 = tal_applications.Where(x => x.Tahashil == talCode && (x.CompNumber.Contains("3") ||
                        x.CompNumber.Contains("4") || x.CompNumber.Contains("5") || x.CompNumber.Contains("6") || x.CompNumber.Contains("7")) &&
                        (!(x.CompNumber.Contains("14") || (x.CompNumber.Contains("13")) || (x.CompNumber.Contains("15"))))).Count();

                        talukaModel.Application_Component_No_8 = tal_applications.Where(x => x.Tahashil == talCode && x.CompNumber.Contains("8")).Count();
                        talukaModel.Application_Component_No_9 = tal_applications.Where(x => x.Tahashil == talCode && x.CompNumber.Contains("9")).Count();
                        talukaModel.Application_Component_No_10 = tal_applications.Where(x => x.Tahashil == talCode && x.CompNumber.Contains("10")).Count();
                        talukaModel.Application_Component_No_11 = tal_applications.Where(x => x.Tahashil == talCode && x.CompNumber.Contains("11")).Count();
                        talukaModel.Application_Component_No_12 = tal_applications.Where(x => x.Tahashil == talCode && x.CompNumber.Contains("12")).Count();
                        talukaModel.Application_Component_No_13 = tal_applications.Where(x => x.Tahashil == talCode && x.CompNumber.Contains("13")).Count();

                    }
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
                // model4.HandicapTarget_Component_No_1 = 
                model4.TalukaTarget = talukaList;
                _list.Add(model4);

            }
            target.TargetList = _list;
            model.SelectedList = new SelectedListViewModel();
            model.SelectedList.SelectedFemaleList = new List<SelectedFemale>();
            model.SelectedList.SelectedHandicappedList = new List<SelectedHandicapped>();
            model.SelectedList.SelectedGeneralList = new List<SelectedGeneral>();
            model.SelectedList.WaitingList = new List<SelectedGeneral>();
            model.SelectedList1 = new SelectedListViewModel();
            model.SelectedList1.SelectedFemaleList = new List<SelectedFemale>();
            model.SelectedList.SelectedHandicappedList = new List<SelectedHandicapped>();
            model.SelectedList.SelectedGeneralList = new List<SelectedGeneral>();
            model.SelectedList.WaitingList = new List<SelectedGeneral>();
            var data = db.SelectedFemale.Where(x => x.DistCode == model.district).ToList();
            if (data.Count > 0)
            {
                model.SelectedList1.SelectedFemaleList = data;
            }
            var data1 = db.SelectedHandicapped.Where(x => x.DistCode == model.district).ToList();
            if (data1.Count > 0)
            {
                model.SelectedList1.SelectedHandicappedList = data1;
            }
            var data2 = db.SelectedGeneral.Where(x => x.DistCode == model.district && x.Type == "Selected").ToList();
            if (data2.Count > 0)
            {
                model.SelectedList1.SelectedGeneralList = data2;
                model.isGenerated = true;
            }
            else
            {
                model.isGenerated = false;
            }
            var data3 = db.SelectedGeneral.Where(x => x.DistCode == model.district && x.Type == "Waiting").ToList();
            if (data3.Count > 0)
            {
                model.SelectedList.WaitingList = data3;
            }
            //ApplicantRegistrationsController ctrl = new ApplicantRegistrationsController();
            //ctrl.SelectRandomList(model.district);
            model.NewTarget = target;
            return View(model);
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

        [NoDirectAccess]
        public ActionResult UploadDocuments(int id)
        {

            var model = new ApplicantRegistration();
            model = db.ApplicantRegistrations.Where(x => x.Id == id).FirstOrDefault(); var applicationTime = new ApplicantRegistration();
            model.appDuration = db.ApplicationDuration.FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult UploadDocuments(ApplicantRegistration model, HttpPostedFileBase AdharCardFU, HttpPostedFileBase ReshanCard, HttpPostedFileBase LivestockDevOffCertificate
            , HttpPostedFileBase CasteCertificate, HttpPostedFileBase ResidentCertificate, HttpPostedFileBase ChildCertificate, HttpPostedFileBase FU712Certificate,
            HttpPostedFileBase TenancyAgreement,
             HttpPostedFileBase FU712orIncomeCertificate, HttpPostedFileBase BachatMemberCertificate, HttpPostedFileBase CompanyMemberCertificate, HttpPostedFileBase DisabilityCertificate
            , HttpPostedFileBase HamiPtra, HttpPostedFileBase TrainingCertificate, HttpPostedFileBase ShedCertificate)
        {
            UserCredential credential;
            string credPath1 = @"~\Documents\credentials.json";
            using (var stream =
                 new FileStream(Server.MapPath(credPath1), FileMode.Open, FileAccess.ReadWrite))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.


                string credPath = Server.MapPath(@"~\Documents\token.json");
                //credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes,"admin", CancellationToken.None, new FileDataStore("MyAppsToken"))
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "admin",
                    CancellationToken.None,
                    new FileDataStore(credPath, false)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            model = db.ApplicantRegistrations.Where(x => x.Id == model.Id).FirstOrDefault();
            var _parent = "";
            if (model.FolderId == null)
            {
                _parent = createFolder(service, "Docs_" + model.AdharCardNo, "1SmRUlp_l9GwK-pmqWXCszEfQ1q3cRWVk", model.Id);
                model.FolderId = _parent;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
                _parent = model.FolderId;

            if (AdharCardFU != null && AdharCardFU.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(AdharCardFU.FileName);
                var exten = Path.GetExtension(AdharCardFU.FileName);
                int docId = 1;
                var docPath = SaveToDrive(AdharCardFU, model.AdharCardNo, _parent, docId);
                model.AdharCardFU = docPath;//"https://drive.google.com/a/aarushsystems.com/file/d/"+docPath +"/view?usp=drivesdk" ;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

            }
            if (ResidentCertificate != null && ResidentCertificate.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(ResidentCertificate.FileName);
                var exten = Path.GetExtension(ResidentCertificate.FileName);
                int docId = 5;
                var docPath = SaveToDrive(ResidentCertificate, model.AdharCardNo, _parent, docId);
                model.ResidentCertificate = docPath;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

            }
            if (LivestockDevOffCertificate != null && LivestockDevOffCertificate.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(LivestockDevOffCertificate.FileName);
                var exten = Path.GetExtension(LivestockDevOffCertificate.FileName);
                int docId = 4;
                var docPath = SaveToDrive(LivestockDevOffCertificate, model.AdharCardNo, _parent, docId);
                model.LivestockDevOffCertificate = docPath;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

            }
            if (ReshanCard != null && ReshanCard.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(ReshanCard.FileName);
                var exten = Path.GetExtension(ReshanCard.FileName);
                int docId = 2;
                var docPath = SaveToDrive(ReshanCard, model.AdharCardNo, _parent, docId);
                model.ReshanCard = docPath;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

            }
            if (CasteCertificate != null && CasteCertificate.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(CasteCertificate.FileName);
                var exten = Path.GetExtension(CasteCertificate.FileName);
                int docId = 3;
                var docPath = SaveToDrive(CasteCertificate, model.AdharCardNo, _parent, docId);
                model.CasteCertificate = docPath;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

            }

            if (ChildCertificate != null && ChildCertificate.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(ChildCertificate.FileName);
                var exten = Path.GetExtension(ChildCertificate.FileName);
                int docId = 6;
                var docPath = SaveToDrive(ChildCertificate, model.AdharCardNo, _parent, docId);
                model.Childcertificate = docPath;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

            }
            if (FU712Certificate != null && FU712Certificate.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(FU712Certificate.FileName);
                var exten = Path.GetExtension(FU712Certificate.FileName);
                int docId = 1;
                var docPath = SaveToDrive(FU712Certificate, model.AdharCardNo, _parent, docId);
                model.FU712Certificate = docPath;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

            }
            if (TenancyAgreement != null && TenancyAgreement.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(TenancyAgreement.FileName);
                var exten = Path.GetExtension(TenancyAgreement.FileName);
                int docId = 8;
                var docPath = SaveToDrive(TenancyAgreement, model.AdharCardNo, _parent, docId);
                model.TenancyAgreement = docPath;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

            }

            if (FU712orIncomeCertificate != null && FU712orIncomeCertificate.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(FU712orIncomeCertificate.FileName);
                var exten = Path.GetExtension(FU712orIncomeCertificate.FileName);
                int docId = 7;
                var docPath = SaveToDrive(FU712orIncomeCertificate, model.AdharCardNo, _parent, docId);
                model.FU712orIncomeCertificate = docPath;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

            }
            if (BachatMemberCertificate != null && BachatMemberCertificate.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(BachatMemberCertificate.FileName);
                var exten = Path.GetExtension(BachatMemberCertificate.FileName);
                int docId = 10;
                var docPath = SaveToDrive(BachatMemberCertificate, model.AdharCardNo, _parent, docId);
                model.BachatMemberCertificate = docPath;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

            }
            if (CompanyMemberCertificate != null && CompanyMemberCertificate.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(CompanyMemberCertificate.FileName);
                var exten = Path.GetExtension(CompanyMemberCertificate.FileName);
                int docId = 11;
                var docPath = SaveToDrive(CompanyMemberCertificate, model.AdharCardNo, _parent, docId);
                model.CompanyMemberCertificate = docPath;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

            }
            if (DisabilityCertificate != null && DisabilityCertificate.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(DisabilityCertificate.FileName);
                var exten = Path.GetExtension(DisabilityCertificate.FileName);
                int docId = 12;
                var docPath = SaveToDrive(DisabilityCertificate, model.AdharCardNo, _parent, docId);
                model.DisabilityCertificate = docPath;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

            }
            if (HamiPtra != null && HamiPtra.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(HamiPtra.FileName);
                var exten = Path.GetExtension(HamiPtra.FileName);
                int docId = 14;
                var docPath = SaveToDrive(HamiPtra, model.AdharCardNo, _parent, docId);
                model.HamiPtra = docPath;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

            }
            if (TrainingCertificate != null && TrainingCertificate.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(TrainingCertificate.FileName);
                var exten = Path.GetExtension(TrainingCertificate.FileName);
                int docId = 13;
                var docPath = SaveToDrive(TrainingCertificate, model.AdharCardNo, _parent, docId);
                model.TrainingCertificate = docPath;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

            }
            if (ShedCertificate != null && ShedCertificate.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(ShedCertificate.FileName);
                var exten = Path.GetExtension(ShedCertificate.FileName);
                int docId = 9;
                var docPath = SaveToDrive(ShedCertificate, model.AdharCardNo, _parent, docId);
                model.ShedCertificate = docPath;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

            }
            model.appDuration = db.ApplicationDuration.FirstOrDefault();

            return View(model);
        }

        public ActionResult DeleteDoc(int id, string doc)
        {
            var model = new ApplicantRegistration();
            model = db.ApplicantRegistrations.Where(x => x.Id == id).FirstOrDefault();
            if(doc == model.AdharCardFU)
            {
                model.AdharCardFU = null;

            }
            else if (doc == model.ReshanCard)
            {
                model.ReshanCard = null;
            }
            else if (doc == model.LivestockDevOffCertificate)
            {
                model.LivestockDevOffCertificate = null;
            }
            else if (doc == model.ResidentCertificate)
            {
                model.ResidentCertificate = null;
            }
            else if (doc == model.CasteCertificate)
            {
                model.CasteCertificate = null;
            }
            else if (doc == model.Childcertificate)
            {
                model.Childcertificate = null;
            }
            else if (doc == model.TrainingCertificate)
            {
                model.TrainingCertificate = null;
            }
            else if (doc == model.TenancyAgreement)
            {
                model.TenancyAgreement = null;
            }
            else if (doc == model.ShedCertificate)
            {
                model.ShedCertificate = null;
            }
            else if (doc == model.FU712Certificate)
            {
                model.FU712Certificate = null;
            }
            else if (doc == model.FU712orIncomeCertificate)
            {
                model.FU712orIncomeCertificate = null;
            }
            else if (doc == model.BachatMemberCertificate)
            {
                model.BachatMemberCertificate = null;
            }
            else if (doc == model.HamiPtra)
            {
                model.HamiPtra = null;
            }
            else if (doc == model.DisabilityCertificate)
            {
                model.DisabilityCertificate = null;
            }
            db.SaveChanges();
            UserCredential credential;

            using (var stream =
                 new FileStream(Server.MapPath("~/Documents/credentials.json"), FileMode.Open, FileAccess.ReadWrite))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = Server.MapPath("~/Documents/token.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "admin",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            service.Files.Delete(doc).Execute();
         
            return RedirectToAction("UploadDocuments",new { id = id });
        }
        public string SaveToDrive(HttpPostedFileBase file1, long aadhar, string _parent, int docId)
        {
            UserCredential credential;

            using (var stream =
                 new FileStream(Server.MapPath("~/Documents/credentials.json"), FileMode.Open, FileAccess.ReadWrite))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = Server.MapPath("~/Documents/token.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "admin",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });



            Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();
            body.Name = aadhar + "_" + docId + "_" + System.IO.Path.GetFileName(file1.FileName);
            body.Description = "Mahamesh File";
            body.MimeType = GetMimeType(file1.FileName);
            if (!string.IsNullOrEmpty(_parent))
            {
                body.Parents = new List<string>() { _parent };
            }


            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = file1.FileName,
                Parents = new List<string>
                {
                    _parent
                }
            };

            // File's content.
            System.IO.BinaryReader b = new System.IO.BinaryReader(file1.InputStream);
            byte[] byteArray = b.ReadBytes(file1.ContentLength);
            System.IO.MemoryStream _stream = new System.IO.MemoryStream(byteArray);
            try
            {
                FilesResource.CreateMediaUpload request = service.Files.Create(body, _stream, GetMimeType(file1.FileName));
                request.Alt = FilesResource.CreateMediaUpload.AltEnum.Json;
                request.Fields = "id";

                request.Upload();
                var d = request.ResponseBody;
                return d.Id;
            }
            catch (Exception e)
            {
                return "";
            }

        }

        private static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
            {
                mimeType = regKey.GetValue("Content Type").ToString();
            }

            return mimeType;
        }

        #region StorageConfiguration
        [HttpGet]
        public string createDirectory()
        {
            UserCredential credential;
            //var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            //{
            //    ClientSecrets = new ClientSecrets
            //    {
            //        ClientId = _clientID,
            //        ClientSecret = _clientSecret
            //    },
            //    Scopes = new[] { DriveService.Scope.Drive, DriveService.Scope.DriveFile, DriveService.Scope.DriveMetadata, DriveService.Scope.DriveAppdata },
            //    DataStore = new FileDataStore(HttpContext.Server.MapPath("~/App_Data/credentials.json")),
            //});
            //   UserCredential usercred = new UserCredential (apiCodeFlow, )
           
            using (var stream =
                new FileStream(Server.MapPath("~/Documents/credentials.json"), FileMode.Open, FileAccess.ReadWrite))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = Server.MapPath("~/token.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "admin",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            Google.Apis.Drive.v3.Data.File NewDirectory = null;
            string _parent = "AppFolder";
            // Create metaData for a new Directory
            Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();
            body.Name = "Mahamesh";
            body.Description = "";
            body.MimeType = "application/vnd.google-apps.folder";
            //body.Parents =  new List<string>() { "root" };
            if (!string.IsNullOrEmpty(_parent))
            {
                body.Parents = new List<string>() { "root" };
            };
            //create parent folder for app
            //   public static DriveService serv2 = new DriveService();
            try
            {
                Google.Apis.Drive.v3.FilesResource.ListRequest checkDirectory = _service.Files.List();

                //checkDirectory.Q = "name='PicAggo'";


                // string folderId = checkDirectory.Execute().Files[0].Id;

                // FilesResource.CreateRequest request = serv2.Files.Create(body);
                FilesResource.CreateRequest request = service.Files.Create(body);
                NewDirectory = request.Execute();
                return NewDirectory.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return "";
            }

        }
        public static string createFolder(DriveService _service, string _title, string _parent, int id)
        {

            Google.Apis.Drive.v3.Data.File NewDirectory = null;

            // Create metaData for a new Directory
            Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();
            body.Name = _title;
            body.Description = "";
            body.MimeType = "application/vnd.google-apps.folder";
            //body.Parents = "root";
            if (!string.IsNullOrEmpty(_parent))
            {
                body.Parents = new List<string>() { _parent };
            };

            //create parent folder for app
            try
            {
                //FilesResource.ListRequest checkDirectory = _service.Files.Get("");


                FilesResource.CreateRequest request = _service.Files.Create(body);
                NewDirectory = request.Execute();


            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }


            return NewDirectory.Id;
        }
        #endregion
    }
}