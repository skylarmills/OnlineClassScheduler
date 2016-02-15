using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WSAD_App1.Models.Data;
using WSAD_App1.Models.ViewModels.Correspondence;

namespace WSAD_App1.Controllers
{
    public class CorrespondenceController : Controller
    {
        // GET: correspondence
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ContactEmailViewModel contactMessage)
        {
            //Validate Contact Message Input
            if (contactMessage == null)
            {
                ModelState.AddModelError("","No Message Provided");
                return View();
            }

            if (string.IsNullOrWhiteSpace(contactMessage.Name) ||
                string.IsNullOrWhiteSpace(contactMessage.Email) ||
                string.IsNullOrWhiteSpace(contactMessage.Message))
            {
                ModelState.AddModelError("", "All fields are required.");
            }

            //Create an Email Message Object
            MailMessage email = new MailMessage();
            email.To.Add("millssk@mail.uc.edu");
            email.From = new MailAddress(contactMessage.Email);
            email.Subject = "This is our correspondence";
            email.Body = string.Format(
                "Name: {0}\r\nMessage: {1}",
                    contactMessage.Name,
                    contactMessage.Message
            );

            //Setup SMTP Client
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("millsskUCAppTest@gmail.com", "$koolRoolz");
            smtpClient.Port = int.Parse("587");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicauthenticationinfo;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            
            //Send EMail
            smtpClient.Send(email);

            //Save to DB
            saveCorrespondence(contactMessage);

            //Notify user message was sent
            return View("emailConfirmation");
        }

        public void saveCorrespondence(ContactEmailViewModel contactMessage)
        {
            //Create DBContext instance
            using (WSADDbContext context = new WSADDbContext())
            {
                //Create correspondenceDTO
                Correspondence correspondenceDTO = new Correspondence()
                {
                    Name = contactMessage.Name,
                    Email = contactMessage.Email,
                    Message = contactMessage.Message
                };

                //Add to DbContext
                correspondenceDTO = context.Correspondences.Add(correspondenceDTO);

                //Save changes
                context.SaveChanges();
            }

        }
    }
}