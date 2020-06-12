using Solution.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solution.Web.Controllers
{
    public class DashboardFrontController : Controller
    {
        IUserService userService;
        public DashboardFrontController()
        {       
            userService = new UserService();
        }
        // GET: DashboardFront
        public ActionResult Dashboard()
        {
            var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            return View();
        }

        // GET: DashboardFront/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DashboardFront/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashboardFront/Create
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

        // GET: DashboardFront/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DashboardFront/Edit/5
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

        // GET: DashboardFront/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DashboardFront/Delete/5
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
