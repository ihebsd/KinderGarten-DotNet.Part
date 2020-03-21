using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solution.Web.Controllers
{
    public class ReputationController : Controller
    {
        // GET: Reputation
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reputation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reputation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reputation/Create
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

        // GET: Reputation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reputation/Edit/5
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

        // GET: Reputation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reputation/Delete/5
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
