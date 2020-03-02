

using Microsoft.AspNet.Identity;
using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Solution.Web.Controllers
{
    public class KinderGartenController : Controller
    {
       
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
            
               var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            var kindergartens = new List<KinderGartenModel>();
            foreach (KinderGarten k in KindergartenService.SearchKindergartenByName(searchString))
            {
                KinderGartenModel ks = new KinderGartenModel()
                {
                    Address=k.Address,
                    Cost=k.Cost,
                    DateCreation=k.DateCreation,
                    Description=k.Description,
                    KinderGartenId=k.KinderGartenId,
                    Image=k.Image,
                    Name=k.Name,
                    NbrEmp=k.NbrEmp,
                    Phone=k.Phone,
                    DirecteurId=k.DirecteurId
                };
                kindergartens.Add(ks);
               
            }
           

            return View(kindergartens);
        }

        // GET: KinderGarten/Details/5
        public ActionResult Details(int? id)
        {
            var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login ;
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
                DirecteurId =(int) k.DirecteurId,
                nameDir = userService.GetById((int)k.DirecteurId).email

            };
            return View(ks);
        }
        

        // GET: KinderGarten/Create

        public ActionResult Create()
        {
            var userId = (int)Session["idu"];
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
            kg.DirecteurId =(int) Session["idu"];

            KindergartenService.Add(kg);
            KindergartenService.Commit();

            var path2 = Path.Combine(Server.MapPath("~/Content/Uploads"), Image.FileName);
            Image.SaveAs(path2);
            return RedirectToAction("Index");
        }

        // GET: KinderGarten/Edit/5
        public ActionResult Edit(int id)
        {
            KinderGarten t = KindergartenService.GetById(id);
            KinderGartenModel tm = new KinderGartenModel();

            tm.Name = t.Name;
            //ImageUrl = Image.FileName,
            tm.DateCreation = t.DateCreation;
            tm.Address = t.Address;
            tm.Cost = t.Cost;
            tm.Description = t.Description;
            tm.Phone = t.Phone;
            tm.NbrEmp = t.NbrEmp;
            return View(tm);
        }

        // POST: KinderGarten/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: KinderGarten/Delete/5
        public ActionResult Delete(int id)
        {
          
            return View();
        }

        // POST: KinderGarten/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, KinderGartenModel tm)
        {
            KinderGarten t = KindergartenService.GetById(id);
            KindergartenService.Delete(t);
            KindergartenService.Commit();

            return RedirectToAction("Index");
        }
    }
}
