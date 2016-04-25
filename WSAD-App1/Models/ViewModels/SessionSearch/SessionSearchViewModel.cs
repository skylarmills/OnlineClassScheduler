using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WSAD_App1.Models.ViewModels.SessionSearch
{
    [DataContract]
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
            Description = sessionDTO.Description;
        }

        [DataMember]
        public int Id { get; private set; }
        [DataMember]
        public string Instructor { get; private set; }
        [DataMember]
        public string MeetingDate { get; private set; }
        [DataMember]
        public string MeetingTime { get; private set; }
        [DataMember]
        public int Occupancy { get; private set; }
        [DataMember]
        public string Course { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}