using Solution.Data;
using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Solution.Web.Controllers
{
    public class PublicationController : Controller
    {
        IPublicationService Service = null;
        ICommentService Service1 = null;
        IUserService userService = null;
        IDislikeInterface dislikeService = null;
        ILikeService likeService = null;

        public PublicationController()
        {
            Service = new PublicationService();
            Service1 = new CommentService();
            userService = new UserService();
            dislikeService = new DislikeService();
            likeService = new LikeService();

        }

        //Affichage and Recherche
        //  GET: Publication
        Boolean testLike = false;
        Boolean testdisLike = false;

        public ActionResult Index(string searchString)
        {
            var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            List<PublicationVM> pubs = new List<PublicationVM>();
            IEnumerable<Publication> pubDomain = Service.GetMany();
            foreach (var pu in pubDomain)
            {
                pubs.Add(new PublicationVM()
                {
                    PublicationId = pu.PublicationId,
                    nbDislike = pu.nbDislike,
                    nbLike = pu.nbLike,
                    datePub = pu.datePub,
                    imagePub = pu.imagePub,
                    titlePub = pu.titlePub,
                    descriptionPub = pu.descriptionPub,
                    ParentFK = pu.ParentFk
                });

            }


            if (searchString == null)
            {

                return View(pubs);

            }
            else
            {

                return View(pubDomain.Where(p => p.titlePub.StartsWith(searchString) || searchString == null).ToList());
            }
            //return View(Service.GetMany());



        }
        public bool TestLike(Publication p)
        {
            foreach (var like in likeService.GetMany())
            {
                if (like.ParentLike == (int)Session["idu"] && like.PublicationLike == p.PublicationId)
                    testLike = true;

            }
            return testLike;
        }
        public bool TestDislike(Publication p)
        {
            foreach (var like in dislikeService.GetMany())
            {
                if (like.ParentDislike == (int)Session["idu"] && like.PublicationDislike == p.PublicationId)
                    testdisLike = true;

            }
            return testdisLike;
        }
        public ActionResult Details(int id)
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
            Publication conge = Service.GetById(id);
            if (conge == null)
            {
                return HttpNotFound();
            }

            var Comments = new List<CommentVM>();
            foreach (Comment c in Service1.GetMany(c => c.PublicationFK == conge.PublicationId))
            {
                Comments.Add(new CommentVM()
                {
                    CommentId = c.CommentId,
                    post = c.post,
                    dateCom = c.dateCom,
                    nomUser = c.nomUser

                });
                ViewData["comments"] = Comments;
            }
            String nom = userService.GetById(conge.ParentFk).nom;
            String prenom = userService.GetById(conge.ParentFk).prenom;
            ViewBag.nom = nom;
            ViewBag.prenom = prenom;
            return View(conge);
        }

        [HttpPost]
        public ActionResult Details(int? id, string contenu)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication conge = Service.GetById(id.GetValueOrDefault());
            if (conge == null)
            {
                return HttpNotFound();
            }

            var Comments = new List<CommentVM>();
            foreach (Comment c in Service1.GetMany(c => c.PublicationFK == conge.PublicationId))
            {
                Comments.Add(new CommentVM()
                {
                    CommentId = c.CommentId,
                    post = c.post,
                    dateCom = c.dateCom,
                    nomUser = c.nomUser


                });
                ViewData["comments"] = Comments;
            }

            //Comment creation
            if (contenu != null)
            {

                Comment CommentDoamin = new Comment();


                CommentDoamin.post = contenu;
                CommentDoamin.dateCom = DateTime.UtcNow;
                CommentDoamin.nomUser = userService.GetById((int)Session["idu"]).nom;
                CommentDoamin.PublicationFK = conge.PublicationId;


                Service1.Add(CommentDoamin);
                Service1.Commit();



                return PartialView("~/Views/Comment/Create.cshtml", new CommentVM
                {
                    post = CommentDoamin.post,
                    dateCom = CommentDoamin.dateCom,
                    nomUser = CommentDoamin.nomUser,
                });

            }

            return View(conge);
        }






        //  GET: Publication/Details/5
        //details and comment
        public ActionResult DetailsComment(int id)
        {
            var cur = new List<Comment>();

            foreach (Comment ce in Service1.GetMany())
            {
                if (ce.PublicationFK == id)
                    cur.Add(ce);
            }

            ViewBag.myComments = new List<Comment>(cur);
            ViewBag.pub = id;

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Publication p = Service.GetById(id);
            PublicationVM p1 = new PublicationVM()
            {
                datePub = p.datePub,
                imagePub = p.imagePub,
                nbLike = p.nbLike,
                nbDislike = p.nbDislike,
                titlePub = p.titlePub,
                descriptionPub = p.descriptionPub,
                Dislike = p.Dislike,
                Like = p.Like,
                ParentFK = p.ParentFk,
                nbVue = p.nbVue
            };
            if (p == null)
                return HttpNotFound();


            return View(p1);
        }


        // GET: Publication/Create
        public ActionResult Create()
        {
            var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            return View();
        }

        // POST: Publication/Create
        [HttpPost]
        public ActionResult Create(PublicationVM publicationvm, HttpPostedFileBase file)
        {

            if (!ModelState.IsValid || file == null || file.ContentLength == 0)
            {
                RedirectToAction("Create");
            }

            Publication PublicationDomain = new Publication()
            {


                datePub = DateTime.Today,
                imagePub = file.FileName,
                nbLike = publicationvm.nbLike,
                nbDislike = publicationvm.nbDislike,
                titlePub = publicationvm.titlePub,
                descriptionPub = publicationvm.descriptionPub,
                Dislike = publicationvm.Dislike,
                Like = publicationvm.Like,
                ParentFk = (int)Session["idu"],
            };

            Service.Add(PublicationDomain);
            Service.Commit();
            var fileName = "";
            if (file.ContentLength > 0)
            {
                fileName = Path.GetFileName(file.FileName);
                var path = Path.
                    Combine(Server.MapPath("~/Content/Uploads/"),
                    fileName);
                file.SaveAs(path);
            }
            //Service.Dispose();

            return RedirectToAction("Index");

        }

        // GET: Publication/Edit/5
        public ActionResult Edit(int id)
        {
            var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            if (id == null)

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Publication p = Service.GetById(id);
            PublicationVM p1 = new PublicationVM()
            {
                //datePub = p.datePub,
                datePub = DateTime.Today,
                imagePub = p.imagePub,
                //imagePub = file.FileName,
                nbLike = p.nbLike,
                nbDislike = p.nbDislike,
                titlePub = p.titlePub,
                descriptionPub = p.descriptionPub,
                //nbofsharing = p.nbofsharing,
                Dislike = p.Dislike,
                Like = p.Like,
                ParentFK = p.ParentFk,
                //  file = p.file,



            };
            if (p == null)
                return HttpNotFound();
            return View(p1);
        }

        // POST: Publication/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PublicationVM pubvm, HttpPostedFileBase file)
        {




            Publication p = Service.GetById(id);



            p.datePub = DateTime.Today;
            p.imagePub = file.FileName;
            p.nbLike = pubvm.nbLike;
            p.nbDislike = pubvm.nbDislike;
            p.titlePub = pubvm.titlePub;
            p.descriptionPub = pubvm.descriptionPub;
            //p.nbofsharing = pubvm.nbofsharing;
            p.Dislike = pubvm.Dislike;
            p.Like = pubvm.Like;
            //  p.file = file.FileName;

            var fileName = "";
            if (file.ContentLength > 0)
            {
                fileName = Path.GetFileName(file.FileName);
                var path = Path.
                    Combine(Server.MapPath("~/Content/Uploads/"),
                    fileName);
                file.SaveAs(path);
            }

            if (p == null)
                return HttpNotFound();

            Console.WriteLine("updaaaaaaaaaaaate");
            Service.Update(p);
            Service.Commit();
            // Service.Dispose();

            return RedirectToAction("Index");

            // TODO: Add delete logic here
            return View(pubvm);



        }


        [HttpGet]
        public RedirectToRouteResult Like(int id, PublicationVM pubvm, HttpPostedFileBase file)
        {

            Like like = new Like()
            {
                ParentLike = (int)Session["idu"],
                PublicationLike = id
            };
            likeService.Add(like);
            likeService.Commit();
            Service.Like(id);
            Dislike dislike = null;
            var like1 = dislikeService.GetMany();
            foreach (var l in like1)
            {
                if (l.ParentDislike == (int)Session["idu"] && l.PublicationDislike == id)
                    dislike = l;
            }
            if (dislike != null)
            {
                dislikeService.Delete(dislike);
                dislikeService.Commit();
                Service.annud(id);
            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        public RedirectToRouteResult AnnulerLike(int id, PublicationVM pubvm)
        {
            Like like = null;
            var like1 = likeService.GetMany();
            foreach (var l in like1)
            {
                if (l.ParentLike == (int)Session["idu"] && l.PublicationLike == id)
                    like = l;
            }

            likeService.Delete(like);
            likeService.Commit();
            Service.annul(id);



            return RedirectToAction("Index");

        }

        [HttpGet]
        public RedirectToRouteResult Dislike(int id, PublicationVM pubvm, HttpPostedFileBase file)
        {



            Dislike dislike = new Dislike()
            {
                ParentDislike = (int)Session["idu"],
                PublicationDislike = id
            };
            dislikeService.Add(dislike);
            dislikeService.Commit();

            Service.Dislike(id);
            Like like = null;
            var like1 = likeService.GetMany();
            foreach (var l in like1)
            {
                if (l.ParentLike == (int)Session["idu"] && l.PublicationLike == id)
                    like = l;
            }
            if (like != null)
            {
                likeService.Delete(like);
                likeService.Commit();
                Service.annul(id);
            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        public RedirectToRouteResult AnnulerDisLike(int id, PublicationVM pubvm)
        {
            Dislike dislike = null;
            var like1 = dislikeService.GetMany();
            foreach (var l in like1)
            {
                if (l.ParentDislike == (int)Session["idu"] && l.PublicationDislike == id)
                    dislike = l;
            }

            dislikeService.Delete(dislike);
            dislikeService.Commit();
            Service.annud(id);



            return RedirectToAction("Index");

        }







        public async Task<ActionResult> Delete(int id)
        {
            try
            {

                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Publication p = Service.GetById(id);

                if (p == null)
                    return HttpNotFound();
                Console.WriteLine("deletedddddddddddddddddddddddddddddddd");
                Service.Delete(p);
                Service.Commit();
                // Service.Dispose();

                return RedirectToAction("Index");

                // TODO: Add delete logic here

            }
            catch
            {
                return View();
            }
        }
    }

}
