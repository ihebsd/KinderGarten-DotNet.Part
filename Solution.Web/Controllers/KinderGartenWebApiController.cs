using Solution.Data;
using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web.Models;
using System;
using System.Collections.Generic;
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
        private KinderGartenService ms = new KinderGartenService();
        List<KinderGartenModel> reclams = new List<KinderGartenModel>();
        PidevContext db = new PidevContext();
        public KinderGartenWebApiController()
        {
            MyService = new KinderGartenService();
            Index();
            reclams = Index().ToList();

        }
        public List<KinderGartenModel> Index()
        {
            List<KinderGarten> mandates = ms.GetMany().ToList();
            List<KinderGartenModel> mandatesXml = new List<KinderGartenModel>();
            foreach (KinderGarten p in mandates)
            {
                mandatesXml.Add(new KinderGartenModel
                {
                    KinderGartenId = p.KinderGartenId,
                    Name = p.Name,
                    Image = p.Image,
                    Address = p.Address,
                    NbrEmp = p.NbrEmp,
                    Cost = p.Cost,
                    Phone = p.Phone,
                    Description = p.Description,

                });
            }
            return mandatesXml;
        }
        // GET: api/FeedBackApi
        [HttpGet]
        [Route("api/KinderGarten")]
        public IEnumerable<KinderGartenModel> Get()
        {
            return reclams;
        }

        // GET: api/FeedBackApi/5
        public KinderGarten Get(int id)
        {
            KinderGarten comp = MyService.GetById(id);

            return comp;
        }
        public KinderGartenModel registerStudent(KinderGarten studentregd)
        {
            KinderGartenModel stdregreply = new KinderGartenModel();
            MyService.Add(studentregd);
            stdregreply.Description = studentregd.Description;
            stdregreply.DirecteurId = studentregd.DirecteurId;
            stdregreply.DateCreation = DateTime.Today;

            return stdregreply;
        }
        // POST: api/FeedBackApi
        [Route("api/KinderGarten")]
        public IHttpActionResult PostNewFeed(KinderGartenModel p)
        {


            using (var ctx = new PidevContext())
            {
                ctx.KinderGartens.Add(new KinderGarten()
                {
                    KinderGartenId=p.KinderGartenId,
                    Name = p.Name,
                    Image = p.Image,
                    Address = p.Address,
                    NbrEmp = p.NbrEmp,
                    Cost = p.Cost,
                    Phone = p.Phone,
                    Description = p.Description,
                    DirecteurId =p.DirecteurId,
                    DateCreation=DateTime.Now,
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        // PUT: api/FeedBackApi/5
        public IHttpActionResult Put(KinderGartenModel student)
        {
            
            using (var ctx = new PidevContext())
            {
                var existingStudent = ctx.KinderGartens.Where(s => s.KinderGartenId == student.KinderGartenId)
                                                        .FirstOrDefault<KinderGarten>();

                if (existingStudent != null)
                {
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

        // DELETE: api/FeedBackApi/5
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new PidevContext())
            {
                var student = ctx.KinderGartens
                    .Where(s => s.KinderGartenId == id)
                    .FirstOrDefault();
                ctx.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
