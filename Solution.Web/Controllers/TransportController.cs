using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solution.Web.Controllers
{
    public class TransportController : Controller
    {
        // GET: Transport
        public ActionResult Index()
        {
            return View();
        }

        // GET: Transport/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Transport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transport/Create
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

        // GET: Transport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Transport/Edit/5
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

        // GET: Transport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Transport/Delete/5
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
