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

namespace Solution.Web.Controllers.Api
{
    public class FeedBackApiController : ApiController 
    {
        IFeedBackService MyService = null;
        private FeedBackService ms = new FeedBackService();
        List<FeedBackModel> reclams = new List<FeedBackModel>();
        PidevContext db = new PidevContext();
        public FeedBackApiController()
        {
            MyService = new FeedBackService();
            Index();
            reclams = Index().ToList();

        }
        public List<FeedBackModel> Index()
        {
            List<FeedBack> mandates = ms.GetMany().ToList();
            List<FeedBackModel> mandatesXml = new List<FeedBackModel>();
            foreach (FeedBack i in mandates)
            {
                mandatesXml.Add(new FeedBackModel
                {
                    Description = i.Description,
                    FeedBackId = i.FeedBackId,
                    FeedBackDate = i.FeedBackDate,
                    sentiment = i.sentiment,                  
                    ParentId = i.ParentId,
                });
            }
            return mandatesXml;
        }
        // GET: api/FeedBackApi
        [HttpGet]
        [Route("api/FeedBack")]
        public IEnumerable<FeedBackModel> Get()
        {
            return reclams;
        }

        // GET: api/FeedBackApi/5
        public FeedBack Get(int id)
        {
            FeedBack comp = MyService.GetById(id);

            return comp;
        }
        public FeedBackModel registerStudent(FeedBack studentregd)
        {
            FeedBackModel stdregreply = new FeedBackModel();
            MyService.Add(studentregd);
            stdregreply.Description = studentregd.Description;
            stdregreply.ParentId = 2;
            stdregreply.FeedBackDate = DateTime.Today;

            return stdregreply;
        }
        // POST: api/FeedBackApi
        [Route("api/FeedPost")]
        public IHttpActionResult PostNewFeed(FeedBackModel student)
        {
           
           

            using (var ctx = new PidevContext())
            {
                ctx.Reputations.Add(new FeedBack()
                {
                    FeedBackId = student.FeedBackId,
                    Description = student.Description,
                    ParentId = 2,
                    FeedBackDate = DateTime.Today
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        // PUT: api/FeedBackPut/5
        public IHttpActionResult Put(FeedBackModel student)
        {
            

            using (var ctx = new PidevContext())
            {
                var existingStudent = ctx.Reputations.Where(s => s.FeedBackId == student.FeedBackId)
                                                        .FirstOrDefault<FeedBack>();

                if (existingStudent != null)
                {
                    existingStudent.Description = student.Description;
                    existingStudent.ParentId = 2;
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
                var student = ctx.Reputations
                    .Where(s => s.FeedBackId == id)
                    .FirstOrDefault();
                ctx.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
