using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSAD_App1.Models.ViewModels.Account;

namespace WSAD_App1.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return this.RedirectToAction("Login");
        }

        /// <summary>
        /// To Create a user account for the application
        /// </summary>
        /// <returns>ViewResult for the create</returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateUserViewModel createUser)
        {
            if (createUser.Password.Equals(createUser.PasswordConfirm))
            {
                return Content("First Name: " + createUser.FirstName + "<br/>"
                    + "Last Name: " + createUser.LastName + "<br/>"
                    + "Email Address: " + createUser.Email + "<br/>"
                    + "Gender: " + createUser.Gender + "<br/>"
                    + "Username: " + createUser.Username + "<br/>"
                    + "Password: " + createUser.Password + "<br/>"
                    + "Receive Emails: " + createUser.ReceiveEmail + "<br/>");
            }
            else
            {
                return Content("Passwords do not match");
            }
        }
        /// <summary>
        /// Logging users into the website
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            return Content("Hello " + username + ". Welcome to our application.");
        }
    }
}