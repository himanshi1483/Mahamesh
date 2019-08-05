using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mahamesh.Controllers
{
    public class SchemesController : Controller
    {
        // GET: Schemes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ImplimentedSchemes()
        {
            return View();
        }

        public ActionResult InProgressSchemes()
        {
            return View();
        }
    }
}