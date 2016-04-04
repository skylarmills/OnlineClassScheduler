using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSAD_App1.Models.Data;

namespace WSAD_App1.Areas.Admin.Models.ViewModels.ManageUserSessions
{
    public class SessionListByUserViewModel
    {
        public SessionListByUserViewModel()
        {

        }

        public SessionListByUserViewModel(UserSession userSession)
        {
            //UserId = userSession.User_Id;
            SessionId = userSession.Session_Id;
            Course = userSession.Session.Course;
            UserName = userSession.User.Username;
            FirstName = userSession.User.FirstName;
            LastName = userSession.User.LastName;

        }
      
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public string Course { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}