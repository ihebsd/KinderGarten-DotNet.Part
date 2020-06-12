

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
using System.Web;
using System.Web.Mvc;

namespace Solution.Web.Controllers
{
    public class KinderGartenController : Controller
    {
        PidevContext db = new PidevContext();
        IKinderGartenService KindergartenService;
        IUserService userService;
        public KinderGartenController()
        {
            KindergartenService = new KinderGartenService();
            userService = new UserService();
        }

        // GET: KinderGarten
        public ActionResult Index(string searchString)
        {
            int userId;
            try { userId = (int)Session["idu"]; }
            catch
            {
                return RedirectToAction("Login", "Login");
            }



            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            var kindergartens = new List<KinderGartenModel>();
            foreach (KinderGarten k in KindergartenService.SearchKindergartenByName(searchString))
            {
                KinderGartenModel ks = new KinderGartenModel()
                {
                    Address = k.Address,
                    Cost = k.Cost,
                    DateCreation = k.DateCreation,
                    Description = k.Description,
                    KinderGartenId = k.KinderGartenId,
                    Image = k.Image,
                    Name = k.Name,
                    NbrEmp = k.NbrEmp,
                    Phone = k.Phone,
                    DirecteurId = k.DirecteurId,
                    nbVue = k.nbVue,
                    Votes = k.Votes
                };
                kindergartens.Add(ks);

            }


            return View(kindergartens);
        }
        public bool HasVoted(int? idK)
        {
            if (idK.HasValue)
            {

                string usern = userService.GetById((int)Session["idu"]).prenom;

                var isIt = db.VoteModels.Where(v => v.UserName.Equals(usern, StringComparison.CurrentCultureIgnoreCase)
                && v.VoteForId == idK).FirstOrDefault();
                if (isIt != null)
                {
                    return true;
                }
            }
            return false;
        }

        // GET: KinderGarten/Details/5
        public ActionResult Details(int? id)
        {
            bool test = HasVoted(id);
            ViewBag.test = test;
            int userId;
            try { userId = (int)Session["idu"]; }
            catch
            {
                return RedirectToAction("Login", "Login");
            }

            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KinderGarten k;
            k = KindergartenService.GetById((int)id);
            if (k == null)
            {
                return HttpNotFound();
            }
            KinderGartenModel ks = new KinderGartenModel()
            {
                Address = k.Address,
                Cost = k.Cost,
                DateCreation = k.DateCreation,
                Description = k.Description,
                KinderGartenId = k.KinderGartenId,
                Image = k.Image,
                Name = k.Name,
                NbrEmp = k.NbrEmp,
                Phone = k.Phone,
                DirecteurId = (int)k.DirecteurId,
                nbVue = k.nbVue,
                nameDir = userService.GetById((int)k.DirecteurId).email,
                Votes = k.Votes

            };
            KindergartenService.IncNbVue(id);
            return View(ks);
        }


        // GET: KinderGarten/Create
        String date;
        public ActionResult Create()
        {
            int userId;
            try { userId = (int)Session["idu"]; }
            catch
            {
                return RedirectToAction("Login", "Login");
            }
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            

            ViewBag.home = mail;
            ViewBag.phone = Phone2;


            return View();
        }


        // POST: KinderGarten/Create
        [HttpPost]
        public ActionResult Create(KinderGartenModel km, HttpPostedFileBase Image)
        {
            KinderGarten kg = new KinderGarten();

            kg.Name = km.Name;
            kg.Image = Image.FileName;
            kg.DateCreation = DateTime.UtcNow;
            kg.Address = km.Address;
            kg.Cost = km.Cost;
            kg.Description = km.Description;
            kg.NbrEmp = km.NbrEmp;
            kg.Phone = km.Phone;
            kg.DirecteurId = (int)Session["idu"];
            kg.Votes = "0,0,0,0,0";

            KindergartenService.Add(kg);
            KindergartenService.Commit();

            var path2 = Path.Combine(Server.MapPath("~/Content/Uploads"), Image.FileName);
            Image.SaveAs(path2);
            return RedirectToAction("Index");
        }

        // GET: KinderGarten/Edit/5
        public ActionResult Edit(int id)
        {
            int userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;


            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            KinderGarten t = KindergartenService.GetById(id);
            KinderGartenModel tm = new KinderGartenModel();

            tm.Name = t.Name;
            tm.Image = t.Image;
            tm.Address = t.Address;
            tm.Cost = t.Cost;
            tm.Description = t.Description;
            tm.Phone = t.Phone;
            tm.NbrEmp = t.NbrEmp;
            return View(tm);
        }

        // POST: KinderGarten/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, KinderGartenModel tm, HttpPostedFileBase Image)
        {
            try
            {
                KinderGarten t = KindergartenService.GetById(id);
                t.Name = tm.Name;
                t.Image = Image.FileName;
                t.Description = tm.Description;
                t.NbrEmp = tm.NbrEmp;
                t.Phone = tm.Phone;
                t.Address = tm.Address;
                t.Cost = tm.Cost;

                var path = Path.Combine(Server.MapPath("~/Content/Uploads"), Image.FileName);
                Image.SaveAs(path);
                KindergartenService.Update(t);
                KindergartenService.Commit();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(tm);
            }
        }


        // GET: KinderGarten/Delete/5
        public ActionResult Delete(int id)
        {

            if (id == null)

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            KinderGarten p = KindergartenService.GetById(id);
            KinderGartenModel p1 = new KinderGartenModel()
            {
                Name = p.Name,
                Image = p.Image,
                Address = p.Address,
                NbrEmp = p.NbrEmp,
                Cost = p.Cost,

                Phone = p.Phone,
                Description = p.Description,


            };
            if (p == null)
                return HttpNotFound();
            return View(p1);
        }

        // POST: KinderGarten/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, KinderGartenModel evm)
        {

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            KinderGarten p = KindergartenService.GetById(id);
            evm = new KinderGartenModel()
            {
                Name = p.Name,
                Image = p.Image,
                Address = p.Address,
                NbrEmp = p.NbrEmp,
                Cost = p.Cost,
                Phone = p.Phone,
                Description = p.Description,

            };
            if (p == null)
                return HttpNotFound();
            Console.WriteLine("deletedddddddddddddddddddddddddddddddd");
            KindergartenService.Delete(p);
            KindergartenService.Commit();
            // Service.Dispose();

            return RedirectToAction("Index");

        }
        UserService UserService = new UserService();
        public JsonResult SendRating(string r, string s, string id, string url)
        {
            int autoId = 0;
            Int16 thisVote = 0;
            Int16 sectionId = 0;
            Int16.TryParse(s, out sectionId);
            Int16.TryParse(r, out thisVote);
            int.TryParse(id, out autoId);

            if (Session["idu"] == null)
            {
                return Json("Not authenticated!");
            }

            if (autoId.Equals(0))
            {
                return Json("Sorry, record to vote doesn't exists");
            }
            string usern = userService.GetById((int)Session["idu"]).prenom;
            switch (s)
            {
                case "5":
                    var isIt = db.VoteModels.Where(v => v.SectionId == sectionId &&
                        v.UserName.Equals(usern, StringComparison.CurrentCultureIgnoreCase) && v.VoteForId == autoId).FirstOrDefault();
                    if (isIt != null)
                    {
                        HttpCookie cookie = new HttpCookie(url, "true");
                        Response.Cookies.Add(cookie);
                        return Json("<br />You have already rated this post, thanks !");
                    }

                    var sch = db.KinderGartens.Where(sc => sc.KinderGartenId == autoId).FirstOrDefault();
                    if (sch != null)
                    {
                        object obj = sch.Votes;

                        string updatedVotes = string.Empty;
                        string[] votes = null;
                        if (obj != null && obj.ToString().Length > 0)
                        {
                            string currentVotes = obj.ToString(); // votes pattern will be 0,0,0,0,0
                            votes = currentVotes.Split(',');
                            // if proper vote data is there in the database
                            if (votes.Length.Equals(5))
                            {
                                // get the current number of vote count of the selected vote, always say -1 than the current vote in the array 
                                int currentNumberOfVote = int.Parse(votes[thisVote - 1]);
                                // increase 1 for this vote
                                currentNumberOfVote++;
                                // set the updated value into the selected votes
                                votes[thisVote - 1] = currentNumberOfVote.ToString();
                            }
                            else
                            {
                                votes = new string[] { "0", "0", "0", "0", "0" };
                                votes[thisVote - 1] = "1";
                            }
                        }
                        else
                        {
                            votes = new string[] { "0", "0", "0", "0", "0" };
                            votes[thisVote - 1] = "1";
                        }

                        // concatenate all arrays now
                        foreach (string ss in votes)
                        {
                            updatedVotes += ss + ",";
                        }
                        updatedVotes = updatedVotes.Substring(0, updatedVotes.Length - 1);

                        db.Entry(sch).State = EntityState.Modified;
                        sch.Votes = updatedVotes;
                        db.SaveChanges();
                        

                        VoteLog vm = new VoteLog()
                        {
                            Active = true,
                            SectionId = Int16.Parse(s),
                            UserName = userService.GetById((int)Session["idu"]).prenom,
                            Vote = thisVote,
                            VoteForId = autoId
                        };

                        db.VoteModels.Add(vm);

                        db.SaveChanges();

                        // keep the school voting flag to stop voting by this member
                        HttpCookie cookie = new HttpCookie(url, "true");
                        Response.Cookies.Add(cookie);
                    }
                    break;
                default:
                    break;
            }
            return Json("<br />You rated " + r + " star(s), thanks !");
        }

    }
}
