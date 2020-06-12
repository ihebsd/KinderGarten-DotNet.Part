using PusherServer;
using Solution.Data;
using Solution.Domain.Entities;
using Solution.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace Solution.Web.Controllers
{
    public class ChatController : Controller
    {
        IUserService userService = new UserService();
        private Pusher pusher;

        //class constructor
        public ChatController()
        {
            var options = new PusherOptions
            {
                Cluster = "eu",
                Encrypted = true
            };

            pusher = new Pusher(
             "895201",
             "ca6eb146868dd4454768",
             "6366285fdb177a616d6c",
             options);

        }
        // GET: administrator/Chat
        public ActionResult Index()
        {
            var userId = (int)Session["idu"];
            String Phone2 = userService.GetById(userId).login;
            String mail = userService.GetById(userId).email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;


            if (Session["user"] == null)
            {

                return Redirect("/Login/Login");


            }

            var currentUser = (User)Session["user"];

            using (var db = new PidevContext())
            {

                ViewBag.allUsers = db.Users.Where(u => u.email != currentUser.email)
                                 .ToList();
            }


            ViewBag.currentUser = currentUser;
            return View();
        }
        public JsonResult ConversationWithContact(int contact)
        {
            if (Session["user"] == null)
            {
                return Json(new { status = "error", message = "User is not logged in" });
            }

            var currentUser = (User)Session["user"];

            var conversations = new List<Conversation>();

            using (var db = new PidevContext())
            {
                conversations = db.Conversations.
                                  Where(c => (c.receiver_id == currentUser.idUser
                                      && c.sender_id == contact) ||
                                      (c.receiver_id == contact
                                      && c.sender_id == currentUser.idUser))
                                  .OrderBy(c => c.created_at)
                                  .ToList();
            }

            return Json(
                new { status = "success", data = conversations },
                JsonRequestBehavior.AllowGet
            );
        }
        [HttpPost]
        public JsonResult SendMessage()
        {
            if (Session["user"] == null)
            {
                return Json(new { status = "error", message = "User is not logged in" });
            }

            var currentUser = (User)Session["user"];

            string socket_id = Request.Form["socket_id"];

            Conversation convo = new Conversation
            {
                sender_id = currentUser.idUser,
                message = Request.Form["message"],
                receiver_id = Convert.ToInt32(Request.Form["contact"]),

            };

            using (var db = new PidevContext())
            {
                db.Conversations.Add(convo);
                db.SaveChanges();
            }

            return Json(convo);
        }
        private String getConvoChannel(int user_id, int contact_id)
        {
            if (user_id > contact_id)
            {
                return "private-chat-" + contact_id + "-" + user_id;
            }

            return "private-chat-" + user_id + "-" + contact_id;
        }
        public JsonResult AuthForChannel(string channel_name, string socket_id)
        {
            if (Session["user"] == null)
            {
                return Json(new { status = "error", message = "User is not logged in" });
            }

            var currentUser = (User)Session["user"];

            if (channel_name.IndexOf("presence") >= 0)
            {

                var channelData = new PresenceChannelData()
                {
                    user_id = currentUser.idUser.ToString(),
                    user_info = new
                    {
                        id = currentUser.idUser,
                        name = currentUser.nom
                    },
                };

                var presenceAuth = pusher.Authenticate(channel_name, socket_id, channelData);

                return Json(presenceAuth);

            }

            if (channel_name.IndexOf(currentUser.idUser.ToString()) == -1)
            {
                return Json(new { status = "error", message = "User cannot join channel" });
            }

            var auth = pusher.Authenticate(channel_name, socket_id);

            return Json(auth);


        }

    }
}

