using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSAD_App1.Models.Data;

namespace WSAD_App1.Models.ViewModels.Course
{
    public class SessionViewModel
    {
        public SessionViewModel()
        {

        }

        public SessionViewModel(Session sessionDTO)
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

    }
}