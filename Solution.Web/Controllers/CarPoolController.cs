using Solution.Data;
using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;


namespace Solution.Web.Controllers
{
    public class CarPoolController : Controller
    {

        private PidevContext db = new PidevContext();
        IUserService Service;
        IKidService ServicePar;
        ICarPoolService sc = new CarPoolService();
        public CarPoolController()
        {
            Service = new UserService();
            ServicePar = new KidService();

        }

        // GET: CarPool
        public ActionResult Index(string searchString)
        {
            List<Kid> Kids = ServicePar.GetMany().ToList();
            ViewBag.MyKid = new SelectList(Kids, "IdKid", "FirstName");

            List<User> Parents = Service.GetMany().ToList();
            ViewBag.MyParent = new SelectList(Parents, "IdUser", "nom");
            var carps = new List<CarPoolModel>();
            foreach (CarPool c in sc.SearchCarpoolByTo(searchString))
            {
                CarPoolModel cs = new CarPoolModel()
                {

                    Id = c.Id,
                    Title = c.Title,
                    From = c.From,
                    To = c.To,
                    Time = c.Time,
                    Date = c.Date,
                    Message = c.Message,
                    idKid = c.idKid,
                    idParent=c.idParent,
                };

                carps.Add(cs);
            }
            return View(carps);
        }

        // GET: CarPool/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarPool/Create
        public ActionResult Create()
        {
            //List<Kid> Kids = ServicePar.GetMany().ToList();

            var userId = (int)Session["idu"];
            var kidss = db.Kids;
            var query = kidss.Where(z => z.idParent == userId).Select(z=>z.FirstName).ToList();
            ViewBag.MyKid = new SelectList(query, "IdKid", "FirstName");
           
            return View();
            
        }

        // POST: CarPool/Create
        [HttpPost]
            public ActionResult create(CarPoolModel collection )
        {

            ICarPoolService sc = new CarPoolService();
            
            CarPool c = new CarPool();
           if (ModelState.IsValid) { 

            c.idParent = (int)Session["idu"];
            c.Id = collection.Id;
            c.Title = collection.Title;
            c.From = collection.From;
            c.To = collection.To;
            c.Time = collection.Time;
            c.Date = collection.Date;
            c.Message = collection.Message;
            c.idKid = collection.idKid;
               
            sc.Add(c);
            sc.Commit();
            
            return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }


        // GET: CarPool/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CarPool/Edit/5
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

        // GET: CarPool/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarPool/Delete/5
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
