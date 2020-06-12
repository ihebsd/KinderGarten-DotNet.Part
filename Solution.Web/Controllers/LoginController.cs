
using Solution.Data;
using Solution.Domain.Entities;
using Solution.Web.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Solution.Presentation.Controllers
{
    public class LoginController : Controller
    {
        //Registration Action
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        // Post new User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Exclude = "IsEmailVerified,ActivationCode")]User user)
        {
            bool Status = false;
            string message = "";

            if (ModelState.IsValid)
            {

                var isExist = isEmailExist(user.email);
                // Email is already Exist
                if (isExist)
                {
                    //ModelState.AddModelError("EmailExist", "Email already exist");
                    message = "Email already exist";
                    return View(user);
                }
                user.ActivationCode = Guid.NewGuid();
                user.password = Crypto.Hash(user.password);
                user.Confirmpassword = Crypto.Hash(user.Confirmpassword);
                user.role = user.role;

                user.IsEmailVerified = false;

                using (PidevContext db = new PidevContext())
                {

                    db.Users.Add(user);
                    db.SaveChanges();
                    message = "Registration Successfully done. Account activation link has been send to your email";
                    SendVerificationLinkEmail(user.email, user.ActivationCode.ToString());

                    Status = true;
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                message = "Invalid Request";
            }
            ViewBag.Message = message;
            ViewBag.Status = Status;

            return View(user);
        }
        // Verifier account from mail 
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (PidevContext db = new PidevContext())
            {
                db.Configuration.ValidateOnSaveEnabled = false;

                var v = db.Users.Where(x => x.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    db.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = Status;
            return View();
        }
        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login)
        {

            string message = "";
            using (PidevContext db = new PidevContext())
            {
                var v = db.Users.Where(x => x.email == login.email).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(login.password), v.password) == 0)
                    {
                        int timeout = login.RememberMe ? 43000 : 1; //One Year
                        var ticket = new FormsAuthenticationTicket(login.email + login.password, login.RememberMe, timeout);
                        string encypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encypted)
                        {
                            Expires = DateTime.Now.AddMinutes(timeout),
                            HttpOnly = true
                        };
                        Response.Cookies.Add(cookie);

                        Session["idu"] = v.idUser;
                        Session["v"] = v;
                        Session["v"] = v.role;
                        Session["user"] = v;
                        if (Session["v"].ToString() == "admin")
                        {
                            return RedirectToAction("Index", "UserBack");
                        }
                        else
                        {
                            return RedirectToAction("Index", "KinderGarten");
                        }

                    }
                    else
                    {

                        message = "Invalid password";
                    }
                }
                else
                {
                    message = "Invalid Crendential provided";
                }
            }

            ViewBag.message = message;

            return View();
        }
        //LOgout

        public async Task<ActionResult> Logout()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
        // detect existed email
        [NonAction]
        public bool isEmailExist(string email)
        {
            using (PidevContext db = new PidevContext())
            {
                var v = db.Users.Where(x => x.email == email).FirstOrDefault();
                return v != null;
            }
        }
        // protocole to send the email with true user
        [NonAction]
        public void SendVerificationLinkEmail(string email, string activationCode, string emailFor = "VerifyAccount")
        {

            var verifyUrl = "https://kindergartenazure.azurewebsites.net/Login/" + emailFor + "/" + activationCode;
            // var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("hsine.gabsi@esprit.tn", "Congratulation for sign in");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "Brigade2001";
            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";
                body = "<br/><br>We are excited to tell you that your account is" +
                             "successfully created. Please click on the below link to verfy your account" +
                             "<br/><br><a href='" + verifyUrl + "'>" + verifyUrl + "</a>";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/><br/> We got request for reset account password. Please click on the below link to reset your password" +
                     "<br/><br/><a href=" + verifyUrl + ">Reset Password link</a>";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            string message = "";

            using (PidevContext db = new PidevContext())
            {
                var account = db.Users.Where(x => x.email == Email).FirstOrDefault();
                if (account != null)
                {
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.email, resetCode, "ResetPassword");
                    account.ResetPasswordCode = resetCode;

                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    message = "Reset password link has been sent to your email";
                }
                else
                {
                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        public ActionResult ResetPassword(string id)
        {
            using (PidevContext db = new PidevContext())
            {
                var user = db.Users.Where(x => x.ResetPasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);

                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (PidevContext db = new PidevContext())
                {
                    var user = db.Users.Where(x => x.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.password = Crypto.Hash(model.NewPassword);
                        user.ResetPasswordCode = "";
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        message = "New password updated successfully";
                    }
                }
            }
            else
            {
                message = "Something invalid";
            }
            ViewBag.Message = message;
            return View(model);
        }
    }

}