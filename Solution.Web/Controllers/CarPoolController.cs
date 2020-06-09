using PagedList;
using PagedList.Mvc;
using Service.Pattern;
using Solution.Data;
using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
        IUserService ps = new UserService();
        IKidService ServicePar;
        ICarPoolService sc = new CarPoolService();
        IKidService ks = new KidService();
        IService<CarPool> servCar;
        public CarPoolController()
        {
            Service = new UserService();
            ServicePar = new KidService();

        }

        // GET: CarPool
        public ActionResult Index(string searchString, int[] array, int? i)
        {
            des = new List<CarPool>();
            List<Kid> Kids = ServicePar.GetMany().ToList();
            ViewBag.MyKid = new SelectList(Kids, "IdKid", "FirstName");

            List<User> Parents = Service.GetMany().ToList();
            ViewBag.MyParent = new SelectList(Parents, "IdUser", "prenom");

            List<User> Parentn = Service.GetMany().ToList();
            ViewBag.MyParentn = new SelectList(Parentn, "IdUser", "nom");

            List<GeoLocation> geo = db.GeoLocations.ToList<GeoLocation>();
            ViewBag.Geo = geo;

            ViewBag.UserGeo = (from s in db.GeoLocations // outer sequence
                               join st in db.Users //inner sequence 
                               on s.ParentFK equals st.idUser   // key selector 

                               select new
                               { // result selector 
                                   idUser = s.ParentFK,
                                   NomUser = st.nom,
                                   PrenomUser = st.prenom,
                                   AddressUser = s.Address,
                                   Lat = s.lat,
                                   Lng = s.lng
                               }).ToList();






            //  var Carpool = db.CarPools;
            //  var daily = Carpool.Where(z => z.Daily == true).ToString();
            //  var everyweekday = Carpool.Where(z => z.EveryWeekDay == true).ToString();
            //  var weekly = Carpool.Where(z => z.Weekly == true).ToString();
            //  ViewBag.weekly = new SelectList(weekly);
            //  ViewBag.everyweekday = new SelectList(everyweekday);
            //  ViewBag.weekly = new SelectList(daily);

            var carps = new List<CarPoolModel>();
            foreach (CarPool c in sc.SearchParentByTo(searchString))
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
                    Daily = c.Daily,
                    Weekly = c.Weekly,
                    EveryWeekDay = c.EveryWeekDay,
                    UntilDate = c.UntilDate,
                    NbPlaceDispo = c.NbPlaceDispo,
                    idParent = c.idParent,

                };

                carps.Add(cs);

            }
            if (searchString == null)
            {
                //just load the main index 
                return View(carps.ToPagedList(i ?? 1, 7));
            }
            else
            {
                return View(carps.Where(car => car.To == (searchString)).ToPagedList(i ?? 1, 7));
            }
        }
        static List<CarPool> des = new List<CarPool>();
        [HttpPost]
        public JsonResult Nearme(List<DistanceGeoModel> distgeo)
        {

            Debug.WriteLine("" + distgeo[0].id + "     " + distgeo[0].Distance + "" + " Km");

            foreach (var arr in distgeo)
            {
                List<CarPool> des_tmp = db.CarPools.Where(x => x.idParent == arr.id).ToList();
                des.AddRange(des_tmp);
            }

            foreach (var tt in des)
            {
                Debug.WriteLine(des);


            }
            return Json(new { success = true, responseText = "message1." }, JsonRequestBehavior.AllowGet);



        }
        public ActionResult IndexNearme(string searchString, int[] array, int? i)
        {
            List<Kid> Kids = ServicePar.GetMany().ToList();
            ViewBag.MyKid = new SelectList(Kids, "IdKid", "FirstName");

            List<User> Parents = Service.GetMany().ToList();
            ViewBag.MyParent = new SelectList(Parents, "IdUser", "prenom");

            List<User> Parentn = Service.GetMany().ToList();
            ViewBag.MyParentn = new SelectList(Parentn, "IdUser", "nom");

            List<GeoLocation> geo = db.GeoLocations.ToList<GeoLocation>();
            ViewBag.Geo = geo;





            //  var Carpool = db.CarPools;
            //  var daily = Carpool.Where(z => z.Daily == true).ToString();
            //  var everyweekday = Carpool.Where(z => z.EveryWeekDay == true).ToString();
            //  var weekly = Carpool.Where(z => z.Weekly == true).ToString();
            //  ViewBag.weekly = new SelectList(weekly);
            //  ViewBag.everyweekday = new SelectList(everyweekday);
            //  ViewBag.weekly = new SelectList(daily);

            List<CarPoolModel> carps = new List<CarPoolModel>();
            foreach (CarPool c in des)
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
                    Daily = c.Daily,
                    Weekly = c.Weekly,
                    EveryWeekDay = c.EveryWeekDay,
                    UntilDate = c.UntilDate,
                    NbPlaceDispo = c.NbPlaceDispo,
                    idParent = c.idParent,

                };

                carps.Add(cs);



            }
            return View(carps);
        }
        // GET: CarPool
        public ActionResult MyIndex(string searchString, string map)
        {
            List<Kid> Kids = ServicePar.GetMany().ToList();
            ViewBag.MyKid = new SelectList(Kids, "IdKid", "FirstName");

            List<User> Parents = Service.GetMany().ToList();
            ViewBag.MyParent = new SelectList(Parents, "IdUser", "prenom");

            List<User> Parentn = Service.GetMany().ToList();
            ViewBag.MyParentn = new SelectList(Parentn, "IdUser", "nom");



            var userId = (int)Session["idu"];
            var carps = new List<CarPoolModel>();

            foreach (CarPool c in sc.SearchParentByTo(searchString))
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
                        Daily = c.Daily,
                        Weekly = c.Weekly,
                        EveryWeekDay = c.EveryWeekDay,
                        UntilDate = c.UntilDate,
                        NbPlaceDispo = c.NbPlaceDispo,
                        idParent = c.idParent,

                    };

                    carps.Add(cs);
                }


            }
            return View(carps);
        }


        // GET: CarPool/Details/5
        public ActionResult Details()
        {

            return View();
        }

        // GET: CarPool/Create
        public ActionResult Create(string FirstName)
        {
            //List<Kid> query = ServicePar.GetMany().ToList();
            var userId = (int)Session["idu"];
            var kidss = db.Kids;
            ViewBag.Kidsss = kidss.Where(z => z.idParent == userId).Select(m => new SelectListItem { Value = m.IdKid.ToString(), Text = m.FirstName });
            //IEnumerable<SelectListItem> query = kidss.Where(z => z.idParent == userId).Select(z => new SelectListItem
            //ViewBag.MyKid = new SelectList(query,"idKid", "FirstName");
            //ViewBag.MyKid = query;
            return View();

        }

        // POST: CarPool/Create
        [HttpPost]
        [WebMethod]
        [ValidateAntiForgeryToken]
        public ActionResult create(CarPoolModel collection, bool hidden_field1 = false, bool hidden_field2 = false, bool hidden_field3 = false)
        {

            ICarPoolService sc = new CarPoolService();
            CarPool c = new CarPool();
            DateTime date = DateTime.Parse(c.Date.ToString());
            
            DateTime today = DateTime.Today;

            if (ModelState.IsValid)
            {

                if (hidden_field1)
                    c.Daily = true;

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
                c.UntilDate = collection.UntilDate;

                c.Others = c.Daily || c.EveryWeekDay || c.Weekly ? false : true;

                c.idParent = (int)Session["idu"];
                c.Id = collection.Id;
                c.Title = collection.Title;
                c.From = collection.From;
                c.To = collection.To;
                
                   
                
                    
                c.Time = collection.Time;
               
                c.Date = collection.Date;
                

                c.Message = collection.Message;
                c.NbPlaceDispo = collection.NbPlaceDispo;
                c.idKid = collection.idKid;


                sc.Add(c);
                sc.Commit();


                return RedirectToAction("MyIndex");
            }
            else
            {
                return View();
            }
        }

        // GET: CarPool/Edit/5
        public ActionResult Edit(int? id, bool hidden_field1 = false, bool hidden_field2 = false, bool hidden_field3 = false)
        {
            var CarPoolToUpdate = db.CarPools.Find(id);
            if (hidden_field1)
                CarPoolToUpdate.Daily = true;

            else
                CarPoolToUpdate.Daily = false;

            if (hidden_field2)
                CarPoolToUpdate.EveryWeekDay = true;

            else
                CarPoolToUpdate.EveryWeekDay = false;

            if (hidden_field3)
                CarPoolToUpdate.Weekly = true;

            else
                CarPoolToUpdate.Weekly = false;

            CarPool collection = sc.GetById((long)id);
            CarPoolModel c = new CarPoolModel();
            c.Id = collection.Id;
            c.Title = collection.Title;
            c.From = collection.From;
            c.To = collection.To;
            c.Time = collection.Time;
            c.Date = collection.Date;
            c.Message = collection.Message;
            c.NbPlaceDispo = collection.NbPlaceDispo;
            c.idKid = collection.idKid;

            List<Kid> query = ServicePar.GetMany().ToList();
            //var userId = (int)Session["idu"];
            // var kidss = db.Kids;
            //  var query = kidss.Where(z => z.idParent == userId).Select(z=>z.FirstName).ToList();
            //ViewBag.MyKid = new SelectList(query, "IdKid", "FirstName");
            var userId = (int)Session["idu"];
            var kidss = db.Kids;
            ViewBag.Kidsss = kidss.Where(z => z.idParent == userId).Select(m => new SelectListItem { Value = m.IdKid.ToString(), Text = m.FirstName });
            return View(c);


        }





        // POST: CarPool/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, CarPoolModel collection, bool hidden_field1 = false, bool hidden_field2 = false, bool hidden_field3 = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CarPoolToUpdate = db.CarPools.Find(id);
            if (hidden_field1)
                CarPoolToUpdate.Daily = true;

            else
                CarPoolToUpdate.Daily = false;

            if (hidden_field2)
                CarPoolToUpdate.EveryWeekDay = true;

            else
                CarPoolToUpdate.EveryWeekDay = false;

            if (hidden_field3)
                CarPoolToUpdate.Weekly = true;

            else
                CarPoolToUpdate.Weekly = false;
            if (TryUpdateModel(CarPoolToUpdate, "",
             new string[] { "ID", "Title", "From", "To", "Time", "Date", "Message", "idKid" }))
                try
                {
                    db.Entry(CarPoolToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("MyIndex");
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

                return RedirectToAction("MyIndex");
            }
            catch
            {
                return View();
            }

        }
        UserLogin cs;
        public ActionResult Profil(string id, string map)
        {
            var userId = (int)Session["idu"];

            List<GeoLocation> geo = db.GeoLocations.ToList<GeoLocation>();
            ViewBag.Geo = geo;

            List<Kid> kidss = db.Kids.ToList<Kid>();
            ViewBag.Kids = kidss;


            foreach (User c in ps.GetMany())
            {
                if (c.idUser == userId)
                {

                    cs = new UserLogin()
                    {

                        nom = c.nom,
                        prenom = c.prenom,
                        email = c.email,



                    };


                }


            }
            return View(cs);
        }
        public ActionResult createkid()
        {
            return View();

        }
        // add kid
        [HttpPost]
        public ActionResult createkid(KidModel collection)
        {

            IKidService sc = new KidService();
            Kid c = new Kid();

            if (ModelState.IsValid)
            {



                c.idParent = (int)Session["idu"];
                c.FirstName = collection.FirstName;
                c.LastName = collection.LastName;
                c.Age = collection.Age;
                c.IsCheked = false;


                sc.Add(c);
                sc.Commit();


                return RedirectToAction("Profil");
            }
            else
            {
                return View();
            }
        }
        public ActionResult DeleteKid(int id)
        {
            Kid c = ks.GetById(id);

            ks.Delete(c);
            ks.Commit();
            return RedirectToAction("Profil");
        }
        //contacter parent
        public ActionResult Contacter(int id)
        {
            int? idcar = sc.GetById(id).idParent;

            //List<GeoLocation> geoU = db.GeoLocations.Where(f => f.ParentFK == idcar).ToList();
            //ViewBag.Geoo = geoU;

            List<User> pru = db.Users.ToList<User>();
            ViewBag.Usr = pru;

            List<Kid> kidss = db.Kids.Where(f => f.idParent == idcar).ToList<Kid>();
            ViewBag.Kids = kidss;

            

            foreach (User c in ViewBag.Usr)
            {
                if (c.idUser == idcar)
                {
                    cs = new UserLogin()
                    {

                        nom = c.nom,
                        prenom = c.prenom,
                        email = c.email,



                    };



                }

                
            }
            return View(cs);

        }
    }
}

