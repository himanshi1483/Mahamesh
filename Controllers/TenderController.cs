using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mahamesh.Controllers
{
    public class TenderController : Controller
    {
        // GET: Tender
        public ActionResult Index()
        {
            return View();
        }

        // GET: Tender
        public ActionResult TenderNotifications()
        {
            return View();
        }

        // GET: Tender
        public ActionResult TenderNotices()
        {
            return View();
        }

        // GET: Tender
        public ActionResult OfficeOrders()
        {
            return View();
        }

        // GET: Tender
        public ActionResult Adverisement()
        {
            return View();
        }
    }
}