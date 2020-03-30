using Service.Pattern;
using Solution.Data;
using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.UI.WebControls;


namespace Solution.Web.Controllers
{
    public class CarPoolController : Controller
    {

        private PidevContext db = new PidevContext();
        IUserService Service;
        IKidService ServicePar;
        ICarPoolService sc = new CarPoolService();
        IService<CarPool> servCar;
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
            ViewBag.MyParent = new SelectList(Parents, "IdUser", "prenom");

            List<User> Parentn = Service.GetMany().ToList();
            ViewBag.MyParentn = new SelectList(Parentn, "IdUser", "nom");

            var Carpool = db.CarPools;
            var daily = Carpool.Where(z => z.Daily == true).ToString();
            var everyweekday = Carpool.Where(z => z.EveryWeekDay == true).ToString();
            var weekly = Carpool.Where(z => z.Weekly == true).ToString();
            ViewBag.weekly = new SelectList(weekly);
            ViewBag.everyweekday = new SelectList(everyweekday);
            ViewBag.weekly = new SelectList(daily);

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
                    Daily=c.Daily,
                    Weekly=c.Weekly,
                    EveryWeekDay=c.EveryWeekDay,
                    idParent = c.idParent,
                };

                carps.Add(cs);
            }
            return View(carps);
        }
        // GET: CarPool
        public ActionResult MyIndex(string searchString)
        {
            List<Kid> Kids = ServicePar.GetMany().ToList();
            ViewBag.MyKid = new SelectList(Kids, "IdKid", "FirstName");

            List<User> Parents = Service.GetMany().ToList();
            ViewBag.MyParent = new SelectList(Parents, "IdUser", "prenom");

            List<User> Parentn = Service.GetMany().ToList();
            ViewBag.MyParentn = new SelectList(Parentn, "IdUser", "nom");

            

            var userId = (int)Session["idu"];
            var carps = new List<CarPoolModel>();
           
                foreach (CarPool c in sc.SearchCarpoolByTo(searchString))
            {
                if (c.idParent == userId)
                {
                   ;
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
                        idParent = c.idParent,
                    };

                    carps.Add(cs);
                }

            }
            return View(carps);
        }
        // GET: CarPool/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarPool/Create
        public ActionResult Create(string FirstName)
        {
            List<Kid> query = ServicePar.GetMany().ToList();
            //var userId = (int)Session["idu"];
            // var kidss = db.Kids;
            //  var query = kidss.Where(z => z.idParent == userId).Select(z=>z.FirstName).ToList();
            ViewBag.MyKid = new SelectList(query, "IdKid", "FirstName");

            return View();

        }

        // POST: CarPool/Create
        [HttpPost]
        [WebMethod]
        public ActionResult create(CarPoolModel collection, bool hidden_field1 = false , bool hidden_field2 = false, bool hidden_field3 = false)
        {
         
            ICarPoolService sc = new CarPoolService();
           
            CarPool c = new CarPool();
            if (ModelState.IsValid)
            {
                

                if (hidden_field1)
                   c.Daily= true;

                else
                    c.Daily = false;

                if (hidden_field2)
                    c.EveryWeekDay = true;

                else
                    c.EveryWeekDay = false;

                if (hidden_field3)
                    c.Weekly = true;

                else
                    c.Weekly = false;

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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarPool collection = db.CarPools.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
            List<Kid> query = ServicePar.GetMany().ToList();
            //var userId = (int)Session["idu"];
            // var kidss = db.Kids;
            //  var query = kidss.Where(z => z.idParent == userId).Select(z=>z.FirstName).ToList();
            ViewBag.MyKid = new SelectList(query, "IdKid", "FirstName");
            return View();

        }

        // POST: CarPool/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, CarPoolModel collection)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CarPoolToUpdate = db.CarPools.Find(id);
            if (TryUpdateModel(CarPoolToUpdate, "",
             new string[] { "ID", "Title", "From", "To", "Time", "Date", "Message", "idKid" }))
                try
                {
                    db.Entry(CarPoolToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            return View(CarPoolToUpdate);
        }


    


        // GET: CarPool/Delete/5
        public ActionResult Delete(int id)
        {
            CarPool c = sc.GetById(id);

            sc.Delete(c);
            sc.Commit();
            return RedirectToAction("MyIndex");
        }

        // POST: CarPool/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
