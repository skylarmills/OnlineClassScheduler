using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSAD_App1.Models.Data;

namespace WSAD_App1.Models.ViewModels.Course
{
    public class EnrollSessionViewModel
    {
        public EnrollSessionViewModel()
        {

        }

        public EnrollSessionViewModel(Session sessionDTO)
        {
            Id = sessionDTO.Id;
            Course = sessionDTO.Course;
            Instructor = sessionDTO.Instructor;
            MeetingDate = sessionDTO.MeetingDate;
            MeetingTime = sessionDTO.MeetingTime;
            Occupancy = sessionDTO.Occupancy;
        }
        public int Id { get; set; }
        public string Course { get; set; }
        public string Instructor { get; set; }
        public string MeetingDate { get; set; }
        public string MeetingTime { get; set; }
        public int Occupancy { get; set; }
        public bool IsSelected { get; set; }
    }
}