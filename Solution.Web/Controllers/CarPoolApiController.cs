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
    public class CarPoolApiController : ApiController
    {

        ICarPoolService MyService = null;
        IKidService ServicePar = null;
        private CarPoolService es = new CarPoolService();
        List<CarPoolModel> Carpools = new List<CarPoolModel>();
        List<KidModel> Kids = new List<KidModel>();


        public CarPoolApiController()
        {
            ServicePar = new KidService();
            MyService = new CarPoolService();
            Index();
            Carpools = Index().ToList();
        }
        public List<CarPoolModel> Index()
        {
            List<CarPool> mandates = es.GetMany().ToList();
            List<CarPoolModel> mandatesXml = new List<CarPoolModel>();
            foreach (CarPool c in mandates)
            {
                mandatesXml.Add(new CarPoolModel
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



                });
            }
            return mandatesXml;
        }


        // GET api/CarpoolWebApi
        [HttpGet]
        public IEnumerable<CarPoolModel> Get()
        {
            return Carpools;
        }

        // GET api/<controller>/5
        public CarPool Get(int id)
        {
            CarPool ev = MyService.GetById(id);


            return ev;
        }

        [Route("api/MyCar")]
        public IEnumerable<CarPoolModel> GetByIdParent(int idp)
        {
            List<CarPool> car;
            using (var ctx = new PidevContext())
            {
                 car = ctx.CarPools.Where(c => c.idParent == idp).ToList();
                
                    }
            List<CarPoolModel> mandatesXml = new List<CarPoolModel>();
            foreach (CarPool c in car)
            {
                mandatesXml.Add(new CarPoolModel
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



                });
            }
            return mandatesXml;
        }

        // POST: api/CarpoolWebApi
        [Route("api/CarPost")]
        public IHttpActionResult PostNewFeed(CarPoolModel collection)
        {


            using (var ctx = new PidevContext())
            {
                ctx.CarPools.Add(new CarPool()
                {

                    idParent = collection.idParent,
                    Id = collection.Id,
                    Title = collection.Title,
                    From = collection.From,
                    To = collection.To,
                    Time = collection.Time,
                    Date = collection.Date,
                    Message = collection.Message,
                    NbPlaceDispo = collection.NbPlaceDispo,
                    idKid = 5,
                    Weekly = collection.Weekly,
                    Daily = collection.Daily,
                    EveryWeekDay = collection.EveryWeekDay,
                    UntilDate = collection.UntilDate,


                }); ;

                ctx.SaveChanges();
            }

            return Ok();
        }

        // PUT: api/CarpoolWebApi/5
        [Route("api/Car/Put")]
        public IHttpActionResult Put(int idcar, CarPoolModel student)
        {

            CarPool existingStudent = MyService.GetById(idcar);

            if (existingStudent != null)
                {

                    existingStudent.Title = student.Title;
                    existingStudent.From = student.From;
                    existingStudent.Time = student.Time;
                    existingStudent.To = student.To;
                    existingStudent.Date = student.Date;
                    existingStudent.NbPlaceDispo = student.NbPlaceDispo;
                    existingStudent.Weekly = student.Weekly;
                    existingStudent.Daily = student.Daily;
                    existingStudent.EveryWeekDay = student.EveryWeekDay;
                    existingStudent.Message = student.Message;
                    existingStudent.idKid = 5;
                    existingStudent.UntilDate = student.UntilDate;
                MyService.Update(existingStudent);
                MyService.Commit();
            }
                else
                {
                    return NotFound();
                }
            

            return Ok();
        }


        // DELETE: api/CarPoolWebApi/5
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new PidevContext())
            {
                var student = ctx.CarPools
                    .Where(s => s.Id == id)
                    .FirstOrDefault();
                ctx.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}