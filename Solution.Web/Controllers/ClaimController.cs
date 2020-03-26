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

           /* var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;*/
            
            List<Claim> Claims = ClaimsService.GetMany().ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                Claims = Claims.Where(m => m.Name.Equals(searchString)).ToList();
            }



            return View(Claims);
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
                Name = c.Name,
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

            var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            return View();
        }

        // POST: Claim/Create
        [HttpPost]
        public ActionResult Create(ClaimModel claimM)
        {
            
            DateTime today = DateTime.Now;
            Claim claims = new Claim()
            {
                Name = claimM.Name,
                Description = claimM.Description,
                ClaimDate = today,
                ParentId = (int)Session["idu"],
                ClaimType = claimM.ClaimType,
                status ="In_progress"
            };
            ClaimsService.Add(claims);
            ClaimsService.Commit();
            return RedirectToAction("IndexFront");
        }

        // GET: Claim/Edit/5
        public ActionResult Edit(int id)
        {
            
            var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            if (id == 0)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
              else
            {
            Claim e = ClaimsService.GetById(id);
            ClaimModel em = new ClaimModel();
            em.Name = e.Name;
            em.Description = e.Description;
            em.ClaimDate = e.ClaimDate;
            em.ClaimType = e.ClaimType;
            em.status = e.status;
            return View(em);
        }
        }
        /* // POST: Claim/Edit/5
         [HttpPost]
          public ActionResult Edit(int id, FormCollection collection)
          {
             Claim c = ClaimsService.GetById(id);
             ClaimModel cm = new ClaimModel();
             cm.Description = c.Description;
             cm.ClaimDate = c.ClaimDate;
             cm.ParentId = c.ParentId;
             cm.ClaimType = c.ClaimType;
             cm.status = c.status;
             ClaimsService.Update(c);
             ClaimsService.Commit();
             return View (cm);

          }*/

        // POST: Claim/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ClaimModel cm)
        {
            DateTime today = DateTime.Now;
            try
            {
                Claim c = ClaimsService.GetById(id);
                c.Name = cm.Name;
                c.Description = cm.Description;
                c.ClaimDate = today;
                c.ClaimType = cm.ClaimType;
                c.status = cm.status;
                ClaimsService.Update(c);
                ClaimsService.Commit();

                return RedirectToAction("IndexFront");
            }
            catch
            {
                return View(cm);
            }
        }
        
         
        // GET: Claim/Delete/5
        public ActionResult Delete(int id)
        {

           /* var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;*/
            return View();
        }

        // POST: Claim/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Claim C = ClaimsService.GetById((int)id);
            if (C.status == "Complete")
            {
                ClaimsService.Delete(C);
                ClaimsService.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.DEL = "You should update the reclamation First";
            }
            return View();
        }
        // GET: ClaimFront
        public ActionResult IndexFront(string searchString)
        {
            var userId = (int)Session["idu"];
            String login = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = login;
            List<Claim> Claims = ClaimsService.GetMany().ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                Claims = Claims.Where(m => m.Name.Equals(searchString)).ToList();
            }
            return View(Claims);            
        }
        // GET: Claim/DeleteFront/5
        public ActionResult DeleteFront(int id)
        {

            var userId = (int)Session["idu"];
            String login = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = login;
            return View();
        }
        // POST: Claim/DeleteFront/5
        [HttpPost]
        public ActionResult DeleteFront(int id, FormCollection collection)
        {
                Claim C = ClaimsService.GetById((int)id);
                ClaimsService.Delete(C);
                ClaimsService.Commit();
                return RedirectToAction("IndexFront");
           
        }
        // GET: Claim/DetailsFront/5
        public ActionResult DetailsFront(int id)
        {
             var userId = (int)Session["idu"];
             String login = userService.GetById(userId).login;
             String mail = userService.GetById(userId).email;
             ViewBag.home = mail;
             ViewBag.phone = login;
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
                Name = c.Name,
                Description = c.Description,
                ClaimDate = c.ClaimDate,
                ParentId = (int)Session["idu"],
                ClaimType = c.ClaimType,
                status = c.status
            };
            return View(cm);
        }
        // GET: Claim/EditBack/5
        public ActionResult EditBack(int id)
        {

           /* var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;*/
            if (id == 0)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            else
            {
                Claim e = ClaimsService.GetById(id);
                ClaimModel em = new ClaimModel();
                em.status = e.status;
                return View(em);
            }
        }
        // POST: Claim/EditBack/5
        [HttpPost]
        public ActionResult EditBack(int id, ClaimModel cm)
        {
            
            try
            {
                Claim c = ClaimsService.GetById(id);
                c.status = cm.status;
                ClaimsService.Update(c);
                ClaimsService.Commit();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(cm);
            }
        }
    }
}
