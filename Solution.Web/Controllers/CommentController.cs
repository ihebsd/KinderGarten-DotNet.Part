using Solution.Data;
using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Solution.Web.Controllers
{
    public class CommentController : Controller
    {
        //  IPublicationService raid = null;
        ICommentService Service = null;
        IUserService userService = null;
        public CommentController()
        {
            Service = new CommentService();
            userService = new UserService();
            //   raid = new PublicationService();
        }
        // GET: Comment
        public ActionResult Index()
        {
            return View(Service.GetMany());
        }

        // GET: Comment/Details/5
        //public ActionResult Details(int id)
        //{
        //    if (id == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    Comment p = Service.GetById(id);
        //    CommentVM p1 = new CommentVM()
        //    {
        //        post = p.post,
        //        imageCom = p.imageCom,
        //        dateCom = p.dateCom,
        //        PublicationFK = p.PublicationFK,
        //    };
        //    if (p == null)
        //        return HttpNotFound();

        //    return View(p1);
        //}

        // GET: Comment/Create
        PidevContext db = new PidevContext();
        public ActionResult Create()
        {


            return View();
        }

        // POST: Comment/Create
        [HttpPost]
        public ActionResult Create(int id, CommentVM comVm)
        {
            Comment CommentDomain = new Comment()
            {

                post = "hhh",
                //  imageCom = comVm.imageCom,
                dateCom = DateTime.Now,
                //  dateCom = comVm.dateCom,
                PublicationFK = id,
                nomUser = userService.GetById((int)Session["idu"]).nom

            };
            Service.Add(CommentDomain);
            Service.Commit();
            //Service.Dispose();
            // return RedirectToAction("Index");
            return RedirectToAction("Index", "Publication");
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Comment p = Service.GetById(id);
            CommentVM p1 = new CommentVM()
            {
                post = p.post,
                // imageCom = p.imageCom,
                //dateCom = p.dateCom,
                dateCom = DateTime.Today,
                PublicationFK = p.PublicationFK,

            };
            if (p == null)
                return HttpNotFound();
            return View(p1);
        }

        // POST: Comment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(int id, CommentVM comVm)
        {

            Comment p = Service.GetById(id);
            p.post = comVm.post;
            // p.imageCom = comVm.imageCom;
            p.dateCom = DateTime.Now;
            Service.Update(p);
            Service.Commit();
            return RedirectToAction("Details", "Publication", new { id = p.PublicationFK });










            // return RedirectToAction("Index");

            // TODO: Add delete logic here

        }

        // GET: Comment/Delete/5


        public async Task<ActionResult> Delete(int id, CommentVM comVm)
        {


            Comment p = Service.GetById(id);

            if (p == null)
                return HttpNotFound();
            Console.WriteLine("deletedddddddddddddddddddddddddddddddd");
            Service.Delete(p);
            Service.Commit();
            // Service.Dispose();

            return RedirectToAction("Details", "Publication", new { id = p.PublicationFK });
        }



    }
}

