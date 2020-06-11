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
    public class PublicationApiController : ApiController
    {
        IPublicationService MyService = null;
        ICommentService CommentService = null;
        IDislikeInterface dislikeService = null;
        ILikeService likeService = null;
        List<PublicationVM> publicationsModels = new List<PublicationVM>();
        PidevContext db = new PidevContext();
        public PublicationApiController()
        {
            MyService = new PublicationService();
            CommentService = new CommentService();
            dislikeService = new DislikeService();
            likeService = new LikeService();
            Index();
            publicationsModels = Index().ToList();

        }
        public List<PublicationVM> Index()
        {
            List<Publication> publications = MyService.GetMany().ToList();
            List<PublicationVM> publicationModels1 = new List<PublicationVM>();
            foreach (Publication p in publications)
            {
                publicationModels1.Add(new PublicationVM
                {
                    PublicationId = p.PublicationId,
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


                });
            }
            return publicationModels1;
        }
        [HttpGet]
        [Route("api/PublicationApi")]
        public IEnumerable<PublicationVM> Get()
        {
            return publicationsModels;
        }
        [HttpGet]
        [Route("api/PublicationApi/Details")]
        public IEnumerable<PublicationVM> Get(int id)
        {
            Publication p = MyService.GetById(id);
            PublicationVM publication1 = new PublicationVM()
            {
                PublicationId = p.PublicationId,
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
            List<PublicationVM> publicationsModels = new List<PublicationVM>();
            publicationsModels.Add(publication1);
            return publicationsModels;
        }

        [Route("api/PublicationApi/Create")]
        public IHttpActionResult Create(PublicationVM p)
        {


            using (var ctx = new PidevContext())
            {
                ctx.Publications.Add(new Publication()
                {
                    datePub = DateTime.Now,
                    imagePub = p.imagePub,
                    nbLike = p.nbLike,
                    nbDislike = p.nbDislike,
                    titlePub = p.titlePub,
                    descriptionPub = p.descriptionPub,
                    Dislike = p.Dislike,
                    Like = p.Like,
                    ParentFk = p.ParentFK,
                    nbVue = p.nbVue

                });

                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                ctx.SaveChanges();


            }

            return Ok();
        }
        [Route("api/PublicationApi/Put")]

        public IHttpActionResult Put(int id, PublicationVM p)
        {

            Publication pu = MyService.GetById(id);

            if (pu != null)
            {
                pu.datePub = DateTime.Now;
                pu.imagePub = p.imagePub;
                pu.nbLike = p.nbLike;
                pu.nbDislike = p.nbDislike;
                pu.titlePub = p.titlePub;
                pu.descriptionPub = p.descriptionPub;
                pu.Dislike = p.Dislike;
                pu.Like = p.Like;
                pu.nbVue = p.nbVue;
                MyService.Update(pu);
                MyService.Commit();
            }
            else
            {
                return NotFound();
            }


            return Ok();
        }
        [Route("api/PublicationApi/Delete")]

        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid publication id");

            using (var ctx = new PidevContext())
            {
                var publication = ctx.Publications
                    .Where(k => k.PublicationId == id)
                    .FirstOrDefault();
                ctx.Entry(publication).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
        [HttpGet]
        [Route("api/like")]

        public IHttpActionResult Like(int idp, int idu)
        {



            Like like = new Like()
            {
                ParentLike = idu,
                PublicationLike = idp
            };
            likeService.Add(like);
            likeService.Commit();
            MyService.Like(idp);
            Dislike dislike = null;
            var like1 = dislikeService.GetMany();
            foreach (var l in like1)
            {
                if (l.ParentDislike == idu && l.PublicationDislike == idp)
                    dislike = l;
            }
            if (dislike != null)
            {
                dislikeService.Delete(dislike);
                dislikeService.Commit();
                MyService.annud(idp);
            }
            return Ok();

        }
        [HttpGet]
        [Route("api/annulerlike")]

        public IHttpActionResult AnnulerLike(int idp, int idu)
        {

            Like like = null;
            var like1 = likeService.GetMany();
            foreach (var l in like1)
            {
                if (l.ParentLike == idu && l.PublicationLike == idp)
                    like = l;
            }

            likeService.Delete(like);
            likeService.Commit();
            MyService.annul(idp);



            return Ok();

        }


        [HttpGet]
        [Route("api/dislike")]
        public IHttpActionResult Dislike(int idp, int idu)
        {



            Dislike dislike = new Dislike()
            {
                ParentDislike = idu,
                PublicationDislike = idp
            };
            dislikeService.Add(dislike);
            dislikeService.Commit();

            MyService.Dislike(idp);
            Like like = null;
            var like1 = likeService.GetMany();
            foreach (var l in like1)
            {
                if (l.ParentLike == idu && l.PublicationLike == idp)
                    like = l;
            }
            if (like != null)
            {
                likeService.Delete(like);
                likeService.Commit();
                MyService.annul(idp);
            }
            return Ok();

        }

        [HttpGet]
        [Route("api/annulerdislike")]
        public IHttpActionResult annulerDislike(int idp, int idu)
        {


            Dislike dislike = null;
            var like1 = dislikeService.GetMany();
            foreach (var l in like1)
            {
                if (l.ParentDislike == idu && l.PublicationDislike == idp)
                    dislike = l;
            }

            dislikeService.Delete(dislike);
            dislikeService.Commit();
            MyService.annud(idp);

            return Ok();

        }
        [HttpGet]
        [Route("api/Comments")]
        public IEnumerable<CommentVM> CommentsByPub(int id)
        {
            List<CommentVM> commentVMs = new List<CommentVM>();

            using (var ctx = new PidevContext())
            {
                var comments = ctx.Comments.Where(c => c.PublicationFK == id).ToList();

                foreach (var c in comments)
                {
                    commentVMs.Add(new CommentVM()
                    {
                        CommentId = c.CommentId,
                        dateCom = c.dateCom,
                        nomUser = c.nomUser,
                        post = c.post,
                        PublicationFK = c.PublicationFK
                    });
                }

            }



            return commentVMs;

        }

        [Route("api/Comment/Create")]
        public IHttpActionResult CreateComm(int id, CommentVM p)
        {


            using (var ctx = new PidevContext())
            {
                ctx.Comments.Add(new Comment()
                {
                    post = p.post,
                    dateCom = DateTime.Now,
                    PublicationFK = id,
                    nomUser = p.nomUser

                });

                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                ctx.SaveChanges();


            }

            return Ok();
        }
        [Route("api/Comment/Put")]

        public IHttpActionResult PutComm(int id, CommentVM p)
        {

            Comment pu = CommentService.GetById(id);

            if (pu != null)
            {
                pu.dateCom = DateTime.Now;
                pu.post = p.post;
                CommentService.Update(pu);
                CommentService.Commit();
            }
            else
            {
                return NotFound();
            }


            return Ok();
        }
        [HttpGet]
        [Route("api/testLike")]
        public IEnumerable<bool> TestLike(int idp,int idu)
        {
            bool Status = false;
          
                foreach (var like in likeService.GetMany())
                {
                    if (like.ParentLike == idu && like.PublicationLike == idp)
                        Status = true;

                }
               
            

            return new List<bool> { Status };
        }
        [HttpGet]
        [Route("api/testDislike")]
        public IEnumerable<bool> TestDislike(int idp, int idu)
        {
            bool Status = false;
           
               
                foreach (var like in dislikeService.GetMany())
                {
                    if (like.ParentDislike == idu && like.PublicationDislike == idp)
                        Status = true;

                }
            

            return new List<bool> { Status };
        }
        [Route("api/Comment/Delete")]

        public IHttpActionResult DeleteComm(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid publication id");

            using (var ctx = new PidevContext())
            {
                var comment = ctx.Comments
                    .Where(k => k.CommentId == id)
                    .FirstOrDefault();
                ctx.Entry(comment).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
