using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;



namespace Solution.Web.Controllers
{
    public class EventController : Controller
    {
        IEventService EventService;
        IUserService userService;

        public EventController()
        {
            EventService = new EventService();
            userService = new UserService();
        }
        // GET: Event

        public ActionResult Index(string searchString, int? i)
        { 
            var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            var events = new List<EventModel>();
            foreach (Event e in EventService.SearchEventByName(searchString))
            {
                EventModel es = new EventModel()
                {
                    AdminConfirmtion = e.AdminConfirmtion,
                    Category =(Category)e.Category,
                    Description = e.Description,
                    Name = e.Name,
                    EventId = e.EventId,
                    image = e.image,
                    DateEvent = e.DateEvent,                
                    

                };
                events.Add(es);
            }
         
            return View(events.ToList().ToPagedList(i ?? 1, 3));

        }

        // GET: Event/Details/5
        public ActionResult Details(int? id)
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
            Event e = EventService.GetById((int)id);
            if (e == null)
            {
                return HttpNotFound();
            }
            EventModel es = new EventModel()
            {
               // AdminConfirmtion = e.AdminConfirmtion,
                DateEvent = e.DateEvent,
                Category = e.Category,
                image = e.image,
                Description = e.Description,
                EventId = e.EventId,
                Name= e.Name,
                
                
                
            };
            return View(es);
        }
        public ActionResult Create()
        {
            int someven = EventService.SumEvent();
            ViewBag.som = someven;

            int somecatother = EventService.SumOther();
            ViewBag.other = somecatother;
            int somecatent = EventService.SumEntr();
            ViewBag.ent = somecatent;
            int somecatedu = EventService.SumEducation();
            ViewBag.edu = somecatedu;




            return View();
        }

        // POST: KinderGarten/Create
        [HttpPost]
        public ActionResult Create(EventModel em, HttpPostedFileBase Image)
        {
          

            Event ev = new Event();
          //  int somcateg = EventService.Sumpercategory(ev.Category);
          //  ViewBag.cat = somcateg;
           
            ev.Name = em.Name;         
            ev.AdminConfirmtion = false;
            ev.Category = em.Category;
            ev.DateEvent = em.DateEvent;
            ev.Description = em.Description;
            ev.image = Image.FileName;
            ev.number_P = em.number_P;
            ev.HeureD = em.HeureD;
            ev.HeureF = em.HeureF;
            ev.DirecteurFk = (int)Session["idu"];
            EventService.Add(ev);
            EventService.Commit();

           var path2 = Path.Combine(Server.MapPath("~/Content/Uploads"), Image.FileName);
            Image.SaveAs(path2);
            return RedirectToAction("Index");
        }


        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            Event e = EventService.GetById(id);
            EventModel em = new EventModel();
            em.Name = e.Name;
            em.number_P = e.number_P;
            em.image = e.image;
            em.Category = e.Category;
            em.DateEvent = e.DateEvent;
            em.Description = e.Description;
            return View(em);
        }


        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EventModel em)
        {
            try
            {
                Event e = EventService.GetById(id);
                em.Name = e.Name;
                //ImageUrl = Image.FileName,
                em.Category = e.Category;
                em.DateEvent = e.DateEvent;
                em.Description = e.Description;
                em.number_P = e.number_P;
                EventService.Update(e);
                EventService.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(em);
            }
        }


        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {

            if (id == null)

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Event p = EventService.GetById(id);
            EventModel p1 = new EventModel()
            {
                Name = p.Name,
                Category = p.Category,
                DateEvent = p.DateEvent,
                image = p.image,
                HeureF = p.HeureF,
                HeureD=p.HeureD,           
                Description = p.Description,         


            };
            if (p == null)
                return HttpNotFound();
            return View(p1);
        }

        // POST: Event/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, EventModel evm)
        {

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Event p = EventService.GetById(id);
            evm = new EventModel()
            {
                Name = p.Name,
                Category = p.Category,
                DateEvent = p.DateEvent,
                image = p.image,
                HeureF = p.HeureF,
                HeureD = p.HeureD,
                Description = p.Description,

            };
            if (p == null)
                return HttpNotFound();
            Console.WriteLine("deletedddddddddddddddddddddddddddddddd");
            EventService.Delete(p);
            EventService.Commit();
            // Service.Dispose();

            return RedirectToAction("Index");

        }
    }
}
