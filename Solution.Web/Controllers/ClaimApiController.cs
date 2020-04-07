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
    public class ClaimApiController : ApiController
    {
        IClaimService MyService = null;
        private ClaimService ms = new ClaimService();
        List<ClaimModel> reclams = new List<ClaimModel>();
        PidevContext db = new PidevContext();       
        // GET: api/ClaimApi
        [HttpGet]
        [Route("api/Claim")]
        public IHttpActionResult GetAllClaims()
        {
            IList<ClaimModel> students = null;

            using (var ctx = new PidevContext())
            {
                students = ctx.Claims.Include("StudentAddress")
                            .Select(s => new ClaimModel()
                            {
                                ComplaintId = s.ComplaintId,
                                Name = s.Name,
                                Description = s.Description,
                                ClaimDate = s.ClaimDate,
                                ClaimType= s.ClaimType,
                                ParentId=s.ParentId,
                                status=s.status
                            }).ToList<ClaimModel>();
            }

            if (students.Count == 0)
            {
                return NotFound();
            }

            return Ok(students);
        }

        // GET: api/ClaimApi/5
        public Claim Get(int id)
        {
            Claim comp = MyService.GetById(id);

            return comp;
        }
        public ClaimModel registerStudent(Claim studentregd)
        {
            ClaimModel stdregreply = new ClaimModel();
            MyService.Add(studentregd);
            stdregreply.Name = studentregd.Name;
            stdregreply.Description = studentregd.Description;
            stdregreply.ParentId = studentregd.ParentId;
            stdregreply.ClaimDate = DateTime.Today;
            stdregreply.ClaimType = studentregd.ClaimType;



            return stdregreply;
        }
        // POST: api/ClaimApi
        [Route("api/ClaimPost")]
        public IHttpActionResult PostNewFeed(ClaimModel student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new PidevContext())
            {
                ctx.Claims.Add(new Claim()
                {
                    ComplaintId = student.ComplaintId,
                    Description = student.Description,
                    Name = student.Name,
                    ParentId = student.ParentId,
                    ClaimDate = DateTime.Today,
                    ClaimType=student.ClaimType,
                    status = "In_Progress"
                  
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        // PUT: api/ClaimApi/5
        
        public IHttpActionResult Put(ClaimModel student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new PidevContext())
            {
                var existingStudent = ctx.Claims.Where(s => s.ComplaintId == student.ComplaintId)
                                                        .FirstOrDefault<Claim>();
                if (existingStudent != null)
                {
                    existingStudent.Name = student.Name;
                    existingStudent.Description = student.Description;
                    existingStudent.ClaimType = student.ClaimType;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        // DELETE: api/ClaimApi/5
        
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new PidevContext())
            {
                var student = ctx.Claims
                    .Where(s => s.ComplaintId == id)
                    .FirstOrDefault();
                ctx.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}

