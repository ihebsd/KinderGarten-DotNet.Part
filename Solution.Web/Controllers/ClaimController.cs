using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Solution.Web.Controllers
{
    public class ClaimController : Controller
    {
       
        IClaimService ClaimsService;
        IUserService userService;
        public ClaimController()
        {
            ClaimsService = new ClaimService();
            userService = new UserService();
        }
        // GET: Claim
        public ActionResult Index(string searchString)
        {
            List<ClaimModel> Claim = new List<ClaimModel>();
            List<Claim> Claims = ClaimsService.GetMany().ToList();
            foreach (Claim c in ClaimsService.SearchKClaimByName(searchString))
            {
                Claim.Add(new ClaimModel
                {    
                    Description = c.Description,
                    ClaimDate = c.ClaimDate,
                    ParentId = c.ParentId,
                    ClaimType = c.ClaimType,
                    status = c.status
                });

            }
            return View(Claim);
        }

        // GET: Claim/Details/5
        public ActionResult Details(int id)
        {
           /* var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;*/
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim c;
            c = ClaimsService.GetById((int)id);
            if (c == null)
            {
                return HttpNotFound();
            }
                ClaimModel cm = new ClaimModel()
            {
                ComplaintId = c.ComplaintId,
                Description = c.Description,
                ClaimDate = c.ClaimDate,
                ParentId = c.ParentId,
                ClaimType = c.ClaimType,
                status = c.status
            };
            return View(cm);
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
            if (id == null)

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
           
            Claim e = ClaimsService.GetById(id);
            ClaimModel em = new ClaimModel();
           em.Description = e.Description;
            em.ClaimDate = e.ClaimDate;
            em.ParentId = e.ParentId;
            em.ClaimType = e.ClaimType;
            em.status = e.status;
            return View(em);
        }
        // POST: Claim/Edit/5
         [HttpPost]
         public ActionResult Edit(int id, Claim cm)
         {
            Claim c = ClaimsService.GetById(id);

            c.Description = cm.Description;
            c.ClaimDate = cm.ClaimDate;
            c.ParentId = cm.ParentId;
            c.ClaimType = cm.ClaimType;
            c.status = cm.status;
            ClaimsService.Update(c);
            ClaimsService.Commit();
            return View (cm);
            /*
         }

         // POST: Claim/Edit/5
        /* [HttpPost]
         public ActionResult Edit(int id, ClaimModel cm)
         {
             try
             {
                 Claim c = ClaimsService.GetById(id);

                 c.Description = cm.Description;
                 c.ClaimDate = cm.ClaimDate;
                 c.ParentId = cm.ParentId;
                 c.ClaimType= cm.ClaimType;
                 c.status = cm.status;
                 ClaimsService.Update(c);
                 ClaimsService.Commit();

                 return RedirectToAction("Index");
             }
             catch
             {
                 return View(cm);
             }*/
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
            Claim C = ClaimsService.GetById((int)id);
            ClaimsService.Delete(C);
            ClaimsService.Commit();
            return RedirectToAction("Index");
        }
    }
}
