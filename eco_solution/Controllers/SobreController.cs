using eco_solution.Prevent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eco_solution.Controllers
{
    public class SobreController : Controller
    {
        // GET: Sobre
        [NoDirect]
        public ActionResult Index()
        {
            return View();
        }
    }
}
