using Solution.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solution.Web.Controllers
{
    public class HomeController : Controller
    {
        IUserService userService;
        public HomeController()
        {        
            userService = new UserService();
        }
        public ActionResult Index()
        {
            var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}