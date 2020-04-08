using Solution.Data;
using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Solution.Web.Controllers
{
    public class EventWebApiController : ApiController
    {

        IEventService MyService = null;
        private EventService es = new EventService();
        List<EventModel> events = new List<EventModel>();

        public EventWebApiController()
        {
            MyService = new EventService();
            Index();
            events = Index().ToList();
        }
        public List<EventModel> Index()
        {
            List<Event> mandates = es.GetMany().ToList();
            List<EventModel> mandatesXml = new List<EventModel>();
            foreach (Event i in mandates)
            {
                mandatesXml.Add(new EventModel
                {
                    EventId = i.EventId,
                    DateEvent = i.DateEvent,
                    Name = i.Name,
                    Description = i.Description,
                    number_P = i.number_P,
                    Category = i.Category,
                    HeureD = i.HeureD,
                    HeureF= i.HeureF                   

                });
            }
            return mandatesXml;
        }
        // GET api/EventWebApi
        [HttpGet]
        public IEnumerable<EventModel> Get()
        {
            return events;
        }

        // GET api/<controller>/5
        public Event Get(int id)
        {
            Event ev = MyService.GetById(id);

            return ev;
        }

        // POST: api/EventWebApi
        [Route("api/EventPost")]
        public IHttpActionResult PostNewFeed(EventModel postt)
        {

            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new PidevContext())
            {
                ctx.Events.Add(new Event()
                {
                    EventId = postt.EventId,
                    DateEvent = DateTime.Today,
                    Name = postt.Name,
                    AdminConfirmtion = false,
                    image = postt.image,
                    Description = postt.Description,
                    number_P = postt.number_P,
                    Category = postt.Category,   
                    DirecteurFk=postt.DirecteurFK,
                    HeureD = DateTime.Today,
                    HeureF = DateTime.Today

                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        // PUT: api/EventWebApi/5
        public IHttpActionResult Put(EventModel student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new PidevContext())
            {
                var existingStudent = ctx.Events.Where(s => s.EventId == student.EventId)
                                                        .FirstOrDefault<Event>();

                if (existingStudent != null)
                {
                    existingStudent.Name = student.Name;
                    existingStudent.number_P = student.number_P;
                    existingStudent.Category = student.Category;
                    existingStudent.Description = student.Description;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }


        // DELETE: api/EventWebApi/5
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new PidevContext())
            {
                var student = ctx.Events
                    .Where(s => s.EventId == id)
                    .FirstOrDefault();
                ctx.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}