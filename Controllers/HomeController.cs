using System.Web.Mvc;

namespace Mahamesh.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}