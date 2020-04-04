using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Solution.Web.Controllers
{
    public class FeedBackController : Controller
    {
        IFeedBackService RepService;
        IUserService userService;
        FeedBackModel feedbackM;
        public FeedBackController()
        {
            RepService = new FeedBackService();
            userService = new UserService();
            feedbackM = new FeedBackModel();
        }
        // GET: Reputation
        public ActionResult Index()
        {
            var maliste = new List<FeedBackModel>();
            List<FeedBack> feedbacks = RepService.GetMany().ToList();

            foreach (var f in feedbacks)
            {
                string sentiment = "Neutre";
                string content = f.Description;
                if (content.Contains("bad"))
                {
                    sentiment = "Negative";
                }
                if (content.Contains("good"))
                {
                    sentiment = "Positive";
                }

                maliste.Add(new FeedBackModel
                {
                    FeedBackId = f.FeedBackId,
                    FeedBackDate=f.FeedBackDate,
                    sentiment = sentiment,                   
                    Description = f.Description,
                    ParentId =f.ParentId
                });
            }
            return View(maliste);
        }

        // GET: Reputation/Create
        public ActionResult Create()
        {
            var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            return View();
        }

        // POST: Reputation/Create
        [HttpPost]
        public ActionResult Create(FeedBackModel rm)
        {
            DateTime today = DateTime.Now;
            FeedBack rep = new FeedBack()
            {              
                FeedBackDate = today,
                Description = rm.Description,
                ParentId = (int)Session["idu"],

            };
            RepService.Add(rep);
            RepService.Commit();


            return RedirectToAction("Index","Home");
        }
        // GET: Reputation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reputation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            FeedBack C = RepService.GetById((int)id);
            RepService.Delete(C);
            RepService.Commit();
            return RedirectToAction("Index");
        }      
    }

}
