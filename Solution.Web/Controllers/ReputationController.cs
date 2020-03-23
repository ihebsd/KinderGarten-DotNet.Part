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
                
            };
            return View(rm);
        }

        // GET: Reputation/Create
        public ActionResult Create()
        {
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

            };
            RepService.Add(rep);
            RepService.Commit();


            return RedirectToAction("Index");
        }

        // GET: Reputation/Edit/5
        public ActionResult Edit(int id)
        {
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
            try
            {
                Reputation r = RepService.GetById(id);
                r.Name = rm.Name;
                r.ReputationDate = rm.ReputationDate;
                r.Description = rm.Description;
                RepService.Update(r);
                RepService.Commit();

                return RedirectToAction("Index");
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
    }
}
