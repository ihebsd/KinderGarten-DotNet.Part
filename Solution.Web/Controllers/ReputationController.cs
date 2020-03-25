using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Solution.Web.Controllers
{
    public class ReputationController : Controller
    {
        IReputationService RepService;
        IUserService userService;
        public ReputationController()
        {
            RepService = new ReputationService();
            userService = new UserService();
        }
        // GET: Reputation
        public ActionResult Index(string searchString)
        {
            List<ReputationModel> Reputation = new List<ReputationModel>();
            List<Reputation> Rep  = RepService.GetMany().ToList();
            foreach (Reputation r in RepService.SearchKReputationByName(searchString))
            {
                Reputation.Add(new ReputationModel
                {
                    ReputationId = r.ReputationId,
                    Name =r.Name,
                    ReputationDate = r.ReputationDate,
                    Description = r.Description,
                    ParentId =r.ParentId,
                });

            }
            return View(Reputation);
        }

        // GET: Reputation/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reputation r;
            r = RepService.GetById((int)id);
            if (r == null)
            {
                return HttpNotFound();
            }
            ReputationModel rm = new ReputationModel()
            {
                ReputationId = r.ReputationId,
                Name = r.Name,
                Description = r.Description,
                ReputationDate = r.ReputationDate,
                ParentId=r.ParentId,
                
            };
            return View(rm);
        }

        // GET: Reputation/Create
        public ActionResult Create()
        {
            var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            return View();
        }

        // POST: Reputation/Create
        [HttpPost]
        public ActionResult Create(ReputationModel rm)
        {
            DateTime today = DateTime.Now;
            Reputation rep = new Reputation()
            {
                Name = rm.Name,
                ReputationDate = today,
                Description = rm.Description,
                ParentId = (int)Session["idu"],

            };
            RepService.Add(rep);
            RepService.Commit();


            return RedirectToAction("IndexFront");
        }

        // GET: Reputation/Edit/5
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
                Reputation r = RepService.GetById(id);
                ReputationModel rm = new ReputationModel();
                rm.Name = r.Name;
                rm.ReputationDate = r.ReputationDate;
                rm.Description = r.Description;


                return View(rm);
            }
        }

        // POST: Reputation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ReputationModel rm)
        {
            DateTime today = DateTime.Now;
            try
            {
                Reputation r = RepService.GetById(id);
                r.Name = rm.Name;
                r.ReputationDate = today;
                r.Description = rm.Description;
                RepService.Update(r);
                RepService.Commit();

                return RedirectToAction("IndexFront");
            }
            catch
            {
                return View(rm);
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
            Reputation C = RepService.GetById((int)id);
            RepService.Delete(C);
            RepService.Commit();
            return RedirectToAction("Index");
        }
        // GET: ReputationFront
        public ActionResult IndexFront(string searchString)
        {
            var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            List<ReputationModel> Reputation = new List<ReputationModel>();
            List<Reputation> Rep = RepService.GetMany().ToList();
            foreach (Reputation r in RepService.SearchKReputationByName(searchString))
            {
                Reputation.Add(new ReputationModel
                {
                    ReputationId = r.ReputationId,
                    Name = r.Name,
                    ReputationDate = r.ReputationDate,
                    Description = r.Description,
                    ParentId =r.ParentId,
                });

            }
            return View(Reputation);
        }
        // GET: Reputation/DetailsFront/5
        public ActionResult DetailsFront(int id)
        {
            var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reputation r;
            r = RepService.GetById((int)id);
            if (r == null)
            {
                return HttpNotFound();
            }
            ReputationModel rm = new ReputationModel()
            {
                ReputationId = r.ReputationId,
                Name = r.Name,
                Description = r.Description,
                ReputationDate = r.ReputationDate,
                ParentId = r.ParentId,
            };
            return View(rm);
        }
        // GET: Reputation/DeleteFront/5
        public ActionResult DeleteFront(int id)
        {
            return View();
        }

        // POST: Reputation/DeleteFront/5
        [HttpPost]
        public ActionResult DeleteFront(int id, FormCollection collection)
        {
            Reputation C = RepService.GetById((int)id);
            RepService.Delete(C);
            RepService.Commit();
            return RedirectToAction("IndexFront");
        }
    }

}
