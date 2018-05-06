using eco_solution.Prevent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eco_solution.Controllers
{
    public class ContatoController : Controller
    {
        // GET: Faq
        [NoDirect]
        public ActionResult Faq()
        {
            return View();
        }

        [NoDirect]
        public ActionResult Termos()
        {
            return View();
        }

        [NoDirect]
        public ActionResult Privacidade()
        {
            return View();
        }


        [NoDirect]
        // GET: Ajuda
        public ActionResult Ajuda()
        {
            return View();
        }

    }
}
