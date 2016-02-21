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
            List<EnrollSessionViewModel> collectionOfUserVM = new List<EnrollSessionViewModel>();
            using (WSADDbContext context = new WSADDbContext())
            {
                //Get all sessions
                var dbSessions = context.Sessions;

                //Move users to ViewModel Object
                foreach (var sessionDTO in dbSessions)
                {
                    collectionOfUserVM.Add(
                        new EnrollSessionViewModel(sessionDTO)
                        );
                }
            }
            //Send ViewModel Collete
            return View(collectionOfUserVM);
        }

        [HttpPost]
        public ActionResult Enroll(List<EnrollSessionViewModel> collectionOfSessionsVM)
        {
            //Filter collectionOfUsers to find only Selected Items
            var vmItemsToEnroll = collectionOfSessionsVM.Where(x => x.IsSelected == true);

            //Get logged in user
            string username = User.Identity.Name;
            User userDTO;
            UserSession userSessionDTO = new UserSession();

            //Do Enroll

            using (WSADDbContext context = new WSADDbContext())
            {
                //Get user db info an Id
                userDTO = context.Users.FirstOrDefault(row => row.Username == username);
                int userId = userDTO.Id;
                //Loop through ViewModel Items to update
                foreach (var vmItem in vmItemsToEnroll)
                {
                    var sessionDTO = context.Sessions.FirstOrDefault(row => row.Id == vmItem.Id);
                    sessionDTO.Occupancy++;

                    
                    userSessionDTO.User_Id = userId;
                    userSessionDTO.Session_Id = vmItem.Id;

                    userSessionDTO = context.UserSessions.Add(userSessionDTO);

                }

                context.SaveChanges();
            }
            return RedirectToAction("Index");

        }
    }
}