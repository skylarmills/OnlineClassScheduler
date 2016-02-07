using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WSAD_App1.Models.Data;
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
        public ActionResult Create(CreateUserViewModel newUser)
        {
            //Check required fields
            if (!ModelState.IsValid)
            {
                return View(newUser);
            }

            //Check Password & PasswordConfirm
            if(!newUser.Password.Equals(newUser.PasswordConfirm))
            {
                ModelState.AddModelError("", "Password does not match Password Confirm");
                return View(newUser);
            }

            //Create DbContext instance
            using (WSADDbContext context = new WSADDbContext())
            {
                //Check username is not a duplicate
                if (context.Users.Any(row => row.Username.Equals(newUser.Username)))
                {
                    ModelState.AddModelError("", "Username '" + newUser.Username + "' already exists. Try Again");
                    newUser.Username = "";
                    return View(newUser);

                }
                //Create User DTO
                User newUserDTO = new Models.Data.User()
                {
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    EmailAddress = newUser.EmailAddress,
                    IsActive = true,
                    IsAdmin = false,
                    Username = newUser.Username,
                    Password = newUser.Password,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now

                };

                //Add to DbContext

                newUserDTO = context.Users.Add(newUserDTO);

                //Save changes
                context.SaveChanges();
            }

            //Redirect to login
            return RedirectToAction("login");

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
        public ActionResult Login(LoginUserViewModel loginUser)
        {

            //Open DB Connection
            bool isValid = false;

            using (WSADDbContext context = new WSADDbContext())
            {
                //Hash Password

                //Query for user based on Username and Password
                if(context.Users.Any(row=>row.Username.Equals(loginUser.Username)
                && row.Password.Equals(loginUser.Password)))
                {
                    isValid = true;
                }
            }

            if(!isValid)
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View();

            }

            else
            {
                //If valid, redirect to the user profile
                FormsAuthentication.SetAuthCookie(loginUser.Username, loginUser.RememberMe);

                return Redirect(FormsAuthentication.GetRedirectUrl(loginUser.Username, loginUser.RememberMe));
            }
        
        }
    }
}