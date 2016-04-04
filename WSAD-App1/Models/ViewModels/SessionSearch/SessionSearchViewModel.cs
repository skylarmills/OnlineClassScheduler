using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSAD_App1.Models.ViewModels.SessionSearch
{
    public class SessionSearchViewModel
    {
        public SessionSearchViewModel(Data.Session sessionDTO)
        {

            Id = sessionDTO.Id;
            Course = sessionDTO.Course;
            Instructor = sessionDTO.Instructor;
            MeetingDate = sessionDTO.MeetingDate;
            MeetingTime = sessionDTO.MeetingTime;
            Occupancy = sessionDTO.Occupancy;
        }

        public int Id { get; private set; }
        public string Instructor { get; private set; }
        public string MeetingDate { get; private set; }
        public string MeetingTime { get; private set; }
        public int Occupancy { get; private set; }
        public string Course { get; set; }
    }
}