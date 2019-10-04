using Mahamesh.Models;
using System.Linq;
using System.Web.Mvc;
using System.Globalization;

namespace Mahamesh.Controllers
{
    public class HomeController : BaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = new HomePageViewModel();
            model.NewsList = db.NewsModels.ToList();
            model.TenderList = db.TenderModels.ToList();
            return View(model);
        }

        public ActionResult Disclaimer()
        {
            return View();
        }
        public ActionResult WebInfo()
        {
            return View();
        }
        public ActionResult Vision()
        {
            return View();
        }
        public ActionResult Mission()
        {
            return View();
        }
        public ActionResult History()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult SheepGoatTraining()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Ambajogai()
        {
            return View();
        }
        public ActionResult Bilakhed()
        {
            return View();
        }
        public ActionResult Bondri()
        {
            return View();
        }
        public ActionResult Dahiwadi()
        {
            return View();
        }
        public ActionResult Mahud()
        {
            return View();
        }
        public ActionResult Mukhed()
        {
            return View();
        }
        public ActionResult Padegaon()
        {
            return View();
        }
        public ActionResult Pohara()
        {
            return View();
        }
        public ActionResult Ranjini()
        {
            return View();
        }
        public ActionResult Tirth()
        {
            return View();
        }
    }
}