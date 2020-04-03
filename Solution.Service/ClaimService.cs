
using Service.Pattern;
using Solution.Data.Infrastructure;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Service
{
    public class ClaimService : Service<Claim>, IClaimService
    {
        static IDatabaseFactory Factory = new DatabaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public ClaimService() : base(utk)
        {
        }
        public void commit()
        {
            utk.Commit();
        }
        public void Dispose()
        {
            utk.Dispose();
        }
        public IEnumerable<Claim> SearchKClaimByName(string searchString)
        {
            IEnumerable<Claim> ClaimDomain = GetMany().ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                ClaimDomain = GetMany(x => x.Name.Contains(searchString));
            }
            return ClaimDomain;
        }

        public bool SendEmail(string toEmail, string subject, string emailBody)
        {
            try
            {
                string senderEmail = "safsafraslen@gmail.com";
                string senderPassword = "raslen123*";
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, emailBody);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
