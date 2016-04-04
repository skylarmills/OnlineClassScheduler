using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSAD_App1.Areas.Admin.Models.ViewModels.ManageUserSessions;
using WSAD_App1.Models.Data;

namespace WSAD_App1.Areas.Admin.Controllers
{
    public class ManageUserSessionsController : Controller
    {
        // GET: Admin/ManageUserSessions
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SessionListByUser(int userId)
        {
            List<UserSession> userSessions;
            //Get sessions and list to User
            using (WSADDbContext context = new WSADDbContext())
            {
                userSessions = context.UserSessions
                    .Include("User")
                    .Include("Session")
                    .Where(row => row.User_Id == userId)
                    .ToList();
            }

            //Convert to ViwModel
            List<SessionListByUserViewModel> userSessionsVMs =
                userSessions.Select(userSession => new SessionListByUserViewModel(userSession)).ToList();

                return View(userSessionsVMs);
        }

        public ActionResult SessionItem(int sessionId)
        {
            return View();
        }
    }
}