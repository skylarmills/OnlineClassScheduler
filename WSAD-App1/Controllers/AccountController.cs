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
    [Authorize]
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
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
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
                    DateModified = DateTime.Now,
                    Gender = newUser.Gender

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
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        public ActionResult UserNavPartial()
        {
            //Capture Logged in User
            string username;
            username = this.User.Identity.Name;

            //Get user info from DB
            UserNavPartialViewModel userNavVM;

            using (WSADDbContext context = new WSADDbContext())
            {
                //search for user
                Models.Data.User userDTO = context.Users.FirstOrDefault(x => x.Username == username);

                if(userDTO == null) { return Content(""); }

                //Build UserNavPartialViewModel
                userNavVM = new UserNavPartialViewModel()
                {
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Id = userDTO.Id
                };
            }


            //Send view model 
            return PartialView(userNavVM);
        }

        public ActionResult UserProfile(int? id = null)
        {
            //Captuer Logged in user
            string username = User.Identity.Name;

            //Retreive user from DB
            UserProfileViewModel profileVM;

            using (WSADDbContext context = new WSADDbContext())
            {
                //Get user from DB
                User userDTO;
                if (id.HasValue)
                {
                    userDTO = context.Users.Find(id.Value);
                }
                else
                {
                    userDTO = context.Users.FirstOrDefault(row => row.Username == username);
                }

                if(userDTO == null)
                {
                    return Content("Inalid Username");
                }

                //Populate UserProfileViewModel
                profileVM = new UserProfileViewModel()
                {
                    DateCreated = userDTO.DateCreated,
                    EmailAddress = userDTO.EmailAddress,
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Gender = userDTO.Gender,
                    Id = userDTO.Id,
                    IsAdmin = userDTO.IsAdmin
                };

            }


            //Return view with ViewModel
            return View(profileVM);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Get User by Id
            EditViewModel editVM;

            using (WSADDbContext context = new WSADDbContext())
            {
                //Get user from database
                User userDTO = context.Users.Find(id);

                if (userDTO == null)
                {
                    return Content("Invalid Id");
                }

                //Create EditViewModel
                editVM = new EditViewModel()
                {
                    EmailAddress = userDTO.EmailAddress,
                    FirstName = userDTO.FirstName, 
                    Id = userDTO.Id,
                    LastName = userDTO.LastName,
                    UserName = userDTO.Username,
                    Gender = userDTO.Gender
                };
            }

            //Send ViewModel to view\
            return View(editVM);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel editVM)
        {

            //Variables
            bool needsPasswordReset = false;
            bool usernameHasChanged = false;
            //Validate Model
            if(!ModelState.IsValid)
            {
                return View(editVM);
            }
            //Check for password change
            if(!string.IsNullOrWhiteSpace(editVM.Password))
            {
                //Compare Password with PasswordConfirm
                if (editVM.Password != editVM.PasswordConfirm)
                {
                    ModelState.AddModelError("", "Password and Password Confirm Must Match");
                    return View(editVM);
                }
                else
                {
                    needsPasswordReset = true;
                }
            }


            //Get User from DB
            using (WSADDbContext context = new WSADDbContext())
            {
                //Get DTO
                User userDTO = context.Users.Find(editVM.Id);
                if(userDTO == null) { return Content("Invalid User Id"); }

                //Check for username change
                if(userDTO.Username != editVM.UserName)
                {
                    usernameHasChanged = true;
                }

                //Set values from ViewModel
                userDTO.FirstName = editVM.FirstName;
                userDTO.DateModified = DateTime.Now;
                userDTO.EmailAddress = editVM.EmailAddress;
                userDTO.LastName = editVM.LastName;
                userDTO.Username = editVM.UserName;
                userDTO.Gender = editVM.Gender;

                if(needsPasswordReset)
                {
                    userDTO.Password = editVM.Password;
                }

                //SaveChanges
                context.SaveChanges();
            }

            if (usernameHasChanged || needsPasswordReset)
            {
                TempData["LogoutMessage"] = "After a username or password change. Please log in with the new username or password.";
                return RedirectToAction("Logout");
            }
            else
            {
                return RedirectToAction("UserProfile");
            }
        }


    }

        
}