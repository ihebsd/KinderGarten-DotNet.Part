using Solution.Data;
using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solution.Web.Controllers
{
    public class AdminNotifController : Controller
    {
        IAnotifService MyService = null;

        PidevContext rc = new PidevContext();
        public AdminNotifController()
        {
            MyService = new AnotifService();
        }
        // GET: AdminNotif
        public ActionResult Index(string searchString)
        {
            var notifs = new List<AdminNotifModel>();

            foreach (AdminNotif p in MyService.SearchnotifBystat(searchString))
            {
                notifs.Add(new AdminNotifModel()
                {
                    Id = p.Id,
                    msg = p.msg,
                    Datenotif = p.Datenotif,
                    userid = p.userid,
                    username = p.username

                });
            }
            int listfilms = notifs.Count();
            ViewBag.LIST = listfilms;
            return View(notifs);
        }

        // GET: AdminNotif/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminNotif/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminNotif/Create
        [HttpPost]
        public ActionResult Create(AdminNotifModel adm)
        {
            DateTime today = DateTime.Now;
            AdminNotif comp = new AdminNotif()
            {
                Datenotif = today,
                msg = adm.msg,
                userid = "1",
                username = "Raslen"
            };
            MyService.Add(comp);
            MyService.Commit();
            return RedirectToAction("Index", "Claim");
        }

        // GET: AdminNotif/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminNotif/Edit/5
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

        // GET: AdminNotif/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminNotif/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            AdminNotif comp = MyService.GetById(id);
            MyService.Delete(comp);
            MyService.Commit();
            return RedirectToAction("Index");
        }
    }
}
