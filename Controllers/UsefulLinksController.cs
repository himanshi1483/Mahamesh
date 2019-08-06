using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mahamesh.Controllers
{
    public class UsefulLinksController : Controller
    {
        // GET: UsefulLinks
        public ActionResult Index()
        {
            return View();
        }

        // GET: UsefulLinks
        public ActionResult FAQ()
        {
            return View();
        }
    }
}