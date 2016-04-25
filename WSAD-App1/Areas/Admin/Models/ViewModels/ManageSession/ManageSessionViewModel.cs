using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSAD_App1.Models.Data;

namespace WSAD_App1.Areas.Admin.Models.ViewModels.ManageSession
{
    public class ManageSessionViewModel
    {
        public ManageSessionViewModel()
        {

        }

        public ManageSessionViewModel(Session sessionDTO)
        {
            this.Id = sessionDTO.Id;
            this.Course = sessionDTO.Course;
            this.Instructor = sessionDTO.Instructor;
            this.MeetingDate = sessionDTO.MeetingDate;
            this.MeetingTime = sessionDTO.MeetingTime;
            this.Occupancy = sessionDTO.Occupancy;
            this.Description = sessionDTO.Description;
        }
        public int Id { get; set; }
        public string Course { get; set; }
        public string Instructor { get; set; }
        public string MeetingDate { get; set; }
        public string MeetingTime { get; set; }
        public int Occupancy { get; set; }
        public string Description { get; set; }
        public bool IsSelected { get; set; }
    }
}