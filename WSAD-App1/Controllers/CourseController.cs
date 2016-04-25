using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSAD_App1.Models.Data;
using WSAD_App1.Models.ViewModels.Course;

namespace WSAD_App1.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            //Setup a DBcontext
            List<EnrollSessionViewModel> collectionOfSessionVM = new List<EnrollSessionViewModel>();
            using (WSADDbContext context = new WSADDbContext())
            {
                //Get all sessions
                var dbSessions = context.Sessions;

                //Move users to ViewModel Object
                foreach (var sessionDTO in dbSessions)
                {
                    collectionOfSessionVM.Add(
                        new EnrollSessionViewModel(sessionDTO)
                        );
                }
            }
            //Send ViewModel Collete
            return View(collectionOfSessionVM);
        }


        [HttpPost]
        public ActionResult Enroll(List<EnrollSessionViewModel> collectionOfSessionsVM)
        {
            //Filter collectionOfUsers to find only Selected Items
            var vmItemsToEnroll = collectionOfSessionsVM.Where(x => x.IsSelected == true);

            //Get logged in user
            string username = User.Identity.Name;
            User userDTO;


            //Do Enroll

            using (WSADDbContext context = new WSADDbContext())
            {


                //Get user db info an Id
                userDTO = context.Users.FirstOrDefault(row => row.Username == username);
                int userId = userDTO.Id;
                //Loop through ViewModel Items to update
                foreach (var vmItem in vmItemsToEnroll)
                {
                    UserSession userSessionDTO = new UserSession();
                    var sessionDTO = context.Sessions.FirstOrDefault(row => row.Id == vmItem.Id);


                    userSessionDTO.User_Id = userId;
                    userSessionDTO.Session_Id = vmItem.Id;

                    //Check that session hasn't already been enrolled by user
                    if (context.UserSessions.Any(row => row.User_Id.Equals(userId)) && context.UserSessions.Any(row => row.Session_Id.Equals(vmItem.Id)))
                    {
                        TempData["error"] = "Error: You are already enrolled in this course.";
                        return RedirectToAction("Index");

                    }

                    else {
                        context.UserSessions.Add(userSessionDTO);
                    }

                }

                context.SaveChanges();
            }

            return RedirectToAction("Index", "Schedule");


        }

        [HttpGet]
        public ActionResult Details(int sessionId)
        {
            SessionViewModel sessionVM = new SessionViewModel();

            using (WSADDbContext context = new WSADDbContext())
            {
                Session session = context.Sessions.FirstOrDefault(row => row.Id == sessionId);
                sessionVM = new SessionViewModel(session);
            }

            return View(sessionVM);
        }
    }
}