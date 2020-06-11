using Solution.Domain.Entities;
using Solution.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Web.Models
{
    public class PublicationVM
    {
        public int PublicationId { get; set; }
        public string titlePub { get; set; }
        public string descriptionPub { get; set; }
        //public int nbofsharing { get; set; }
        public string imagePub { get; set; }
        [DataType(DataType.Date)]

        // public string datePub { get; set; }

        public DateTime datePub { get; set; }
        public int nbLike { get; set; }
        public int nbVue { get; set; }
        public int nbDislike { get; set; }
        public Boolean Like { get; set; }
        public Boolean Dislike { get; set; }
        // public string file { get; set; }

        // public int StatFK { get; set; }

        public int ParentFK { get; set; }
        public ICollection<Comment> Comments { get; set; }


        Boolean testLike = false;
        Boolean testdisLike = false;


        IDislikeInterface dislikeService = new DislikeService();
        ILikeService likeService = new LikeService();
        public bool TestLike(int idp,int idu)
        {
            foreach (var like in likeService.GetMany())
            {
                if (like.ParentLike == idu && like.PublicationLike ==idp)
                    testLike = true;

            }
            return testLike;
        }
        public bool TestDislike(int idp, int idu)
        {
            foreach (var like in dislikeService.GetMany())
            {
                if (like.ParentDislike == idu && like.PublicationDislike == idp)
                    testdisLike = true;

            }
            return testdisLike;
        }


    }
}