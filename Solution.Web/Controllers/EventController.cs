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
        public ActionResult Index(string searchString)
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
            return View(events);

        }

        // GET: Event/Details/5
        public ActionResult Details(int? id)
        {
            var userId = (int)Session["idu"];
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
            return View();
        }
        public ActionResult Create()
        {         
            return View();
        }

        // POST: KinderGarten/Create
        [HttpPost]
        public ActionResult Create(EventModel em, HttpPostedFileBase Image)
        {
            Event ev = new Event();

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
        public ActionResult Edit(int id, FormCollection collection)
        {
            Event e = EventService.GetById(id);
            EventModel em = new EventModel();

            em.Name = e.Name;
            //ImageUrl = Image.FileName,
            em.Category = e.Category;
            em.DateEvent = e.DateEvent;
            em.Description = e.Description;
            em.number_P = e.number_P;            
            return View(em);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Event/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, EventModel EventModel)
        {
            Event t = EventService.GetById(id);
            EventService.Delete(t);
            EventService.Commit();

            return RedirectToAction("Index");
        }
    }
}
