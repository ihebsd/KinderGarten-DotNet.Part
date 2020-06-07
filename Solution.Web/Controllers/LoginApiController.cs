

using Solution.Data;
using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Security;

namespace Solution.Web.Controllers
{
    public class LoginApiController : ApiController
    {
        IUserService userService = new UserService();
        [HttpGet]
        [Route("api/LoginApi/VerifyAccount")]
        public IEnumerable<bool> VerifyAccount(string id)
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

                    return new List<bool> { Status };
                }

            }

            return new List<bool> { false };
        }
        [NonAction]
        public void SendVerificationLinkEmail(string email, string activationCode, string emailFor = "VerifyAccount")
        {

            var verifyUrl = "http://localhost:9080/POC_PI_AWS-web/rest/user/" + emailFor + "/" + activationCode;
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

        [AllowAnonymous]
        [Route("api/Login/Register")]
        public IHttpActionResult Register(UserLogin user)
        {


            string message = "";
            user.IsEmailVerified = false;
            using (var ctx = new PidevContext())
            {
                User user1 = new User()
                {
                    password = Crypto.Hash(user.password),
                    Confirmpassword = Crypto.Hash(user.Confirmpassword),
                    role = user.role,
                    email = user.email,
                    nom = user.nom,
                    prenom = user.prenom,
                    login = user.login,
                    ActivationCode = Guid.NewGuid(),
                    IsEmailVerified = false

                };
                ctx.Users.Add(user1);
                message = "Registration Successfully done. Account activation link has been send to your email";
                SendVerificationLinkEmail(user.email, user1.ActivationCode.ToString());

                ctx.SaveChanges();
            }

            return Ok();
        }

        [HttpGet]
        [Route("api/Login/Login")]
        public IEnumerable<User> Login(string email, string password)
        {
            using (var ctx = new PidevContext())
            {
                User v = ctx.Users.Where(x => x.email == email).FirstOrDefault();
                if (v != null)
                {
                    if (v.password == Crypto.Hash(password))
                    {
                        return new List<User> { v };
                    }
                    return new List<User> { new User() { idUser = -1 } };
                }
                return new List<User> { new User() { idUser = 0 } };

            }
        }

        [AllowAnonymous]
        [Route("api/LoginApi/ForgotPassword")]
        public IHttpActionResult ForgotPassword(UserLogin us)
        {


            using (PidevContext db = new PidevContext())
            {
                var account = db.Users.Where(x => x.email == us.email).FirstOrDefault();
                if (account != null)
                {
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.email, resetCode, "ResetPassword");
                    account.ResetPasswordCode = resetCode;

                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                }
                else
                {
                }

            }
            return Ok();


        }
        [HttpGet]
        [Route("api/UserApi")]
        public IEnumerable<UserLogin> GetUsers()
        {
            List<User> users = userService.GetMany().ToList();
            List<UserLogin> usersM = new List<UserLogin>();
            foreach (User u in users)
            {
                usersM.Add(new UserLogin { email = u.email });

            }
            return usersM;
        }
        [HttpGet]
        [Route("api/LoginApi/Verify")]
        public IEnumerable<bool> Verify(string id)
        {
            bool Exist = false;
            using (PidevContext db = new PidevContext())
            {
                db.Configuration.ValidateOnSaveEnabled = false;

                var v = db.Users.Where(x => x.ResetPasswordCode == id).FirstOrDefault();
                if (v != null)
                {

                    Exist = true;

                    return new List<bool> { Exist };
                }

            }

            return new List<bool> { Exist };
        }

        [Route("api/LoginApi/ResetPassword")]

        public IHttpActionResult ResetPassword(UserLogin model)
        {

            using (PidevContext db = new PidevContext())
            {
                var user = db.Users.Where(x => x.ResetPasswordCode == model.ResetPasswordCode).FirstOrDefault();
                if (user != null)
                {
                    user.password = Crypto.Hash(model.password);
                    user.Confirmpassword = Crypto.Hash(model.Confirmpassword);

                    user.ResetPasswordCode = "";
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                }
            }


            return Ok();
        }
    }
}

