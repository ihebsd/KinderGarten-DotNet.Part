using Solution.Data;
using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Solution.Web.Controllers
{
    public class KinderGartenWebApiController : ApiController
    {
        IKinderGartenService MyService = null;
        private KinderGartenService ks = new KinderGartenService();
        List<KinderGartenModel> kinderGartenModels = new List<KinderGartenModel>();
        PidevContext db = new PidevContext();
        public KinderGartenWebApiController()
        {
            MyService = new KinderGartenService();
            Index();
            kinderGartenModels = Index().ToList();

        }
        public List<KinderGartenModel> Index()
        {
            List<KinderGarten> kinderGartens = ks.GetMany().ToList();
            List<KinderGartenModel> kinderGartenModels1 = new List<KinderGartenModel>();
            foreach (KinderGarten p in kinderGartens)
            {
                kinderGartenModels1.Add(new KinderGartenModel
                {
                    KinderGartenId = p.KinderGartenId,
                    Name = p.Name,
                    Image = p.Image,
                    Address = p.Address,
                    NbrEmp = p.NbrEmp,
                    Cost = p.Cost,
                    Phone = p.Phone,
                    Description = p.Description,
                    DateCreation = p.DateCreation,
                    longitude=p.longitude,
                    latitude=p.latitude,
                    DirecteurId=p.DirecteurId,


                });
            }
            return kinderGartenModels1;
        }
        [HttpGet]
        [Route("api/KinderGarten")]
        public IEnumerable<KinderGartenModel> Get()
        {
            return kinderGartenModels;
        }
        [HttpGet]
        [Route("api/KinderGarten/Details")]
        public KinderGarten Get(int id)
        {
            KinderGarten Kinder = MyService.GetById(id);

            return Kinder;
        }

        [Route("api/KinderGarten/Create")]
        public IHttpActionResult Create(KinderGartenModel kgm)
        {


            using (var ctx = new PidevContext())
            {
                ctx.KinderGartens.Add(new KinderGarten()
                {
                    Name = kgm.Name,
                    Image = kgm.Image,
                    Address = kgm.Address,
                    NbrEmp = kgm.NbrEmp,
                    Cost = kgm.Cost,
                    Phone = kgm.Phone,
                    Description = kgm.Description,
                    DirecteurId = kgm.DirecteurId,
                    DateCreation = DateTime.Now,
                    latitude=kgm.latitude,
                    longitude=kgm.longitude,
                    Votes = "0,0,0,0,0"
                });

                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                ctx.SaveChanges();


            }

            return Ok();
        }
        [Route("api/KinderGarten/Put")]

        public IHttpActionResult Put(int id, KinderGartenModel kgm)
        {


            KinderGarten kg = MyService.GetById(id);

                if (kg != null)
                {
                    kg.Address = kgm.Address;
                    kg.Description = kgm.Description;
                    kg.Cost = kgm.Cost;
                    kg.Name = kgm.Name;
                    kg.NbrEmp = kgm.NbrEmp;
                    kg.Phone = kgm.Phone;
                    kg.Image = kgm.Image;
                kg.longitude = kgm.longitude;
                kg.latitude = kgm.latitude;
                MyService.Update(kg);
                MyService.Commit();
                }
                else
                {
                    return NotFound();
                }
            

            return Ok();
        }
        [Route("api/KinderGarten/Delete")]

        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid KinderGarten id");

            using (var ctx = new PidevContext())
            {
                var Kinder = ctx.KinderGartens
                    .Where(k => k.KinderGartenId == id)
                    .FirstOrDefault();
                ctx.Entry(Kinder).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
