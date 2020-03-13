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
    public class ClaimController : Controller
    {
        IClaimService ClaimsService;
        public ClaimController()
        {
            ClaimsService = new ClaimService();
        }
        // GET: Claim
        public ActionResult Index()
        {
            var claims = new List<ClaimModel>();
            return View(claims);
        }

        // GET: Claim/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Claim/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Claim/Create
        [HttpPost]
        public ActionResult Create(ClaimModel claimM)
        {
            DateTime today = DateTime.Now;
            Claim claims = new Claim()
            {
                Description = claimM.Description,
                ClaimDate = today,
                ParentId = claimM.ParentId,
                ClaimType = claimM.ClaimType,
                status = claimM.status
            };
            ClaimsService.Add(claims);
            ClaimsService.Commit();


            return RedirectToAction("Index");
        }

        // GET: Claim/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Claim/Edit/5
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

        // GET: Claim/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Claim/Delete/5
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
