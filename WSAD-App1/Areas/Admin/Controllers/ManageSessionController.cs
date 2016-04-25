using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSAD_App1.Models.Data;
using WSAD_App1.Areas.Admin.Models.ViewModels.ManageSession;


namespace WSAD_App1.Areas.Admin.Controllers
{
    [Authorize(Roles="Admin")]
    public class ManageSessionController : Controller
    {
        // GET: ManageSession
        public ActionResult Index()
        {
            //Setup a DBcontext
            List<ManageSessionViewModel> collectionOfSessionVM = new List<ManageSessionViewModel>();
            using (WSADDbContext context = new WSADDbContext())
            {
                //Get all Sessions
                var dbSessions = context.Sessions;

                //Move Sessions to ViewModel Object
                foreach (var SessionDTO in dbSessions)
                {
                    collectionOfSessionVM.Add(
                        new ManageSessionViewModel(SessionDTO)
                        );
                }
            }
            //Send ViewModel Collete
            return View(collectionOfSessionVM);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateSessionViewModel newSession)
        {
            //Check required fields
            if (!ModelState.IsValid)
            {
                return View(newSession);
            }

            //Create DbContext
            using (WSADDbContext context = new WSADDbContext())
            {
                //Check for duplicate sessions
                if (context.Sessions.Any(row => row.Course.Equals(newSession.Course)))
                {
                    ModelState.AddModelError("", "Session '" + newSession.Course + "' already exists. Try Again");
                    newSession.Course = "";
                    return View(newSession);
                }

                //Create Session DTO
                Session newSessionDTO = new WSAD_App1.Models.Data.Session()
                {
                    Course = newSession.Course,
                    Instructor = newSession.Instructor,
                    MeetingDate = newSession.MeetingDate,
                    MeetingTime = newSession.MeetingTime,
                    Description = newSession.Description

                };

                //Add to DbContext

                newSessionDTO = context.Sessions.Add(newSessionDTO);

                //Save changes
                context.SaveChanges();
            }

            //Redirect to login
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(List<ManageSessionViewModel> collectionOfSessionsVM)
        {
            //Filter collectionOfSessions to find only Selected Items
            var vmItemsToDelete = collectionOfSessionsVM.Where(x => x.IsSelected == true);

            //Do delete

            using (WSADDbContext context = new WSADDbContext())
            {
                //Loop through ViewModel Items to delete
                foreach (var vmItem in vmItemsToDelete)
                {
                    var dtoToDelete = context.Sessions.FirstOrDefault(row => row.Id == vmItem.Id);
                    context.Sessions.Remove(dtoToDelete);
                }

                context.SaveChanges();
            }
                return RedirectToAction("Index");
            
        }
    }
}