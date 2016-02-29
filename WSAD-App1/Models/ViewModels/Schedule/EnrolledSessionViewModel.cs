using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSAD_App1.Models.Data;
using WSAD_App1.Models.ViewModels.Course;

namespace WSAD_App1.Models.ViewModels.Schedule
{
    public class EnrolledSessionViewModel
    {
        public EnrolledSessionViewModel()
        {

        }


        public EnrolledSessionViewModel(UserSession userSessionDTO)
        {
            this.User_Id = userSessionDTO.User_Id;
            this.Session_Id = userSessionDTO.Session_Id;
            this.Session = new SessionViewModel(userSessionDTO.Session);
            }
        public int User_Id { get; set; }
        public int Session_Id { get; set; }
        public SessionViewModel Session { get; set; }
        }
    }