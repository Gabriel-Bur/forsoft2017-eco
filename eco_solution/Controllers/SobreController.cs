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
        public ActionResult Index()
        {
            return View();
        }

        // GET: Sobre/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sobre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sobre/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sobre/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sobre/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sobre/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sobre/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
